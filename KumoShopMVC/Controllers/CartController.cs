using KumoShopMVC.Data;
using Microsoft.AspNetCore.Mvc;
using KumoShopMVC.Helpers;
using KumoShopMVC.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using KumoShopMVC.Services;
using Newtonsoft.Json;

namespace KumoShopMVC.Controllers
{
	public class CartController : Controller
	{
		private readonly KumoShopContext db;
        private readonly IVnPayService _vnPayService;
        const string CART_KEY = "MYCART";

		public CartController(KumoShopContext context, IVnPayService vnPayService)
		{
			db = context;
            _vnPayService = vnPayService;
        }
		public IActionResult Index()
		{
			var guestCartItems = HttpContext.Session.Get<List<CartItemVM>>(CART_KEY) ?? new List<CartItemVM>();
			return View("Index", guestCartItems);
		}

		public List<CartItemVM> Cart => HttpContext.Session.Get<List<CartItemVM>>(CART_KEY) ?? new List<CartItemVM>();

		[HttpPost]
		public IActionResult AddToCart(int id, int quantity=1, string colors = "", int sizes = 0)
		{
			var product = db.ProductDetailsViews.FirstOrDefault(p => p.ProductId == id);
			if (product == null)
			{
				return Json(new { success = false, message = "Product not found" });
			}

			var cart = Cart;
			var item = cart.SingleOrDefault(p => p.ProductId == id
												  && p.Color == colors
												  && p.Size == sizes);
			if (item == null)
			{
				item = new CartItemVM
				{
					ProductId = product.ProductId,
					NameProduct = product.NameProduct ?? "",
					Price = (float)(product.Price ?? 0),
					Image = product.ImageUrl ?? "",
					Color = colors,
					Size = sizes,
					Quantity = quantity
				};
				cart.Add(item);
			}
			else
			{
				item.Quantity += quantity;
			}

			HttpContext.Session.Set(CART_KEY, cart);
			return Json(new { success = true, message = "Added to cart" });
		}

		[HttpPost]
		public IActionResult RemoveFromCart(int productId, string colors = "", int sizes = 0)
		{
			var cart = Cart;
			var itemToRemove = cart.SingleOrDefault(p => p.ProductId == productId
														  && p.Color == colors
														  && p.Size == sizes);
			if (itemToRemove != null)
			{
				cart.Remove(itemToRemove);
				HttpContext.Session.Set(CART_KEY, cart);
			}

			return View("Index", cart);
		}
		[Authorize]
		[HttpGet]
		public IActionResult CheckOut()
		{
			if(Cart.Count == 0)
			{
				Redirect("/");
			}
			return View(Cart);
		}
		[Authorize]
		[HttpPost]
		public IActionResult CheckOut(CheckoutVM model, string payment = "COD")
		{
			var customerId = int.Parse(HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value);
			var user = new User();
			if (model.LikeUser)
			{
				user = db.Users.SingleOrDefault(u => u.UserId == customerId);
			}
			if (ModelState.IsValid)
			{
				if (payment == "Thanh toán bằng VNPay")
				{
					var vnPayModel = new PaymentInformationModel()
					{
						Amount = Cart.Sum(p => p.SubTotal),
						Description = model.Desc ?? "",
						CreatedDate = DateTime.Now,
						FullName = model.FullName ?? "",
						Address = model.Address ?? "",
						PhoneNumber = model.PhoneNumber ?? ""
					};
                    TempData["Cart"] = JsonConvert.SerializeObject(Cart);
                    TempData["PaymentModel"] = JsonConvert.SerializeObject(vnPayModel);
                    var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
                    return Redirect(paymentUrl);
                }
				else
				{
					var order = new Order()
					{
						UserId = customerId,
						Fullname = model.FullName ?? user.Fullname,
						Address = model.Address ?? user.Address,
						Phone = model.PhoneNumber ?? user.Phone,
						OrderDate = DateTime.Now,
						DescOrder = model.Desc,
						StatusId = 2,
						PaymentMethode = "COD"
					};

					db.Database.BeginTransaction();
					try
					{
						db.Database.CommitTransaction();
						db.Add(order);
						db.SaveChanges();
						var orderItems = new List<OrderItem>();
						foreach (var item in Cart)
						{
							orderItems.Add(new OrderItem()
							{
								OrderId = order.OrderId,
								ProductId = item.ProductId,
								NameProduct = item.NameProduct,
								Color = item.Color,
								Size = item.Size,
								Image = item.Image,
								Price = item.Price,
								Quantity = item.Quantity,
								IsRating = false
							});
						}
						db.AddRange(orderItems);
						db.SaveChanges();
						ViewData["OrderItems"] = orderItems;
						HttpContext.Session.Set<List<CartItemVM>>(MySetting.CART_KEY, new List<CartItemVM>());
						return View("Success", order.OrderId);
					}
					catch
					{
						db.Database.RollbackTransaction();
					}
				}
			}

			return View(Cart);
		}
		[HttpPost]
		public IActionResult UpdateCart(int productId, int quantity, string colors = "", int sizes = 0)
		{
			if (quantity <= 0)
			{
				TempData["Error"] = "Quantity must be greater than zero.";
				return RedirectToAction("Index");
			}

			var cart = Cart;
			var item = cart.SingleOrDefault(p => p.ProductId == productId
												 && p.Color == colors
												 && p.Size == sizes);
			if (item != null)
			{
				item.Quantity = quantity;
				HttpContext.Session.Set(CART_KEY, cart); 
			}
			else
			{
				TempData["Error"] = "Item not found in cart.";
			}

			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult PaymentFail()
		{
			return View();
		}
		[Authorize]
		public IActionResult PaymentSuccess()
		{
			return View("Success");
		}

		[Authorize]
		public ActionResult PaymentCallBack()
		{
			var response = _vnPayService.PaymentExecute(Request.Query);
			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
				return RedirectToAction("PaymentFail");
			}
            var vnPayModel = JsonConvert.DeserializeObject<PaymentInformationModel>((string)TempData["PaymentModel"]);
            var cart = JsonConvert.DeserializeObject<List<CartItemVM>>((string)TempData["Cart"]);
            var customerId = int.Parse(HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value);
			var user = db.Users.SingleOrDefault(u => u.UserId == customerId);
            var order = new Order()
            {
                UserId = customerId,
                Fullname = vnPayModel.FullName,
                Address = vnPayModel.Address,
                Phone = vnPayModel.PhoneNumber,
                OrderDate = vnPayModel.CreatedDate,
                DescOrder = vnPayModel.Description,
                StatusId = 2,
                PaymentMethode = "VnPay"
            };

            db.Database.BeginTransaction();
            try
            {
                db.Database.CommitTransaction();
                db.Add(order);
                db.SaveChanges();
                var orderItems = new List<OrderItem>();
                foreach (var item in cart)
                {
                    orderItems.Add(new OrderItem()
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        NameProduct = item.NameProduct,
                        Color = item.Color,
                        Size = item.Size,
                        Image = item.Image,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        IsRating = false
                    });
                }
                db.AddRange(orderItems);
                db.SaveChanges();
            }
            catch
            {
                db.Database.RollbackTransaction();
            }
            TempData["Message"] = $"Thanh toán VN Pay thành công";
            return View("Success", order.OrderId);
        }
	}
}

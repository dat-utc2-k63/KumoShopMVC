using KumoShopMVC.Data;
using Microsoft.AspNetCore.Mvc;
using KumoShopMVC.Helpers;
using KumoShopMVC.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace KumoShopMVC.Controllers
{
	public class CartController : Controller
	{
		private readonly KumoShopContext db;
		const string CART_KEY = "MYCART";

		public CartController(KumoShopContext context)
		{
			db = context;
		}
		public IActionResult Index()
		{

			var guestCartItems = HttpContext.Session.Get<List<CartItemVM>>(CART_KEY) ?? new List<CartItemVM>();
			return View("Index", guestCartItems);
		}

		public List<CartItemVM> Cart => HttpContext.Session.Get<List<CartItemVM>>(CART_KEY) ?? new List<CartItemVM>();

		[HttpPost]
		public IActionResult AddToCart(int id, int quantity = 1, string colors = "", int sizes = 0)
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
		public IActionResult CheckOut(CheckoutVM model)
		{
				if (ModelState.IsValid)
			{
				var customerId = int.Parse(HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value);
				var user = new User();

				if (model.LikeUser)
				{
					user = db.Users.SingleOrDefault(u => u.UserId == customerId);
				}
				var order = new Order()
				{
					UserId = customerId,
					Fullname = model.FullName ?? user.Fullname,
					Address = model.Address ?? user.Address,
					Phone = model.PhoneNumber ?? user.Phone,
					OrderDate = DateTime.Now,
					DescOrder = model.Desc,
					StatusId = 0,
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
							SubTotal = item.Price * item.Quantity
						});
					}
					db.AddRange(orderItems);
					db.SaveChanges();
					HttpContext.Session.Set<List<CartItemVM>>(MySetting.CART_KEY, new List<CartItemVM>());
                    return View("Success");
				}
				catch
				{
					db.Database.RollbackTransaction();
				}
			}

			return View(Cart);
		}
	}
}

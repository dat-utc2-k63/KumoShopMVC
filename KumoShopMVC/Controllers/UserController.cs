using AutoMapper;
using KumoShopMVC.Data;
using KumoShopMVC.Helpers;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace KumoShopMVC.Controllers
{
	public class UserController : Controller
	{
		private readonly KumoShopContext db;
		private readonly IMapper _mapper;

		public UserController(KumoShopContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterVM model, IFormFile Avatar)
		{
			if (ModelState.IsValid)
			{
				var user = _mapper.Map<User>(model);
				user.RandomKey = MyUtil.GenerRateRandomKey();
				user.Password = model.Password.ToMd5Hash(user.RandomKey);
				user.Status = true;
				user.RoleId = 1;
				if(Avatar != null)
				{
					user.Avatar = MyUtil.UpLoadAvatar(Avatar, "User");
				}
				db.Users.Add(user);
				db.SaveChanges();
				return RedirectToAction("Index", "Home");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Login(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoginAsync(LoginVM model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var user = db.Users.SingleOrDefault(u => u.Email == model.Email);
				if (user == null)
				{
					ModelState.AddModelError("error", "Không có user này");
				}
				else
				{
					var hashedPassword = model.Password.ToMd5Hash(user.RandomKey);
					if (user.Password != hashedPassword)
					{
						ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
					}
					else
					{
						var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Email, model.Email),
							new Claim(ClaimTypes.Name, user.Fullname),
							new Claim(MySetting.CLAIM_CUSTOMERID, user.UserId.ToString()),
							new Claim(ClaimTypes.Role, user.RoleId.ToString())
						};
						var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(claimsPrincipal);
						// Lưu thông tin vào ViewData hoặc ViewBag để sử dụng trong View

						if (Url.IsLocalUrl(ReturnUrl))
						{
							return Redirect(ReturnUrl);
						}
						else
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}
			}
			return View(model);
		}
		[Authorize]
		public IActionResult MyOrder()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login");
            }
			
            var orders = db.Orders
						.Include(o => o.OrderItems)
						.Include(o => o.Status)
						.Where(o => o.UserId == userId)
                        .ToList();
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this user.");
            }

            var ordersVM = orders.Select(order => new OrderVM
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                DescOrder = order.DescOrder,
                ShippingDate = order.ShippingDate,
				StatusShippingId = order.StatusId ?? 0,
				StatusShipping = order.Status?.NameStatus ?? "Unknown",
				FullName = order.Fullname,
                Address = order.Address,
                Phone = order.Phone,
                PaymentMethode = order.PaymentMethode ?? "",
                OrderItems = order.OrderItems.Select(oi => new OrderItemVM
                {
					OrderItemId = oi.OrderItemId,
					OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    NameProduct = oi.NameProduct ?? string.Empty, 
                    Price = (float)(oi.Price ?? 0), 
                    Image = oi.Image ?? string.Empty,
                    Color = oi.Color ?? string.Empty, 
                    Size = oi.Size ?? 0 ,
					Quantity = oi.Quantity ?? 0,
					IsRating = oi.IsRating ?? false
				}).ToList()
            }).ToList();

            return View(ordersVM);
        }
		[HttpGet]
		[Authorize]
		public IActionResult _RatingModalPartial()
		{
			var model = new RatingProductVM();
			return PartialView(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult WriteRating(RatingProductVM model)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);

			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
			{
				return Json(new { success = false, redirectUrl = Url.Action("Login") });
			}

			if (!ModelState.IsValid)
			{
				return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
			}

			using (var transaction = db.Database.BeginTransaction())
			{
				try
				{
					var rating = new RatingProduct
					{
						ProductId = model.ProductId,
						Fullname = model.FullName,
						UserId = userId,
						CreateDate = DateTime.Now,
						RatePoint = model.RatePoint,
						DescRating = model.DescRating
					};
					db.Add(rating);
					db.SaveChanges();
					var orderItem = db.OrderItems.FirstOrDefault(o => o.OrderItemId == model.OrderItemId);
					if (orderItem != null)
					{
						orderItem.IsRating = true;
						db.SaveChanges();
					}
					transaction.Commit();
					return Json(new { success = true, message = "Your rating has been submitted successfully!" });

				}
				catch (Exception ex)
				{
					db.Database.RollbackTransaction();
					return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
				}
			}

		}

		[Authorize]
		public IActionResult Profile()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);
			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
			{
				return RedirectToAction("Login");
			}

			var user = db.Users.SingleOrDefault(u => u.UserId == userId);
			if (user == null)
			{
				return NotFound();
			}

			var model = new RegisterVM
			{
				Email = user.Email,
				Password = user.Password.ToMd5Hash(user.RandomKey),
				FullName = user.Fullname ?? "",
				Status = user.Status ?? true,
				RoleId = user.RoleId = 1,
				Address = user.Address,
				Phone = user.Phone,
				Avatar = user.Avatar,
				AboutUs = user.AboutUs
			};
			ViewData["UserFullName"] = user.Fullname;
			ViewData["UserAddress"] = user.Address;
			ViewData["UserAvatar"] = user.Avatar;

			return View(model);
		}

        [HttpPost]
        [Authorize]
        public IActionResult UpdateProfile(UpdateProfileVM model, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return RedirectToAction("Login");
                }

                var user = db.Users.SingleOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return NotFound();
                }

                user.Email = model.Email;
                user.Fullname = model.FullName;
                user.Address = model.Address;
                user.Phone = model.Phone;
                user.AboutUs = model.AboutUs;
                if (Avatar != null)
                {
                    user.Avatar = MyUtil.UpLoadAvatar(Avatar, "User");
                }
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    user.Password = model.NewPassword.ToMd5Hash(user.RandomKey);
                }
                db.SaveChanges();
                return RedirectToAction("Profile");
            }

            return View(model);
        }

        [Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[Authorize]
		public IActionResult AddToFavourite(int productId, int quantity = 1)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);
			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
			{
				return RedirectToAction("Login");
			}

			var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
			if (product == null)
			{
				return Json(new { success = false});
			}

			var favourite = db.Favourites.SingleOrDefault(f => f.UserId == userId) ?? new Favourite { UserId = userId };
			if (favourite.FavouriteId == 0)
			{
				db.Favourites.Add(favourite);
				db.SaveChanges();
			}

			if (db.FavouriteItems.Any(fi => fi.FavouriteId == favourite.FavouriteId && fi.ProductId == productId))
			{
				return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích" });
			}

			db.FavouriteItems.Add(new FavouriteItem
			{
				FavouriteId = favourite.FavouriteId,
				ProductId = productId,
				CreateDate = DateTime.Now
			});
			db.SaveChanges();

			return Json(new { success = true, message = "Sản phẩm đã được thêm vào danh sách yêu thích" });
		}
		[HttpPost]
		[Authorize]
		public IActionResult RemoveFavourite(int productId)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);
			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
			{
				return Json(new { success = false, message = "Vui lòng đăng nhập để sử dụng tính năng này" });
			}

			var favourite = db.Favourites.SingleOrDefault(f => f.UserId == userId);
			if (favourite == null)
			{
				return Json(new { success = false, message = "Danh sách yêu thích không tồn tại" });
			}

			var favouriteItem = db.FavouriteItems.SingleOrDefault(fi => fi.FavouriteId == favourite.FavouriteId && fi.ProductId == productId);
			if (favouriteItem == null)
			{
				return Json(new { success = false, message = "Sản phẩm không có trong danh sách yêu thích" });
			}

			db.FavouriteItems.Remove(favouriteItem);
			db.SaveChanges();

			return Json(new
			{
				success = true,
				message = "Sản phẩm đã được xóa khỏi danh sách yêu thích",
				productId = productId,
				favouriteCount = db.FavouriteItems.Count(fi => fi.FavouriteId == favourite.FavouriteId) // Trả về số lượng sản phẩm còn lại trong danh sách yêu thích
			});
		}

		[Authorize]
		public IActionResult WishList()
		{
			if (!int.TryParse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value, out int userId))
			{
				return RedirectToAction("Login");
			}

			var favouriteItems = db.FavouriteItems
				.Where(f => f.Favourite.UserId == userId)
				.Select(f => new FavouriteItemVM
			{
				FavouriteId = f.FavouriteId,
				ProductId = f.Product.ProductId,
				NameProduct = f.Product.NameProduct ?? "",
				Price = (float)(f.Product.Price ?? 0),
				Images = f.Product.Images.FirstOrDefault().ImageUrl
			})
			.ToList();
			return View("WishList", favouriteItems);
		}
	}
}

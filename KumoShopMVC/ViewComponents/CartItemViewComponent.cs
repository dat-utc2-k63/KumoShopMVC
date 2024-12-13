using Microsoft.AspNetCore.Mvc;
using KumoShopMVC.ViewModels;
using System.Collections.Generic;
using KumoShopMVC.Helpers;

namespace KumoShopMVC.ViewComponents
{
	public class CartItemViewComponent : ViewComponent
	{

		public IViewComponentResult Invoke()
		{
			// Lấy danh sách cart items từ session
			var cartItems = HttpContext.Session.Get<List<CartItemVM>>(MySetting.CART_KEY) ?? new List<CartItemVM>();

			return View("Default", cartItems); 
		}
	}
}

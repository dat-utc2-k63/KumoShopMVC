using KumoShopMVC.Data;
using KumoShopMVC.Models;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KumoShopMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly KumoShopContext db;

		public HomeController(ILogger<HomeController> logger, KumoShopContext context)
		{
			_logger = logger;
			db = context;
		}

		[Route("/404")]
		public IActionResult PageNotFound()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult Index()
		{
			var products = db.Products.ToList();

			var result = products.Select(p => new ProductVM
			{
				ProductId = p.ProductId,
				NameProduct = p.NameProduct ?? "",
				Brand = p.Brands ?? "",
				Gender = p.Gender.HasValue ? p.Gender.Value : false,
				Price = (float)(p.Price ?? 0),
				Images = db.Images
					.Where(img => img.ProductId == p.ProductId)
					.Select(img => img.ImageUrl ?? "")
					.ToList(),
				IsHot = p.IsHot ?? false,
				IsNew = p.IsNew ?? false
			}).ToList();

			return View(result);
		}

	}
}

using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KumoShopMVC.Controllers
{
    public class AdminController : Controller
    {
		private readonly KumoShopContext db;

		public IActionResult Index()
        {
            return View();
        }
		[Authorize]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var product = new Product
			{
				NameProduct = model.NameProduct,
				Brands = model.Brand,
				Gender = model.Gender,
				Price = model.Price,
				Discount = model.Discount,
				IsHot = model.IsHot,
				IsNew = model.IsNew
			};

			db.Products.Add(product);
			db.SaveChanges();

			return RedirectToAction("Index");
		}
		public IActionResult Edit()
		{
			return View();
		}

		[Authorize]
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var product = db.Products.Find(id);
			if (product == null)
			{
				return NotFound();
			}

			var model = new ProductVM
			{
				ProductId = product.ProductId,
				NameProduct = product.NameProduct,
				Brand = product.Brands,
				Gender = product.Gender ?? false,
				Price = (float)(product.Price ?? 0),
				Discount = (float)(product.Discount ?? 0),
				IsHot = product.IsHot ?? false,
				IsNew = product.IsNew ?? false
			};

			return View(model);
		}
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var product = db.Products.Find(model.ProductId);
			if (product == null)
			{
				return NotFound();
			}

			product.NameProduct = model.NameProduct;
			product.Brands = model.Brand;
			product.Gender = model.Gender;
			product.Price = model.Price;
			product.Discount = model.Discount;
			product.IsHot = model.IsHot;
			product.IsNew = model.IsNew;

			db.SaveChanges();

			return RedirectToAction("Index");
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var product = db.Products.Find(id);
			if (product == null)
			{
				return NotFound();
			}

			db.Products.Remove(product);
			db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}

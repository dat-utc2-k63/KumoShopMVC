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
		public IActionResult ProductList()
		{
			return View();
		}
		public IActionResult ProductCreate()
		{
			return View();
		}

		public IActionResult ProductEdit()
		{
			return View();
		}
        public IActionResult CategoryList()
        {
            return View();
        }

        public IActionResult CategoryCreate()
		{
			return View();
		}
		public IActionResult CategoryEdit()
		{
			return View();
		}
		public IActionResult OrderList()
		{
			return View();
		}
		public IActionResult OrderDetail()
		{
			return View();
		}
		public IActionResult RoleList()
		{
			return View();
		}
		public IActionResult RoleCreate()
		{
			return View();
		}
		public IActionResult RoleEdit()
		{
			return View();
		}
		public IActionResult UserList()
		{
			return View();
		}
		public IActionResult UserCreate()
		{
			return View();
		}
		public IActionResult UserEdit()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateProduct(ProductVM model)
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

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var product = db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
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
		public IActionResult DeleteProduct(int id)
		{
			var product = db.Products.FirstOrDefault(p => p.ProductId == id);
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

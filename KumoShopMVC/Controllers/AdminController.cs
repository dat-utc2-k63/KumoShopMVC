using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KumoShopMVC.Controllers
{
    public class AdminController : Controller
    {
		private readonly KumoShopContext db;
		
		public AdminController(KumoShopContext db)
		{
			this.db = db;
		}

		[HttpGet]
		public IActionResult Index()
        {
			var products = db.Products.ToList();
            return View(products);
        }

		[HttpGet]
		public IActionResult ProductList(int? category, int pageNumber = 1, int pageSize = 5)
		{

            var products = db.Products.Include(p => p.Category).AsQueryable();

            if (category.HasValue)
            {
                products = products.Where(p => p.CategoryId == category.Value);
            }

            var productList = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var result = productList.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                NameProduct = p.NameProduct ?? "",
                Brand = p.Brands ?? "",
                Gender = p.Gender.HasValue ? p.Gender.Value : false,
                Price = (float)(p.Price ?? 0),
                Discount = (float)(p.Discount ?? 0),
                IsHot = p.IsHot ?? false,
                IsNew = p.IsNew ?? false,
                Quantity = p.Quantity ?? 0,
                NameCategory = p.Category.NameCategory ?? "",
                RatePoint = (int)(p.RatingProducts.Average(r => r.RatePoint) ?? 0)
            }).ToList();

            // Gán thông tin phân trang vào ViewBag
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(result);
        }

		[HttpGet]
		public IActionResult ProductCreate()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ProductEdit(int id)
		{
            var product = db.Products
			.Include(p => p.Category)
			.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
			var productDetail = new ProductDetailVM
			{
				ProductId = product.ProductId,
				NameProduct = product.NameProduct ?? "",
                NameCategory = product.Category.NameCategory ?? "",
                Brand = product.Brands ?? "",
				Gender = product.Gender.HasValue ? product.Gender.Value : false,
				Images = db.Images
						.Where(img => img.ProductId == product.ProductId)
						.Select(img => img.ImageUrl ?? "")
						.ToList(),
								Colors = db.ProductColors
						.Where(pc => pc.ProductId == product.ProductId)
						.Select(pc => pc.Color != null ? pc.Color.ColorName : "")
						.ToList(),
								Sizes = db.ProductSizes
						.Where(ps => ps.ProductId == product.ProductId)
						.Select(ps => ps.Size != null ? ps.Size.SizeNumber : 0)
						.ToList(),
				Price = (float)(product.Price ?? 0),
				DescProduct = product.DescProduct ?? string.Empty,
				
				Quantity = product.Quantity ?? 0
			};

            return View(productDetail);
		}

        public IActionResult CategoryList(int id)
        {
            var categories = db.Categories.Select(c => new CategoryMenuVM
            {
                CaterogyId = c.CategoryId,
                NameCategory = c.NameCategory ?? "",
                MinPrice = c.Products.Min(p => (float?)p.Price) ?? 0,
                MaxPrice = c.Products.Max(p => (float?)p.Price) ?? 0,
                Products = c.Products.Select(p => new ProductDetailVM
                {
                    ProductId = p.ProductId,
                    NameProduct = p.NameProduct ?? "",
                    Price = (float)(p.Price ?? 0),
					DescProduct = p.DescProduct ?? string.Empty,
					
                }).ToList()
            }).ToList(); // Assuming each category has a Products collection
        

            return View(categories);
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

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateProduct(ProductVM model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}


			else
			{
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

				return RedirectToAction("ProductList");
			}

			
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductDetailVM model)
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
			//product.IsHot = model.IsHot;
			//product.IsNew = model.IsNew;
			product.DescProduct = model.DescProduct;
			

			db.SaveChanges();
            TempData["SuccessMessage"] = "Product created successfully!";

            return RedirectToAction("Index");
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteProduct(int id)
		{
            var product = db.Products
                             .FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

           
            // Xóa sản phẩm
            db.Products.Remove(product);
            db.SaveChanges();
            db.SaveChanges();

			return RedirectToAction("ProductList");
		}
	}
}

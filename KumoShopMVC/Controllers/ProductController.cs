using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static KumoShopMVC.ViewModels.ProductDetailVM;

namespace KumoShopMVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly KumoShopContext db;

		public ProductController(KumoShopContext context)
		{
			db = context;
		}

		public IActionResult Index(int? category)
		{
			var products = db.Products.AsQueryable();

			if (category.HasValue)
			{
				products = products.Where(p => p.CategoryId == category.Value);
			}

			var productList = products.ToList();

			var result = productList.Select(p => new ProductVM
			{
				ProductId = p.ProductId,
				NameProduct = p.NameProduct ?? "",
				Brand = p.Brands ?? "",
				Gender = p.Gender.HasValue ? p.Gender.Value : false,
				Price = (float)(p.Price ?? 0),
				Discount = (float)(p.Discount ?? 0),
				IsHot = p.IsHot ?? false,
				IsNew = p.IsNew ?? false
			}).ToList();

			return View(result);
		}

		public IActionResult Search(string? query)
		{
			var products = db.Products.AsQueryable();
			if (!string.IsNullOrEmpty(query))
			{
				products = products.Where(p => p.NameProduct.Contains(query));
			}

			var result = products.Select(p => new ProductVM
			{
				ProductId = p.ProductId,
				NameProduct = p.NameProduct ?? "",
				Brand = p.Brands ?? "",
				Gender = p.Gender.HasValue ? p.Gender.Value : false,
				Price = (float)(p.Price ?? 0),
				Discount = (float)(p.Discount ?? 0),
				IsHot = p.IsHot ?? false,
				IsNew = p.IsNew ?? false
			}).ToList();

			ViewData["SearchQuery"] = query;
			return View(result);
		}

		public IActionResult Detail(int id)
		{
			var product = db.Products
				.Include(p => p.Category)
				.FirstOrDefault(p => p.ProductId == id);

			if (product == null)
			{
				TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
				return Redirect("/404");
			}

			var productDetail = new ProductDetailVM
			{
				ProductId = product.ProductId,
				NameProduct = product.NameProduct ?? "",
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

				Quantity = product.Quantity ?? 0,
				SoLuongTon = 10,

			Ratings = db.RatingProducts
			.Where(r => r.ProductId == product.ProductId)
			.Select(r => new RatingProductVM
			{
				RatingId = r.RatingId,
				CreateDate = r.CreateDate,
				RatePoint = r.RatePoint ?? 0,
				DescRating = r.DescRating ?? "",
				ProductId = r.ProductId ,
				FullName = r.User.Fullname,
				Images = r.RatingImages
					.Select(img => img.ImageUrl ?? "")
					.ToList()
			})
			.ToList()

			};

			return View(productDetail);
		}

    }
	}

using KumoShopMVC.Data;
using KumoShopMVC.Helpers;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
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

        public IActionResult Index(int? category, int page = 1, int pageSize = 6)
        {
            var products = db.Products.AsQueryable();

            if (category.HasValue)
            {
                products = products.Where(p => p.CategoryId == category.Value);
            }

            var totalProducts = products.Count();

            var productList = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = productList.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                NameProduct = p.NameProduct ?? "",
                Brand = p.Brands ?? "",
                Gender = p.Gender.HasValue ? p.Gender.Value : false,
                Price = (float)(p.Price ?? 0),
                Discount = (float)(p.Discount ?? 0),
                Images = db.Images
                    .Where(img => img.ProductId == p.ProductId)
                    .Select(img => img.ImageUrl ?? "")
                    .ToList(),
                IsHot = p.IsHot ?? false,
                IsNew = p.IsNew ?? false
            }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["TotalProducts"] = totalProducts;

            return View(result);
        }
        public IActionResult Search(string? query, float? minPrice, float? maxPrice, string? brands, bool? genders, int page = 1, int pageSize = 6)
        {
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.NameProduct.Contains(query));
            }
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice);
            }

            if (genders != null)
            {
                products = products.Where(p => p.Gender == genders); // Assuming Gender is stored as string (e.g., "Male", "Female", "Unisex")
            }

            var totalProducts = products.Count();
            var productList = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var result = productList.Select(p => new ProductVM
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
                Discount = (float)(p.Discount ?? 0),
                IsHot = p.IsHot ?? false,
                IsNew = p.IsNew ?? false
            }).ToList();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewData["SearchQuery"] = query;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["TotalProducts"] = totalProducts;
            return View(result);
        }

        public IActionResult Detail(int id)
        {

            var product = db.Products
                .Include(p => p.Category)
                .Include(p => p.RatingProducts)
                .FirstOrDefault(p => p.ProductId == id);

            bool IsFavourite = false;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                IsFavourite = db.Favourites.Any(f => f.UserId == userId && f.FavouriteItems.Any(fi => fi.ProductId == id));
            }
            else
            {
                IsFavourite = false;
            }
            ViewData["IsFavourite"] = IsFavourite;
            if (product == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var productDetail = new ProductDetailVM
            {
                ProductId = product.ProductId,
                NameProduct = product.NameProduct ?? "",
                NameCategory = product.Category.NameCategory,
                Brand = product.Brands ?? "",
                Gender = product.Gender.HasValue ? product.Gender.Value : false,
                AverageRatePoint = product.RatingProducts.Any() ? product.RatingProducts.Average(r => r.RatePoint) : 0,
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
                ProductId = r.ProductId,
                FullName = r.User.Fullname,
                Images = r.RatingImages
                    .Select(img => img.ImageUrl ?? "")
                    .ToList()
            })
            .ToList()

            };

            return View(productDetail);
        }
        public IActionResult SortByPrice(string sortOrder, int page = 1, int pageSize = 6)
        {
            var products = db.Products.AsQueryable();

            // Kiểm tra điều kiện sắp xếp
            switch (sortOrder.ToLower())
            {
                case "asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    TempData["Message"] = "Thứ tự sắp xếp không hợp lệ!";
                    return RedirectToAction(nameof(Index));
            }

            var totalProducts = products.Count();

            var productList = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = productList.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                NameProduct = p.NameProduct ?? "",
                Brand = p.Brands ?? "",
                Gender = p.Gender.HasValue ? p.Gender.Value : false,
                Price = (float)(p.Price ?? 0),
                Discount = (float)(p.Discount ?? 0),
                Images = db.Images
                    .Where(img => img.ProductId == p.ProductId)
                    .Select(img => img.ImageUrl ?? "")
                    .ToList(),
                IsHot = p.IsHot ?? false,
                IsNew = p.IsNew ?? false
            }).ToList();

            // Tính tổng số trang
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["TotalProducts"] = totalProducts;
            ViewData["SortOrder"] = sortOrder;

            return View("Index", result);
        }

    }
}
using KumoShopMVC.Data;
using KumoShopMVC.Helpers;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using X.PagedList.Extensions;



namespace KumoShopMVC.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
		private readonly KumoShopContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(KumoShopContext context, IWebHostEnvironment webHostEnvironment)
		{
			db = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //[Authorize(Policy = "Admin")]
        public IActionResult Index(string filter = "dd-MM-yyyy")
        {
            var monthlyReports = db.Orders
                .Include(o => o.OrderItems)
                .AsEnumerable()
                .Where(o => o.OrderDate.HasValue) 
                .GroupBy(o => o.OrderDate.Value.ToString(filter))
                .Select(g => new
                {
                    MonthYear = g.Key,
                    TotalOrders = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderItems != null
                                              ? o.OrderItems.Sum(oi => (oi.Price ?? 0) * (oi.Quantity ?? 0))
                                              : 0)
                })
                .ToList();

            var dashBoardAdmin = new DashBoardAdmin
            {
                TotalUser = db.Users.Count(u =>u.RoleId == 1), // Tổng khách hàng
                totalProducts = db.Products.Count(), // Tổng số sản phẩm
                totalOrders = db.Orders.Count(),
                totalRevenue = db.Orders
                                .SelectMany(o => o.OrderItems)
                                .Sum(oi => (oi.Price ?? 0) * (oi.Quantity ?? 0)), // Tổng doanh thu

                // Dữ liệu biểu đồ
                monthYears = monthlyReports.Select(r => r.MonthYear).ToList(),
                totalOrdersData = monthlyReports.Select(r => r.TotalOrders).ToList(),
                totalRevenueData = monthlyReports.Select(r => r.TotalRevenue).ToList()
            };

            // Truyền ViewModel vào View
            return View(dashBoardAdmin);
        }


        [HttpGet]
        public IActionResult ProductList(int pageNumber = 1, int pageSize = 5)
        {
            var products = db.Products.AsQueryable()
                .Include(p =>p.Category)
                .Include(p =>p.RatingProducts);  // Removed the Include for Category

            var productList = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var result = productList.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                NameProduct = p.NameProduct ?? "",
                NameCategory = p.Category.NameCategory ??"",
                Brand = p.Brands ?? "",
                Gender = p.Gender,
                Price = (float)(p.Price ?? 0),
                IsHot = p.IsHot ?? false,
                IsNew = p.IsNew ?? false,
                Images = db.Images
                    .Where(img => img.ProductId == p.ProductId)
                    .Select(img => img.ImageUrl ?? "")
                    .ToList(),
                Colors = db.ProductColors
                    .Where(pc => pc.ProductId == p.ProductId)
                    .Select(pc => pc.Color != null ? pc.Color.ColorName : "")
                    .ToList(),
                Sizes = db.ProductSizes
                    .Where(ps => ps.ProductId == p.ProductId)
                    .Select(ps => ps.Size != null ? ps.Size.SizeNumber : 0)
                    .ToList(),
                AverageRatePoint = p.RatingProducts.Any() ? p.RatingProducts.Average(r => r.RatePoint) : 0
            }).ToList();

            // Gán thông tin phân trang vào ViewBag
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(result);
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

		[HttpGet]
        public IActionResult CategoryList(int? id, int pageNumber = 1, int pageSize = 2)
        {
			int totalCategories = db.Categories.Count();
            var categories = db.Categories
							.OrderBy(c => c.NameCategory) // Optional: Order categories by name
							.Skip((pageNumber - 1) * pageSize)
							.Take(pageSize)
							.Select(c => new CategoryMenuVM
							{
								CaterogyId = c.CategoryId,
								NameCategory = c.NameCategory ?? "",
                                CreateDate = c.CreateDate,
							})
							.ToList();

            // Pass data to the view
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCategories / pageSize);
            ViewBag.CurrentPage = pageNumber;

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
		
		[HttpGet]
		public IActionResult RoleCreate()
		{
			return View(new RoleVM());
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleCreate(RoleVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var role = new Role
                {
                    NameRole = model.NameRole,
                    CreateDate = DateTime.Now
                };

                db.Roles.Add(role);
                db.SaveChanges();

                return RedirectToAction("RoleList", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving data: " + ex.Message);
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult RoleEdit(int id)
        {
            // Lấy vai trò theo id từ cơ sở dữ liệu
            var role = db.Roles.FirstOrDefault(r => r.RoleId == id);

            // Kiểm tra nếu không tìm thấy vai trò
            if (role == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy vai trò
            }

            // Chuyển đổi từ Role sang RoleVM
            var model = new RoleVM
            {
                RoleId = role.RoleId,
                NameRole = role.NameRole,
                CreateDate = role.CreateDate
            };

            // Truyền RoleVM vào View
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleEdit(RoleVM model)
        {
            if (!ModelState.IsValid)
            {
                // Nếu ModelState không hợp lệ, trả lại View với dữ liệu người dùng đã nhập
                return View(model);
            }

            // Tìm role từ cơ sở dữ liệu
            var existed_role = db.Roles.FirstOrDefault(r => r.RoleId == model.RoleId);
            if (existed_role == null)
            {
                // Nếu không tìm thấy role, trả về lỗi 404
                return NotFound();
            }

            // Cập nhật thông tin
            existed_role.NameRole = model.NameRole;
            existed_role.CreateDate = model.CreateDate;

            // Lưu thay đổi
            db.Update(existed_role);
            db.SaveChanges();

            // Chuyển hướng về danh sách vai trò sau khi cập nhật thành công
            return RedirectToAction("RoleList", "Admin");
        }
        public IActionResult UserList(int? page)
        {
            int pageSize = 5; // Số lượng mục hiển thị trên mỗi trang
            int pageNumber = page ?? 1; // Lấy số trang từ tham số, mặc định là trang 1

            // Lấy danh sách người dùng từ cơ sở dữ liệu và chuyển đổi thành một danh sách trang
            var users = db.Users
                .Select(u => new UserVM
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    Fullname = u.Fullname,
                    Phone = u.Phone,
                    Address = u.Address,
                    Status = u.Status ?? false,
                    Avatar = u.Avatar,
                    CreateDate = u.CreateDate,
                    Namerole = u.Role != null ? u.Role.NameRole : "No Role"
                })
                .OrderBy(u => u.UserId) // Sắp xếp theo UserId để đảm bảo phân trang hoạt động đúng
                .ToPagedList(pageNumber, pageSize); // Phân trang

            // Trả về view với model là danh sách phân trang
            return View(users);
        }

        public IActionResult UserCreate()
        {
            ViewBag.Roles = db.Roles
            .Select(r => new SelectListItem
            {
                Value = r.RoleId.ToString(),
                Text = r.NameRole
            })
            .ToList();
            return View(new UserVM());
		}
        [HttpPost]
       
        public IActionResult UserCreate(UserVM model, IFormFile Avatar)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                return View(model);
            }

            var role = db.Roles.FirstOrDefault(r => r.RoleId == model.RoleId);
            if (role == null)
            {
                ModelState.AddModelError("RoleId", "Selected role not found in the database.");
                ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                Fullname = model.Fullname,
                Phone = model.Phone,
                Address = model.Address,
                Status = model.Status,
                CreateDate = model.CreateDate.HasValue ? model.CreateDate.Value : DateTime.Now,
                RoleId = role.RoleId
            };

            if (Avatar != null && Avatar.Length > 0)
            {
                if (Avatar.ContentType.StartsWith("image/"))
                {
                    try
                    {
                        string fileName = MyUtil.UpLoadAvatar(Avatar, "User");
                        user.Avatar = fileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Avatar", "Error uploading avatar: " + ex.Message);
                        ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("Avatar", "Please upload a valid image file.");
                    ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                    return View(model);
                }
            }

            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("UserList", "Admin");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error saving user: " + ex.Message;
                ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult UserEdit(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole", user.RoleId);

            var model = new UserVM
            {
                UserId = user.UserId,
                Email = user.Email,
                Fullname = user.Fullname ?? "",
                Status = user.Status ?? true,
                RoleId = user.RoleId ?? 0,
                Namerole = user.Role.NameRole,
                Address = user.Address,
                Phone = user.Phone,
                Avatar = user.Avatar
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UserEdit(UserVM model, IFormFile? Avatar)
        {
          
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Thông tin không hợp lệ. Vui lòng kiểm tra lại.";
                return View(model);
            }

            var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId);
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole", user.RoleId);
            if (user == null)
            {
                TempData["Error"] = "Người dùng không tồn tại.";
                return RedirectToAction("UserList", "Admin");
            }

            user.Email = model.Email;
            user.Fullname = model.Fullname;
            user.Phone = model.Phone;
            user.Address = model.Address;
            user.Status = model.Status;
            user.RoleId = model.RoleId;

            // Xử lý ảnh đại diện nếu có thay đổi
            if (Avatar != null)
            {
                if (Avatar.Length > 0 && Avatar.ContentType.StartsWith("image/"))
                {
                    try
                    {
                        user.Avatar = MyUtil.UpLoadAvatar(Avatar, "User");
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = "Lỗi khi tải lên ảnh đại diện: " + ex.Message;
                        return View(model);
                    }
                }
                else
                {
                    TempData["Error"] = "Tệp không hợp lệ. Vui lòng chọn một hình ảnh.";
                    return View(model);
                }
            }
            // Nếu Avatar không thay đổi, giữ nguyên giá trị cũ

            // Cập nhật đối tượng và lưu vào cơ sở dữ liệu
            try
            {
                db.Entry(user).State = EntityState.Modified;  // Đảm bảo rằng EF nhận diện tất cả thay đổi
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thông tin thành công!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi lưu thông tin: " + ex.Message;
                return View(model);
            }

            return RedirectToAction("UserList", "Admin");
        }


        public IActionResult DeleteUser(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);  // Tìm user theo UserId

            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user); 
			TempData["sucess"] = "Xóa User thành công";
            db.SaveChanges();  
            return RedirectToAction("UserList");
        }
		public IActionResult DeleteRole(int roleId)
		{
			var role = db.Roles.FirstOrDefault(r => r.RoleId == roleId);  // Tìm role theo RoleId
			if(role == null)
			{
				return NotFound();  // Nếu không tìm thấy role, trả về lỗi 404
            }
            db.Roles.Remove(role); 
            TempData["sucess"] = "Xóa Role thành công";
            db.SaveChanges();
            return RedirectToAction("RoleList");  // Quay lại trang danh sách role sau khi xóa
        }

        public IActionResult RoleList(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var roles = db.Roles
                                .OrderBy(r => r.RoleId)
                                .Select(r => new RoleVM
                                {
                                    RoleId = r.RoleId,
                                    NameRole = r.NameRole,
                                    CreateDate = r.CreateDate
                                })
                                .ToPagedList(pageNumber, pageSize);

            return View(roles);
        }

        public IActionResult Contact()
		{
			return View();
		}

        [HttpGet]
        public IActionResult ProductCreate()
        {
            ViewBag.Categories = db.Categories
                .Select(r => new SelectListItem
                {
                    Value = r.CategoryId.ToString(),
                    Text = r.NameCategory
                })
                .ToList();

            var colors = db.Colors.ToList();
            var sizes = db.Sizes.ToList();
            ViewBag.Colors = colors;
            ViewBag.Sizes = sizes;

            return View(new ProductVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductCreate(ProductVM model, List<IFormFile>? Images)
        {
            if (ModelState.IsValid)
            {
                        var product = new Product
                        {
                            NameProduct = model.NameProduct,
                            Brands = model.Brand,
                            Gender = model.Gender,
                            Price = model.Price,
                            IsHot = model.IsHot,
                            IsNew = model.IsNew,
                            CategoryId = model.CategoryId,
                            CreateDate = DateTime.Now
                        };
                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    db.Add(product);
                    db.SaveChanges();
                    var productSize = new List<ProductSize>();

                    if (model.Sizes != null && model.Sizes.Any())
                    {
                        foreach (var item in model.Sizes)
                        {
                            productSize.Add(new ProductSize()
                            {
                                ProductId = product.ProductId,
                                SizeId = item
                            });
                        }
                    }

                    db.AddRange(productSize);
                    db.SaveChanges();
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    var productColor = new List<ProductColor>();

                    if (model.Colors != null && model.Colors.Any())
                    {
                        foreach (var item in model.Colors)
                        {
                            int colorId = int.Parse(item);
                            productColor.Add(new ProductColor()
                            {
                                ProductId = product.ProductId,
                                ColorId = colorId
                            });
                        }
                    }
                    db.AddRange(productColor);
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    var image = new List<Image>();

                    if (model.Images != null && model.Images.Any())
                    {
                        var uploadedImageNames = MyUtil.UpLoadListProduct(Images, "products");
                        foreach (var item in model.Images)
                        {
                            image.Add(new Image()
                            {
                                ProductId = product.ProductId,
                                ImageUrl = item
                            });
                        }
                    }
                    db.AddRange(image);
                    db.SaveChanges();
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
                return RedirectToAction("ProductList");

			}
			else
            {
                return View(model);
            }
        }

        
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductDetailVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductEdit", model);
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
            product.IsHot = model.IsHot;
            product.IsNew = model.IsNew;
            product.DescProduct = model.DescProduct;
            //product.Discount = 0;
            //product.Status = true;
            //product.CreateDate = DateTime.Now;
            //product.Quantity = 100;

            //product.Images = model.Images
            db.SaveChanges();
            TempData["SuccessMessage"] = "Product created successfully!";

            return RedirectToAction("ProductList");
        }

        private static DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
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

        [HttpPost]
        public IActionResult CreateCategory(string NameCategory, DateTime DateCreate)
        {
            if (string.IsNullOrWhiteSpace(NameCategory))
            {
                // Handle validation error
                ModelState.AddModelError("NameCategory", "Category name is required.");
                return View();
            }

            // Create a new category object
            var newCategory = new Category
            {
                NameCategory = NameCategory,
                CreateDate = DateTime.Now
            };

            // Add the new category to the database
            db.Categories.Add(newCategory);
            db.SaveChanges();

            // Redirect to the category list page
            return RedirectToAction("CategoryList");
        }

    }
}

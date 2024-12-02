﻿using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KumoShopMVC.Controllers
{
    public class AdminController : Controller
    {
		private readonly KumoShopContext db;
		public AdminController(KumoShopContext context)
		{
			db = context;
		}

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
			var roles = db.Roles.Select(
								r => new RoleVM
								{
									RoleId = r.RoleId,
									NameRole = r.NameRole,
									CreateDate = r.CreateDate
								}).ToList();
			return View(roles);
		}
		[HttpGet]
		public IActionResult RoleCreate()
		{
			return View(new RoleVM());
		}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleCreate(RoleVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			if (ModelState.IsValid)
			{
			var role = new Role
			{
				NameRole = model.NameRole,
				CreateDate = DateTime.Now
			};
				//Lưu vào cơ sở dữ liệu
				db.Roles.Add(role);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);

		}
        public IActionResult RoleEdit(int id)
        {
            // Lấy vai trò theo id từ cơ sở dữ liệu
            var role = db.Roles.FirstOrDefault(r => r.RoleId == id);

            // Kiểm tra nếu không tìm thấy vai trò
            if (role == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy vai trò
            }

   
            var model = new RoleVM
            {
                RoleId = role.RoleId,
                NameRole = role.NameRole,
                
                CreateDate = role.CreateDate
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleEdit(RoleVM model)
        {
            if (model == null)
            {
                ModelState.AddModelError("", "Invalid model data.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var role = db.Roles.FirstOrDefault(r => r.RoleId == model.RoleId);

                // Kiểm tra nếu không tìm thấy vai trò
                if (role == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy vai trò
                }

                // Cập nhật thông tin vai trò
                role.NameRole = model.NameRole;
                if (model.CreateDate.HasValue)
                {
                    role.CreateDate = model.CreateDate.Value;
                }
                // Giữ nguyên CreateDate nếu không có giá trị mới
                db.Update(role);
                db.SaveChanges();

                return RedirectToAction("RoleList","Admin"); // Redirect đến danh sách vai trò sau khi lưu
            }

            return View(model); // Nếu không hợp lệ, hiển thị lại form với lỗi
        }


        public IActionResult UserList()
		{
            var users = db.Users
      .Select(u => new UserVM
      {
          UserId = u.UserId,
          Email = u.Email,
          Fullname = u.Fullname,
          Phone = u.Phone,
          Address = u.Address,
          Status = u.Status,
          Avatar = u.Avatar,
          CreateDate = u.CreateDate,
          Namerole = u.Role != null ? u.Role.NameRole: "No Role"
      })
      .ToList();
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
        [ValidateAntiForgeryToken]
		public IActionResult UserCreate(UserVM model)
		{
            if (!ModelState.IsValid)
            {
                //ViewBag.Roles = db.Roles.Select(r => new { r.RoleId, r.NameRole }).ToList();
				ViewBag.Roles=new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                return View(model);
            }
            var role = db.Roles.FirstOrDefault(r => r.NameRole == model.Namerole);
            if (role == null)
            {
                ModelState.AddModelError("Namerole", "Role not found in the database.");
                ViewBag.Roles = new SelectList(db.Roles.ToList(), "RoleId", "NameRole");
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                Fullname = model.Fullname,
                Phone = model.Phone,
                Address = model.Address,
                Status = model.Status ?? false, // Mặc định false nếu không được cung cấp
                Avatar = model.Avatar,
                CreateDate = model.CreateDate ?? DateTime.Now, // Nếu không có giá trị, dùng ngày hiện tại
                RoleId = role.RoleId // Gán RoleId dựa trên tên vai trò
            };

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }
        public IActionResult UserEdit(int id)
		{
			var user = db.Users
			.Where(u => u.UserId == id)
			.Select(u => new UserVM
			{
				UserId = u.UserId,
				Email = u.Email,
				Fullname = u.Fullname,
				Phone = u.Phone,
				Address = u.Address,
				Status = u.Status,
				Avatar = u.Avatar,
				CreateDate = u.CreateDate,
				Namerole = u.Role.NameRole
			}).FirstOrDefault();
			if(user == null)
			{
                return NotFound();
            }
                return View(user);
		}
        //public IActionResult UserEdit(UserVM user)
        //{
        //	var existed_user=db.Users.FirstOrDefault(u => u.UserId == user.UserId);
        //          if (existed_user == null)
        //	{
        //              return NotFound();
        //          }
        //	if (ModelState.IsValid)
        //	{

        //	}
        //      }
       
        public IActionResult DeleteUser(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);  // Tìm user theo UserId

            if (user == null)
            {
                return NotFound();  // Nếu không tìm thấy user, trả về lỗi 404
            }

            db.Users.Remove(user);  // Xóa user khỏi cơ sở dữ liệu
			TempData["sucess"] = "Xóa User thành công";
            db.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            return RedirectToAction("UserList");  // Quay lại trang danh sách user sau khi xóa
        }
		public IActionResult DeleteRole(int roleId)
		{
			var role = db.Roles.FirstOrDefault(r => r.RoleId == roleId);  // Tìm role theo RoleId
			if(role == null)
			{
				return NotFound();  // Nếu không tìm thấy role, trả về lỗi 404
            }
            db.Roles.Remove(role);  // Xóa role khỏi cơ sở dữ liệu
            TempData["sucess"] = "Xóa Role thành công";
            db.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            return RedirectToAction("RoleList");  // Quay lại trang danh sách role sau khi xóa
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

using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KumoShopMVC.Controllers
{
    public class AdminController : Controller
    {
		private readonly KumoShopContext db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(KumoShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;
            _webHostEnvironment = webHostEnvironment;
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
            // Lấy danh sách đơn hàng từ cơ sở dữ liệu
            var orders = db.Orders
                .Select(o => new OrderVM
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    DescOrder = o.DescOrder,
                    ShippingDate = o.ShippingDate,
                    FullName = o.Fullname,
                    Address = o.Address,
                    Phone = o.Phone,
                    PaymentMethode = o.PaymentMethode,
					StatusShipping = o.Status.NameStatus, 
                    OrderItems = db.OrderItems.Where(oi => oi.OrderId == o.OrderId).Select(oi => new OrderItemVM
                    {
                        NameProduct = oi.Product.NameProduct,
                        Quantity = (int)oi.Product.Quantity,
                        Price = (float)oi.Product.Price
                    }).ToList()
                })
                .ToList();

            // Truyền dữ liệu vào view
            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderEdit(int id)
        {
            var order = db.Orders
                .Include(o => o.Status)  // Bao gồm thông tin trạng thái
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();  // Nếu không tìm thấy đơn hàng, trả về lỗi 404
            }

            // Chuyển dữ liệu từ Order thành OrderVM
            var orderVM = new OrderVM
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate ?? DateTime.MinValue,
                DescOrder = order.DescOrder ?? "No Description",
                ShippingDate = order.ShippingDate,
                FullName = order.Fullname ?? "Unknown",
                Address = order.Address ?? "No Address",
                Phone = order.Phone ?? "No Phone",
                PaymentMethode = order.PaymentMethode ?? "Unknown",
                StatusShipping = order.Status.NameStatus,  // Lấy tên trạng thái giao hàng
                OrderItems = order.OrderItems.Select(oi => new OrderItemVM
                {
                    NameProduct = oi.Product?.NameProduct ?? "No Product",
                    Quantity = oi.Product?.Quantity ?? 0,
                    Price = (float)oi.Product.Price
                }).ToList()
            };

            return View(orderVM);  // Truyền OrderVM vào view
        }

        // Cập nhật đơn hàng sau khi sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderEdit(OrderVM model)
        {
            if (ModelState.IsValid)
            {
                var order = db.Orders.FirstOrDefault(o => o.OrderId == model.OrderId);
                if (order == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin đơn hàng
                order.OrderDate = model.OrderDate;
                order.DescOrder = model.DescOrder;
                order.ShippingDate = model.ShippingDate;
                order.Fullname = model.FullName;
                order.Address = model.Address;
                order.Phone = model.Phone;
                order.PaymentMethode = model.PaymentMethode;

                db.SaveChanges();
                return RedirectToAction("OrderList");  // Sau khi lưu thay đổi, chuyển về danh sách đơn hàng
            }
            return View(model);  // Nếu có lỗi validation, hiển thị lại form
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound(); // Nếu không tìm thấy đơn hàng
            }

            // Xóa các mục trong đơn hàng (nếu có) trước khi xóa đơn hàng
            var orderItems = db.OrderItems.Where(oi => oi.OrderId == order.OrderId).ToList();
            db.OrderItems.RemoveRange(orderItems); // Xóa tất cả các mục của đơn hàng

            db.Orders.Remove(order); // Xóa đơn hàng
            db.SaveChanges(); // Lưu thay đổi

            return RedirectToAction("OrderList"); // Quay lại danh sách đơn hàng
        }

        public IActionResult OrderDetail()
		{
            // Lấy danh sách đơn hàng từ cơ sở dữ liệu
            var orders = db.OrderItems
                .Select(oi => new OrderItemVM
                {

                    ProductId = oi.ProductId,
                    OrderId = oi.OrderId,
                    NameProduct = oi.NameProduct,
                    Price = (float)oi.Price,
                    Image = oi.Image,
                    Color = oi.Color,
                    Size = (int)oi.Size,
                    Quantity = (int)oi.Quantity,
                    SubTotal = (double)oi.SubTotal
                })
                .ToList();

            // Truyền dữ liệu vào view
            return View(orders);
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
			var contact = db.Contacts
                .Select(c => new ContactVM
                {
                    ContactId = c.ContactId,
                    Name = c.Name,
                    Email = c.Email,
                    Subject = c.Subject,
                    DescContact = c.DescContact,
                    Status = c.Status,
                    CreateDate = c.CreateDate,
                })
                .ToList();
            return View(contact);
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

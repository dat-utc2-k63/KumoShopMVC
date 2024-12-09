using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KumoShopMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly KumoShopContext db;

        public ContactController(KumoShopContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactVM model)
        {
            var contact = new Contact
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                DescContact = model.DescContact,
                Status = false,
                CreateDate = DateTime.Now
            };
            db.Add(contact);
            db.SaveChanges();
            return View(contact);
        }
    }
}

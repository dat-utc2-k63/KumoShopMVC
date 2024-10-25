using Microsoft.AspNetCore.Mvc;

namespace KumoShopMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

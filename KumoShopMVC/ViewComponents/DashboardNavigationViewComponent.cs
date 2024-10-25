using Microsoft.AspNetCore.Mvc;

namespace KumoShopMVC.ViewComponents
{
	public class DashboardNavigationViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View("Default");
		}
	}
}

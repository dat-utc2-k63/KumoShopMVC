using KumoShopMVC.Data;
using KumoShopMVC.Helpers;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace KumoShopMVC.ViewComponents
{
	public class DashboardNavigationViewComponent : ViewComponent
	{
		private readonly KumoShopContext db;
		public DashboardNavigationViewComponent(KumoShopContext context) => db = context;

		public IViewComponentResult Invoke()
		{
			return View("Default");
		}

	}
}

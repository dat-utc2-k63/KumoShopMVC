using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KumoShopMVC.ViewComponents
{
	public class CategoryMenuViewComponent : ViewComponent
	{
		private readonly KumoShopContext db;

		public CategoryMenuViewComponent(KumoShopContext context) => db = context;
		public IViewComponentResult Invoke()
		{
			var data = db.Categories.Select(cate => new CategoryMenuVM
			{
				CaterogyId = cate.CategoryId,
				NameCategory = cate.NameCategory,
				CreateDate = cate.CreateDate
			}).OrderBy(p => p.NameCategory);
			return View("Default", data);
		}

	}
}

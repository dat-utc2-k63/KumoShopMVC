﻿using KumoShopMVC.Data;

namespace KumoShopMVC.ViewModels
{
	public class CategoryMenuVM
	{
		public int CaterogyId { get; set; }
		public string NameCategory { get; set; }
		public DateTime? CreateDate { get; set; }


        public ICollection<ProductDetailVM>? Products { get; set; }
    }
}

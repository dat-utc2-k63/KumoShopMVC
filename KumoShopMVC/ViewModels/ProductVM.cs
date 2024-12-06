using KumoShopMVC.Data;
using System.Linq;

namespace KumoShopMVC.ViewModels
{
	public class ProductVM
	{
		
		public int ProductId { get; set; }
		public string NameProduct { get; set; }
		public string? NameCategory { get; set; }
		public bool Status { get; set; }
		public string? Brand { get; set; }
		public bool Gender { get; set; }
		public bool IsHot { get; set; }
		public bool IsNew { get; set; }
		public float? Discount { get; set; }

		public List<string>? Colors { get; set; }
		public List<int>? Sizes { get; set; }
		public List<string>? Images { get; set; }
		public float? Price { get; set; }

		public int Quantity { get; set; }
		public int RatePoint { get; set; }

        public List<ProductVM> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


    }
	public class ProductDetailVM
	{
		public int ProductId { get; set; }
<<<<<<< HEAD
		public string? NameProduct { get; set; }
		public string? NameCategory	{ get; set; }
=======
		public string NameProduct { get; set; }
		public string NameCategory { get; set; }
>>>>>>> product-category-view
		public bool Status { get; set; }
		public List<string>? Colors { get; set; }
		public List<int>? Sizes { get; set; }
		public List<string>? Images { get; set; }
		public float? Price { get; set; }
		public string? DescProduct { get; set; }
		public string? Brand { get; set; }
		public bool Gender { get; set; }
		public int Quantity { get; set; }
		public int SoLuongTon { get; set; }
		public float? Discount { get; set; }
		public List<RatingProductVM> Ratings { get; set; } = new List<RatingProductVM>();
<<<<<<< HEAD
        public double? AverageRatePoint { get; set; }
=======

        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

>>>>>>> product-category-view
    }

}

using KumoShopMVC.Data;

namespace KumoShopMVC.ViewModels
{
	public class ProductVM
	{
		public int ProductId { get; set; }
		public string NameProduct { get; set; }
		public string NameCategory { get; set; }
		public bool Status { get; set; }
		public string? Brand { get; set; }
		public bool Gender { get; set; }
		public bool? IsHot { get; set; }
		public bool? IsNew { get; set; }
		public float? Discount { get; set; }

		public List<string>? Colors { get; set; }
		public List<int>? Sizes { get; set; }
		public List<string>? Images { get; set; }
		public float? Price { get; set; }

	}
	public class ProductDetailVM
	{
		public int ProductId { get; set; }
		public string NameProduct { get; set; }
		public string NameCategory	{ get; set; }
		public bool Status { get; set; }
		public List<string>? Colors { get; set; }
		public List<int>? Sizes { get; set; }
		public List<string>? Images { get; set; }
		public float? Price { get; set; }
		public string DescProduct { get; set; }
		public string Brand { get; set; }
		public bool Gender { get; set; }
		public int Quantity { get; set; }
		public int SoLuongTon { get; set; }
		public float? Discount { get; set; }
		public List<RatingProductVM> Ratings { get; set; } = new List<RatingProductVM>();


	}

}

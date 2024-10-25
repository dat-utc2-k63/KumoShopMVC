namespace KumoShopMVC.ViewModels
{
	public class FavouriteItemVM
	{
		public int FavouriteId { get; set; }
		public int ProductId { get; set; }
		public int UserId { get; set; }
		public bool Status { get; set; }

		public string? NameProduct { get; set; }
		public float Price { get; set; }
		public int Quantity { get; set; }
		public string? Images { get; set; }
	}
}

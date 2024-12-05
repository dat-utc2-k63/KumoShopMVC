namespace KumoShopMVC.ViewModels
{
	public class OrderItemVM
	{
		public int OrderItemId { get; set; }

		public int? ProductId { get; set; }
		public bool Status { get; set; }

		public string NameProduct { get; set; }
		public float Price { get; set; }
		public string? Image { get; set; }
		public string? Color { get; set; }
		public int Size { get; set; }
		public int OrderId { get; set; }
		public int Quantity { get; set; }
		public double SubTotal;
		public bool IsRating { get; set; }
	}
}

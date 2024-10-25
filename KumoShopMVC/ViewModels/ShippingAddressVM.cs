namespace KumoShopMVC.ViewModels
{
	public class ShippingAddressVM
	{
		public int UserId { get; set; }
		public int ShippingAddressId { get; set; }
		public string? FullName { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public DateTime? CreateDate { get; set; }
	}
}

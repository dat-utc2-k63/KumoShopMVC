using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace KumoShopMVC.ViewModels
{
	public class OrderVM
	{
		public int OrderId { get; set; }
		public DateTime? OrderDate { get; set; }
		public string? DescOrder { get; set; }
		public DateTime? ShippingDate { get; set; }
		public string? StatusShipping { get; set; }
		public string? FullName { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string PaymentMethode { get; set; }
		public List<OrderItemVM> OrderItems { get; set; } = new List<OrderItemVM>();


	}
}

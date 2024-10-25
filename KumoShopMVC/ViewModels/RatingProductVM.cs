using System.Collections.Generic;

namespace KumoShopMVC.ViewModels
{
	public class RatingProductVM
	{
		public int RatingId { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public DateTime? CreateDate { get; set; }
		public int RatePoint { get; set; }
		public string? DescRating { get; set; }
		public List<string>? Images { get; set; } = new List<string>();
		public string? FullName { get; set; }


	}
}

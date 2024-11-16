using KumoShopMVC.Data;

namespace KumoShopMVC.ViewModels
{
	public class CategoryMenuVM
	{
		public int CaterogyId { get; set; }
		public string NameCategory { get; set; }

		public DateTime? CreateDate { get; set; }

        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

        public ICollection<ProductDetailVM>? Products { get; set; }
    }
}

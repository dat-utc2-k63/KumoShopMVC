namespace KumoShopMVC.ViewModels
{
	public class CartModel
	{
        public List<CartItemVM> CartItems { get; set; }
        public int Quantity { get; set; }
	}
}

using KumoShopMVC.ViewModels;

namespace KumoShopMVC.Services
{
	public interface IVnPayService
	{
		string CreatePaymentUrl(HttpContext context, VnPayRequestModel model);
		VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
	}
}

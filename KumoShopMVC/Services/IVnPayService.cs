using KumoShopMVC.ViewModels;

namespace KumoShopMVC.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, PaymentInformationModel model);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}

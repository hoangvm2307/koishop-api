using KoishopBusinessObjects.VnPayModel;
using Microsoft.AspNetCore.Http;

namespace KoishopServices.Interfaces.Third_Party
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}

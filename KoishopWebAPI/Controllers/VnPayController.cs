using KoishopBusinessObjects.VnPayModel;
using KoishopServices.Interfaces;
using KoishopServices.Interfaces.Third_Party;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers
{
    public class VnPayController : BaseApiController
    {
        private readonly IVnPayService _vnPayService;

        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentInformationModel paymentModel)
        {
            var paymentUrl = _vnPayService.CreatePaymentUrl(paymentModel, HttpContext);
            return Ok(new { PaymentUrl = paymentUrl });
        }

        [HttpGet]
        public IActionResult ExecutePayment()
        {
            IQueryCollection queryParams = HttpContext.Request.Query;
            var paymentResponse = _vnPayService.PaymentExecute(queryParams);
            return Ok(paymentResponse);
        }
    }
}

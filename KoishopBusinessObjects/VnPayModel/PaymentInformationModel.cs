using System.ComponentModel.DataAnnotations.Schema;

namespace KoishopBusinessObjects.VnPayModel
{
    [NotMapped]
    public class PaymentInformationModel
    {
        public string OrderId { get; set; }
        public double Amount { get; set; }
    }
}

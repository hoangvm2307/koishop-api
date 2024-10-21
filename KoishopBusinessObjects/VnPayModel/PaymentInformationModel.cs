using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects.VnPayModel
{
    [NotMapped]
    public class PaymentInformationModel
    {
        public string OrderId { get; set; }
        public double Amount { get; set; }
    }
}

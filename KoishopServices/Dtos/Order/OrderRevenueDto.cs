using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Dtos.Order
{
    public class OrderRevenueDto
    {
        public decimal Revenue { get; set; }
        public int TotalReservedOrder { get; set; }
        public int CompletedOrders { get; set; }
    }
}

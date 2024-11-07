using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Dtos.Order
{
    public class OrderRevenueDto
    {
        public decimal RevenueFromKoiShop { get; set; }
        public decimal RevenueFromCommission {  get; set; }
        public int TotalCommissionOrders { get; set; }
        public int CompletedOrders { get; set; }
    }
}

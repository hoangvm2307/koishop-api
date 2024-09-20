using DTOs.OrderItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Order;

public class OrderCreationDto
{
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public int? UserId { get; set; }
}

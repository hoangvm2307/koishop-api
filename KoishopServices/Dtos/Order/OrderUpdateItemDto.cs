using DTOs.OrderItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Order;

public class OrderUpdateItemDto
{
    public List<OrderItemCreationDto> OrderItemCreationDtos { get; set; }
}

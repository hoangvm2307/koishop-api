using DTOs.Order;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetListOrder();
    Task<OrderDto> GetOrderById(int id);
    Task<string> AddOrder(OrderCreationDto orderCreationDto);
    Task<bool> UpdateOrder(int id, OrderUpdateDto orderUpdateDto);
    Task<bool> RemoveOrder(int id);
}

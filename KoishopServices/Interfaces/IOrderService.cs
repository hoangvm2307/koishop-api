using DTOs.Order;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using KoishopServices.Dtos.Order;
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
    Task<string> AddOrder(OrderCreationDto orderCreationDto, CancellationToken cancellationToken);
    Task<bool> UpdateOrder(int id, OrderUpdateDto orderUpdateDto);
    Task<bool> RemoveOrder(int id);
    Task<bool> AfterPaymentSuccess(int id, CancellationToken cancellationToken);
    Task<bool> UpdateOrderStatus(OrderStatusUpdateDto orderStatusUpdateDto, CancellationToken cancellationToken);
}

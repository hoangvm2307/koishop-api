using DTOs.Order;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using KoishopServices.Common.Pagination;
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
    Task<OrderDto> GetOrderById(int id, CancellationToken cancellationToken);
    Task<OrderDto> AddOrder(OrderCreationDto orderCreationDto, CancellationToken cancellationToken);
    Task<bool> UpdateOrderItem(int id, OrderUpdateItemDto orderUpdateDto, CancellationToken cancellationToken);
    Task<bool> RemoveOrder(int id);
    Task<bool> AfterPaymentSuccess(int id, CancellationToken cancellationToken);
    Task<bool> UpdateOrderStatus(OrderStatusUpdateDto orderStatusUpdateDto, CancellationToken cancellationToken);
    Task<PagedResult<OrderDto>> GetOrderByUserId(FilterOrderDto filterOrderDto, CancellationToken cancellationToken);
}

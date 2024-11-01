using DTOs.Order;
using KoishopServices.Common.Pagination;
using KoishopServices.Dtos.Dashboard;
using KoishopServices.Dtos.Order;

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
    Task<PagedResult<OrderDto>> FilterOrder(FilterOrderDto filterOrderDto, CancellationToken cancellationToken);
    Task<Dictionary<int, OrderRevenueDto>> GetRevenueOfYear(int year, CancellationToken cancellationToken);
    Task<IEnumerable<OrderFavCustomer>> GetTotalOrderByUser();
}

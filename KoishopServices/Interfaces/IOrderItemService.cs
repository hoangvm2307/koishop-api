using DTOs.OrderItem;

namespace KoishopServices.Interfaces;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemDto>> GetListOrderItem();
    Task<OrderItemDto> GetOrderItemById(int id);
    Task AddOrderItem(OrderItemCreationDto orderItemCreationDto);
    Task<bool> UpdateOrderItem(int id, OrderItemUpdateDto orderItemUpdateDto);
    Task<bool> RemoveOrderItem(int id);
}

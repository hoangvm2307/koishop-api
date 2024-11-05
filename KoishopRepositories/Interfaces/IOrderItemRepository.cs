using KoishopBusinessObjects;

namespace KoishopRepositories.Interfaces;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetAllOrderitemAsync();
    Task<IEnumerable<OrderItem>> GetOrdersItemsByOrderId(int orderId);

}

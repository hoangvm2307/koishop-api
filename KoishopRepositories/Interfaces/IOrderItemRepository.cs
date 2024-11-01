using KoishopBusinessObjects;

namespace KoishopRepositories.Interfaces;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetAllOrderitemAsync();
}

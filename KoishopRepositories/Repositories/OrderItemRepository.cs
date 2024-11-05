using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    private readonly KoishopContext _context;
    public OrderItemRepository(KoishopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderitemAsync()
    {
        return await _context.OrderItems
                .Where(e => e.isDeleted == false || e.isDeleted == null)
                .Include(e => e.KoiFish)
                .AsTracking().ToListAsync();    
    }

    public async Task<IEnumerable<OrderItem>> GetOrdersItemsByOrderId(int orderId)
    {
        return await _context.OrderItems
                .Where(e => (e.isDeleted == false || e.isDeleted == null) && e.OrderId.Equals(orderId))
                .ToListAsync();
    }
}

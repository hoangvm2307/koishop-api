using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.Repositories;

public class ConsignmentItemRepository : GenericRepository<ConsignmentItem>, IConsignmentItemRepository
{
    private readonly KoishopContext _context;

    public ConsignmentItemRepository(KoishopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ConsignmentItem>> GetAllConsignmentItemAsync()
    {
        return await _context.ConsignmentItems
            .Include(e => e.Consignment)
            .Include(e => e.KoiFish)
            .AsNoTracking().ToListAsync(); 
    }

    public async Task<IEnumerable<ConsignmentItem>> GetAllConsignmentItemByConsignmentIdAsync(int consignmentId)
    {
        return await _context.ConsignmentItems.AsNoTracking()
            .Where(e => e.isDeleted == false && e.ConsignmentId.Equals(consignmentId))
            .Include(e => e.KoiFish)
            .ToListAsync(); 
    }

    public async Task<ConsignmentItem> GetConsignmentByIdAsync(int id)
    {
        return await _context.ConsignmentItems
            .Where(e => e.isDeleted == false)
            .Include(e => e.KoiFish)
            .AsNoTracking().FirstOrDefaultAsync(q => q.Id.Equals(id));
    }
}

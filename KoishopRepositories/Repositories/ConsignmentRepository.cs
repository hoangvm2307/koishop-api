using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.Repositories;

public class ConsignmentRepository : GenericRepository<Consignment>, IConsignmentRepository
{
  private readonly KoishopContext _context;
  public ConsignmentRepository(KoishopContext context) : base(context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Consignment>> GetAllConsignmentAsync()
  {
    return await _context.Consignments
        .Where(e => e.isDeleted == false)
        .Include(e => e.ConsignmentItems)
        .AsNoTracking().ToListAsync();
  }

  public async Task<Consignment> GetConsignmentByIdAsync(int id)
  {
    return await _context.Consignments
        .Where(e => e.isDeleted == false)
        .Include(e => e.ConsignmentItems)
        .ThenInclude(e => e.KoiFish)
        .AsNoTracking().FirstOrDefaultAsync(q => q.Id.Equals(id));
  }

  public async Task<IEnumerable<Consignment>> GetByUserIdAsync(int userId)
  {
    return await _context.Consignments
        .Where(e => e.isDeleted == false && e.UserID.Equals(userId))
        .AsNoTracking().ToListAsync();
  }
}

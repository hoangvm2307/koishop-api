using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using KoishopRepositories.Repositories.Extensions;
using KoishopRepositories.Repositories.RequestHelpers;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.Repositories;

public class KoiFishRepository : GenericRepository<KoiFish>, IKoiFishRepository
{
    private readonly KoishopContext _context;
    public KoiFishRepository(KoishopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<KoiFish>> GetKoiFishs(KoiFishParams koiFishParams)
    {
        return _context.KoiFishes
            .Search(koiFishParams.SearchTerm)
            .Sort(koiFishParams.OrderBy)
            .Filter(koiFishParams)
            .Include(fish => fish.Breed)
            .Include(fish => fish.FishCare)
            .Include(fish => fish.Ratings)
            .AsQueryable();
    }

    public async Task<KoiFish> GetKoiFishDetail(int id)
    {
        return await _context.KoiFishes
            .Where(e => e.isDeleted == false)
            .Include(fish => fish.Breed)
            .Include(fish => fish.OrderItems)
            .Include(fish => fish.ConsignmentItems)
            .Include(fish => fish.FishCare)
            .Include(fish => fish.Ratings)
            .AsNoTracking().FirstOrDefaultAsync(q => q.Id.Equals(id));      
    }
}

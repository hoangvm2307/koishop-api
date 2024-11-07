using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.Repositories;

public class FishCareRepository : GenericRepository<FishCare>, IFishCareRepository
{
    private readonly KoishopContext _context;

    public FishCareRepository(KoishopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<FishCare>> GetFishCareByFishId(int fishId)
    {
        return await _context.FishCares
            .Where(c => c.KoiFishId.Equals(fishId) && c.isDeleted == false)
            .OrderByDescending(c => c.DateModified)
            .ToListAsync();
    }
}

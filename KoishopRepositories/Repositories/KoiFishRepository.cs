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
            .Where(c => c.isDeleted == false)
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

    public async Task<List<KoiFish>> GetKoiFishByIds(List<int> ids)
    {
        return await _context.KoiFishes
            .Where(e => e.isDeleted == false && ids.Contains(e.Id))
            .Include(fish => fish.Breed)
            .Include(fish => fish.OrderItems)
            .Include(fish => fish.ConsignmentItems)
            .Include(fish => fish.FishCare)
            .Include(fish => fish.Ratings)
            .AsNoTracking().ToListAsync();      
    }

    public async Task<List<KoiFish>> GetRelatedKoiFishBy(KoiFish koiFish)
    {
        return await _context.KoiFishes
            .Where(e => e.isDeleted == false && 
                    e.Id != koiFish.Id &&
                    (e.BreedId == koiFish.BreedId
                    ||e.Type.Trim().ToLower() == koiFish.Type.Trim().ToLower()
                    ||e.Origin.Trim().ToLower() == koiFish.Origin.Trim().ToLower()))
            .Include(fish => fish.Breed)
            .Include(fish => fish.Ratings)
            .AsNoTracking().ToListAsync();      
    }
    
    public async Task<List<KoiFish>> GetRandomKoiFishExcludingCurrent(int currentKoiFishId)
    {
        var random = new Random();
        var allKoiFish = await _context.KoiFishes
            .Where(kf => kf.Id != currentKoiFishId)
            .ToListAsync();

        var randomKoiFish = allKoiFish.OrderBy(kf => random.Next()).ToList();

        return randomKoiFish;
    }

    public decimal GetMaxPrices()
    {
        return _context.KoiFishes
            .Select(k => k.Price)
            .Max();
    }

    public decimal GetMinPrices()
    {
        return _context.KoiFishes
            .Select(k => k.Price)
            .Min();
    }

    public int GetMaxAge()
    {
        return _context.KoiFishes
            .Select(k => k.Age)
            .Max();
    }

    public int GetMinAges()
    {
        return _context.KoiFishes
            .Select(k => k.Age)
            .Min();
    }

    public async Task<List<string>> GetDistinctOriginsAsync()
    {
        return await _context.KoiFishes
            .Where(k => !string.IsNullOrEmpty(k.Origin))
            .Select(k => k.Origin)
            .Distinct()
            .OrderBy(o => o)
            .ToListAsync();
    }

    public List<string> GetDistinctSizes()
    {
        return new List<string>
        {
            "over_10",
            "6_10",
            "8_12",
            "under_8",
            "under_6"
        };
    }

    public async Task<List<string>> GetDistinctGendersAsync()
    {
        return await _context.KoiFishes
            .Where(k => !string.IsNullOrEmpty(k.Gender))
            .Select(k => k.Gender)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();
    }

    public async Task<List<string>> GetDistinctTypesAsync()
    {
        return await _context.KoiFishes
            .Where(k => !string.IsNullOrEmpty(k.Type))
            .Select(k => k.Type)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();
    }

    public async Task<List<string>> GetDistinctStatusAsync()
    {
        return await _context.KoiFishes
            .Where(k => !string.IsNullOrEmpty(k.Status))
            .Select(k => k.Status)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();
    }

    public async Task<List<string>> GetDistinctBreedAsync()
    {
        return await _context.KoiFishes
            .Where(k => k.BreedId.HasValue)
            .Select(k => k.Breed.BreedName)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();
    }
}

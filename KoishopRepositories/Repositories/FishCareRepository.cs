using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;

namespace KoishopRepositories.Repositories;

public class FishCareRepository : GenericRepository<FishCare>, IFishCareRepository
{
    public FishCareRepository(KoishopContext context) : base(context)
    {
    }
}

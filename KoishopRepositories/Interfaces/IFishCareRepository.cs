using KoishopBusinessObjects;

namespace KoishopRepositories.Interfaces;

public interface IFishCareRepository : IGenericRepository<FishCare>
{
    Task<List<FishCare>> GetFishCareByFishId(int fishId);

}

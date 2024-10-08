using KoishopBusinessObjects;
using KoishopRepositories.Repositories.RequestHelpers;

namespace KoishopRepositories.Interfaces;

public interface IKoiFishRepository : IGenericRepository<KoiFish>
{
    Task<IQueryable<KoiFish>> GetKoiFishs(KoiFishParams koiFishParams);
    Task<KoiFish> GetKoiFishDetail(int id);
}

using KoishopBusinessObjects;

namespace KoishopRepositories.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<List<Rating>> GetListAsync() ;
    }
}

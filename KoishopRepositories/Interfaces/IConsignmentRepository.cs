using KoishopBusinessObjects;

namespace KoishopRepositories.Interfaces;

public interface IConsignmentRepository : IGenericRepository<Consignment>
{
    Task<IEnumerable<Consignment>> GetAllConsignmentAsync();
    Task<Consignment> GetConsignmentByIdAsync(int id);
    Task<IEnumerable<Consignment>> GetByUserIdAsync(int userId);
}

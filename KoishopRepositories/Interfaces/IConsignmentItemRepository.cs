using KoishopBusinessObjects;

namespace KoishopRepositories.Interfaces;

public interface IConsignmentItemRepository : IGenericRepository<ConsignmentItem>
{
    Task<ConsignmentItem> GetConsignmentByIdAsync(int id);
    Task<IEnumerable<ConsignmentItem>> GetAllConsignmentItemByConsignmentIdAsync(int consignmentId);
    Task<IEnumerable<ConsignmentItem>> GetAllConsignmentItemAsync();
}

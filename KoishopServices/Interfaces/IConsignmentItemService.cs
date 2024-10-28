using DTOs.ConsignmentItem;

namespace KoishopServices.Interfaces;

public interface IConsignmentItemService
{
    Task<IEnumerable<ConsignmentItemDto>> GetListConsignmentItem();
    Task<ConsignmentItemDto> GetConsignmentItemById(int id);
    Task<IEnumerable<ConsignmentItemDto>> GetConsignmentItemByConsignmentId(int consignmentId);
    Task AddConsignmentItem(ConsignmentItemCreationDto consignmentItemCreationDto);
    Task<bool> UpdateConsignmentItem(int id, ConsignmentItemUpdateDto consignmentItemUpdateDto);
    Task<bool> RemoveConsignmentItem(int id);
}

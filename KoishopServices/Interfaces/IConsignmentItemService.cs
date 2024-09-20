using DTOs.Breed;
using DTOs.ConsignmentItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces;

public interface IConsignmentItemService
{
    Task<IEnumerable<ConsignmentItemDto>> GetListConsignmentItem();
    Task<ConsignmentItemDto> GetConsignmentItemById(int id);
    Task AddConsignmentItem(ConsignmentItemCreationDto consignmentItemCreationDto);
    Task<bool> UpdateConsignmentItem(int id, ConsignmentItemUpdateDto consignmentItemUpdateDto);
    Task<bool> RemoveConsignmentItem(int id);
}

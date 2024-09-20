using DTOs.Consignment;
using DTOs.Consignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces;

public interface IConsignmentService
{
    Task<IEnumerable<ConsignmentDto>> GetListConsignment();
    Task<ConsignmentDto> GetConsignmentById(int id);
    Task AddConsignment(ConsignmentCreationDto consignmentCreationDto);
    Task<bool> UpdateConsignment(int id, ConsignmentUpdateDto consignmentUpdateDto);
    Task<bool> RemoveConsignment(int id);
}

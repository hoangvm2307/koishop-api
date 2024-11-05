using DTOs.Consignment;
using KoishopServices.Dtos.Consignment;
using KoishopServices.Dtos.Dashboard;

namespace KoishopServices.Interfaces;

public interface IConsignmentService
{
    Task<IEnumerable<ConsignmentDto>> GetListConsignment();
    Task<ConsignmentDto> GetConsignmentById(int id);
    Task<IEnumerable<ConsignmentDto>> GetListConsignmentByUserId(int userId);
    Task AddConsignment(ConsignmentCreationDto consignmentCreationDto);
    Task<bool> UpdateConsignment(int id, ConsignmentUpdateDto consignmentUpdateDto);
    Task<bool> UpdateStatusConsigment(int id, ConsignmentStatusUpdateDto consignmentUpdateDto);
    Task<bool> RemoveConsignment(int id);
    Task<TotalConsignment> GetTotalConsignmentByType();
    Task<IEnumerable<TotalConsignmentByMonth>> GetMonthlyTotalConsignmentAsync();
}

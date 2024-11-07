using AutoMapper;
using DTOs.Consignment;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopRepositories.Interfaces;
using KoishopServices.Dtos.Consignment;
using KoishopServices.Dtos.Dashboard;
using KoishopServices.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace KoishopServices.Services;

public class ConsignmentService : IConsignmentService
{
  private readonly IMapper _mapper;
  private readonly IConsignmentRepository _consignmentRepository;

  public ConsignmentService(IMapper mapper, IConsignmentRepository consignmentRepository)
  {
    this._mapper = mapper;
    this._consignmentRepository = consignmentRepository;
  }
  public async Task AddConsignment(ConsignmentCreationDto consignmentCreationDto)
  {
    // Validation before create and mapping
    if (consignmentCreationDto.StartDate.Date < DateTime.Now.Date)
    {
      throw new ArgumentException("StartDate cannot be in the past.");
    }
    if (consignmentCreationDto.EndDate.HasValue && consignmentCreationDto.EndDate.Value.Date < consignmentCreationDto.StartDate.Date)
    {
      throw new ArgumentException("EndDate cannot be earlier than StartDate.");
    }
    if (string.IsNullOrEmpty(consignmentCreationDto.ConsignmentType))
    {
      throw new ArgumentException("ConsignmentType is required.");
    }
    if (string.IsNullOrEmpty(consignmentCreationDto.Status))
    {
      throw new ArgumentException("Status is required.");
    }

    var consignment = _mapper.Map<Consignment>(consignmentCreationDto);

    switch (consignment.ConsignmentType.ToUpper())
    {
      case ConsignmentType.OFFLINE:
        var days = Math.Abs(((DateTime)consignment.EndDate - consignment.StartDate).Days);
        var totalOfflineFee = CostConstant.CONSIGNMENT_AMOUNT_OFFLINE * days * consignment.ConsignmentItems.Count;
        consignment.Price = consignment.ConsignmentItems.Count >= 10 ? totalOfflineFee - totalOfflineFee * CostConstant.CONSIGNMENT_DISCOUNT : totalOfflineFee;
        break;
      case ConsignmentType.ONLINE:
        var totalOnlineFee = CostConstant.CONSIGNMENT_AMOUNT_ONLINE * 30 * consignment.ConsignmentItems.Count;
        consignment.Price = consignment.ConsignmentItems.Count >= 10 ? totalOnlineFee - totalOnlineFee * CostConstant.CONSIGNMENT_DISCOUNT : totalOnlineFee;
        break;
    }

    await _consignmentRepository.AddAsync(consignment);
  }

  public async Task<bool> CancelConsignment(int id)
  {
    var consignment = await _consignmentRepository.GetConsignmentByIdAsync(id);
    if (consignment == null) return false;

    if (consignment.Status == ConsignmentStatus.CANCELLED) return false;

    consignment.Status = ConsignmentStatus.CANCELLED;

    foreach (var item in consignment.ConsignmentItems)
    {
      item.KoiFish.Status = KoiFishStatus.SOLD;
    }

    await _consignmentRepository.UpdateAsync(consignment);
    return true;
  }

  public async Task<ConsignmentDto> GetConsignmentById(int id)
  {
    var consignment = await _consignmentRepository.GetConsignmentByIdAsync(id);
    if (consignment == null)
      return null;
    return _mapper.Map<ConsignmentDto>(consignment);
  }

  public async Task<IEnumerable<ConsignmentDto>> GetListConsignment()
  {
    var consignments = await _consignmentRepository.GetAllConsignmentAsync();
    var result = _mapper.Map<List<ConsignmentDto>>(consignments);
    return result;
  }
  public async Task<bool> UpdateStatusConsigment(int id, ConsignmentStatusUpdateDto consignmentStatusUpdateDto)
  {
    if (consignmentStatusUpdateDto == null)
      return false;

    var validStatuses = new[] { ConsignmentStatus.PENDING, ConsignmentStatus.APPROVED, ConsignmentStatus.REJECTED, ConsignmentStatus.COMPLETED };
    if (!validStatuses.Contains(consignmentStatusUpdateDto.Status))
    {
      throw new ArgumentException("Invalid status provided.");
    }

    var existingConsignment = await _consignmentRepository.GetByIdAsync(id);
    if (existingConsignment == null)
      return false;

    _mapper.Map(consignmentStatusUpdateDto, existingConsignment);
    await _consignmentRepository.UpdateAsync(existingConsignment);

    return true;
  }

  public async Task<IEnumerable<ConsignmentDto>> GetListConsignmentByUserId(int userId)
  {
    var consignments = await _consignmentRepository.GetByUserIdAsync(userId);
    var result = _mapper.Map<List<ConsignmentDto>>(consignments);
    return result;
  }

  public async Task<bool> RemoveConsignment(int id)
  {
    var exist = await _consignmentRepository.GetByIdAsync(id);
    if (exist == null)
      return false;
    await _consignmentRepository.DeleteAsync(exist);
    return true;
  }

  public async Task<bool> UpdateConsignment(int id, ConsignmentUpdateDto consignmentUpdateDto)
  {
    var existingConsignment = await _consignmentRepository.GetByIdAsync(id);
    if (existingConsignment == null)
      return false;

    //TODO: Add validation before Update and mapping
    if (consignmentUpdateDto.StartDate.Date < DateTime.Now.Date)
    {
      throw new ArgumentException("StartDate cannot be in the past.");
    }
    if (consignmentUpdateDto.EndDate.HasValue && consignmentUpdateDto.EndDate.Value.Date < consignmentUpdateDto.StartDate.Date)
    {
      throw new ArgumentException("EndDate cannot be earlier than StartDate.");
    }
    if (string.IsNullOrEmpty(consignmentUpdateDto.ConsignmentType))
    {
      throw new ArgumentException("ConsignmentType is required.");
    }
    if (consignmentUpdateDto.Price <= 0)
    {
      throw new ArgumentException("Price must be greater than zero.");
    }
    if (string.IsNullOrEmpty(consignmentUpdateDto.Status))
    {
      throw new ArgumentException("Status is required.");
    }
    _mapper.Map(consignmentUpdateDto, existingConsignment);
    await _consignmentRepository.UpdateAsync(existingConsignment);
    return true;
  }

  public async Task<TotalConsignment> GetTotalConsignmentByType()
  {
    var consignments = await _consignmentRepository.GetAllAsync();

    var totalConsignment = new TotalConsignment
    {
      TotalConsignmentOnline = consignments
            .Count(e => !string.IsNullOrEmpty(e.ConsignmentType) &&
                    e.ConsignmentType == ConsignmentType.ONLINE),

      TotalConsignmentOffline = consignments
            .Count(e => !string.IsNullOrEmpty(e.ConsignmentType) &&
                    e.ConsignmentType == ConsignmentType.OFFLINE)
    };

    return totalConsignment;
  }

  public async Task<IEnumerable<TotalConsignmentByMonth>> GetMonthlyTotalConsignmentAsync()
  {
    var endDate = DateTime.UtcNow;
    var startDate = endDate.AddMonths(-12 + 1).Date.AddDays(1 - endDate.Day);


    var consignments = await _consignmentRepository.GetAllAsync();

    // Filter and process in memory
    var filteredSubscriptions = consignments
        .Where(u => u.DateCreated >= startDate && u.DateCreated <= endDate)
        .OrderBy(u => u.DateCreated)
        .ToList();


    var dataStatistics = filteredSubscriptions
        .GroupBy(u => new { u.DateCreated.Year, u.DateCreated.Month })
        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
        .Select(g => new TotalConsignmentByMonth
        {
          Date = new DateTime(g.Key.Year, g.Key.Month, 1),
          TotalConsignment = g.Count(),
        })
        .ToList();

    return dataStatistics;
  }
}

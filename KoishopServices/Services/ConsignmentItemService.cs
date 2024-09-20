using AutoMapper;
using DTOs.ConsignmentItem;
using DTOs.ConsignmentItem;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class ConsignmentItemService : IConsignmentItemService
{
    private readonly IMapper _mapper;
    private readonly IConsignmentItemRepository _consignmentItemRepository;

    public ConsignmentItemService(IMapper mapper, IConsignmentItemRepository consignmentItemRepository)
    {
        this._mapper = mapper;
        this._consignmentItemRepository = consignmentItemRepository;
    }
    public async Task AddConsignmentItem(ConsignmentItemCreationDto consignmentItemCreationDto)
    {
        //TODO: Add validation before create and mapping
        var consignmentItem = _mapper.Map<ConsignmentItem>(consignmentItemCreationDto);
        await _consignmentItemRepository.AddAsync(consignmentItem);
    }

    public async Task<ConsignmentItemDto> GetConsignmentItemById(int id)
    {
        var consignmentItem = await _consignmentItemRepository.GetByIdAsync(id);
        if (consignmentItem == null)
            return null;
        return _mapper.Map<ConsignmentItemDto>(consignmentItem);
    }

    public async Task<IEnumerable<ConsignmentItemDto>> GetListConsignmentItem()
    {
        var consignmentItems = await _consignmentItemRepository.GetAllAsync();
        var result = _mapper.Map<List<ConsignmentItemDto>>(consignmentItems);
        return result;
    }

    public async Task<bool> RemoveConsignmentItem(int id)
    {
        var exist = await _consignmentItemRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _consignmentItemRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateConsignmentItem(int id, ConsignmentItemUpdateDto consignmentItemUpdateDto)
    {
        var existingConsignmentItem = await _consignmentItemRepository.GetByIdAsync(id);
        if (existingConsignmentItem == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(consignmentItemUpdateDto, existingConsignmentItem);
        await _consignmentItemRepository.UpdateAsync(existingConsignmentItem);
        return true;
    }
}

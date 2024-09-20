using AutoMapper;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class OrderItemService  : IOrderItemService
{
    private readonly IMapper _mapper;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemService(IMapper mapper, IOrderItemRepository orderItemRepository)
    {
        this._mapper = mapper;
        this._orderItemRepository = orderItemRepository;
    }
    public async Task AddOrderItem(OrderItemCreationDto orderItemCreationDto)
    {
        //TODO: Add validation before create and mapping
        var orderItem = _mapper.Map<OrderItem>(orderItemCreationDto);
        await _orderItemRepository.AddAsync(orderItem);
    }

    public async Task<OrderItemDto> GetOrderItemById(int id)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
            return null;
        return _mapper.Map<OrderItemDto>(orderItem);
    }

    public async Task<IEnumerable<OrderItemDto>> GetListOrderItem()
    {
        var orderItems = await _orderItemRepository.GetAllAsync();
        var result = _mapper.Map<List<OrderItemDto>>(orderItems);
        return result;
    }

    public async Task<bool> RemoveOrderItem(int id)
    {
        var exist = await _orderItemRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _orderItemRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateOrderItem(int id, OrderItemUpdateDto orderItemUpdateDto)
    {
        var existingOrderItem = await _orderItemRepository.GetByIdAsync(id);
        if (existingOrderItem == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(orderItemUpdateDto, existingOrderItem);
        await _orderItemRepository.UpdateAsync(existingOrderItem);
        return true;
    }
}

using AutoMapper;
using DTOs.Order;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IMapper mapper, IOrderRepository orderRepository)
    {
        this._mapper = mapper;
        this._orderRepository = orderRepository;
    }
    public async Task AddOrder(OrderCreationDto orderCreationDto)
    {
        //TODO: Add validation before create and mapping
        if ( string.IsNullOrEmpty(orderCreationDto.Status))
        {
            throw new ArgumentException("Status is required!");
        }
        // Total ammount -> tính tiền
        
        var order = _mapper.Map<Order>(orderCreationDto);
        await _orderRepository.AddAsync(order);
    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return null;
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetListOrder()
    {
        var orders = await _orderRepository.GetAllAsync();
        var result = _mapper.Map<List<OrderDto>>(orders);
        return result;
    }

    public async Task<bool> RemoveOrder(int id)
    {
        var exist = await _orderRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _orderRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(orderUpdateDto, existingOrder);
        await _orderRepository.UpdateAsync(existingOrder);
        return true;
    }
}

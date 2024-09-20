using DTOs.Order;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = await _orderService.GetListOrder();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderCreationDto orderCreationDto)
    {
        await _orderService.AddOrder(orderCreationDto);
        return CreatedAtAction(nameof(GetOrders), orderCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
    {
        var isUpdated = await _orderService.UpdateOrder(id, orderUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var isDeleted = await _orderService.RemoveOrder(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

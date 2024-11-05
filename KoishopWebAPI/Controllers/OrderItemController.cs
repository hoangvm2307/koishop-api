using DTOs.OrderItem;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;
public class OrderItemController : BaseApiController
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderItemDto>>> GetOrderItems()
    {
        var orderItems = await _orderItemService.GetListOrderItem();
        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrderItem(OrderItemCreationDto orderItemCreationDto)
    {
        await _orderItemService.AddOrderItem(orderItemCreationDto);
        return CreatedAtAction(nameof(GetOrderItems), orderItemCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItemById(int id)
    {
        var orderItem = await _orderItemService.GetOrderItemById(id);
        if (orderItem == null)
            return NotFound();
        return Ok(orderItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderItem(int id, OrderItemUpdateDto orderItemUpdateDto)
    {
        var isUpdated = await _orderItemService.UpdateOrderItem(id, orderItemUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrderItem(int id)
    {
        var isDeleted = await _orderItemService.RemoveOrderItem(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }

    [HttpGet("order/{orderId}")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItemByOrderId(int orderId)
    {
        var orderItem = await _orderItemService.GetOrderItemByOrderId(orderId);
        return Ok(orderItem);
    }
}

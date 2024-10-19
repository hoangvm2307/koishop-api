using DTOs.Order;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;

public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Retrieves a list of all orders.
    /// </summary>
    /// <returns>A list of <see cref="OrderDto"/> objects representing the orders.</returns>
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = await _orderService.GetListOrder();
        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderCreationDto">The data transfer object that contains the order creation details.</param>
    /// <returns>VnPay payment URL or error.</returns>
    [HttpPost]
    public async Task<ActionResult<JsonResponse<string>>> CreateOrder([FromBody] OrderCreationDto orderCreationDto)
    {
        var result = await _orderService.AddOrder(orderCreationDto);
        return Ok(new JsonResponse<string>(result));
    }

    /// <summary>
    /// Retrieves a specific order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>The <see cref="OrderDto"/> object representing the order, or a 404 Not Found response if not found.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Updates an existing order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="orderUpdateDto">The data transfer object containing the updated order details.</param>
    /// <returns>An empty No Content response if the update is successful, or 404 Not Found if the order does not exist.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
    {
        var isUpdated = await _orderService.UpdateOrder(id, orderUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns>An empty No Content response if the deletion is successful, or 404 Not Found if the order does not exist.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var isDeleted = await _orderService.RemoveOrder(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

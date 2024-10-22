using DTOs.Order;
using KoishopServices.Common.Pagination;
using KoishopServices.Dtos.Order;
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
    /// Retrieves a list of all orders with filter options.
    /// </summary>
    [HttpGet("filter")]
    public async Task<ActionResult<PagedResult<OrderDto>>> GetOrdersByUserId([FromQuery]FilterOrderDto filterOrderDto, CancellationToken cancellationToken = default)
    {
        var orders = await _orderService.FilterOrder(filterOrderDto, cancellationToken);
        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderCreationDto">The data transfer object that contains the order creation details.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An object representing the order or error.</returns>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderCreationDto orderCreationDto
        , CancellationToken cancellationToken = default)
    {
        var result = await _orderService.AddOrder(orderCreationDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>The <see cref="OrderDto"/> object representing the order, or a 404 Not Found response if not found.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id, CancellationToken cancellationToken = default)
    {
        var order = await _orderService.GetOrderById(id, cancellationToken);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Updates order items of existing order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="orderUpdateDto">The data transfer object containing the updated order details.</param>
    /// <returns>An empty No Content response if the update is successful, or 404 Not Found if the order does not exist.</returns>
    [HttpPut("order/{id}/orderItems")]
    public async Task<ActionResult> UpdateOrderItem(int id, OrderUpdateItemDto orderUpdateDto, CancellationToken cancellationToken = default)
    {
        var isUpdated = await _orderService.UpdateOrderItem(id, orderUpdateDto, cancellationToken);
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

    /// <summary>
    /// Handles the logic after a successful payment through VnPay for the order with the given ID.
    /// </summary>
    /// <param name="id">The ID of the order to update after payment success.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>Returns NoContent if successful, or BadRequest if the operation fails.</returns>
    [HttpPut("order/{id}/payment-success")]
    public async Task<ActionResult> AfterPaymentSuccess(int id, CancellationToken cancellationToken = default)
    {
        var afterPaymentSuccess = await _orderService.AfterPaymentSuccess(id, cancellationToken);
        if (!afterPaymentSuccess)
            return BadRequest();
        return NoContent();
    }

    /// <summary>
    /// Updates the status of the order based on the provided status update details.
    /// </summary>
    /// <param name="orderStatusUpdateDto">Data Transfer Object containing the new order status and id.</param>
    /// <param name="cancellationToken">Token to cancel the operation (optional).</param>
    /// <returns>Returns NoContent if successful, or BadRequest if the operation fails.</returns>
    [HttpPatch("order/status")]
    public async Task<ActionResult> UpdateOrderStatus(OrderStatusUpdateDto orderStatusUpdateDto, CancellationToken cancellationToken = default)
    {
        var afterPaymentSuccess = await _orderService.UpdateOrderStatus(orderStatusUpdateDto, cancellationToken);
        if (!afterPaymentSuccess)
            return BadRequest();
        return NoContent();
    }
}

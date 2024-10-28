using DTOs.OrderItem;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Order;

/// <summary>
/// DTO for creating a new Order
/// </summary>
public class OrderCreationDto
{
    /// <summary>
    /// A list of items that are part of this order.
    /// </summary>
    [Required]
    [SwaggerSchema(Description = "A list of items that are part of this order.")]
    public List<OrderItemCreationDto> OrderItemCreationDtos { get; set; }

    /// <summary>
    /// Specifies whether the order need to consignment or not.
    /// </summary>
    [Required]
    [SwaggerSchema(Description = "Specifies whether the order need to consignment or not.")]
    public bool IsConsignment { get; set; }
}

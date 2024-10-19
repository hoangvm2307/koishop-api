using DTOs.OrderItem;
using KoishopBusinessObjects;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Order;

/// <summary>
/// DTO for creating a new Order
/// </summary>
public class OrderCreationDto
{
    /// <summary>
    /// The date and time when the order was created. Defaults to the current date and time.
    /// </summary>
    [SwaggerSchema(Description = "The date and time when the order was created. Defaults to the current date and time.")]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// The ID of the user placing the order.
    /// </summary>
    [Required]
    [SwaggerSchema(Description = "The ID of the user placing the order.")]
    public int UserId { get; set; }

    /// <summary>
    /// A list of items that are part of this order.
    /// </summary>
    [Required]
    [SwaggerSchema(Description = "A list of items that are part of this order.")]
    public List<OrderItemCreationDto> OrderItemCreationDtos { get; set; }
}

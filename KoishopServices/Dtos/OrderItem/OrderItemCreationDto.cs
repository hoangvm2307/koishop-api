using DTOs.KoiFish;
using DTOs.Order;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.OrderItem;

/// <summary>
/// DTO for creating a new Order Item
/// </summary>
public class OrderItemCreationDto
{
    /// <summary>
    /// The ID of the KoiFish being ordered. This can be null if not yet assigned.
    /// </summary>
    [SwaggerSchema(Description = "The ID of the KoiFish being ordered. This can be null if not yet assigned.")]
    public int? KoiFishId { get; set; }
}
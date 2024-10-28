using Swashbuckle.AspNetCore.Annotations;

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
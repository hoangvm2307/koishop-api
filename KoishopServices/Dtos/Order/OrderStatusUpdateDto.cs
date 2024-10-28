using Swashbuckle.AspNetCore.Annotations;

namespace KoishopServices.Dtos.Order
{
    public class OrderStatusUpdateDto
    {
        /// <summary>
        /// The ID of the order need to update.
        /// </summary>
        [SwaggerSchema(Description = "The ID of the order need to update.")]
        public int Id { get; set; }

        /// <summary>
        /// The status of the Order. Allowed values are: PENDING, PROCESSING, DELIVERING, DELIVERED, CANCELLED.
        /// </summary>
        [SwaggerSchema(Description = "The status of the Order. Allowed values are: PENDING, PROCESSING, DELIVERING, DELIVERED, CANCELLED.")]
        public string Status { get; set; }
    }
}

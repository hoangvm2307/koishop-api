using Swashbuckle.AspNetCore.Annotations;

namespace KoishopServices.Dtos.Order
{
    public class FilterOrderDto
    {
        public FilterOrderDto()
        {
            
        }
        public FilterOrderDto(int no, int pageSize)
        {
            PageNumber = no;
            PageSize = pageSize;
        }

        /// <summary>
        /// The page number for pagination.
        /// </summary>
        [SwaggerSchema(Description = "The page number for pagination.")]
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items per page for pagination.
        /// </summary>
        [SwaggerSchema(Description = "The number of items per page for pagination.")]
        public int PageSize { get; set; }

        /// <summary>
        /// The ID of the user to filter by. Default is -1 (no filtering).
        /// </summary>
        [SwaggerSchema(Description = "The ID of the user to filter by. Default is -1 (no filtering).")]
        public int? UserId { get; set; } = -1;

        /// <summary>
        /// The status of the order.
        /// </summary>
        [SwaggerSchema(Description = "The status of the order.")]
        public string? Status {  get; set; }

        /// <summary>
        /// The date of the order. Format: dd/MM/yyyy.
        /// </summary>
        [SwaggerSchema(Description = "The date of the order. Format: dd/MM/yyyy.")]
        public string? OrderDate { get; set; }

        /// <summary>
        /// The quantity of order items in order. Default is -1 (no filtering).
        /// </summary>
        [SwaggerSchema(Description = "The quantity of order items in order. Default is -1 (no filtering).")]
        public int? Quantity { get; set; } = -1;

        /// <summary>
        /// The field to sort by. Default is "TotalAmount".
        /// </summary>
        [SwaggerSchema(Description = "The field to sort by. Default is \"TotalAmount\".")]
        public string SortBy { get; set; } = "TotalAmount";

        /// <summary>
        /// Specifies whether to sort in descending order. Default is false (ascending order).
        /// </summary>
        [SwaggerSchema(Description = "Specifies whether to sort in descending order. Default is false (ascending order).")]
        public bool IsDescending { get; set; } = false;
    }
}

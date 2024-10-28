using Swashbuckle.AspNetCore.Annotations;

namespace KoishopServices.Dtos.Rating
{
    public class FilterRatingDto
    {
        public FilterRatingDto()
        {
            
        }
        public FilterRatingDto(int no, int pageSize)
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
        /// The rating value to filter by (e.g., 1-5 scale). Default is -1 (no filtering).
        /// </summary>
        [SwaggerSchema(Description = "The rating value to filter by (e.g., 1-5 scale). Default is -1 (no filtering).")]
        public int? RatingValue { get; set; } = -1;

        /// <summary>
        /// The ID of the user to filter by. Default is -1 (no filtering).
        /// </summary>
        [SwaggerSchema(Description = "The ID of the user to filter by. Default is -1 (no filtering).")]
        public int? UserId { get; set; } = -1;

        /// <summary>
        /// The ID of the Koi fish to filter by. Default is -1 (no filtering).
        /// </summary>
        [SwaggerSchema(Description = "The ID of the Koi fish to filter by. Default is -1 (no filtering).")]
        public int? KoiFishId { get; set; } = -1;

        /// <summary>
        /// The field to sort by. Default is "RatingValue".
        /// </summary>
        [SwaggerSchema(Description = "The field to sort by. Default is \"RatingValue\".")]
        public string SortBy { get; set; } = "RatingValue";

        /// <summary>
        /// Specifies whether to sort in descending order. Default is false (ascending order).
        /// </summary>
        [SwaggerSchema(Description = "Specifies whether to sort in descending order. Default is false (ascending order).")]
        public bool IsDescending { get; set; } = false;
    }
}

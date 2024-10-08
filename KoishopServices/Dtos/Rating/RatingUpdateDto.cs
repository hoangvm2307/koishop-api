using DTOs.KoiFish;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Rating;

public class RatingUpdateDto
{
    [SwaggerSchema(Description = "The ID of existing Rating")]
    public int Id { get; set; }

    [SwaggerSchema(Description = "New Rating value to update (e.g., 1 - 5 scale)")]
    public int RatingValue { get; set; }

    [SwaggerSchema(Description = "New Customer comment to update")]
    public string? Comment { get; set; }

    [SwaggerSchema(Description = "The ID of Customer who created this Rating")]
    public int UserId { get; set; }

    [SwaggerSchema(Description = "The ID of Koi fish was being rated")]
    public int KoiFishId { get; set; }
}

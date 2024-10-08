using DTOs.AccountDtos;
using DTOs.KoiFish;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Rating;
/// <summary>
/// DTO for creating new Rating
/// </summary>
public class RatingCreationDto
{
    /// <summary>
    /// The value that customer rates for KoiFish (e.g., 1-5 scale)
    /// </summary>
    [SwaggerSchema(Description = "The value that customer rates for KoiFish (e.g., 1-5 scale)")]
    public int RatingValue { get; set; }

    /// <summary>
    /// Comment of customer, its can be null
    /// </summary>
    [SwaggerSchema(Description = "Optional customer comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// The ID of customer who creates a new rating
    /// </summary>
    [SwaggerSchema(Description = "The ID of the customer who creates the rating")]
    public int UserId { get; set; }

    /// <summary>
    /// The ID of the Koi fish being rated by the customer
    /// </summary>
    [SwaggerSchema(Description = "The ID of the Koi fish being rated by the customer")]
    public int KoiFishId { get; set; }
}

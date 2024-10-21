using Swashbuckle.AspNetCore.Annotations;

namespace DTOs.KoiFish;

/// <summary>
/// DTO for creating a new Koi Fish.
/// </summary>
public class KoiFishCreationDto
{
    /// <summary>
    /// The name of the Koi Fish.
    /// </summary>
    [SwaggerSchema(Description = "The name of the Koi Fish.")]
    public string? Name { get; set; }

    /// <summary>
    /// The origin of the Koi Fish.
    /// </summary>
    [SwaggerSchema(Description = "The origin of the Koi Fish.")]
    public string? Origin { get; set; }

    /// <summary>
    /// The gender of the Koi Fish. Allowed values are: MALE, FEMALE, UNKNOWN.
    /// </summary>
    [SwaggerSchema(Description = "The gender of the Koi Fish. Allowed values are: MALE, FEMALE, UNKNOWN.")]
    public string? Gender { get; set; }

    /// <summary>
    /// The age of the Koi Fish in years.
    /// </summary>
    [SwaggerSchema(Description = "The age of the Koi Fish in years.")]
    public int Age { get; set; }

    /// <summary>
    /// The size of the Koi Fish in centimeters.
    /// </summary>
    [SwaggerSchema(Description = "The size of the Koi Fish in centimeters.")]
    public decimal Size { get; set; }

    /// <summary>
    /// The personality description of the Koi Fish.
    /// </summary>
    [SwaggerSchema(Description = "The personality description of the Koi Fish.")]
    public string? Personality { get; set; }

    /// <summary>
    /// The daily food amount for the Koi Fish in grams.
    /// </summary>
    [SwaggerSchema(Description = "The daily food amount for the Koi Fish in grams.")]
    public decimal DailyFoodAmount { get; set; }

    /// <summary>
    /// The type of Koi Fish. Allowed values are: PUREIMPORTED, HYBRIDF1, PUREVIETNAMESE.
    /// </summary>
    [SwaggerSchema(Description = "The type of Koi Fish. Allowed values are: PUREIMPORTED, HYBRIDF1, PUREVIETNAMESE.")]
    public string? Type { get; set; }

    /// <summary>
    /// The selling price of the Koi Fish.
    /// </summary>
    [SwaggerSchema(Description = "The selling price of the Koi Fish.")]
    public decimal Price { get; set; }

    /// <summary>
    /// The list price of the Koi Fish.
    /// </summary>
    [SwaggerSchema(Description = "The list price of the Koi Fish.")]
    public decimal ListPrice { get; set; }

    /// <summary>
    /// The image URL of the Koi Fish.
    /// </summary>
    [SwaggerSchema(Description = "The image URL of the Koi Fish.")]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The status of the Koi Fish. Allowed values are: AVAILABLE, SOLD, RESERVED.
    /// </summary>
    [SwaggerSchema(Description = "The status of the Koi Fish. Allowed values are: AVAILABLE, SOLD, RESERVED.")]
    public string? Status { get; set; }

    /// <summary>
    /// The ID of the user who created the Koi Fish entry.
    /// </summary>
    [SwaggerSchema(Description = "The ID of the user who created the Koi Fish entry.")]
    public int? UserId { get; set; }
}

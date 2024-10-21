using Swashbuckle.AspNetCore.Annotations;

namespace DTOs.KoiFish;

public class FilterKoiFishParamDto
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public List<string>? Origin { get; set; }
    
    /// <summary>
    /// The page number for pagination.
    /// </summary>
    [SwaggerSchema(Description = "Values include: over_10, 6_10, 8_12, under_8, under_6.")]
    public List<string>? Sizes { get; set; }
    
    /// <summary>
    /// The page number for pagination.
    /// </summary>
    [SwaggerSchema(Description = "Values include: MALE, FEMALE, UNKNOWN.")]
    public List<string>? Genders { get; set; }
    
    /// <summary>
    /// The page number for pagination.
    /// </summary>
    [SwaggerSchema(Description = "Values include: PUREIMPORTED, HYBRIDF1, PUREVIETNAMESE.")]
    public List<string>? Types { get; set; }
    
    /// <summary>
    /// The page number for pagination.
    /// </summary>
    [SwaggerSchema(Description = "Values include: AVAILABLE, SOLD, RESERVED.")]
    public List<string>? Status { get; set; }
    public List<string>? BreedName { get; set; }
}

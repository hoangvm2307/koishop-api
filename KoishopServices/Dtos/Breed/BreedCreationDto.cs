using System.ComponentModel.DataAnnotations;

namespace DTOs.Breed;

public class BreedCreationDto
{
    [Required]
    public string? BreedName { get; set; }
    public string? ScreeningRatio { get; set; }
    public string? Personality { get; set; }
}

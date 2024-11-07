namespace DTOs.FishCare;

public class FishCareCreationDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? CareInstructions { get; set; }
    public string? Status { get; set; }
    public int CareId { get; set; }
    public int? KoiFishId { get; set; }
    public int? UserId { get; set; }
}

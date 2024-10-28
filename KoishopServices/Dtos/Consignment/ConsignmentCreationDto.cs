using DTOs.ConsignmentItem;

namespace DTOs.Consignment;

public class ConsignmentCreationDto
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ConsignmentType { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public int? UserID {  get; set; }
    public virtual ICollection<ConsignmentItemCreationDto>? ConsignmentItems { get; set; }
}

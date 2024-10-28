using DTOs.ConsignmentItem;

namespace DTOs.Consignment;

public class ConsignmentDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ConsignmentType { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public int? UserID {  get; set; }
    public virtual ICollection<ConsignmentItemDto>? ConsignmentItems { get; set; }
}

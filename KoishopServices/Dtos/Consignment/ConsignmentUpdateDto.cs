namespace DTOs.Consignment;

public class ConsignmentUpdateDto
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ConsignmentType { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public int? UserID {  get; set; }
}

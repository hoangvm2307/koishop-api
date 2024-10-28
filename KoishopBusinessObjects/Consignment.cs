using System.ComponentModel.DataAnnotations.Schema;

namespace KoishopBusinessObjects;

public class Consignment : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ConsignmentType { get; set; } 
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public int? UserID {  get; set; }
    [ForeignKey(nameof(UserID))]
    public virtual User User { get; set; }
    public virtual ICollection<ConsignmentItem>? ConsignmentItems { get; set; }

}

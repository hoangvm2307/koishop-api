using System.ComponentModel.DataAnnotations.Schema;

namespace KoishopBusinessObjects;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }


    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace KoishopBusinessObjects;

public class OrderItem : BaseEntity
{
    public decimal Price { get; set; }
    public string Type { get; set; }

    [ForeignKey(nameof(Order))]
    public int? OrderId { get; set; }
    public virtual Order? Order { get; set; }
    [ForeignKey(nameof(KoiFish))]
    public int? KoiFishId { get; set; }
    public virtual KoiFish? KoiFish { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects;

public class OrderItem : BaseEntity
{
    public decimal Price { get; set; }

    [ForeignKey(nameof(Order))]
    public int? OrderId { get; set; }
    public virtual Order? Order { get; set; }
    [ForeignKey(nameof(KoiFish))]
    public int? KoiFishId { get; set; }
    public virtual KoiFish? KoiFish { get; set; }
}

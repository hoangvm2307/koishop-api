using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects;

public class ConsignmentItem : BaseEntity
{
    [ForeignKey(nameof(Consignment))]
    public int? ConsignmentId { get; set; }
    public virtual Consignment? Consignment { get; set; }
    [ForeignKey(nameof(KoiFish))]
    public int? KoiFishId { get; set; }
    public virtual KoiFish? KoiFish { get; set; }
    public decimal Price { get; set; }
}

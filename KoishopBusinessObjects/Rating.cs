using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects;

public class Rating : BaseEntity
{
    public int RatingValue { get; set; } 
    public string? Comment { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    [ForeignKey(nameof(KoiFish))]
    public int KoiFishId { get; set; }
    public virtual KoiFish? KoiFish { get; set; }
}

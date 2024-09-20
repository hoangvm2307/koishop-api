using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects;

public class KoiFish : BaseEntity
{
    public string? Name { get; set; }
    public string? Origin { get; set; }
    public string? Gender { get; set; } 
    public int Age { get; set; }
    public decimal Size { get; set; }
    public string? Personality { get; set; }
    public decimal DailyFoodAmount { get; set; }
    public string? Type { get; set; } 
    public decimal Price { get; set; }
    public decimal ListPrice { get; set; }
    public string? ImageUrl { get; set; }
    public string? Status { get; set; }

    [ForeignKey(nameof(User))]
    public int? UserId { get; set; } 
    public virtual User? User { get; set; }
    [ForeignKey(nameof(Breed))]
    public int? BreedId { get; set; }
    public virtual Breed? Breed { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
    public virtual ICollection<ConsignmentItem>? ConsignmentItems { get; set; }
    public virtual ICollection<Rating>? Ratings { get; set; }
    public virtual ICollection<FishCare>? FishCare { get; set; }
}

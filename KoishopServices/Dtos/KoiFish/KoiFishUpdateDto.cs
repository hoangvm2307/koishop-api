using DTOs.Breed;
using DTOs.Consignment;
using DTOs.ConsignmentItem;
using DTOs.FishCare;
using DTOs.OrderItem;
using DTOs.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.KoiFish;

public class KoiFishUpdateDto
{
    public int Id { get; set; }
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
    public int? UserId { get; set; }
    public int? BreedId { get; set; }
}

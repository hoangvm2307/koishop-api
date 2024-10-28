using DTOs.Breed;
using DTOs.ConsignmentItem;
using DTOs.FishCare;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using KoishopServices.Dtos.Rating;

namespace DTOs.KoiFish;

public class KoiFishDto
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
    public virtual User? User { get; set; }
    public int? BreedId { get; set; }
    public virtual BreedDto? Breed { get; set; }
    public virtual ICollection<OrderItemDto>? OrderItems { get; set; }
    public virtual ICollection<ConsignmentItemDto>? ConsignmentItems { get; set; }
    public virtual ICollection<RatingDto>? Ratings { get; set; }
    public virtual ICollection<FishCareDto>? FishCare { get; set; }
}

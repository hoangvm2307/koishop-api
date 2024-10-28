namespace DTOs.OrderItem;

public class OrderItemDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int? OrderId { get; set; }
    public int? KoiFishId { get; set; }
    public string KoiFishName { get; set; }
}

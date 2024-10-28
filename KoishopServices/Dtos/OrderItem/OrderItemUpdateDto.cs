namespace DTOs.OrderItem;

public class OrderItemUpdateDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public int? OrderId { get; set; }
    public int? KoiFishId { get; set; }
}

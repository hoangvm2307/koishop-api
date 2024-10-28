using DTOs.OrderItem;

namespace DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public int? UserId { get; set; }
    public string UserName { get; set; }
    public virtual ICollection<OrderItemDto>? OrderItems { get; set; }
}

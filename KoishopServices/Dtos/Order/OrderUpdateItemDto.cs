using DTOs.OrderItem;

namespace DTOs.Order;

public class OrderUpdateItemDto
{
    public List<OrderItemCreationDto> OrderItemCreationDtos { get; set; }
}

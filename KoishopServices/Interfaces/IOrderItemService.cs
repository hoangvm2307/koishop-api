using DTOs.OrderItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemDto>> GetListOrderItem();
    Task<OrderItemDto> GetOrderItemById(int id);
    Task AddOrderItem(OrderItemCreationDto orderItemCreationDto);
    Task<bool> UpdateOrderItem(int id, OrderItemUpdateDto orderItemUpdateDto);
    Task<bool> RemoveOrderItem(int id);
}

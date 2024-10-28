using AutoMapper;
using DTOs.Order;
using KoishopServices.Dtos.OrderItem;

namespace KoishopServices.Dtos.Order
{
    public static class OrderDtoMappingExtension
    {
        public static OrderDto MapToOrderDto(this KoishopBusinessObjects.Order projectFrom, IMapper mapper)
        {
            var result = mapper.Map<OrderDto>(projectFrom);
            return result;
        }
        public static List<OrderDto> MapToOrderDtoList(this IEnumerable<KoishopBusinessObjects.Order> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToOrderDto(mapper)).ToList();

        public static OrderDto MapToOrderDto(this KoishopBusinessObjects.Order projectFrom, IMapper mapper, string username, Dictionary<int, string?> koifishName)
        {
            var dto = mapper.Map<OrderDto>(projectFrom);
            dto.UserName = username;           
            dto.OrderItems = projectFrom.OrderItems.MapToOrderItemDtoList(mapper, koifishName);
            return dto;
        }
        public static List<OrderDto> MapToOrderDtoList(this IEnumerable<KoishopBusinessObjects.Order> projectFrom, IMapper mapper, Dictionary<int, string> username, Dictionary<int, string?> koifishName)
            => projectFrom.Select(x => x.MapToOrderDto(mapper,
                username.ContainsKey((int)x.UserId) ? username[(int)x.UserId] : "Lỗi",
                koifishName
                )).ToList();
    }
}

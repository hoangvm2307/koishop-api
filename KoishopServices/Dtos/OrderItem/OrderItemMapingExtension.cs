using AutoMapper;
using DTOs.OrderItem;

namespace KoishopServices.Dtos.OrderItem
{
    public static class OrderItemMapingExtension
    {
        public static OrderItemDto MapToOrderItemDto(this KoishopBusinessObjects.OrderItem projectFrom, IMapper mapper)
        {
            var result = mapper.Map<OrderItemDto>(projectFrom);
            return result;
        }
        public static List<OrderItemDto> MapToOrderItemDtoList(this IEnumerable<KoishopBusinessObjects.OrderItem> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToOrderItemDto(mapper)).ToList();

        public static OrderItemDto MapToOrderItemDto(this KoishopBusinessObjects.OrderItem projectFrom, IMapper mapper, string koifishName)
        {
            var dto = mapper.Map<OrderItemDto>(projectFrom);
            dto.KoiFishName = koifishName;
            return dto;
        }
        public static List<OrderItemDto> MapToOrderItemDtoList(this IEnumerable<KoishopBusinessObjects.OrderItem> projectFrom, IMapper mapper, Dictionary<int, string?> koifishName)
         => projectFrom.Select(x => x.MapToOrderItemDto(mapper,
             koifishName.ContainsKey((int)x.KoiFishId) ? koifishName[(int)x.KoiFishId] : "Lỗi")).ToList();
    }
}

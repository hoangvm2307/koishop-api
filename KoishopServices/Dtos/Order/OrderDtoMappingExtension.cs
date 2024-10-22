using AutoMapper;
using DTOs.AccountDtos;
using DTOs.KoiFish;
using DTOs.Order;
using DTOs.OrderItem;
using DTOs.Rating;
using KoishopBusinessObjects;
using KoishopServices.Dtos.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

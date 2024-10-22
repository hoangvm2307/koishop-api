using AutoMapper;
using DTOs.AccountDtos;
using DTOs.KoiFish;
using DTOs.Order;
using DTOs.OrderItem;
using DTOs.Rating;
using KoishopBusinessObjects;
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
            result.UserDto = mapper.Map<UserDto>(projectFrom.User);
            return result;
        }
        public static List<OrderDto> MapToOrderDtoList(this IEnumerable<KoishopBusinessObjects.Order> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToOrderDto(mapper)).ToList();
    }
}

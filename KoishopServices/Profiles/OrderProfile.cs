using AutoMapper;
using DTOs.Order;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderCreationDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
    }
}

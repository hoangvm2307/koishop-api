using AutoMapper;
using DTOs.KoiFish;
using DTOs.OrderItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<OrderItemCreationDto, OrderItem>();
        CreateMap<OrderItemUpdateDto, OrderItem>();
    }
}

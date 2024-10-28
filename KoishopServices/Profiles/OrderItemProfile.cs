using AutoMapper;
using DTOs.OrderItem;
using KoishopBusinessObjects;

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

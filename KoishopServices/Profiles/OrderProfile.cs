using AutoMapper;
using DTOs.Order;
using KoishopBusinessObjects;

namespace KoishopServices.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderCreationDto, Order>();
        CreateMap<OrderUpdateItemDto, Order>();
    }
}

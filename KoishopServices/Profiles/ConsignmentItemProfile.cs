using AutoMapper;
using DTOs.ConsignmentItem;
using KoishopBusinessObjects;

namespace KoishopServices.Profiles;

public class ConsignmentItemProfile : Profile
{
    public ConsignmentItemProfile()
    {
        CreateMap<ConsignmentItem, ConsignmentItemDto>()
            .ForMember(dest => dest.Consignment, opt => opt.MapFrom(src => src.Consignment))
            .ForMember(dest => dest.KoiFish, opt => opt.MapFrom(src => src.KoiFish))
            .ReverseMap();
        CreateMap<ConsignmentItemCreationDto, ConsignmentItem>();
        CreateMap<ConsignmentItemUpdateDto, ConsignmentItem>();
    }
}

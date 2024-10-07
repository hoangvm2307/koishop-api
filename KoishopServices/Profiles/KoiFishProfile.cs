using AutoMapper;
using DTOs.KoiFish;
using KoishopBusinessObjects;

namespace KoishopServices.Profiles;

public class KoiFishProfile : Profile
{
    public KoiFishProfile()
    {
        CreateMap<KoiFishDto, KoiFish>().ReverseMap();
        CreateMap<KoiFishCreationDto, KoiFish>();
        CreateMap<KoiFishUpdateDto, KoiFish>();
    }
}

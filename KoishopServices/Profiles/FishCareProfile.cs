using AutoMapper;
using DTOs.FishCare;
using KoishopBusinessObjects;

namespace KoishopServices.Profiles;

public class FishCareProfile : Profile
{
    public FishCareProfile()
    {
        CreateMap<FishCareDto, FishCare>().ReverseMap();
        CreateMap<FishCareCreationDto, FishCare>();
        CreateMap<FishCareUpdateDto, FishCare>();
    }
}

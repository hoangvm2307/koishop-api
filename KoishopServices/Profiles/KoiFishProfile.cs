using AutoMapper;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using KoishopRepositories.Repositories.RequestHelpers;

namespace KoishopServices.Profiles;

public class KoiFishProfile : Profile
{
    public KoiFishProfile()
    {
        CreateMap<KoiFishDto, KoiFish>().ReverseMap();
        CreateMap<KoiFishCreationDto, KoiFish>();
        CreateMap<KoiFishUpdateDto, KoiFish>();
        CreateMap<IPagedList<KoiFish>, IPagedList<KoiFishDto>>().ConvertUsing<PagedListConverter<KoiFish, KoiFishDto>>();
    }
}

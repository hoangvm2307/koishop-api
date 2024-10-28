using AutoMapper;
using DTOs.Breed;
using KoishopBusinessObjects;

namespace KoishopServices.Profiles;

public class BreedProfile : Profile
{
    public BreedProfile()
    {
        CreateMap<BreedDto, Breed>().ReverseMap();
        CreateMap<BreedCreationDto, Breed>();
        CreateMap<BreedUpdateDto, Breed>();
    }
}

using AutoMapper;
using DTOs.Breed;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

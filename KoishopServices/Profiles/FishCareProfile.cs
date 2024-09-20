using AutoMapper;
using DTOs.FishCare;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

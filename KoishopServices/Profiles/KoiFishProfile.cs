using AutoMapper;
using DTOs.FishCare;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

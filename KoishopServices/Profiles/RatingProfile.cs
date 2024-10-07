using AutoMapper;
using DTOs.Breed;
using DTOs.Rating;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<RatingDto, Rating>().ReverseMap();
            CreateMap<RatingCreationDto, Rating>();
            CreateMap<RatingUpdateDto, Rating>();
        }
    }
}

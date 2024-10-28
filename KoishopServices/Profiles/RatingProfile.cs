using AutoMapper;
using DTOs.AccountDtos;
using DTOs.Rating;
using KoishopBusinessObjects;
using KoishopServices.Dtos.Rating;

namespace KoishopServices.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
        CreateMap<Rating, RatingDto>()
            .ForMember(dest => dest.UserDto, opt => opt.MapFrom(src => src.User))
            .ReverseMap();
        
        CreateMap<RatingCreationDto, Rating>();
        CreateMap<RatingUpdateDto, Rating>();
        CreateMap<User, UserDto>(); 
        }
    }
}

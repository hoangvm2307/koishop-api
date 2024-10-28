using AutoMapper;
using DTOs.AccountDtos;
using KoishopBusinessObjects;

namespace KoishopServices.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}

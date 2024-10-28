using AutoMapper;
using DTOs.AccountDtos;
using DTOs.KoiFish;

namespace KoishopServices.Dtos.Rating
{
    public static class RatingDtoMappingExtension
    {
        public static RatingDto MapToRatingDto(this KoishopBusinessObjects.Rating projectFrom, IMapper mapper)
        {
            var result = mapper.Map<RatingDto>(projectFrom);
            result.UserDto = mapper.Map<UserDto>(projectFrom.User);
            result.KoiFishDto = mapper.Map<KoiFishDto>(projectFrom.KoiFish);
            return result;
        }
        public static List<RatingDto> MapToRatingDtoList(this IEnumerable<KoishopBusinessObjects.Rating> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToRatingDto(mapper)).ToList();
    }
}

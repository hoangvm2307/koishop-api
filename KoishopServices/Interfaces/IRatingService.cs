using DTOs.Breed;
using DTOs.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingDto>> GetListRating();
        Task<RatingDto> GetRatingById(int id);
        Task AddRating(RatingCreationDto ratingCreationDto);
        Task<bool> UpdateRating(int id, RatingUpdateDto ratingUpdateDto);
        Task<bool> RemoveRating(int id);
    }
}

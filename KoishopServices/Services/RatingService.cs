using AutoMapper;
using DTOs.Rating;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopRepositories.Interfaces;
using KoishopServices.Common.Exceptions;
using KoishopServices.Common.Pagination;
using KoishopServices.Dtos.Rating;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoishopServices.Services
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _ratingRepository;
        private readonly UserManager<User> _userManager;
        private readonly IKoiFishRepository _koiFishRepository;
        public RatingService(IMapper mapper
            , IRatingRepository ratingRepository
            , IKoiFishRepository koiFishRepository
            , UserManager<User> userManager)
        {
            _mapper = mapper;
            _ratingRepository = ratingRepository;
            _koiFishRepository = koiFishRepository;
            _userManager = userManager;
        }

        public async Task AddRating(RatingCreationDto ratingCreationDto)
        {
            var user = await _userManager.FindByIdAsync(ratingCreationDto.UserId.ToString());
            if (user == null)
            {
                throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST);
            }
            var koiFish = await _koiFishRepository.GetByIdAsync(ratingCreationDto.KoiFishId);
            if (koiFish == null)
            {
                throw new NotFoundException(ExceptionConstants.KOIFISH_NOT_EXIST);
            }
            if (ratingCreationDto.RatingValue > 5 || ratingCreationDto.RatingValue < 1)
            {
                throw new ValidationException(ExceptionConstants.INVALID_RATING_VALUE);
            }

            var rating = _mapper.Map<Rating>(ratingCreationDto);
            rating.DateCreated = DateTime.Now;
            await _ratingRepository.AddAsync(rating);
        }

        public async Task<PagedResult<RatingDto>> FilterRating(FilterRatingDto filterRatingDto, CancellationToken cancellationToken)
        {
            Func<IQueryable<Rating>, IQueryable<Rating>> queryOptions = query =>
            {
                query = query.Where(x => x.isDeleted == false);                
                if (filterRatingDto.UserId != -1)
                {
                    query = query.Where(x => x.UserId == filterRatingDto.UserId);
                }
                if (filterRatingDto.KoiFishId != -1)
                {
                    query = query.Where(x => x.KoiFishId == filterRatingDto.KoiFishId);
                }
                if (filterRatingDto.RatingValue != -1)
                {
                    query = query.Where(x => x.RatingValue == filterRatingDto.RatingValue);
                }
                if (!string.IsNullOrEmpty(filterRatingDto.SortBy))
                {
                    query = filterRatingDto.IsDescending 
                        ? query.OrderByDescending(e => EF.Property<object>(e, filterRatingDto.SortBy))
                        : query.OrderBy(e => EF.Property<object>(e, filterRatingDto.SortBy));
                }
                return query;
            };
            var result = await _ratingRepository.FindAllAsync(filterRatingDto.PageNumber, filterRatingDto.PageSize, queryOptions, cancellationToken);
            
            return PagedResult<RatingDto>.Create(
                totalCount: result.TotalCount,
                pageCount: result.PageCount,
                pageSize: result.PageSize,
                pageNumber: result.PageNo,
                data: result.MapToRatingDtoList(_mapper));
        }

        public async Task<IEnumerable<RatingDto>> GetListRating()
        {
            var ratings = await _ratingRepository.GetListAsync();
            var ratingDtos = _mapper.Map<List<RatingDto>>(ratings);
            foreach (var rating in ratingDtos)
            {
                var username = await _userManager.FindByIdAsync(rating.UserId.ToString());
                rating.UserName = username.UserName;
            }
            return ratingDtos;
        }

        public async Task<RatingDto> GetRatingById(int id, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.FindAsync(x => x.Id == id, q => q.Include(f => f.KoiFish)
                                                                                    .Include(u => u.User), cancellationToken);
            if (rating == null)
                return null;
            return rating.MapToRatingDto(_mapper);
        }

        public async Task<bool> RemoveRating(int id)
        {
            var exist = await _ratingRepository.GetByIdAsync(id);
            if (exist == null)
                return false;
            await _ratingRepository.DeleteAsync(exist);
            return true;
        }

        public async Task<bool> UpdateRating(int id, RatingUpdateDto ratingUpdateDto)
        {
            var existingRating = await _ratingRepository.GetByIdAsync(id);
            if (existingRating == null)
            {
                throw new NotFoundException(ExceptionConstants.RATING_NOT_EXIST);
            }
            var user = await _userManager.FindByIdAsync(ratingUpdateDto.UserId.ToString());
            if (user == null)
            {
                throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST);
            }
            var koiFish = await _koiFishRepository.GetByIdAsync(ratingUpdateDto.KoiFishId);
            if (koiFish == null)
            {
                throw new NotFoundException(ExceptionConstants.KOIFISH_NOT_EXIST);
            }
            if (ratingUpdateDto.RatingValue > 5 || ratingUpdateDto.RatingValue < 1)
            {
                throw new ValidationException(ExceptionConstants.INVALID_RATING_VALUE);
            }

            _mapper.Map(ratingUpdateDto, existingRating);
            await _ratingRepository.UpdateAsync(existingRating);
            return true;
        }
    }
}

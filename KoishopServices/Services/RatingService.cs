using AutoMapper;
using DTOs.Breed;
using DTOs.Rating;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopRepositories.Interfaces;
using KoishopRepositories.Repositories;
using KoishopServices.Common.Exceptions;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _ratingRepository.AddAsync(rating);
        }

        public async Task<IEnumerable<RatingDto>> GetListRating()
        {
            var ratings = await _ratingRepository.GetAllAsync();
            var result = _mapper.Map<List<RatingDto>>(ratings);
            return result;
        }

        public async Task<RatingDto> GetRatingById(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating == null)
                return null;
            return _mapper.Map<RatingDto>(rating);
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
                return false;

            //TODO: Add validation before Update and mapping
            _mapper.Map(ratingUpdateDto, existingRating);
            await _ratingRepository.UpdateAsync(existingRating);
            return true;
        }
    }
}

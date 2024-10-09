using DTOs.Breed;
using DTOs.Rating;
using KoishopServices.Common.Pagination;
using KoishopServices.Dtos.Rating;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        /// <summary>
        /// Retrieves a list of all ratings with filter and sorting.
        /// </summary>
        /// <returns>List of RatingDto objects.</returns>
        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<RatingDto>>> FilterRating([FromQuery]FilterRatingDto filterRatingDto, CancellationToken cancellationToken = default)
        {
            var result = await _ratingService.FilterRating(filterRatingDto, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all ratings.
        /// </summary>
        /// <returns>List of RatingDto objects.</returns>
        [HttpGet]
        public async Task<ActionResult<List<RatingDto>>> GetRatings()
        {
            var ratings = await _ratingService.GetListRating();
            return Ok(ratings);
        }

        /// <summary>
        /// Creates a new rating.
        /// </summary>
        /// <param name="ratingCreationDto">The data for the new rating.</param>
        /// <returns>Returns the created rating information.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateRating([FromBody] RatingCreationDto ratingCreationDto)
        {
            await _ratingService.AddRating(ratingCreationDto);
            return CreatedAtAction(nameof(GetRatings), ratingCreationDto);
        }

        /// <summary>
        /// Retrieves a specific rating by its ID.
        /// </summary>
        /// <param name="id">The ID of the rating to retrieve.</param>
        /// <returns>Returns the rating if found, otherwise NotFound.</returns>
        [HttpGet("/rating/{id}")]
        public async Task<ActionResult<RatingDto>> GetRatingById([FromRoute] int id)
        {
            var rating = await _ratingService.GetRatingById(id);
            if (rating == null)
                return NotFound();
            return Ok(rating);
        }

        /// <summary>
        /// Updates an existing rating.
        /// </summary>
        /// <param name="id">The ID of the rating to update.</param>
        /// <param name="ratingUpdateDto">The updated rating data.</param>
        /// <returns>Returns NoContent if successful, otherwise NotFound.</returns>
        [HttpPut("/rating/{id}")]
        public async Task<ActionResult> UpdateRating([FromRoute] int id,[FromBody] RatingUpdateDto ratingUpdateDto)
        {
            var isUpdated = await _ratingService.UpdateRating(id, ratingUpdateDto);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a rating by its ID.
        /// </summary>
        /// <param name="id">The ID of the rating to delete.</param>
        /// <returns>Returns NoContent if successful, otherwise NotFound.</returns>
        [HttpDelete("/rating/{id}")]
        public async Task<ActionResult> DeleteRating(int id)
        {
            var isDeleted = await _ratingService.RemoveRating(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}

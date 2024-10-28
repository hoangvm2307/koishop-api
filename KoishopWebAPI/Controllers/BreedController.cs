using DTOs.Breed;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;

public class BreedController : BaseApiController
{
    private readonly IBreedService _breedService;

    public BreedController(IBreedService breedService)
    {
        _breedService = breedService;
    }

    /// <summary>
    /// Get all breed list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<BreedDto>>> GetBreeds()
    {
        var breeds = await _breedService.GetListBreed();
        return Ok(breeds);
    }

    /// <summary>
    /// Add a new breed
    /// </summary>
    /// <param name="breedCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateBreed([FromForm] BreedCreationDto breedCreationDto)
    {
        await _breedService.AddBreed(breedCreationDto);
        return CreatedAtAction(nameof(GetBreeds), breedCreationDto);
    }

    /// <summary>
    /// get a detail breed's infromation
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BreedDto>> GetBreedById(int id)
    {
        var breed = await _breedService.GetBreedById(id);
        if (breed == null)
            return NotFound();
        return Ok(breed);
    }

    /// <summary>
    /// Update breed's information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="breedUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBreed(int id, BreedUpdateDto breedUpdateDto)
    {
        var isUpdated = await _breedService.UpdateBreed(id, breedUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a breed
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBreed(int id)
    {
        var isDeleted = await _breedService.RemoveBreed(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

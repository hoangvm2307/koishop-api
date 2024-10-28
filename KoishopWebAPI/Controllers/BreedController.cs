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

    [HttpGet]
    public async Task<ActionResult<List<BreedDto>>> GetBreeds()
    {
        var breeds = await _breedService.GetListBreed();
        return Ok(breeds);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBreed([FromForm] BreedCreationDto breedCreationDto)
    {
        await _breedService.AddBreed(breedCreationDto);
        return CreatedAtAction(nameof(GetBreeds), breedCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BreedDto>> GetBreedById(int id)
    {
        var breed = await _breedService.GetBreedById(id);
        if (breed == null)
            return NotFound();
        return Ok(breed);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBreed(int id, BreedUpdateDto breedUpdateDto)
    {
        var isUpdated = await _breedService.UpdateBreed(id, breedUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBreed(int id)
    {
        var isDeleted = await _breedService.RemoveBreed(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

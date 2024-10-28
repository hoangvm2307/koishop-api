using DTOs.FishCare;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;
public class FishCareController : BaseApiController
{
    private readonly IFishCareService _fishCareService;

    public FishCareController(IFishCareService fishCareService)
    {
        _fishCareService = fishCareService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FishCareDto>>> GetFishCares()
    {
        var fishCares = await _fishCareService.GetListFishCare();
        return Ok(fishCares);
    }

    [HttpPost]
    public async Task<ActionResult> CreateFishCare(FishCareCreationDto fishCareCreationDto)
    {
        await _fishCareService.AddFishCare(fishCareCreationDto);
        return CreatedAtAction(nameof(GetFishCares), fishCareCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FishCareDto>> GetFishCareById(int id)
    {
        var fishCare = await _fishCareService.GetFishCareById(id);
        if (fishCare == null)
            return NotFound();
        return Ok(fishCare);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFishCare(int id, FishCareUpdateDto fishCareUpdateDto)
    {
        var isUpdated = await _fishCareService.UpdateFishCare(id, fishCareUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFishCare(int id)
    {
        var isDeleted = await _fishCareService.RemoveFishCare(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

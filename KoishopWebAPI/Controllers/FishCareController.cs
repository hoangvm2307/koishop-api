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

    /// <summary>
    /// Get all fishcare list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<FishCareDto>>> GetFishCares()
    {
        var fishCares = await _fishCareService.GetListFishCare();
        return Ok(fishCares);
    }

    /// <summary>
    /// Add a new fishcare to system
    /// </summary>
    /// <param name="fishCareCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateFishCare(FishCareCreationDto fishCareCreationDto)
    {
        await _fishCareService.AddFishCare(fishCareCreationDto);
        return CreatedAtAction(nameof(GetFishCares), fishCareCreationDto);
    }

    /// <summary>
    /// Get detail fishcare's information
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<FishCareDto>> GetFishCareById(int id)
    {
        var fishCare = await _fishCareService.GetFishCareById(id);
        if (fishCare == null)
            return NotFound();
        return Ok(fishCare);
    }

    /// <summary>
    /// Update fishcare's information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fishCareUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFishCare(int id, FishCareUpdateDto fishCareUpdateDto)
    {
        var isUpdated = await _fishCareService.UpdateFishCare(id, fishCareUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a fishcare
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFishCare(int id)
    {
        var isDeleted = await _fishCareService.RemoveFishCare(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

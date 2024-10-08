using DTOs.KoiFish;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;
public class KoiFishController : BaseApiController
{
    private readonly IKoiFishService _koiFishService;

    public KoiFishController(IKoiFishService koiFishService)
    {
        _koiFishService = koiFishService;
    }

    [HttpGet]
    public async Task<ActionResult<List<KoiFishDto>>> GetKoiFishs()
    {
        var koiFishs = await _koiFishService.GetListKoiFish();
        return Ok(koiFishs);
    }

    /// <summary>
    /// Creates a new koi fish.
    /// </summary>
    /// <param name="koiFishCreationDto">The data for the new koi fish.</param>
    /// <returns>Returns the created koi fish information.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateKoiFish([FromBody] KoiFishCreationDto koiFishCreationDto)
    {
        await _koiFishService.AddKoiFish(koiFishCreationDto);
        return CreatedAtAction(nameof(GetKoiFishs), koiFishCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KoiFishDto>> GetKoiFishById(int id)
    {
        var koiFish = await _koiFishService.GetKoiFishById(id);
        if (koiFish == null)
            return NotFound();
        return Ok(koiFish);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateKoiFish(int id, KoiFishUpdateDto koiFishUpdateDto)
    {
        var isUpdated = await _koiFishService.UpdateKoiFish(id, koiFishUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteKoiFish(int id)
    {
        var isDeleted = await _koiFishService.RemoveKoiFish(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

using System.Text.Json;
using DTOs.KoiFish;
using KoishopRepositories.Repositories.RequestHelpers;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;
public class KoiFishController : BaseApiController
{
  private readonly IKoiFishService _koiFishService;

  public KoiFishController(IKoiFishService koiFishService)
  {
    _koiFishService = koiFishService;
  }

  /// <summary>
  /// Get KoiFish list with paging, sort, search, filter
  /// </summary>
  /// <param name="koiFishParams"></param>
  /// <returns>List KoiFish</returns>
  [HttpGet]
  public async Task<ActionResult<IPagedList<KoiFishDto>>> GetKoiFishs([FromQuery] KoiFishParams koiFishParams)
  {
    var koiFishs = await _koiFishService.GetListKoiFish(koiFishParams);
    Response.Headers.Add("Pagination", JsonSerializer.Serialize(koiFishs.MetaData));
    return Ok(koiFishs);
  }


  /// <summary>
  /// Get param filter KoiFish
  /// </summary>
  /// <returns></returns>
  [HttpGet("filter")]
  public async Task<ActionResult<FilterKoiFishParamDto>> GetFilterKoiFish()
  {
    return Ok(await _koiFishService.GetFilterParam());
  }

  /// <summary>
  /// Create a new koi fish.
  /// </summary>
  /// <param name="koiFishCreationDto">The data for the new koi fish.</param>
  /// <returns>Returns the created koi fish information.</returns>
  [HttpPost]
  public async Task<ActionResult> CreateKoiFish([FromBody] KoiFishCreationDto koiFishCreationDto)
  {
    await _koiFishService.AddKoiFish(koiFishCreationDto);
    return CreatedAtAction(nameof(GetKoiFishs), koiFishCreationDto);
  }

  /// <summary>
  /// Create a new koi fish with userId
  /// </summary>
  /// <param name="koiFishCreationDto"></param>
  /// <param name="userId"></param>
  /// <returns></returns>
  [HttpPost("{userId}")]
  public async Task<ActionResult> CreateKoiFishWithUser([FromBody] KoiFishCreationDto koiFishCreationDto, string userId)
  {
    var koiFish = await _koiFishService.AddKoiFishWithUser(koiFishCreationDto, userId);
    return CreatedAtAction(nameof(GetKoiFishs), koiFish);
  }

  /// <summary>
  /// Get a KoiFish by id
  /// </summary>
  /// <param name="id"></param>
  [HttpGet("{id}")]
  public async Task<ActionResult<KoiFishDto>> GetKoiFishById(int id)
  {
    var koiFish = await _koiFishService.GetKoiFishById(id);
    if (koiFish == null)
      return NotFound();
    return Ok(koiFish);
  }

  /// <summary>
  /// Get KoiFish by list ids
  /// </summary>
  /// <param name="ids"></param>
  /// <returns></returns>
  [HttpGet("list-ids")]
  public async Task<ActionResult<List<KoiFishDto>>> GetKoiFishByIds([FromQuery] int[] ids)
  {
    var koiFish = await _koiFishService.GetKoiFishByIds(ids.ToList());
    if (koiFish == null)
      return NotFound();
    return Ok(koiFish);
  }
  [HttpPatch("{id}/status")]
  public async Task<ActionResult> UpdateKoiFishStatus(int id, string status)
  {
    await _koiFishService.UpdateKoiFishStatus(id, status);
    return NoContent();
  }

  /// <summary>
  /// Get related fish list
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("related/{id}")]
  public async Task<ActionResult<List<KoiFishDto>>> GetRelatedKoiFishBy(int id)
  {
    var listFish = await _koiFishService.GetRelatedKoiFishBy(id);
    if (listFish == null)
      return NotFound();
    return Ok(listFish);
  }

  /// <summary>
  /// Update a KoiFish by id
  /// </summary>
  /// <param name="id"></param>
  /// <param name="koiFishUpdateDto"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  public async Task<ActionResult> UpdateKoiFish(int id, KoiFishUpdateDto koiFishUpdateDto)
  {
    var isUpdated = await _koiFishService.UpdateKoiFish(id, koiFishUpdateDto);
    if (!isUpdated)
      return NotFound();
    return NoContent();
  }

  /// <summary>
  /// Delete a KoiFish by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteKoiFish(int id)
  {
    var isDeleted = await _koiFishService.RemoveKoiFish(id);
    if (!isDeleted)
      return NotFound();
    return NoContent();
  }
}

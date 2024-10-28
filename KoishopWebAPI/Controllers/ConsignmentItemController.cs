using DTOs.ConsignmentItem;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;
public class ConsignmentItemController : BaseApiController
{
    private readonly IConsignmentItemService _consignmentItemService;

    public ConsignmentItemController(IConsignmentItemService consignmentItemService)
    {
        _consignmentItemService = consignmentItemService;
    }

    /// <summary>
    /// Get all consignment item
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<ConsignmentItemDto>>> GetConsignmentItems()
    {
        var consignmentItems = await _consignmentItemService.GetListConsignmentItem();
        return Ok(consignmentItems);
    }

    /// <summary>
    /// Add a new consignment
    /// </summary>
    /// <param name="consignmentItemCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateConsignmentItem(ConsignmentItemCreationDto consignmentItemCreationDto)
    {
        await _consignmentItemService.AddConsignmentItem(consignmentItemCreationDto);
        return CreatedAtAction(nameof(GetConsignmentItems), consignmentItemCreationDto);
    }

    /// <summary>
    /// Get detail of a consignment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("detail/{id}")]
    public async Task<ActionResult<ConsignmentItemDto>> GetConsignmentItemById(int id)
    {
        var consignmentItem = await _consignmentItemService.GetConsignmentItemById(id);
        if (consignmentItem == null)
            return NotFound();
        return Ok(consignmentItem);
    }

    /// <summary>
    /// Get list consignment item in consignment
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    [HttpGet("{consignmentId}")]
    public async Task<ActionResult<ConsignmentItemDto>> GetConsignmentItemByConsignmentId(int consignmentId)
    {
        var consignmentItem = await _consignmentItemService.GetConsignmentItemByConsignmentId(consignmentId);
        if (consignmentItem == null)
            return NotFound();
        return Ok(consignmentItem);
    }

    /// <summary>
    /// Update consignment's information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="consignmentItemUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateConsignmentItem(int id, ConsignmentItemUpdateDto consignmentItemUpdateDto)
    {
        var isUpdated = await _consignmentItemService.UpdateConsignmentItem(id, consignmentItemUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a consignment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteConsignmentItem(int id)
    {
        var isDeleted = await _consignmentItemService.RemoveConsignmentItem(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

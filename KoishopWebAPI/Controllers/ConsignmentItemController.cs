using DTOs.ConsignmentItem;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;
public class ConsignmentItemController : BaseApiController
{
    private readonly IConsignmentItemService _consignmentItemService;

    public ConsignmentItemController(IConsignmentItemService consignmentItemService)
    {
        _consignmentItemService = consignmentItemService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ConsignmentItemDto>>> GetConsignmentItems()
    {
        var consignmentItems = await _consignmentItemService.GetListConsignmentItem();
        return Ok(consignmentItems);
    }

    [HttpPost]
    public async Task<ActionResult> CreateConsignmentItem(ConsignmentItemCreationDto consignmentItemCreationDto)
    {
        await _consignmentItemService.AddConsignmentItem(consignmentItemCreationDto);
        return CreatedAtAction(nameof(GetConsignmentItems), consignmentItemCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConsignmentItemDto>> GetConsignmentItemById(int id)
    {
        var consignmentItem = await _consignmentItemService.GetConsignmentItemById(id);
        if (consignmentItem == null)
            return NotFound();
        return Ok(consignmentItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateConsignmentItem(int id, ConsignmentItemUpdateDto consignmentItemUpdateDto)
    {
        var isUpdated = await _consignmentItemService.UpdateConsignmentItem(id, consignmentItemUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteConsignmentItem(int id)
    {
        var isDeleted = await _consignmentItemService.RemoveConsignmentItem(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

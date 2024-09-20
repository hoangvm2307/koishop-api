using DTOs.Consignment;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;

public class ConsignmentController : BaseApiController
{
    private readonly IConsignmentService _consignmentService;

    public ConsignmentController(IConsignmentService consignmentService)
    {
        _consignmentService = consignmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ConsignmentDto>>> GetConsignments()
    {
        var consignments = await _consignmentService.GetListConsignment();
        return Ok(consignments);
    }

    [HttpPost]
    public async Task<ActionResult> CreateConsignment(ConsignmentCreationDto consignmentCreationDto)
    {
        await _consignmentService.AddConsignment(consignmentCreationDto);
        return CreatedAtAction(nameof(GetConsignments), consignmentCreationDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConsignmentDto>> GetConsignmentById(int id)
    {
        var consignment = await _consignmentService.GetConsignmentById(id);
        if (consignment == null)
            return NotFound();
        return Ok(consignment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateConsignment(int id, ConsignmentUpdateDto consignmentUpdateDto)
    {
        var isUpdated = await _consignmentService.UpdateConsignment(id, consignmentUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteConsignment(int id)
    {
        var isDeleted = await _consignmentService.RemoveConsignment(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

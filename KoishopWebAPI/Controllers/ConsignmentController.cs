using DTOs.Consignment;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;

public class ConsignmentController : BaseApiController
{
    private readonly IConsignmentService _consignmentService;

    public ConsignmentController(IConsignmentService consignmentService)
    {
        _consignmentService = consignmentService;
    }

    /// <summary>
    /// Get all consignments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<ConsignmentDto>>> GetConsignments()
    {
        var consignments = await _consignmentService.GetListConsignment();
        return Ok(consignments);
    }

    /// <summary>
    /// Create a new consignment
    /// </summary>
    /// <param name="consignmentCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateConsignment(ConsignmentCreationDto consignmentCreationDto)
    {
        await _consignmentService.AddConsignment(consignmentCreationDto);
        return CreatedAtAction(nameof(GetConsignments), consignmentCreationDto);
    }

    /// <summary>
    /// Get a consignment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("detail/{id}")]
    public async Task<ActionResult<ConsignmentDto>> GetConsignmentById(int id)
    {
        var consignment = await _consignmentService.GetConsignmentById(id);
        if (consignment == null)
            return NotFound();
        return Ok(consignment);
    }

    /// <summary>
    /// Get user's list consignment
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ConsignmentDto>> GetConsignmentByUserId(int userId)
    {
        var consignment = await _consignmentService.GetListConsignmentByUserId(userId);
        if (consignment == null)
            return NotFound();
        return Ok(consignment);
    }

    /// <summary>
    /// Update consignment's information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="consignmentUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateConsignment(int id, ConsignmentUpdateDto consignmentUpdateDto)
    {
        var isUpdated = await _consignmentService.UpdateConsignment(id, consignmentUpdateDto);
        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a consingment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteConsignment(int id)
    {
        var isDeleted = await _consignmentService.RemoveConsignment(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}

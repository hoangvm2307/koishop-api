using DTOs.Consignment;
using KoishopServices.Dtos.Dashboard;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoishopWebAPI.Controllers;

public class DashboardController : BaseApiController
{
    private readonly IConsignmentService _consignmentService;
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;

    public DashboardController(IConsignmentService consignmentService, IOrderService orderService, IOrderItemService orderItemService)
    {
        _consignmentService = consignmentService;
        _orderService = orderService;
        _orderItemService = orderItemService;
    }

    [HttpGet("fav-customer")]
    public async Task<ActionResult<List<OrderFavCustomer>>> GetOrderFavCustomer()
    {
        var favCustomer = await _orderService.GetTotalOrderByUser();
        return Ok(favCustomer);
    }

    [HttpGet("ranking-origin")]
    public async Task<ActionResult<List<FavOrigin>>> GetRankingOrigin()
    {
        var favCustomer = await _orderItemService.GetTotalOrderByOrigin();
        return Ok(favCustomer);
    }

    [HttpGet("consignment-by-type")]
    public async Task<ActionResult<List<TotalConsignment>>> GetTotalConsignmentByType()
    {
        var favCustomer = await _consignmentService.GetTotalConsignmentByType();
        return Ok(favCustomer);
    }

    [HttpGet("consignment-by-month")]
    public async Task<ActionResult<List<TotalConsignmentByMonth>>> GetTotalConsignmentByMonth()
    {
        var favCustomer = await _consignmentService.GetMonthlyTotalConsignmentAsync();
        return Ok(favCustomer);
    }
}

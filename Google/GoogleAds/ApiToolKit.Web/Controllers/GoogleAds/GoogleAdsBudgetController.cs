using ApiToolKit.Application.DTOs.GoogleAds.Budget;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/budgets")]
[Produces("application/json")]
public class GoogleAdsBudgetController : ControllerBase
{
    private readonly IGoogleAdsBudgetService _budgetService;

    public GoogleAdsBudgetController(IGoogleAdsBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    /// <summary>List campaign budgets for a customer</summary>
    [HttpGet]
    public async Task<IActionResult> ListBudgets([FromQuery] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListBudgetsRequest { CustomerId = customerId };
        var result = await _budgetService.ListBudgetsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new campaign budget</summary>
    [HttpPost]
    public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _budgetService.CreateBudgetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

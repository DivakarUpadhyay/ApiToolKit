using ApiToolKit.Application.DTOs.Shopee.Ams;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/ams")]
[Produces("application/json")]
public class ShopeeAmsController : ControllerBase
{
    private readonly IShopeeAmsService _svc;

    public ShopeeAmsController(IShopeeAmsService svc)
    {
        _svc = svc;
    }

    /// <summary>Get list of affiliate campaigns</summary>
    [HttpGet("campaigns")]
    public async Task<IActionResult> GetAffiliateCampaignList([FromQuery] GetAffiliateCampaignListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAffiliateCampaignListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new affiliate campaign</summary>
    [HttpPost("campaigns")]
    public async Task<IActionResult> CreateAffiliateCampaign([FromBody] CreateAffiliateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateAffiliateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific affiliate campaign</summary>
    [HttpGet("campaigns/detail")]
    public async Task<IActionResult> GetAffiliateCampaign([FromQuery] GetAffiliateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAffiliateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing affiliate campaign</summary>
    [HttpPost("campaigns/update")]
    public async Task<IActionResult> UpdateAffiliateCampaign([FromBody] UpdateAffiliateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.UpdateAffiliateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete one or more affiliate campaigns</summary>
    [HttpPost("campaigns/delete")]
    public async Task<IActionResult> DeleteAffiliateCampaign([FromBody] DeleteAffiliateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.DeleteAffiliateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add items to an affiliate campaign</summary>
    [HttpPost("campaigns/items/add")]
    public async Task<IActionResult> AddItemToCampaign([FromBody] AddItemToCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.AddItemToCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove items from an affiliate campaign</summary>
    [HttpPost("campaigns/items/remove")]
    public async Task<IActionResult> RemoveItemFromCampaign([FromBody] RemoveItemFromCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.RemoveItemFromCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of items in an affiliate campaign</summary>
    [HttpGet("campaigns/items")]
    public async Task<IActionResult> GetCampaignItemList([FromQuery] GetCampaignItemListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetCampaignItemListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of orders generated through affiliate campaigns</summary>
    [HttpGet("orders")]
    public async Task<IActionResult> GetAffiliateOrderList([FromQuery] GetAffiliateOrderListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAffiliateOrderListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of affiliates for a campaign</summary>
    [HttpGet("affiliates")]
    public async Task<IActionResult> GetAffiliateList([FromQuery] GetAffiliateListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAffiliateListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific affiliate</summary>
    [HttpGet("affiliates/detail")]
    public async Task<IActionResult> GetAffiliate([FromQuery] GetAffiliateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAffiliateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Approve one or more affiliate applications</summary>
    [HttpPost("affiliates/approve")]
    public async Task<IActionResult> ApproveAffiliate([FromBody] ApproveAffiliateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.ApproveAffiliateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Reject one or more affiliate applications</summary>
    [HttpPost("affiliates/reject")]
    public async Task<IActionResult> RejectAffiliate([FromBody] RejectAffiliateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.RejectAffiliateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of affiliate tracking links</summary>
    [HttpGet("links")]
    public async Task<IActionResult> GetAffiliateLinkList([FromQuery] GetAffiliateLinkListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAffiliateLinkListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get daily performance metrics for an affiliate campaign</summary>
    [HttpGet("campaigns/performance")]
    public async Task<IActionResult> GetCampaignPerformance([FromQuery] GetCampaignPerformanceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetCampaignPerformanceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of commission settlement records</summary>
    [HttpGet("settlements")]
    public async Task<IActionResult> GetCommissionSettlementList([FromQuery] GetCommissionSettlementListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetCommissionSettlementListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the current AMS configuration</summary>
    [HttpGet("config")]
    public async Task<IActionResult> GetAmsConfig()
    {
        var result = await _svc.GetAmsConfigAsync(new GetAmsConfigRequest());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the AMS configuration</summary>
    [HttpPost("config")]
    public async Task<IActionResult> UpdateAmsConfig([FromBody] UpdateAmsConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.UpdateAmsConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

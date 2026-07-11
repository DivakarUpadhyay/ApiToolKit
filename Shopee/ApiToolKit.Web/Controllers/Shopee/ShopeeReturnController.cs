using ApiToolKit.Application.DTOs.Shopee.Return;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Shopee;
[ApiController][Route("api/shopee/returns")][Produces("application/json")]
public class ShopeeReturnController : ControllerBase
{
    private readonly IShopeeReturnService _svc;
    public ShopeeReturnController(IShopeeReturnService svc) { _svc = svc; }
    /// <summary>Get paginated list of return/refund requests</summary>
    [HttpGet("list")] public async Task<IActionResult> GetReturnList([FromQuery] GetReturnListRequest r) { var res = await _svc.GetReturnListAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get details of a specific return request</summary>
    [HttpGet("detail")] public async Task<IActionResult> GetReturnDetail([FromQuery] GetReturnDetailRequest r) { var res = await _svc.GetReturnDetailAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Accept/confirm a return request</summary>
    [HttpPost("confirm")] public async Task<IActionResult> ConfirmReturn([FromBody] ConfirmReturnRequest r) { var res = await _svc.ConfirmReturnAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Raise a dispute on a return request</summary>
    [HttpPost("dispute")] public async Task<IActionResult> DisputeReturn([FromBody] DisputeReturnRequest r) { var res = await _svc.DisputeReturnAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Make a counter-offer on a return</summary>
    [HttpPost("offer")] public async Task<IActionResult> OfferReturn([FromBody] OfferReturnRequest r) { var res = await _svc.OfferReturnAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Accept an offer made by the buyer</summary>
    [HttpPost("accept-offer")] public async Task<IActionResult> AcceptOffer([FromBody] AcceptOfferRequest r) { var res = await _svc.AcceptOfferAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Cancel a compensation dispute</summary>
    [HttpPost("cancel-dispute")] public async Task<IActionResult> CancelDispute([FromBody] CancelDisputeRequest r) { var res = await _svc.CancelDisputeAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Upload proof/evidence for a return</summary>
    [HttpPost("upload-proof")] public async Task<IActionResult> UploadProof([FromBody] UploadProofRequest r) { var res = await _svc.UploadProofAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get available dispute reasons</summary>
    [HttpGet("{shopId}/dispute-reasons")] public async Task<IActionResult> GetDisputeReasons(long shopId) { var res = await _svc.GetReturnDisputeReasonAsync(new GetReturnDisputeReasonRequest { ShopId = shopId }); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get available return solutions</summary>
    [HttpGet("{shopId}/solutions/{returnSn}")] public async Task<IActionResult> GetAvailableSolutions(long shopId, string returnSn) { var res = await _svc.GetAvailableSolutionsAsync(shopId, returnSn); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get shipping carriers for seller-arranged returns</summary>
    [HttpGet("{shopId}/carriers/{returnSn}")] public async Task<IActionResult> GetShippingCarrier(long shopId, string returnSn) { var res = await _svc.GetShippingCarrierAsync(shopId, returnSn); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Query previously uploaded evidence</summary>
    [HttpGet("{shopId}/proof/{returnSn}")] public async Task<IActionResult> QueryProof(long shopId, string returnSn) { var res = await _svc.QueryProofAsync(shopId, returnSn); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get reverse/post-return tracking info</summary>
    [HttpGet("{shopId}/reverse-tracking/{returnSn}")] public async Task<IActionResult> GetReverseTrackingInfo(long shopId, string returnSn) { var res = await _svc.GetReverseTrackingInfoAsync(shopId, returnSn); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Upload shipping proof for a return</summary>
    [HttpPost("{shopId}/shipping-proof")] public async Task<IActionResult> UploadShippingProof(long shopId, [FromQuery] string returnSn, [FromQuery] string trackingNumber, [FromQuery] string carrierId) { var res = await _svc.UploadShippingProofAsync(shopId, returnSn, trackingNumber, carrierId); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Convert image files to URLs for evidence submission</summary>
    [HttpPost("{shopId}/convert-image")] public async Task<IActionResult> ConvertImage(long shopId, [FromBody] List<string> imageUrlList) { var res = await _svc.ConvertImageAsync(shopId, imageUrlList); return res.Success ? Ok(res) : BadRequest(res); }
}

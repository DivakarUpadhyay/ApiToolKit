using ApiToolKit.Application.DTOs.Shopee.Payment;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Shopee;
[ApiController][Route("api/shopee/payment")][Produces("application/json")]
public class ShopeePaymentController : ControllerBase
{
    private readonly IShopeePaymentService _svc;
    public ShopeePaymentController(IShopeePaymentService svc) { _svc = svc; }
    /// <summary>Get list of escrow transactions (released orders)</summary>
    [HttpGet("escrow/list")] public async Task<IActionResult> GetEscrowList([FromQuery] GetEscrowListRequest r) { var res = await _svc.GetEscrowListAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get escrow detail for a single order</summary>
    [HttpGet("escrow/detail")] public async Task<IActionResult> GetEscrowDetail([FromQuery] GetEscrowDetailRequest r) { var res = await _svc.GetEscrowDetailAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get escrow details for multiple orders in batch</summary>
    [HttpPost("escrow/detail-batch")] public async Task<IActionResult> GetEscrowDetailBatch([FromQuery] long shopId, [FromBody] List<string> orderSnList) { var res = await _svc.GetEscrowDetailBatchAsync(shopId, orderSnList); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get wallet transaction list</summary>
    [HttpGet("wallet/transactions")] public async Task<IActionResult> GetWalletTransactionList([FromQuery] GetWalletTransactionListRequest r) { var res = await _svc.GetWalletTransactionListAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get list of available payment methods for a shop</summary>
    [HttpGet("methods")] public async Task<IActionResult> GetPaymentMethodList([FromQuery] GetPaymentMethodListRequest r) { var res = await _svc.GetPaymentMethodListAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get shop-level installment payment status</summary>
    [HttpGet("installment/shop")] public async Task<IActionResult> GetShopInstallmentStatus([FromQuery] GetInstallmentStatusRequest r) { var res = await _svc.GetShopInstallmentStatusAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Enable or disable installment payment for a shop</summary>
    [HttpPost("installment/shop")] public async Task<IActionResult> SetShopInstallmentStatus([FromBody] SetInstallmentStatusRequest r) { var res = await _svc.SetShopInstallmentStatusAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get installment payment status for specific items</summary>
    [HttpPost("installment/items/status")] public async Task<IActionResult> GetItemInstallmentStatus([FromBody] GetItemInstallmentRequest r) { var res = await _svc.GetItemInstallmentStatusAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Enable or disable installment for a specific item</summary>
    [HttpPost("installment/items")] public async Task<IActionResult> SetItemInstallmentStatus([FromBody] SetItemInstallmentRequest r) { var res = await _svc.SetItemInstallmentStatusAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Trigger income report generation</summary>
    [HttpPost("income/generate")] public async Task<IActionResult> GenerateIncomeReport([FromBody] GenerateIncomeReportRequest r) { var res = await _svc.GenerateIncomeReportAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get generated income report</summary>
    [HttpGet("income/report")] public async Task<IActionResult> GetIncomeReport([FromQuery] GetIncomeReportRequest r) { var res = await _svc.GetIncomeReportAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get billing transaction info</summary>
    [HttpGet("billing/transactions")] public async Task<IActionResult> GetBillingTransactionInfo([FromQuery] GetBillingTransactionRequest r) { var res = await _svc.GetBillingTransactionInfoAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get payout detail (legacy)</summary>
    [HttpGet("payout/detail")] public async Task<IActionResult> GetPayoutDetail([FromQuery] GetPayoutDetailRequest r) { var res = await _svc.GetPayoutDetailAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get payout info (current)</summary>
    [HttpGet("payout/info")] public async Task<IActionResult> GetPayoutInfo([FromQuery] GetPayoutInfoRequest r) { var res = await _svc.GetPayoutInfoAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
}

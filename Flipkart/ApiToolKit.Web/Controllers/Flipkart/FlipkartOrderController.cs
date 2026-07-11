using ApiToolKit.Application.DTOs.Flipkart.Order;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Flipkart;

[ApiController]
[Route("api/flipkart/orders")]
[Produces("application/json")]
public class FlipkartOrderController : ControllerBase
{
    private readonly IFlipkartOrderService _service;
    public FlipkartOrderController(IFlipkartOrderService service) => _service = service;

    /// <summary>Filter shipments by criteria</summary>
    [HttpPost("shipments/filter")]
    public async Task<IActionResult> FilterShipments([FromBody] FilterShipmentsRequest request)
    {
        var result = await _service.FilterShipmentsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shipments by shipment IDs</summary>
    [HttpGet("shipments")]
    public async Task<IActionResult> GetShipmentsByIds([FromQuery] string shipmentIds)
    {
        var result = await _service.GetShipmentsByIdsAsync(new GetShipmentsByIdsRequest { ShipmentIds = shipmentIds });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shipments by order item IDs</summary>
    [HttpGet("shipments/by-order-items")]
    public async Task<IActionResult> GetShipmentsByOrderItemIds([FromQuery] string orderItemIds)
    {
        var result = await _service.GetShipmentsByOrderItemIdsAsync(new GetShipmentsByOrderItemIdsRequest { OrderItemIds = orderItemIds });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shipments by order IDs</summary>
    [HttpGet("shipments/by-orders")]
    public async Task<IActionResult> GetShipmentsByOrderIds([FromQuery] string orderIds)
    {
        var result = await _service.GetShipmentsByOrderIdsAsync(new GetShipmentsByOrderIdsRequest { OrderIds = orderIds });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generate labels (Pack API)</summary>
    [HttpPost("shipments/labels")]
    public async Task<IActionResult> GenerateLabels([FromBody] GenerateLabelsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.GenerateLabelsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Download labels as PDF+invoice stream</summary>
    [HttpGet("shipments/{shipmentIds}/labels")]
    public async Task<IActionResult> DownloadLabels(string shipmentIds)
    {
        var result = await _service.DownloadLabelsAsync(new DownloadLabelsRequest { ShipmentIds = shipmentIds });
        return result.Success ? File(result.Data!, "application/pdf") : BadRequest(result);
    }

    /// <summary>Download label-only PDF</summary>
    [HttpPost("shipments/{shipmentIds}/label-only/pdf")]
    public async Task<IActionResult> DownloadLabelsPdf(string shipmentIds, [FromQuery] bool reprint = false)
    {
        var result = await _service.DownloadLabelsPdfAsync(new DownloadLabelsRequest { ShipmentIds = shipmentIds, Reprint = reprint });
        return result.Success ? File(result.Data!, "application/pdf") : BadRequest(result);
    }

    /// <summary>Download label-only as byte stream</summary>
    [HttpPost("shipments/{shipmentIds}/label-only")]
    public async Task<IActionResult> DownloadLabelsBytes(string shipmentIds, [FromQuery] bool reprint = false)
    {
        var result = await _service.DownloadLabelsBytesAsync(new DownloadLabelsRequest { ShipmentIds = shipmentIds, Reprint = reprint });
        return result.Success ? File(result.Data!, "application/octet-stream") : BadRequest(result);
    }

    /// <summary>Get invoices for shipments</summary>
    [HttpGet("shipments/{shipmentIds}/invoices")]
    public async Task<IActionResult> GetInvoices(string shipmentIds)
    {
        var result = await _service.GetInvoicesAsync(new DownloadLabelsRequest { ShipmentIds = shipmentIds });
        return result.Success ? File(result.Data!, "application/pdf") : BadRequest(result);
    }

    /// <summary>Mark shipments as dispatched</summary>
    [HttpPost("shipments/dispatch")]
    public async Task<IActionResult> DispatchShipments([FromBody] DispatchShipmentsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.DispatchShipmentsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shipment details</summary>
    [HttpGet("shipments/{shipmentIds}")]
    public async Task<IActionResult> GetShipmentDetails(string shipmentIds)
    {
        var result = await _service.GetShipmentDetailsAsync(new GetShipmentsByIdsRequest { ShipmentIds = shipmentIds });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Cancel shipments</summary>
    [HttpPost("shipments/cancel")]
    public async Task<IActionResult> CancelShipments([FromBody] CancelShipmentsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CancelShipmentsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get handover counts for a location</summary>
    [HttpGet("shipments/handover/counts")]
    public async Task<IActionResult> GetHandoverCounts([FromQuery] string locationId)
    {
        var result = await _service.GetHandoverCountsAsync(new GetHandoverCountsRequest { LocationId = locationId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generate manifest</summary>
    [HttpPost("shipments/manifest")]
    public async Task<IActionResult> GenerateManifest([FromBody] GenerateManifestRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.GenerateManifestAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Mark self-ship shipment as dispatched</summary>
    [HttpPost("shipments/self-ship/dispatch")]
    public async Task<IActionResult> SelfShipDispatch([FromBody] SelfShipDispatchRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.SelfShipDispatchAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Confirm self-ship delivery</summary>
    [HttpPost("shipments/self-ship/delivery")]
    public async Task<IActionResult> SelfShipDelivery([FromBody] SelfShipDeliveryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.SelfShipDeliveryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get OTC shipment details</summary>
    [HttpGet("shipments/otc/{locationId}")]
    public async Task<IActionResult> GetOtcDetails(string locationId, [FromQuery] string? requestType)
    {
        var result = await _service.GetOtcDetailsAsync(new GetOtcDetailsRequest { LocationId = locationId, RequestType = requestType });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Record a service attempt</summary>
    [HttpPost("services/attempt")]
    public async Task<IActionResult> ServiceAttempt([FromBody] ServiceAttemptRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.ServiceAttemptAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Mark a service as complete</summary>
    [HttpPost("services/complete")]
    public async Task<IActionResult> ServiceComplete([FromBody] ServiceCompleteRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.ServiceCompleteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

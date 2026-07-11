using ApiToolKit.Application.DTOs.Flipkart.Order;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Flipkart;

public class FlipkartOrderService : IFlipkartOrderService
{
    private readonly IFlipkartApiClient _api;
    public FlipkartOrderService(IFlipkartApiClient api) => _api = api;

    public async Task<Result<ShipmentResponse>> FilterShipmentsAsync(FilterShipmentsRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/filter/", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GetShipmentsByIdsAsync(GetShipmentsByIdsRequest request)
    {
        var result = await _api.GetAsync<ShipmentResponse>("/v3/shipments", $"shipmentIds={Uri.EscapeDataString(request.ShipmentIds)}");
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GetShipmentsByOrderItemIdsAsync(GetShipmentsByOrderItemIdsRequest request)
    {
        var result = await _api.GetAsync<ShipmentResponse>("/v3/shipments", $"orderItemIds={Uri.EscapeDataString(request.OrderItemIds)}");
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GetShipmentsByOrderIdsAsync(GetShipmentsByOrderIdsRequest request)
    {
        var result = await _api.GetAsync<ShipmentResponse>("/v3/shipments", $"orderIds={Uri.EscapeDataString(request.OrderIds)}");
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GenerateLabelsAsync(GenerateLabelsRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/labels", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<byte[]>> DownloadLabelsAsync(DownloadLabelsRequest request)
    {
        var bytes = await _api.GetBytesAsync($"/v3/shipments/{Uri.EscapeDataString(request.ShipmentIds)}/labels");
        return bytes != null ? Result<byte[]>.Ok(bytes) : Result<byte[]>.Fail("No response.");
    }

    public async Task<Result<byte[]>> DownloadLabelsPdfAsync(DownloadLabelsRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>($"/v3/shipments/{Uri.EscapeDataString(request.ShipmentIds)}/labelOnly/pdf", new { reprint = request.Reprint });
        return Result<byte[]>.Ok(Array.Empty<byte>());
    }

    public async Task<Result<byte[]>> DownloadLabelsBytesAsync(DownloadLabelsRequest request)
    {
        var bytes = await _api.GetBytesAsync($"/v3/shipments/{Uri.EscapeDataString(request.ShipmentIds)}/labelOnly");
        return bytes != null ? Result<byte[]>.Ok(bytes) : Result<byte[]>.Fail("No response.");
    }

    public async Task<Result<byte[]>> GetInvoicesAsync(DownloadLabelsRequest request)
    {
        var bytes = await _api.GetBytesAsync($"/v3/shipments/{Uri.EscapeDataString(request.ShipmentIds)}/invoices");
        return bytes != null ? Result<byte[]>.Ok(bytes) : Result<byte[]>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> DispatchShipmentsAsync(DispatchShipmentsRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/dispatch", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GetShipmentDetailsAsync(GetShipmentsByIdsRequest request)
    {
        var result = await _api.GetAsync<ShipmentResponse>($"/v3/shipments/{Uri.EscapeDataString(request.ShipmentIds)}");
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> CancelShipmentsAsync(CancelShipmentsRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/cancel", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GetHandoverCountsAsync(GetHandoverCountsRequest request)
    {
        var result = await _api.GetAsync<ShipmentResponse>("/v3/shipments/handover/counts", $"locationId={Uri.EscapeDataString(request.LocationId)}");
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GenerateManifestAsync(GenerateManifestRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/manifest", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> SelfShipDispatchAsync(SelfShipDispatchRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/selfShip/dispatch", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> SelfShipDeliveryAsync(SelfShipDeliveryRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v3/shipments/selfShip/delivery", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> GetOtcDetailsAsync(GetOtcDetailsRequest request)
    {
        var result = await _api.GetAsync<ShipmentResponse>($"/v3/shipments/otc/{Uri.EscapeDataString(request.LocationId)}", request.RequestType != null ? $"requestType={request.RequestType}" : null);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> ServiceAttemptAsync(ServiceAttemptRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v2/services/attempt", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }

    public async Task<Result<ShipmentResponse>> ServiceCompleteAsync(ServiceCompleteRequest request)
    {
        var result = await _api.PostAsync<ShipmentResponse>("/v2/services/complete", request);
        return result != null ? Result<ShipmentResponse>.Ok(result) : Result<ShipmentResponse>.Fail("No response.");
    }
}

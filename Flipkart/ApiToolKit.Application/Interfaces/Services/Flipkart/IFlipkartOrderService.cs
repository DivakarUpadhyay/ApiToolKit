using ApiToolKit.Application.DTOs.Flipkart.Order;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Flipkart;

public interface IFlipkartOrderService
{
    Task<Result<ShipmentResponse>> FilterShipmentsAsync(FilterShipmentsRequest request);
    Task<Result<ShipmentResponse>> GetShipmentsByIdsAsync(GetShipmentsByIdsRequest request);
    Task<Result<ShipmentResponse>> GetShipmentsByOrderItemIdsAsync(GetShipmentsByOrderItemIdsRequest request);
    Task<Result<ShipmentResponse>> GetShipmentsByOrderIdsAsync(GetShipmentsByOrderIdsRequest request);
    Task<Result<ShipmentResponse>> GenerateLabelsAsync(GenerateLabelsRequest request);
    Task<Result<byte[]>> DownloadLabelsAsync(DownloadLabelsRequest request);
    Task<Result<byte[]>> DownloadLabelsPdfAsync(DownloadLabelsRequest request);
    Task<Result<byte[]>> DownloadLabelsBytesAsync(DownloadLabelsRequest request);
    Task<Result<byte[]>> GetInvoicesAsync(DownloadLabelsRequest request);
    Task<Result<ShipmentResponse>> DispatchShipmentsAsync(DispatchShipmentsRequest request);
    Task<Result<ShipmentResponse>> GetShipmentDetailsAsync(GetShipmentsByIdsRequest request);
    Task<Result<ShipmentResponse>> CancelShipmentsAsync(CancelShipmentsRequest request);
    Task<Result<ShipmentResponse>> GetHandoverCountsAsync(GetHandoverCountsRequest request);
    Task<Result<ShipmentResponse>> GenerateManifestAsync(GenerateManifestRequest request);
    Task<Result<ShipmentResponse>> SelfShipDispatchAsync(SelfShipDispatchRequest request);
    Task<Result<ShipmentResponse>> SelfShipDeliveryAsync(SelfShipDeliveryRequest request);
    Task<Result<ShipmentResponse>> GetOtcDetailsAsync(GetOtcDetailsRequest request);
    Task<Result<ShipmentResponse>> ServiceAttemptAsync(ServiceAttemptRequest request);
    Task<Result<ShipmentResponse>> ServiceCompleteAsync(ServiceCompleteRequest request);
}

using ApiToolKit.Application.DTOs.Shopee.Logistics;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeLogisticsService
{
    Task<Result<LogisticsInfoResponse>> GetLogisticInfoAsync(GetLogisticInfoRequest request);
    Task<Result<bool>> InitLogisticsAsync(InitLogisticsRequest request);
    Task<Result<bool>> ShipOrderAsync(ShipOrderRequest request);
    Task<Result<TrackingNumberResponse>> GetTrackingNumberAsync(GetTrackingNumberRequest request);
    Task<Result<object>> GetOrderLogisticsAsync(long shopId, string orderSn);
    Task<Result<bool>> UpdateLogisticsOrderAsync(UpdateLogisticsRequest request);
    Task<Result<AddressListResponse>> GetAddressListAsync(GetAddressListRequest request);
}

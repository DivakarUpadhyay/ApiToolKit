using ApiToolKit.Application.DTOs.Shopee.FirstMile;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeFirstMileService
{
    Task<Result<GenerateFirstMileTrackingNumberResponse>> GenerateFirstMileTrackingNumberAsync(GenerateFirstMileTrackingNumberRequest request);
    Task<Result<GetFirstMileTrackingNumberResponse>> GetFirstMileTrackingNumberAsync(GetFirstMileTrackingNumberRequest request);
    Task<Result<UploadFirstMileTrackingNumberResponse>> UploadFirstMileTrackingNumberAsync(UploadFirstMileTrackingNumberRequest request);
    Task<Result<UnbindFirstMileTrackingNumberResponse>> UnbindFirstMileTrackingNumberAsync(UnbindFirstMileTrackingNumberRequest request);
    Task<Result<GetUnbindOrderListResponse>> GetUnbindOrderListAsync(GetUnbindOrderListRequest request);
    Task<Result<CreateChannelResponse>> CreateChannelAsync(CreateChannelRequest request);
    Task<Result<GetFirstMileChannelListResponse>> GetFirstMileChannelListAsync(GetFirstMileChannelListRequest request);
    Task<Result<GetFirstMileTrackingListResponse>> GetFirstMileTrackingListAsync(GetFirstMileTrackingListRequest request);
}

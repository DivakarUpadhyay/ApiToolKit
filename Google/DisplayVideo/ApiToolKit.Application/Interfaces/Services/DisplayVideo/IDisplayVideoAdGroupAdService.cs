using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupAd;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoAdGroupAdService
{
    Task<Result<object>> DeleteAdGroupAdAsync(DeleteAdGroupAdRequest request);
    Task<Result<object>> UpdateAdGroupAdAsync(UpdateAdGroupAdRequest request);
    Task<Result<object>> GetAdGroupAdAsync(GetAdGroupAdRequest request);
    Task<Result<object>> ListAdGroupAdsAsync(ListAdGroupAdsRequest request);
    Task<Result<object>> CreateAdGroupAdAsync(CreateAdGroupAdRequest request);
}

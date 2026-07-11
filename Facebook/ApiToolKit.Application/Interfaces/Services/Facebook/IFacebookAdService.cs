using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookAdService
{
    Task<Result<CreateAdResponse>> CreateAdAsync(CreateAdRequest request, string accessToken);
    Task<Result<AdResponse>> GetAdAsync(string adId, string accessToken);
    Task<Result<GetAdsResponse>> GetAdsAsync(GetAdsRequest request, string accessToken);
    Task<Result<bool>> UpdateAdAsync(UpdateAdRequest request, string accessToken);
    Task<Result<bool>> DeleteAdAsync(string adId, string accessToken);
    Task<Result<AdCreativeResponse>> CreateAdCreativeAsync(AdCreativeRequest request, string accessToken);
    Task<Result<object>> GetAdPreviewAsync(string adId, string adFormat, string accessToken);
    Task<Result<object>> GetAdCreativesAsync(string accountId, string accessToken, int limit);
}

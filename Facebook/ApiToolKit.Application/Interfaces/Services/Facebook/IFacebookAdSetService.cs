using ApiToolKit.Application.DTOs.Facebook.AdSet;
using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookAdSetService
{
    Task<Result<CreateAdSetResponse>> CreateAdSetAsync(CreateAdSetRequest request, string accessToken);
    Task<Result<AdSetResponse>> GetAdSetAsync(string adSetId, string accessToken);
    Task<Result<GetAdSetsResponse>> GetAdSetsAsync(GetAdSetsRequest request, string accessToken);
    Task<Result<bool>> UpdateAdSetAsync(UpdateAdSetRequest request, string accessToken);
    Task<Result<bool>> DeleteAdSetAsync(string adSetId, string accessToken);
    Task<Result<GetAdsResponse>> GetAdSetAdsAsync(string adSetId, string accessToken, int limit);
}

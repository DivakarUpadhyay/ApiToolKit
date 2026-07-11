using ApiToolKit.Application.DTOs.GoogleAds.Audience;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsAudienceService
{
    Task<Result<List<AudienceResponse>>> ListAudiencesAsync(ListAudiencesRequest request);
    Task<Result<MutateOperationResponse>> CreateAudienceAsync(CreateAudienceRequest request);
}

using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.DTOs.GoogleAds.Keyword;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsKeywordService
{
    Task<Result<KeywordListResponse>> ListKeywordsAsync(ListKeywordsRequest request);
    Task<Result<MutateOperationResponse>> CreateKeywordAsync(CreateKeywordRequest request);
    Task<Result<MutateOperationResponse>> UpdateKeywordAsync(UpdateKeywordRequest request);
    Task<Result<bool>> RemoveKeywordAsync(string customerId, string resourceName);
}

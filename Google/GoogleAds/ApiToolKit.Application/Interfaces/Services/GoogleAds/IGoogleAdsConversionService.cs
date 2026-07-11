using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.DTOs.GoogleAds.Conversion;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsConversionService
{
    Task<Result<List<ConversionActionResponse>>> ListConversionActionsAsync(ListConversionActionsRequest request);
    Task<Result<MutateOperationResponse>> CreateConversionActionAsync(CreateConversionActionRequest request);
}

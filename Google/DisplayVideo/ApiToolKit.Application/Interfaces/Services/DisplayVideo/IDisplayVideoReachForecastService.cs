using ApiToolKit.Application.DTOs.DisplayVideo.ReachForecast;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoReachForecastService
{
    Task<Result<object>> RetrievePlannableProductsReachForecastAsync(RetrievePlannableProductsReachForecastRequest request);
    Task<Result<object>> GenerateReachForecastAsync(GenerateReachForecastRequest request);
    Task<Result<object>> RetrievePlannableUserListsReachForecastAsync(RetrievePlannableUserListsReachForecastRequest request);
    Task<Result<object>> RetrievePlannableUserInterestsReachForecastAsync(RetrievePlannableUserInterestsReachForecastRequest request);
    Task<Result<object>> RetrievePlannableLocationsReachForecastAsync(RetrievePlannableLocationsReachForecastRequest request);
}

using ApiToolKit.Application.DTOs.DisplayVideo.TargetingTypeTargetingOption;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoTargetingTypeTargetingOptionService
{
    Task<Result<object>> GetTargetingOptionAsync(GetTargetingOptionRequest request);
    Task<Result<object>> SearchTargetingOptionAsync(SearchTargetingOptionRequest request);
    Task<Result<object>> ListTargetingOptionsAsync(ListTargetingOptionsRequest request);
}

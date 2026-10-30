using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupTargetingTypeAssignedTargetingOption;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoAdGroupTargetingTypeAssignedTargetingOptionService
{
    Task<Result<object>> ListAssignedTargetingOptionsAsync(ListAssignedTargetingOptionsRequest request);
    Task<Result<object>> GetAssignedTargetingOptionAsync(GetAssignedTargetingOptionRequest request);
    Task<Result<object>> CreateAssignedTargetingOptionAsync(CreateAssignedTargetingOptionRequest request);
    Task<Result<object>> DeleteAssignedTargetingOptionAsync(DeleteAssignedTargetingOptionRequest request);
}

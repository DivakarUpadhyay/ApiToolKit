using ApiToolKit.Application.DTOs.DisplayVideo.TargetingTypeAssignedTargetingOption;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoTargetingTypeAssignedTargetingOptionService
{
    Task<Result<object>> ListAssignedTargetingOptionsAsync(ListAssignedTargetingOptionsRequest request);
    Task<Result<object>> CreateAssignedTargetingOptionAsync(CreateAssignedTargetingOptionRequest request);
    Task<Result<object>> DeleteAssignedTargetingOptionAsync(DeleteAssignedTargetingOptionRequest request);
    Task<Result<object>> GetAssignedTargetingOptionAsync(GetAssignedTargetingOptionRequest request);
}

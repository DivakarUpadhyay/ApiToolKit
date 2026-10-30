using ApiToolKit.Application.DTOs.DisplayVideo.PartnerTargetingTypeAssignedTargetingOption;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoPartnerTargetingTypeAssignedTargetingOptionService
{
    Task<Result<object>> CreateAssignedTargetingOptionAsync(CreateAssignedTargetingOptionRequest request);
    Task<Result<object>> DeleteAssignedTargetingOptionAsync(DeleteAssignedTargetingOptionRequest request);
    Task<Result<object>> GetAssignedTargetingOptionAsync(GetAssignedTargetingOptionRequest request);
    Task<Result<object>> ListAssignedTargetingOptionsAsync(ListAssignedTargetingOptionsRequest request);
}

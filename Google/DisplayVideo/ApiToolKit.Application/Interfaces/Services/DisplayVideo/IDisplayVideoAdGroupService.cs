using ApiToolKit.Application.DTOs.DisplayVideo.AdGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoAdGroupService
{
    Task<Result<object>> BulkEditAssignedTargetingOptionsAdGroupAsync(BulkEditAssignedTargetingOptionsAdGroupRequest request);
    Task<Result<object>> BulkListAssignedTargetingOptionsAdGroupAsync(BulkListAssignedTargetingOptionsAdGroupRequest request);
    Task<Result<object>> ListAdGroupsAsync(ListAdGroupsRequest request);
    Task<Result<object>> DeleteAdGroupAsync(DeleteAdGroupRequest request);
    Task<Result<object>> GetAdGroupAsync(GetAdGroupRequest request);
    Task<Result<object>> CreateAdGroupAsync(CreateAdGroupRequest request);
    Task<Result<object>> UpdateAdGroupAsync(UpdateAdGroupRequest request);
}

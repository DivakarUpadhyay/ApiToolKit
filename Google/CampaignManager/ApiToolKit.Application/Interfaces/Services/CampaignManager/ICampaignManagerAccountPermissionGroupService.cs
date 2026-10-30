using ApiToolKit.Application.DTOs.CampaignManager.AccountPermissionGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAccountPermissionGroupService
{
    Task<Result<object>> GetAccountPermissionGroupAsync(GetAccountPermissionGroupRequest request);
    Task<Result<object>> ListAccountPermissionGroupsAsync(ListAccountPermissionGroupsRequest request);
}

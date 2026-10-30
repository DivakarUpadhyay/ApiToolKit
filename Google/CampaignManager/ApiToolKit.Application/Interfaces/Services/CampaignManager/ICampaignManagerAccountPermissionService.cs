using ApiToolKit.Application.DTOs.CampaignManager.AccountPermission;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAccountPermissionService
{
    Task<Result<object>> GetAccountPermissionAsync(GetAccountPermissionRequest request);
    Task<Result<object>> ListAccountPermissionsAsync(ListAccountPermissionsRequest request);
}

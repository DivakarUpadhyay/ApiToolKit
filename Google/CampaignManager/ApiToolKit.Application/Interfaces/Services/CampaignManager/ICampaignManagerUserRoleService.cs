using ApiToolKit.Application.DTOs.CampaignManager.UserRole;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerUserRoleService
{
    Task<Result<object>> ListUserRolesAsync(ListUserRolesRequest request);
    Task<Result<object>> InsertUserRoleAsync(InsertUserRoleRequest request);
    Task<Result<object>> UpdateUserRoleAsync(UpdateUserRoleRequest request);
    Task<Result<object>> GetUserRoleAsync(GetUserRoleRequest request);
    Task<Result<object>> DeleteUserRoleAsync(DeleteUserRoleRequest request);
    Task<Result<object>> UpdateUserRoleAltAsync(UpdateUserRoleAltRequest request);
}

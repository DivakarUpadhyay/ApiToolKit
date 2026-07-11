using ApiToolKit.Application.DTOs.Identity.Toolkit;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Identity;

public interface IAdminUserService
{
    Task<Result<ListUsersResponse>> ListUsersAsync(ListUsersRequest request, string accessToken);
    Task<Result<UserInfo>> GetUserAsync(AdminGetUserRequest request, string accessToken);
    Task<Result<UserInfo>> CreateUserAsync(AdminCreateUserRequest request, string accessToken);
    Task<Result<UserInfo>> UpdateUserAsync(AdminUpdateUserRequest request, string accessToken);
    Task<Result<bool>> DeleteUserAsync(AdminDeleteUserRequest request, string accessToken);
}

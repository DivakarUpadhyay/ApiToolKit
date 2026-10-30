using ApiToolKit.Application.DTOs.DisplayVideo.User;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoUserService
{
    Task<Result<object>> CreateUserAsync(CreateUserRequest request);
    Task<Result<object>> GetUserAsync(GetUserRequest request);
    Task<Result<object>> UpdateUserAsync(UpdateUserRequest request);
    Task<Result<object>> DeleteUserAsync(DeleteUserRequest request);
    Task<Result<object>> BulkEditAssignedUserRolesAsync(BulkEditAssignedUserRolesRequest request);
    Task<Result<object>> ListUsersAsync(ListUsersRequest request);
}

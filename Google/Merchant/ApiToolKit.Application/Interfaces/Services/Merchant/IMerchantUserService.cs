using ApiToolKit.Application.DTOs.Merchant.User;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantUserService
{
    Task<Result<object>> ListUsersAsync(ListUsersRequest request);
    Task<Result<object>> GetUserAsync(GetUserRequest request);
    Task<Result<object>> DeleteUserAsync(DeleteUserRequest request);
    Task<Result<object>> CreateUserAsync(CreateUserRequest request);
    Task<Result<object>> UpdateUserAsync(UpdateUserRequest request);
}

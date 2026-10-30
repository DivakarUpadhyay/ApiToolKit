using ApiToolKit.Application.DTOs.Merchant.User;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantUserService : MerchantServiceBase, IMerchantUserService
{
    public MerchantUserService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListUsersAsync(ListUsersRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListUsers", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetUserAsync(GetUserRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users/{Esc(r.UserId)}";
        return await SendAsync(r.AccountKey, "GetUser", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DeleteUserAsync(DeleteUserRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users/{Esc(r.UserId)}";
        return await SendAsync(r.AccountKey, "DeleteUser", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> CreateUserAsync(CreateUserRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "userId", r.UserId);
        return await SendAsync(r.AccountKey, "CreateUser", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> UpdateUserAsync(UpdateUserRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users/{Esc(r.UserId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateUser", HttpMethod.Patch, url, query, r.Body);
    }
}

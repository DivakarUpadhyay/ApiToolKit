using ApiToolKit.Application.DTOs.TagManager.Account;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerAccountService
{
    Task<Result<object>> ListAccountsAsync(ListAccountsRequest request);
    Task<Result<object>> GetAccountAsync(GetAccountRequest request);
    Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest request);
}

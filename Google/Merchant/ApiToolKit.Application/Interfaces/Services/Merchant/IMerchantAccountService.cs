using ApiToolKit.Application.DTOs.Merchant.Account;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantAccountService
{
    Task<Result<object>> CreateTestAccountAsync(CreateTestAccountRequest request);
    Task<Result<object>> CreateAndConfigureAccountAsync(CreateAndConfigureAccountRequest request);
    Task<Result<object>> DeleteAccountAsync(DeleteAccountRequest request);
    Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest request);
    Task<Result<object>> GetAccountAsync(GetAccountRequest request);
    Task<Result<object>> ListAccountsAsync(ListAccountsRequest request);
    Task<Result<object>> ListSubaccountsAsync(ListSubaccountsRequest request);
}

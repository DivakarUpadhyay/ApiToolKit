using ApiToolKit.Application.DTOs.Merchant.GbpAccount;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantGbpAccountService
{
    Task<Result<object>> ListGbpAccountsAsync(ListGbpAccountsRequest request);
    Task<Result<object>> LinkGbpAccountAsync(LinkGbpAccountRequest request);
}

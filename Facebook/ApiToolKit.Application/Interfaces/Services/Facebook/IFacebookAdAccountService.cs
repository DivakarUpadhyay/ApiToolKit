using ApiToolKit.Application.DTOs.Facebook.AdAccount;
using ApiToolKit.Application.DTOs.Facebook.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookAdAccountService
{
    Task<Result<AdAccountResponse>> GetAdAccountAsync(string accountId, string accessToken);
    Task<Result<List<AdAccountResponse>>> GetAdAccountsByUserAsync(GetAdAccountsRequest request, string accessToken);
    Task<Result<AdAccountBillingResponse>> GetAdAccountBillingAsync(string accountId, string accessToken);
    Task<Result<List<AdAccountUserResponse>>> GetAdAccountUsersAsync(AdAccountUsersRequest request, string accessToken);
    Task<Result<GetCampaignsResponse>> GetAdAccountCampaignsAsync(GetCampaignsRequest request, string accessToken);
    Task<Result<object>> GetAdAccountCreativesAsync(string accountId, string accessToken, int limit);
}

using ApiToolKit.Application.DTOs.CampaignManager.Account;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAccountService
{
    Task<Result<object>> GetAccountAsync(GetAccountRequest request);
    Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest request);
    Task<Result<object>> PatchAccountAsync(PatchAccountRequest request);
    Task<Result<object>> ListAccountsAsync(ListAccountsRequest request);
}

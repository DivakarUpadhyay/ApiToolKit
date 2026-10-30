using ApiToolKit.Application.DTOs.Merchant.AutomaticImprovement;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantAutomaticImprovementService : MerchantServiceBase, IMerchantAutomaticImprovementService
{
    public MerchantAutomaticImprovementService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAutomaticImprovementsAsync(GetAutomaticImprovementsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/automaticImprovements";
        return await SendAsync(r.AccountKey, "GetAutomaticImprovements", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAutomaticImprovementsAsync(UpdateAutomaticImprovementsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/automaticImprovements";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAutomaticImprovements", HttpMethod.Patch, url, query, r.Body);
    }
}

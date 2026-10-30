using ApiToolKit.Application.DTOs.Merchant.IssueresolutionIssueresolution;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantIssueresolutionIssueresolutionService : MerchantServiceBase, IMerchantIssueresolutionIssueresolutionService
{
    public MerchantIssueresolutionIssueresolutionService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> TriggeractionIssueresolutionAsync(TriggeractionIssueresolutionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/issueresolution/v1/accounts/{Esc(r.AccountId)}:triggeraction";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "languageCode", r.LanguageCode);
        return await SendAsync(r.AccountKey, "TriggeractionIssueresolution", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> RenderaccountissuesIssueresolutionAsync(RenderaccountissuesIssueresolutionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/issueresolution/v1/accounts/{Esc(r.AccountId)}:renderaccountissues";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "timeZone", r.TimeZone);
        Add(query, "languageCode", r.LanguageCode);
        return await SendAsync(r.AccountKey, "RenderaccountissuesIssueresolution", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> RenderproductissuesIssueresolutionAsync(RenderproductissuesIssueresolutionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/issueresolution/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}:renderproductissues";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "timeZone", r.TimeZone);
        Add(query, "languageCode", r.LanguageCode);
        return await SendAsync(r.AccountKey, "RenderproductissuesIssueresolution", HttpMethod.Post, url, query, r.Body);
    }
}

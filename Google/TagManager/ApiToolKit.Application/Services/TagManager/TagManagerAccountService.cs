using ApiToolKit.Application.DTOs.TagManager.Account;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerAccountService : TagManagerServiceBase, ITagManagerAccountService
{
    public TagManagerAccountService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAccountsAsync(ListAccountsRequest r)
    {
        var url = "https://tagmanager.googleapis.com/tagmanager/v2/accounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAccounts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetAccountAsync(GetAccountRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}";
        return await SendAsync(r.AccountKey, "GetAccount", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateAccount", HttpMethod.Put, url, query, new { name = r.Name, shareData = r.ShareData });
    }
}

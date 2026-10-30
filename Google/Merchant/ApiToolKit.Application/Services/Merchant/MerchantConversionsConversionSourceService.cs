using ApiToolKit.Application.DTOs.Merchant.ConversionsConversionSource;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantConversionsConversionSourceService : MerchantServiceBase, IMerchantConversionsConversionSourceService
{
    public MerchantConversionsConversionSourceService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateConversionSourceAsync(CreateConversionSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/conversions/v1/accounts/{Esc(r.AccountId)}/conversionSources";
        return await SendAsync(r.AccountKey, "CreateConversionSource", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateConversionSourceAsync(UpdateConversionSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/conversions/v1/accounts/{Esc(r.AccountId)}/conversionSources/{Esc(r.ConversionSourceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateConversionSource", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteConversionSourceAsync(DeleteConversionSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/conversions/v1/accounts/{Esc(r.AccountId)}/conversionSources/{Esc(r.ConversionSourceId)}";
        return await SendAsync(r.AccountKey, "DeleteConversionSource", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> UndeleteConversionSourceAsync(UndeleteConversionSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/conversions/v1/accounts/{Esc(r.AccountId)}/conversionSources/{Esc(r.ConversionSourceId)}:undelete";
        return await SendAsync(r.AccountKey, "UndeleteConversionSource", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetConversionSourceAsync(GetConversionSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/conversions/v1/accounts/{Esc(r.AccountId)}/conversionSources/{Esc(r.ConversionSourceId)}";
        return await SendAsync(r.AccountKey, "GetConversionSource", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListConversionSourcesAsync(ListConversionSourcesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/conversions/v1/accounts/{Esc(r.AccountId)}/conversionSources";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "showDeleted", r.ShowDeleted?.ToString().ToLower());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListConversionSources", HttpMethod.Get, url, query);
    }
}

using ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSource;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantDatasourcesDataSourceService : MerchantServiceBase, IMerchantDatasourcesDataSourceService
{
    public MerchantDatasourcesDataSourceService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> FetchDataSourceAsync(FetchDataSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources/{Esc(r.DataSourceId)}:fetch";
        return await SendAsync(r.AccountKey, "FetchDataSource", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteDataSourceAsync(DeleteDataSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources/{Esc(r.DataSourceId)}";
        return await SendAsync(r.AccountKey, "DeleteDataSource", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> CreateDataSourceAsync(CreateDataSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources";
        return await SendAsync(r.AccountKey, "CreateDataSource", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListDataSourcesAsync(ListDataSourcesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListDataSources", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateDataSourceAsync(UpdateDataSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources/{Esc(r.DataSourceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateDataSource", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetDataSourceAsync(GetDataSourceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources/{Esc(r.DataSourceId)}";
        return await SendAsync(r.AccountKey, "GetDataSource", HttpMethod.Get, url);
    }
}

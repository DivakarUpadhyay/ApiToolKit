using ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSourceFileUpload;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantDatasourcesDataSourceFileUploadService : MerchantServiceBase, IMerchantDatasourcesDataSourceFileUploadService
{
    public MerchantDatasourcesDataSourceFileUploadService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetFileUploadAsync(GetFileUploadRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/datasources/v1/accounts/{Esc(r.AccountId)}/dataSources/{Esc(r.DataSourceId)}/fileUploads/{Esc(r.FileUploadId)}";
        return await SendAsync(r.AccountKey, "GetFileUpload", HttpMethod.Get, url);
    }
}

using ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSource;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantDatasourcesDataSourceService
{
    Task<Result<object>> FetchDataSourceAsync(FetchDataSourceRequest request);
    Task<Result<object>> DeleteDataSourceAsync(DeleteDataSourceRequest request);
    Task<Result<object>> CreateDataSourceAsync(CreateDataSourceRequest request);
    Task<Result<object>> ListDataSourcesAsync(ListDataSourcesRequest request);
    Task<Result<object>> UpdateDataSourceAsync(UpdateDataSourceRequest request);
    Task<Result<object>> GetDataSourceAsync(GetDataSourceRequest request);
}

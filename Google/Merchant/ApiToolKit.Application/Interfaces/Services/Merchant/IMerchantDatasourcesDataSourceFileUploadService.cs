using ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSourceFileUpload;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantDatasourcesDataSourceFileUploadService
{
    Task<Result<object>> GetFileUploadAsync(GetFileUploadRequest request);
}

using ApiToolKit.Application.DTOs.Merchant.ConversionsConversionSource;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantConversionsConversionSourceService
{
    Task<Result<object>> CreateConversionSourceAsync(CreateConversionSourceRequest request);
    Task<Result<object>> UpdateConversionSourceAsync(UpdateConversionSourceRequest request);
    Task<Result<object>> DeleteConversionSourceAsync(DeleteConversionSourceRequest request);
    Task<Result<object>> UndeleteConversionSourceAsync(UndeleteConversionSourceRequest request);
    Task<Result<object>> GetConversionSourceAsync(GetConversionSourceRequest request);
    Task<Result<object>> ListConversionSourcesAsync(ListConversionSourcesRequest request);
}

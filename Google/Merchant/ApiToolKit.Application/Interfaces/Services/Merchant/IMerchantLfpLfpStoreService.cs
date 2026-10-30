using ApiToolKit.Application.DTOs.Merchant.LfpLfpStore;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantLfpLfpStoreService
{
    Task<Result<object>> GetLfpStoreAsync(GetLfpStoreRequest request);
    Task<Result<object>> DeleteLfpStoreAsync(DeleteLfpStoreRequest request);
    Task<Result<object>> InsertLfpStoreAsync(InsertLfpStoreRequest request);
    Task<Result<object>> ListLfpStoresAsync(ListLfpStoresRequest request);
}

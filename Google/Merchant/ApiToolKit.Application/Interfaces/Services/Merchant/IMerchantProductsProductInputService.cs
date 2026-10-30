using ApiToolKit.Application.DTOs.Merchant.ProductsProductInput;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantProductsProductInputService
{
    Task<Result<object>> InsertProductInputAsync(InsertProductInputRequest request);
    Task<Result<object>> DeleteProductInputAsync(DeleteProductInputRequest request);
    Task<Result<object>> UpdateProductInputAsync(UpdateProductInputRequest request);
}

using ApiToolKit.Application.DTOs.Merchant.ProductsProduct;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantProductsProductService
{
    Task<Result<object>> ListProductsAsync(ListProductsRequest request);
    Task<Result<object>> GetProductAsync(GetProductRequest request);
}

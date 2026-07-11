using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyCartService
{
    Task<Result<CartResponse>> CreateCartAsync(string shopDomain, CreateCartRequest request);
    Task<Result<CartResponse>> GetCartAsync(string shopDomain, string cartId);
    Task<Result<CartResponse>> AddCartLinesAsync(string shopDomain, string cartId, AddCartLinesRequest request);
    Task<Result<CartResponse>> UpdateCartLinesAsync(string shopDomain, string cartId, UpdateCartLinesRequest request);
    Task<Result<CartResponse>> RemoveCartLinesAsync(string shopDomain, string cartId, RemoveCartLinesRequest request);
    Task<Result<CartResponse>> UpdateCartBuyerIdentityAsync(string shopDomain, string cartId, CartBuyerIdentityRequest request);
}

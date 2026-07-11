using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyStorefrontCustomerService
{
    Task<Result<CustomerAccessTokenResponse>> CustomerLoginAsync(string shopDomain, string email, string password);
    Task<Result<CreateCustomerResponse>> CustomerRegisterAsync(string shopDomain, RegisterCustomerRequest request);
    Task<Result<StorefrontCustomerResponse>> GetCustomerAsync(string shopDomain, string customerAccessToken);
    Task<Result<bool>> CustomerLogoutAsync(string shopDomain, string customerAccessToken);
}

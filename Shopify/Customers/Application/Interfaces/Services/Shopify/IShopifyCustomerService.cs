using ApiToolKit.Application.DTOs.Shopify.Customers;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyCustomerService
{
    Task<Result<ListCustomersResponse>> ListCustomersAsync(string shopDomain, int? limit = null, string? email = null, string? sinceId = null);
    Task<Result<CustomerResponse>> GetCustomerAsync(string shopDomain, string customerId);
    Task<Result<int>> CountCustomersAsync(string shopDomain);
    Task<Result<ListCustomersResponse>> SearchCustomersAsync(string shopDomain, string query, int? limit = null);
    Task<Result<ListOrdersResponse>> GetCustomerOrdersAsync(string shopDomain, string customerId);
    Task<Result<CustomerResponse>> CreateCustomerAsync(string shopDomain, CreateCustomerRequest request);
    Task<Result<CustomerResponse>> UpdateCustomerAsync(string shopDomain, string customerId, UpdateCustomerRequest request);
    Task<Result<bool>> DeleteCustomerAsync(string shopDomain, string customerId);
    Task<Result<AccountActivationUrlResponse>> GenerateAccountActivationUrlAsync(string shopDomain, string customerId);
    Task<Result<bool>> SendInviteAsync(string shopDomain, string customerId, SendInviteRequest request);
}

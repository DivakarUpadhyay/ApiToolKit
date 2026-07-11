using ApiToolKit.Application.DTOs.Shopify.Customers;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyAddressService
{
    Task<Result<ListAddressesResponse>> ListAddressesAsync(string shopDomain, string customerId);
    Task<Result<AddressResponse>> GetAddressAsync(string shopDomain, string customerId, string addressId);
    Task<Result<AddressResponse>> CreateAddressAsync(string shopDomain, string customerId, CreateAddressRequest request);
    Task<Result<AddressResponse>> UpdateAddressAsync(string shopDomain, string customerId, string addressId, UpdateAddressRequest request);
    Task<Result<AddressResponse>> SetDefaultAddressAsync(string shopDomain, string customerId, string addressId);
    Task<Result<bool>> DeleteAddressAsync(string shopDomain, string customerId, string addressId);
}

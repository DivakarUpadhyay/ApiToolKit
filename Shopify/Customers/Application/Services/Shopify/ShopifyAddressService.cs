using ApiToolKit.Application.DTOs.Shopify.Customers;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyAddressService : IShopifyAddressService
{
    private readonly IShopifyApiClient _apiClient;
    private readonly IShopifyAuthRepository _authRepo;

    public ShopifyAddressService(IShopifyApiClient apiClient, IShopifyAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public async Task<Result<ListAddressesResponse>> ListAddressesAsync(string shopDomain, string customerId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.GetAsync<ListAddressesResponse>(shopDomain, $"customers/{customerId}/addresses.json", null, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "ListAddresses",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return Result<ListAddressesResponse>.Ok(response ?? new ListAddressesResponse());
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "ListAddresses", IsSuccess = false, ResponseBody = ex.Message });
            return Result<ListAddressesResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddressResponse>> GetAddressAsync(string shopDomain, string customerId, string addressId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.GetAsync<AddressResponse>(shopDomain, $"customers/{customerId}/addresses/{addressId}.json", null, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "GetAddress",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return response?.CustomerAddress != null
                ? Result<AddressResponse>.Ok(response)
                : Result<AddressResponse>.Fail("Address not found.", 404);
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "GetAddress", IsSuccess = false, ResponseBody = ex.Message });
            return Result<AddressResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddressResponse>> CreateAddressAsync(string shopDomain, string customerId, CreateAddressRequest request)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var body = new { customer_address = request };
            var response = await _apiClient.PostAsync<AddressResponse>(shopDomain, $"customers/{customerId}/addresses.json", body, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "CreateAddress",
                RequestBody = JsonConvert.SerializeObject(body),
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 201
            });
            return response?.CustomerAddress != null
                ? Result<AddressResponse>.Ok(response)
                : Result<AddressResponse>.Fail("Failed to create address.");
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "CreateAddress", IsSuccess = false, ResponseBody = ex.Message });
            return Result<AddressResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddressResponse>> UpdateAddressAsync(string shopDomain, string customerId, string addressId, UpdateAddressRequest request)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var body = new { customer_address = request };
            var response = await _apiClient.PutAsync<AddressResponse>(shopDomain, $"customers/{customerId}/addresses/{addressId}.json", body, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "UpdateAddress",
                RequestBody = JsonConvert.SerializeObject(body),
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return response?.CustomerAddress != null
                ? Result<AddressResponse>.Ok(response)
                : Result<AddressResponse>.Fail("Failed to update address.");
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "UpdateAddress", IsSuccess = false, ResponseBody = ex.Message });
            return Result<AddressResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddressResponse>> SetDefaultAddressAsync(string shopDomain, string customerId, string addressId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.PutAsync<AddressResponse>(shopDomain, $"customers/{customerId}/addresses/{addressId}/default.json", null, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "SetDefaultAddress",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return response?.CustomerAddress != null
                ? Result<AddressResponse>.Ok(response)
                : Result<AddressResponse>.Fail("Failed to set default address.");
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "SetDefaultAddress", IsSuccess = false, ResponseBody = ex.Message });
            return Result<AddressResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteAddressAsync(string shopDomain, string customerId, string addressId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            await _apiClient.DeleteAsync<object>(shopDomain, $"customers/{customerId}/addresses/{addressId}.json", token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "DeleteAddress", IsSuccess = true, StatusCode = 200 });
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "DeleteAddress", IsSuccess = false, ResponseBody = ex.Message });
            return Result<bool>.Fail(ex.Message);
        }
    }
}

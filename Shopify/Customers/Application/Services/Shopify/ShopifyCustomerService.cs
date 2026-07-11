using ApiToolKit.Application.DTOs.Shopify.Customers;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyCustomerService : IShopifyCustomerService
{
    private readonly IShopifyApiClient _apiClient;
    private readonly IShopifyAuthRepository _authRepo;

    public ShopifyCustomerService(IShopifyApiClient apiClient, IShopifyAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public async Task<Result<ListCustomersResponse>> ListCustomersAsync(string shopDomain, int? limit = null, string? email = null, string? sinceId = null)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var queryParams = new Dictionary<string, string>();
            if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();
            if (!string.IsNullOrEmpty(email)) queryParams["email"] = email;
            if (!string.IsNullOrEmpty(sinceId)) queryParams["since_id"] = sinceId;

            var response = await _apiClient.GetAsync<ListCustomersResponse>(shopDomain, "customers.json", queryParams, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "ListCustomers",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return Result<ListCustomersResponse>.Ok(response ?? new ListCustomersResponse());
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "ListCustomers", IsSuccess = false, ResponseBody = ex.Message });
            return Result<ListCustomersResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CustomerResponse>> GetCustomerAsync(string shopDomain, string customerId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.GetAsync<CustomerResponse>(shopDomain, $"customers/{customerId}.json", null, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "GetCustomer",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return response?.Customer != null
                ? Result<CustomerResponse>.Ok(response)
                : Result<CustomerResponse>.Fail("Customer not found.", 404);
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "GetCustomer", IsSuccess = false, ResponseBody = ex.Message });
            return Result<CustomerResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<int>> CountCustomersAsync(string shopDomain)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.GetAsync<Dictionary<string, int>>(shopDomain, "customers/count.json", null, token?.AccessToken);
            int count = 0;
            if (response != null && response.TryGetValue("count", out int c))
                count = c;
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "CountCustomers", IsSuccess = true, StatusCode = 200, ResponseBody = count.ToString() });
            return Result<int>.Ok(count);
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "CountCustomers", IsSuccess = false, ResponseBody = ex.Message });
            return Result<int>.Fail(ex.Message);
        }
    }

    public async Task<Result<ListCustomersResponse>> SearchCustomersAsync(string shopDomain, string query, int? limit = null)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var queryParams = new Dictionary<string, string> { ["query"] = query };
            if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

            var response = await _apiClient.GetAsync<ListCustomersResponse>(shopDomain, "customers/search.json", queryParams, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "SearchCustomers",
                RequestBody = query,
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return Result<ListCustomersResponse>.Ok(response ?? new ListCustomersResponse());
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "SearchCustomers", IsSuccess = false, ResponseBody = ex.Message });
            return Result<ListCustomersResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ListOrdersResponse>> GetCustomerOrdersAsync(string shopDomain, string customerId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.GetAsync<ListOrdersResponse>(shopDomain, $"customers/{customerId}/orders.json", null, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "GetCustomerOrders",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return Result<ListOrdersResponse>.Ok(response ?? new ListOrdersResponse());
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "GetCustomerOrders", IsSuccess = false, ResponseBody = ex.Message });
            return Result<ListOrdersResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CustomerResponse>> CreateCustomerAsync(string shopDomain, CreateCustomerRequest request)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var body = new { customer = request };
            var response = await _apiClient.PostAsync<CustomerResponse>(shopDomain, "customers.json", body, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "CreateCustomer",
                RequestBody = JsonConvert.SerializeObject(body),
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 201
            });
            return response?.Customer != null
                ? Result<CustomerResponse>.Ok(response)
                : Result<CustomerResponse>.Fail("Failed to create customer.");
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "CreateCustomer", IsSuccess = false, ResponseBody = ex.Message });
            return Result<CustomerResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CustomerResponse>> UpdateCustomerAsync(string shopDomain, string customerId, UpdateCustomerRequest request)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var body = new { customer = request };
            var response = await _apiClient.PutAsync<CustomerResponse>(shopDomain, $"customers/{customerId}.json", body, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "UpdateCustomer",
                RequestBody = JsonConvert.SerializeObject(body),
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return response?.Customer != null
                ? Result<CustomerResponse>.Ok(response)
                : Result<CustomerResponse>.Fail("Failed to update customer.");
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "UpdateCustomer", IsSuccess = false, ResponseBody = ex.Message });
            return Result<CustomerResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteCustomerAsync(string shopDomain, string customerId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            await _apiClient.DeleteAsync<object>(shopDomain, $"customers/{customerId}.json", token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "DeleteCustomer", IsSuccess = true, StatusCode = 200 });
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "DeleteCustomer", IsSuccess = false, ResponseBody = ex.Message });
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<AccountActivationUrlResponse>> GenerateAccountActivationUrlAsync(string shopDomain, string customerId)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            var response = await _apiClient.PostAsync<AccountActivationUrlResponse>(shopDomain, $"customers/{customerId}/account_activation_url.json", null, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "GenerateAccountActivationUrl",
                ResponseBody = JsonConvert.SerializeObject(response),
                IsSuccess = true,
                StatusCode = 200
            });
            return response != null
                ? Result<AccountActivationUrlResponse>.Ok(response)
                : Result<AccountActivationUrlResponse>.Fail("Failed to generate activation URL.");
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "GenerateAccountActivationUrl", IsSuccess = false, ResponseBody = ex.Message });
            return Result<AccountActivationUrlResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> SendInviteAsync(string shopDomain, string customerId, SendInviteRequest request)
    {
        try
        {
            var token = await _authRepo.GetTokenByShopAsync(shopDomain);
            await _apiClient.PostAsync<object>(shopDomain, $"customers/{customerId}/send_invite.json", request, token?.AccessToken);
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog
            {
                ShopDomain = shopDomain,
                ApiName = "SendInvite",
                RequestBody = JsonConvert.SerializeObject(request),
                IsSuccess = true,
                StatusCode = 200
            });
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await _authRepo.SaveApiLogAsync(new ShopifyApiLog { ShopDomain = shopDomain, ApiName = "SendInvite", IsSuccess = false, ResponseBody = ex.Message });
            return Result<bool>.Fail(ex.Message);
        }
    }
}

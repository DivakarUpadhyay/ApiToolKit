using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyStorefrontCustomerService : IShopifyStorefrontCustomerService
{
    private readonly IShopifyStorefrontRepository _repo;
    private readonly IShopifyStorefrontApiClient _apiClient;

    public ShopifyStorefrontCustomerService(IShopifyStorefrontRepository repo, IShopifyStorefrontApiClient apiClient)
    {
        _repo = repo;
        _apiClient = apiClient;
    }

    public async Task<Result<CustomerAccessTokenResponse>> CustomerLoginAsync(string shopDomain, string email, string password)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CustomerAccessTokenResponse>.Fail("Shopify Storefront configuration not found.");

        const string mutation = @"
mutation customerAccessTokenCreate($input: CustomerAccessTokenCreateInput!) {
  customerAccessTokenCreate(input: $input) {
    customerAccessToken {
      accessToken
      expiresAt
    }
    customerUserErrors {
      code
      field
      message
    }
  }
}";

        var variables = new { input = new { email, password } };
        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CustomerLogin",
            RequestBody = JsonConvert.SerializeObject(new { email }),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["customerAccessTokenCreate"]?["customerAccessToken"] != null,
            StatusCode = response?["data"]?["customerAccessTokenCreate"]?["customerAccessToken"] != null ? 200 : 401,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CustomerAccessTokenResponse>.Fail("No response from Shopify Storefront API.");

        var tokenData = response["data"]?["customerAccessTokenCreate"];
        var accessTokenNode = tokenData?["customerAccessToken"];
        var userErrors = tokenData?["customerUserErrors"]?.ToObject<List<JObject>>()
            ?.Select(e => e["message"]?.ToString() ?? "").ToList();

        if (accessTokenNode == null)
        {
            var errorMsg = userErrors?.FirstOrDefault() ?? "Login failed.";
            return Result<CustomerAccessTokenResponse>.Fail(errorMsg, 401);
        }

        return Result<CustomerAccessTokenResponse>.Ok(new CustomerAccessTokenResponse
        {
            CustomerAccessToken = accessTokenNode["accessToken"]?.ToString(),
            ExpiresAt = accessTokenNode["expiresAt"]?.ToString(),
            UserErrors = userErrors
        });
    }

    public async Task<Result<CreateCustomerResponse>> CustomerRegisterAsync(string shopDomain, RegisterCustomerRequest request)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CreateCustomerResponse>.Fail("Shopify Storefront configuration not found.");

        const string mutation = @"
mutation customerCreate($input: CustomerCreateInput!) {
  customerCreate(input: $input) {
    customer {
      id
      email
      firstName
      lastName
      phone
      createdAt
    }
    customerUserErrors {
      code
      field
      message
    }
  }
}";

        var variables = new
        {
            input = new
            {
                email = request.Email,
                password = request.Password,
                firstName = request.FirstName,
                lastName = request.LastName,
                phone = request.Phone,
                acceptsMarketing = request.AcceptsMarketing
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CustomerRegister",
            RequestBody = JsonConvert.SerializeObject(new { request.Email, request.FirstName, request.LastName }),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["customerCreate"]?["customer"] != null,
            StatusCode = response?["data"]?["customerCreate"]?["customer"] != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CreateCustomerResponse>.Fail("No response from Shopify Storefront API.");

        var createData = response["data"]?["customerCreate"];
        var customerNode = createData?["customer"];
        var userErrors = createData?["customerUserErrors"]?.ToObject<List<JObject>>()
            ?.Select(e => e["message"]?.ToString() ?? "").ToList();

        if (customerNode == null)
        {
            var errorMsg = userErrors?.FirstOrDefault() ?? "Registration failed.";
            return Result<CreateCustomerResponse>.Fail(errorMsg, 422);
        }

        return Result<CreateCustomerResponse>.Ok(new CreateCustomerResponse
        {
            Customer = new StorefrontCustomerDto
            {
                Id = customerNode["id"]?.ToString(),
                Email = customerNode["email"]?.ToString(),
                FirstName = customerNode["firstName"]?.ToString(),
                LastName = customerNode["lastName"]?.ToString(),
                Phone = customerNode["phone"]?.ToString(),
                CreatedAt = customerNode["createdAt"]?.ToString()
            },
            UserErrors = userErrors
        });
    }

    public async Task<Result<StorefrontCustomerResponse>> GetCustomerAsync(string shopDomain, string customerAccessToken)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<StorefrontCustomerResponse>.Fail("Shopify Storefront configuration not found.");

        const string query = @"
query GetCustomer($customerAccessToken: String!) {
  customer(customerAccessToken: $customerAccessToken) {
    id
    email
    firstName
    lastName
    phone
    createdAt
  }
}";

        var variables = new { customerAccessToken };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetCustomer",
            RequestBody = JsonConvert.SerializeObject(new { customerAccessToken }),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["customer"] != null,
            StatusCode = response?["data"]?["customer"] != null ? 200 : 401,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<StorefrontCustomerResponse>.Fail("No response from Shopify Storefront API.");

        var customerNode = response["data"]?["customer"];
        if (customerNode == null)
            return Result<StorefrontCustomerResponse>.Fail("Customer not found or token is invalid.", 401);

        return Result<StorefrontCustomerResponse>.Ok(new StorefrontCustomerResponse
        {
            Customer = new StorefrontCustomerDto
            {
                Id = customerNode["id"]?.ToString(),
                Email = customerNode["email"]?.ToString(),
                FirstName = customerNode["firstName"]?.ToString(),
                LastName = customerNode["lastName"]?.ToString(),
                Phone = customerNode["phone"]?.ToString(),
                CreatedAt = customerNode["createdAt"]?.ToString()
            }
        });
    }

    public async Task<Result<bool>> CustomerLogoutAsync(string shopDomain, string customerAccessToken)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<bool>.Fail("Shopify Storefront configuration not found.");

        const string mutation = @"
mutation customerAccessTokenDelete($customerAccessToken: String!) {
  customerAccessTokenDelete(customerAccessToken: $customerAccessToken) {
    deletedAccessToken
    deletedCustomerAccessTokenId
    userErrors {
      field
      message
    }
  }
}";

        var variables = new { customerAccessToken };
        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CustomerLogout",
            RequestBody = JsonConvert.SerializeObject(new { customerAccessToken }),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"] != null,
            StatusCode = response?["data"] != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<bool>.Fail("No response from Shopify Storefront API.");

        return Result<bool>.Ok(true, "Customer logged out successfully.");
    }
}

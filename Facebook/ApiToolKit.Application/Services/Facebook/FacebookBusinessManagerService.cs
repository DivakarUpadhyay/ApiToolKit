using ApiToolKit.Application.DTOs.Facebook.BusinessManager;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookBusinessManagerService : IFacebookBusinessManagerService {
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookBusinessManagerRepository _repo;
    public FacebookBusinessManagerService(IFacebookApiClient apiClient, IFacebookBusinessManagerRepository repo) { _apiClient = apiClient; _repo = repo; }
    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null) {
        await _repo.SaveApiLogAsync(new FacebookApiLog { ApiName = apiName, RequestBody = request ?? string.Empty, ResponseBody = response ?? string.Empty, IsSuccess = success, StatusCode = statusCode, CreatedAt = DateTime.UtcNow });
    }

    public async Task<Result<Business>> GetBusinessAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}?fields=id,name,verification_status,link,primary_page", accessToken: accessToken);
            if (response == null) return Result<Business>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<Business>(JsonConvert.SerializeObject(response));
            await LogAsync("GetBusiness", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<Business>.Ok(result) : Result<Business>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetBusiness", false, 500, businessId, ex.Message);
            return Result<Business>.Fail(ex.Message);
        }
    }

    public async Task<Result<BusinessPagesResponse>> GetOwnedPagesAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/owned_pages?fields=id,name,category,link,access_status", accessToken: accessToken);
            if (response == null) return Result<BusinessPagesResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<BusinessPagesResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetOwnedPages", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<BusinessPagesResponse>.Ok(result) : Result<BusinessPagesResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetOwnedPages", false, 500, businessId, ex.Message);
            return Result<BusinessPagesResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<BusinessAdAccountsResponse>> GetOwnedAdAccountsAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/owned_ad_accounts?fields=id,name,account_status,currency,timezone_name", accessToken: accessToken);
            if (response == null) return Result<BusinessAdAccountsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<BusinessAdAccountsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetOwnedAdAccounts", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<BusinessAdAccountsResponse>.Ok(result) : Result<BusinessAdAccountsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetOwnedAdAccounts", false, 500, businessId, ex.Message);
            return Result<BusinessAdAccountsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<BusinessInstagramAccountsResponse>> GetOwnedInstagramAccountsAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/owned_instagram_accounts?fields=id,username,name", accessToken: accessToken);
            if (response == null) return Result<BusinessInstagramAccountsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<BusinessInstagramAccountsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetOwnedInstagramAccounts", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<BusinessInstagramAccountsResponse>.Ok(result) : Result<BusinessInstagramAccountsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetOwnedInstagramAccounts", false, 500, businessId, ex.Message);
            return Result<BusinessInstagramAccountsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<BusinessCatalogsResponse>> GetOwnedCatalogsAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/owned_product_catalogs?fields=id,name,vertical", accessToken: accessToken);
            if (response == null) return Result<BusinessCatalogsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<BusinessCatalogsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetOwnedCatalogs", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<BusinessCatalogsResponse>.Ok(result) : Result<BusinessCatalogsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetOwnedCatalogs", false, 500, businessId, ex.Message);
            return Result<BusinessCatalogsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<BusinessUsersResponse>> GetBusinessUsersAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/business_users?fields=id,name,email,role,status", accessToken: accessToken);
            if (response == null) return Result<BusinessUsersResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<BusinessUsersResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetBusinessUsers", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<BusinessUsersResponse>.Ok(result) : Result<BusinessUsersResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetBusinessUsers", false, 500, businessId, ex.Message);
            return Result<BusinessUsersResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> InviteBusinessUserAsync(string businessId, InviteBusinessUserRequest request, string accessToken) {
        try {
            var body = new { email = request.Email, role = request.Role };
            var response = await _apiClient.PostAsync<dynamic>($"/{businessId}/business_users", body, accessToken);
            await LogAsync("InviteBusinessUser", true, 200, businessId, JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("No response from API.");
        } catch (Exception ex) {
            await LogAsync("InviteBusinessUser", false, 500, businessId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> RemoveBusinessUserAsync(string businessId, string userId, string accessToken) {
        try {
            var response = await _apiClient.DeleteAsync<dynamic>($"/{businessId}/business_users/{userId}", accessToken);
            await LogAsync("RemoveBusinessUser", true, 200, $"{businessId}/{userId}", JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("No response from API.");
        } catch (Exception ex) {
            await LogAsync("RemoveBusinessUser", false, 500, $"{businessId}/{userId}", ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<SystemUsersResponse>> GetSystemUsersAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/system_users?fields=id,name,role", accessToken: accessToken);
            if (response == null) return Result<SystemUsersResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<SystemUsersResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetSystemUsers", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<SystemUsersResponse>.Ok(result) : Result<SystemUsersResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetSystemUsers", false, 500, businessId, ex.Message);
            return Result<SystemUsersResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<SystemUser>> CreateSystemUserAsync(string businessId, CreateSystemUserRequest request, string accessToken) {
        try {
            var body = new { name = request.Name, role = request.Role };
            var response = await _apiClient.PostAsync<dynamic>($"/{businessId}/system_users", body, accessToken);
            if (response == null) return Result<SystemUser>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<SystemUser>(JsonConvert.SerializeObject(response));
            await LogAsync("CreateSystemUser", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<SystemUser>.Ok(result) : Result<SystemUser>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("CreateSystemUser", false, 500, businessId, ex.Message);
            return Result<SystemUser>.Fail(ex.Message);
        }
    }

    public async Task<Result<BusinessPagesResponse>> GetClientPagesAsync(string businessId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{businessId}/client_pages?fields=id,name,category", accessToken: accessToken);
            if (response == null) return Result<BusinessPagesResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<BusinessPagesResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetClientPages", true, 200, businessId, JsonConvert.SerializeObject(response));
            return result != null ? Result<BusinessPagesResponse>.Ok(result) : Result<BusinessPagesResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetClientPages", false, 500, businessId, ex.Message);
            return Result<BusinessPagesResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> ClaimPageAsync(string businessId, ClaimPageRequest request, string accessToken) {
        try {
            var body = new { page_id = request.PageId };
            var response = await _apiClient.PostAsync<dynamic>($"/{businessId}/owned_pages", body, accessToken);
            await LogAsync("ClaimPage", true, 200, businessId, JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("No response from API.");
        } catch (Exception ex) {
            await LogAsync("ClaimPage", false, 500, businessId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }
}

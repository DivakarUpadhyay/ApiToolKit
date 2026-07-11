using ApiToolKit.Application.DTOs.Facebook.Capi;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookCapiService : IFacebookCapiService {
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookCapiRepository _repo;
    public FacebookCapiService(IFacebookApiClient apiClient, IFacebookCapiRepository repo) { _apiClient = apiClient; _repo = repo; }

    private static string? Hash(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value.Trim().ToLowerInvariant()));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null) {
        await _repo.SaveApiLogAsync(new FacebookApiLog { ApiName = apiName, RequestBody = request ?? string.Empty, ResponseBody = response ?? string.Empty, IsSuccess = success, StatusCode = statusCode, CreatedAt = DateTime.UtcNow });
    }

    public async Task<Result<CApiEventsResponse>> SendEventsAsync(SendCApiEventsRequest request) {
        try {
            var body = new {
                data = request.Data,
                test_event_code = request.TestEventCode,
                access_token = request.AccessToken
            };
            var response = await _apiClient.PostAsync<dynamic>($"/{request.PixelId}/events", body, accessToken: null);
            if (response == null) return Result<CApiEventsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CApiEventsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("SendCApiEvents", true, 200, request.PixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CApiEventsResponse>.Ok(result) : Result<CApiEventsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("SendCApiEvents", false, 500, request.PixelId, ex.Message);
            return Result<CApiEventsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CApiEventsResponse>> SendPurchaseEventAsync(PurchaseEventRequest r) {
        try {
            var userData = new CApiUserData(Email: Hash(r.Email), Phone: Hash(r.Phone), ClientIpAddress: r.ClientIpAddress, ClientUserAgent: r.ClientUserAgent);
            var customData = new CApiCustomData(Value: r.Value, Currency: r.Currency, ContentName: r.ContentName, ContentIds: r.ContentIds, OrderId: r.OrderId, ContentType: "product");
            var evt = new CApiEvent("Purchase", r.EventTime, r.EventSourceUrl, UserData: userData, CustomData: customData);
            var body = new { data = new[] { evt }, test_event_code = r.TestEventCode, access_token = r.AccessToken };
            var response = await _apiClient.PostAsync<dynamic>($"/{r.PixelId}/events", body, accessToken: null);
            if (response == null) return Result<CApiEventsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CApiEventsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("SendPurchaseEvent", true, 200, r.PixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CApiEventsResponse>.Ok(result) : Result<CApiEventsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("SendPurchaseEvent", false, 500, r.PixelId, ex.Message);
            return Result<CApiEventsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CApiEventsResponse>> SendAddToCartEventAsync(AddToCartEventRequest r) {
        try {
            var userData = new CApiUserData(ClientIpAddress: r.ClientIpAddress, ClientUserAgent: r.ClientUserAgent);
            var customData = new CApiCustomData(Value: r.Value, Currency: r.Currency, ContentName: r.ContentName, ContentIds: r.ContentIds, ContentType: "product");
            var evt = new CApiEvent("AddToCart", r.EventTime, r.EventSourceUrl, UserData: userData, CustomData: customData);
            var body = new { data = new[] { evt }, test_event_code = r.TestEventCode, access_token = r.AccessToken };
            var response = await _apiClient.PostAsync<dynamic>($"/{r.PixelId}/events", body, accessToken: null);
            if (response == null) return Result<CApiEventsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CApiEventsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("SendAddToCartEvent", true, 200, r.PixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CApiEventsResponse>.Ok(result) : Result<CApiEventsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("SendAddToCartEvent", false, 500, r.PixelId, ex.Message);
            return Result<CApiEventsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CApiEventsResponse>> SendPageViewEventAsync(PageViewEventRequest r) {
        try {
            var userData = new CApiUserData(ClientIpAddress: r.ClientIpAddress, ClientUserAgent: r.ClientUserAgent);
            var evt = new CApiEvent("PageView", r.EventTime, r.EventSourceUrl, UserData: userData);
            var body = new { data = new[] { evt }, test_event_code = r.TestEventCode, access_token = r.AccessToken };
            var response = await _apiClient.PostAsync<dynamic>($"/{r.PixelId}/events", body, accessToken: null);
            if (response == null) return Result<CApiEventsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CApiEventsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("SendPageViewEvent", true, 200, r.PixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CApiEventsResponse>.Ok(result) : Result<CApiEventsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("SendPageViewEvent", false, 500, r.PixelId, ex.Message);
            return Result<CApiEventsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CApiEventsResponse>> SendLeadEventAsync(LeadEventRequest r) {
        try {
            var userData = new CApiUserData(Email: Hash(r.Email), Phone: Hash(r.Phone), ClientIpAddress: r.ClientIpAddress, ClientUserAgent: r.ClientUserAgent);
            var customData = new CApiCustomData(ContentName: r.ContentName);
            var evt = new CApiEvent("Lead", r.EventTime, r.EventSourceUrl, UserData: userData, CustomData: customData);
            var body = new { data = new[] { evt }, test_event_code = r.TestEventCode, access_token = r.AccessToken };
            var response = await _apiClient.PostAsync<dynamic>($"/{r.PixelId}/events", body, accessToken: null);
            if (response == null) return Result<CApiEventsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<CApiEventsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("SendLeadEvent", true, 200, r.PixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<CApiEventsResponse>.Ok(result) : Result<CApiEventsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("SendLeadEvent", false, 500, r.PixelId, ex.Message);
            return Result<CApiEventsResponse>.Fail(ex.Message);
        }
    }
}

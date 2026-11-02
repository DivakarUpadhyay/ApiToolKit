using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppAccountService : WhatsAppServiceBase, IFacebookWhatsAppAccountService
{
    public FacebookWhatsAppAccountService(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo) : base(api, repo) { }

    private const string WabaFields = "id,name,currency,timezone_id,message_template_namespace,account_review_status,business_verification_status,ownership_type";

    // ─── WABA webhooks (subscribed apps) ────────────────────────────────────

    public Task<Result<object>> SubscribeAppToWabaAsync(WaSubscribeAppRequest r)
    {
        var body = new Dictionary<string, object?>();
        if (!string.IsNullOrEmpty(r.OverrideCallbackUri)) body["override_callback_uri"] = r.OverrideCallbackUri;
        if (!string.IsNullOrEmpty(r.VerifyToken)) body["verify_token"] = r.VerifyToken;
        return RunAsync("SubscribeAppToWaba", () => Api.PostAsync<object>($"/{r.WabaId}/subscribed_apps", r.AccessToken, body), r);
    }

    public Task<Result<object>> GetSubscribedAppsAsync(string wabaId, string accessToken) =>
        RunAsync("GetSubscribedApps", () => Api.GetAsync<object>($"/{wabaId}/subscribed_apps", accessToken));

    public Task<Result<object>> UnsubscribeAppFromWabaAsync(string wabaId, string accessToken) =>
        RunAsync("UnsubscribeAppFromWaba", () => Api.DeleteAsync<object>($"/{wabaId}/subscribed_apps", accessToken));

    // ─── Business portfolio ─────────────────────────────────────────────────

    public Task<Result<object>> GetOwnedWabasAsync(string businessId, string accessToken) =>
        RunAsync("GetOwnedWabas", () => Api.GetAsync<object>($"/{businessId}/owned_whatsapp_business_accounts", accessToken,
            new Dictionary<string, string> { ["fields"] = WabaFields }));

    public Task<Result<object>> GetClientWabasAsync(string businessId, string accessToken) =>
        RunAsync("GetClientWabas", () => Api.GetAsync<object>($"/{businessId}/client_whatsapp_business_accounts", accessToken,
            new Dictionary<string, string> { ["fields"] = WabaFields }));

    // ─── Phone numbers ──────────────────────────────────────────────────────

    public Task<Result<object>> AddPhoneNumberAsync(WaAddPhoneNumberRequest r) =>
        RunAsync("AddPhoneNumber", () => Api.PostAsync<object>($"/{r.WabaId}/phone_numbers", r.AccessToken, new
        {
            cc = r.Cc,
            phone_number = r.PhoneNumber,
            verified_name = r.VerifiedName
        }), r);

    public Task<Result<object>> UpdateDisplayNameAsync(WaDisplayNameRequest r) =>
        RunAsync("UpdateDisplayName", () => Api.PostAsync<object>($"/{r.PhoneNumberId}", r.AccessToken, null,
            new Dictionary<string, string> { ["new_display_name"] = r.NewDisplayName }), r);

    public Task<Result<object>> GetPhoneSettingsAsync(string phoneNumberId, string accessToken, bool includeSipCredentials)
    {
        var query = includeSipCredentials ? new Dictionary<string, string> { ["include_sip_credentials"] = "true" } : null;
        return RunAsync("GetPhoneSettings", () => Api.GetAsync<object>($"/{phoneNumberId}/settings", accessToken, query));
    }

    public Task<Result<object>> UpdatePhoneSettingsAsync(WaRawSettingsRequest r) =>
        RunAsync("UpdatePhoneSettings", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/settings", r.AccessToken, r.Settings), r);

    // ─── Conversational components ──────────────────────────────────────────

    public Task<Result<object>> GetConversationalAutomationAsync(string phoneNumberId, string accessToken) =>
        RunAsync("GetConversationalAutomation", () => Api.GetAsync<object>($"/{phoneNumberId}", accessToken,
            new Dictionary<string, string> { ["fields"] = "conversational_automation" }));

    public Task<Result<object>> UpdateConversationalAutomationAsync(WaConversationalAutomationRequest r)
    {
        var body = new Dictionary<string, object?>();
        if (r.EnableWelcomeMessage.HasValue) body["enable_welcome_message"] = r.EnableWelcomeMessage.Value;
        if (r.Prompts != null) body["prompts"] = r.Prompts;
        if (r.Commands != null)
            body["commands"] = r.Commands.Select(c => new { command_name = c.CommandName, command_description = c.CommandDescription });
        return RunAsync("UpdateConversationalAutomation", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/conversational_automation", r.AccessToken, body), r);
    }

    // ─── Commerce settings ──────────────────────────────────────────────────

    public Task<Result<object>> GetCommerceSettingsAsync(string phoneNumberId, string accessToken) =>
        RunAsync("GetCommerceSettings", () => Api.GetAsync<object>($"/{phoneNumberId}/whatsapp_commerce_settings", accessToken));

    public Task<Result<object>> UpdateCommerceSettingsAsync(WaCommerceSettingsRequest r)
    {
        var query = new Dictionary<string, string>();
        if (r.IsCartEnabled.HasValue) query["is_cart_enabled"] = r.IsCartEnabled.Value ? "true" : "false";
        if (r.IsCatalogVisible.HasValue) query["is_catalog_visible"] = r.IsCatalogVisible.Value ? "true" : "false";
        return RunAsync("UpdateCommerceSettings", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/whatsapp_commerce_settings", r.AccessToken, null, query), r);
    }

    // ─── Business compliance (India) ────────────────────────────────────────

    public Task<Result<object>> GetBusinessComplianceAsync(string phoneNumberId, string accessToken) =>
        RunAsync("GetBusinessCompliance", () => Api.GetAsync<object>($"/{phoneNumberId}/business_compliance_info", accessToken));

    public Task<Result<object>> UpdateBusinessComplianceAsync(WaRawSettingsRequest r) =>
        RunAsync("UpdateBusinessCompliance", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/business_compliance_info", r.AccessToken, r.Settings), r);

    // ─── Health, WABA flags ─────────────────────────────────────────────────

    public Task<Result<object>> GetHealthStatusAsync(string nodeId, string accessToken) =>
        RunAsync("GetHealthStatus", () => Api.GetAsync<object>($"/{nodeId}", accessToken,
            new Dictionary<string, string> { ["fields"] = "health_status" }));

    public Task<Result<object>> SetMarketingOnCloudApiAsync(WaMarketingOnCloudApiRequest r) =>
        RunAsync("SetMarketingOnCloudApi", () => Api.PostAsync<object>($"/{r.WabaId}", r.AccessToken,
            new { disable_marketing_messages_on_cloud_api = r.Disable }), r);

    public Task<Result<object>> EnableTemplateInsightsAsync(WaWabaRequest r) =>
        RunAsync("EnableTemplateInsights", () => Api.PostAsync<object>($"/{r.WabaId}", r.AccessToken, null,
            new Dictionary<string, string> { ["is_enabled_for_insights"] = "true" }), r);

    // ─── Embedded Signup ────────────────────────────────────────────────────

    public Task<Result<object>> ExchangeEmbeddedSignupCodeAsync(WaExchangeCodeRequest r) =>
        RunAsync("ExchangeEmbeddedSignupCode", () => Api.GetAsync<object>("/oauth/access_token", string.Empty,
            new Dictionary<string, string>
            {
                ["client_id"] = r.AppId,
                ["client_secret"] = r.AppSecret,
                ["code"] = r.Code
            }), r);
}

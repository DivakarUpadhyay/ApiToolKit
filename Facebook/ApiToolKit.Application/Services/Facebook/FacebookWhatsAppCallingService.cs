using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppCallingService : WhatsAppServiceBase, IFacebookWhatsAppCallingService
{
    private static readonly HashSet<string> Actions = new(StringComparer.OrdinalIgnoreCase)
    {
        "connect", "pre_accept", "accept", "reject", "terminate"
    };

    public FacebookWhatsAppCallingService(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo) : base(api, repo) { }

    public Task<Result<object>> GetCallSettingsAsync(string phoneNumberId, string accessToken, bool includeSipCredentials)
    {
        var query = includeSipCredentials ? new Dictionary<string, string> { ["include_sip_credentials"] = "true" } : null;
        return RunAsync("GetCallSettings", () => Api.GetAsync<object>($"/{phoneNumberId}/settings", accessToken, query));
    }

    public Task<Result<object>> UpdateCallSettingsAsync(WaRawSettingsRequest r) =>
        RunAsync("UpdateCallSettings", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/settings", r.AccessToken, r.Settings), r);

    public Task<Result<object>> CallActionAsync(string action, WaCallActionRequest r)
    {
        action = action.Replace('-', '_').ToLowerInvariant();
        if (!Actions.Contains(action))
            return Task.FromResult(Result<object>.Fail("action must be connect, pre_accept, accept, reject or terminate."));
        if (action != "connect" && string.IsNullOrEmpty(r.CallId))
            return Task.FromResult(Result<object>.Fail("CallId is required."));
        if (action is "connect" or "pre_accept" or "accept" && r.Session == null)
            return Task.FromResult(Result<object>.Fail("Session (sdp_type + sdp) is required."));

        var body = new Dictionary<string, object?> { ["messaging_product"] = "whatsapp", ["action"] = action };
        if (action == "connect")
        {
            if (!string.IsNullOrEmpty(r.To)) body["to"] = r.To;
            if (!string.IsNullOrEmpty(r.Recipient)) body["recipient"] = r.Recipient;
        }
        else body["call_id"] = r.CallId;

        if (r.Session != null) body["session"] = new { sdp_type = r.Session.SdpType, sdp = r.Session.Sdp };
        if (action is "connect" or "accept")
        {
            if (!string.IsNullOrEmpty(r.BizOpaqueCallbackData)) body["biz_opaque_callback_data"] = r.BizOpaqueCallbackData;
            if (r.Recording != null) body["recording"] = r.Recording;
            if (r.Transcription != null) body["transcription"] = r.Transcription;
        }

        return RunAsync($"Call_{action}", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/calls", r.AccessToken, body), r);
    }

    public Task<Result<object>> GetCallPermissionsAsync(string phoneNumberId, string accessToken, string? userWaId, string? recipient)
    {
        if (string.IsNullOrEmpty(userWaId) && string.IsNullOrEmpty(recipient))
            return Task.FromResult(Result<object>.Fail("Provide userWaId or recipient."));
        var query = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(userWaId)) query["user_wa_id"] = userWaId;
        if (!string.IsNullOrEmpty(recipient)) query["recipient"] = recipient;
        return RunAsync("GetCallPermissions", () => Api.GetAsync<object>($"/{phoneNumberId}/call_permissions", accessToken, query));
    }

    public Task<Result<object>> SendCallPermissionRequestAsync(WaCallPermissionRequestMessage r)
    {
        var body = MessageBody(r.To, null, "interactive");
        body["interactive"] = new
        {
            type = "call_permission_request",
            body = new { text = r.BodyText },
            action = new { name = "call_permission_request" }
        };
        return RunAsync("SendCallPermissionRequest", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }

    public Task<Result<object>> SendVoiceCallButtonAsync(WaVoiceCallButtonMessage r)
    {
        var parameters = new Dictionary<string, object?> { ["display_text"] = r.DisplayText };
        if (r.TtlMinutes.HasValue) parameters["ttl_minutes"] = r.TtlMinutes.Value;
        if (!string.IsNullOrEmpty(r.Payload)) parameters["payload"] = r.Payload;

        var body = MessageBody(r.To, null, "interactive");
        body["interactive"] = new
        {
            type = "voice_call",
            body = new { text = r.BodyText },
            action = new { name = "voice_call", parameters }
        };
        return RunAsync("SendVoiceCallButton", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }
}

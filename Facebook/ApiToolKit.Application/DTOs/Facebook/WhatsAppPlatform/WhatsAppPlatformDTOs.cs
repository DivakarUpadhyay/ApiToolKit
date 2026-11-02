using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;

// ─── Common ───────────────────────────────────────────────────────────────────
public class WaPhoneRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}

public class WaWabaRequest
{
    public string WabaId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}

public class WaUserListRequest : WaPhoneRequest
{
    /// <summary>Phone numbers or WhatsApp IDs.</summary>
    public List<string> Users { get; set; } = new();
}

public class WaMediaFile
{
    public byte[] Content { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = "application/octet-stream";
    public string FileName { get; set; } = "media";
}

// ─── Messaging extras ─────────────────────────────────────────────────────────
public class WaTypingIndicatorRequest : WaPhoneRequest
{
    /// <summary>ID of the received message (marks it read and shows "typing…" for up to 25 s).</summary>
    public string MessageId { get; set; } = string.Empty;
}

/// <summary>
/// Sends any message type supported by POST /{phone-number-id}/messages — e.g. interactive carousel,
/// address, order_details, order_status, voice_call, call_permission_request — or to a group.
/// </summary>
public class WaSendRawMessageRequest : WaPhoneRequest
{
    public string? To { get; set; }
    /// <summary>Business-scoped user ID (BSUID) instead of a phone number.</summary>
    public string? Recipient { get; set; }
    /// <summary>individual (default) or group.</summary>
    public string RecipientType { get; set; } = "individual";
    /// <summary>Message type, e.g. text, image, interactive, template.</summary>
    public string Type { get; set; } = "text";
    /// <summary>The object sent under the property named by <see cref="Type"/>.</summary>
    public JObject Content { get; set; } = new();
    public string? ReplyToMessageId { get; set; }
    public string? BizOpaqueCallbackData { get; set; }
}

public class WaSendCarouselRequest : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    /// <summary>2–10 card objects (card_index, type, header, body, action) as defined by Meta.</summary>
    public JArray Cards { get; set; } = new();
}

public class WaSendMarketingMessageRequest : WaPhoneRequest
{
    public string? To { get; set; }
    public string? Recipient { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = "en_US";
    public JArray? Components { get; set; }
    /// <summary>CLOUD_API_FALLBACK (default) or STRICT.</summary>
    public string? ProductPolicy { get; set; }
    public bool? MessageActivitySharing { get; set; }
}

public class WaProductSectionDto
{
    public string Title { get; set; } = string.Empty;
    public List<string> ProductRetailerIds { get; set; } = new();
}

/// <summary>Multi-product message: up to 10 sections, 30 products in total.</summary>
public class WaSendMultiProductRequest : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string CatalogId { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? FooterText { get; set; }
    public List<WaProductSectionDto> Sections { get; set; } = new();
}

// ─── Templates extras ─────────────────────────────────────────────────────────
public class WaTemplateSearchQuery
{
    public string WabaId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? Category { get; set; }
    public string? Language { get; set; }
    public string? Fields { get; set; }
    public int? Limit { get; set; }
    public string? After { get; set; }
    public string? Before { get; set; }
}

public class WaTemplateLibraryQuery
{
    public string AccessToken { get; set; } = string.Empty;
    public string? Search { get; set; }
    public string? Topic { get; set; }
    public string? Usecase { get; set; }
    public string? Industry { get; set; }
    public string? Language { get; set; }
    public string? Name { get; set; }
}

public class WaCreateTemplateFromLibraryRequest : WaWabaRequest
{
    public string Name { get; set; } = string.Empty;
    public string Language { get; set; } = "en_US";
    public string Category { get; set; } = "UTILITY";
    public string LibraryTemplateName { get; set; } = string.Empty;
    public JArray? LibraryTemplateButtonInputs { get; set; }
    public JObject? LibraryTemplateBodyInputs { get; set; }
}

public class WaMigrateTemplatesRequest
{
    public string DestinationWabaId { get; set; } = string.Empty;
    public string SourceWabaId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public int? PageNumber { get; set; }
    public int? Count { get; set; }
    public List<string>? TemplateIds { get; set; }
}

// ─── Account & settings ───────────────────────────────────────────────────────
public class WaSubscribeAppRequest : WaWabaRequest
{
    /// <summary>Optional per-WABA callback URL override.</summary>
    public string? OverrideCallbackUri { get; set; }
    public string? VerifyToken { get; set; }
}

public class WaAddPhoneNumberRequest : WaWabaRequest
{
    public string Cc { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string VerifiedName { get; set; } = string.Empty;
}

public class WaDisplayNameRequest : WaPhoneRequest
{
    public string NewDisplayName { get; set; } = string.Empty;
}

public class WaCommandDto
{
    public string CommandName { get; set; } = string.Empty;
    public string CommandDescription { get; set; } = string.Empty;
}

public class WaConversationalAutomationRequest : WaPhoneRequest
{
    public bool? EnableWelcomeMessage { get; set; }
    /// <summary>Up to 4 ice breakers, 80 chars each.</summary>
    public List<string>? Prompts { get; set; }
    /// <summary>Up to 30 commands.</summary>
    public List<WaCommandDto>? Commands { get; set; }
}

public class WaCommerceSettingsRequest : WaPhoneRequest
{
    public bool? IsCartEnabled { get; set; }
    public bool? IsCatalogVisible { get; set; }
}

public class WaRawSettingsRequest : WaPhoneRequest
{
    /// <summary>Body exactly as Meta documents it (e.g. { "calling": { ... } } or { "storage_configuration": { ... } }).</summary>
    public JObject Settings { get; set; } = new();
}

public class WaMarketingOnCloudApiRequest : WaWabaRequest
{
    public bool Disable { get; set; }
}

public class WaExchangeCodeRequest
{
    public string AppId { get; set; } = string.Empty;
    public string AppSecret { get; set; } = string.Empty;
    /// <summary>Code returned by Embedded Signup (valid for 30 seconds).</summary>
    public string Code { get; set; } = string.Empty;
}

// ─── Analytics ────────────────────────────────────────────────────────────────
public class WaPricingAnalyticsRequest : WaWabaRequest
{
    public long Start { get; set; }
    public long End { get; set; }
    /// <summary>DAILY, HALF_HOUR or MONTHLY.</summary>
    public string Granularity { get; set; } = "DAILY";
    public List<string>? PhoneNumbers { get; set; }
    public List<string>? CountryCodes { get; set; }
    public List<string>? MetricTypes { get; set; }
    public List<string>? PricingTypes { get; set; }
    public List<string>? PricingCategories { get; set; }
    public List<string>? Dimensions { get; set; }
}

public class WaCallAnalyticsRequest : WaWabaRequest
{
    public long Start { get; set; }
    public long End { get; set; }
    public string Granularity { get; set; } = "DAILY";
    public List<string>? PhoneNumbers { get; set; }
    public List<string>? CountryCodes { get; set; }
    public List<string>? Directions { get; set; }
    public List<string>? Dimensions { get; set; }
    public List<string>? MetricTypes { get; set; }
}

public class WaTemplateAnalyticsRequest : WaWabaRequest
{
    /// <summary>UNIX timestamp, or YYYY-MM-DD when UseWabaTimezone is true.</summary>
    public string Start { get; set; } = string.Empty;
    public string End { get; set; } = string.Empty;
    /// <summary>Up to 10 template IDs.</summary>
    public List<string> TemplateIds { get; set; } = new();
    public List<string>? MetricTypes { get; set; }
    /// <summary>CLOUD_API (default) or MARKETING_MESSAGES_LITE_API.</summary>
    public string? ProductType { get; set; }
    public bool? UseWabaTimezone { get; set; }
}

public class WaTemplateGroupAnalyticsRequest : WaWabaRequest
{
    public string Start { get; set; } = string.Empty;
    public string End { get; set; } = string.Empty;
    public List<string> TemplateGroupIds { get; set; } = new();
    public List<string>? MetricTypes { get; set; }
    public bool? UseWabaTimezone { get; set; }
}

public class WaGroupAnalyticsRequest : WaWabaRequest
{
    public long Start { get; set; }
    public long End { get; set; }
    public List<string> MetricTypes { get; set; } = new();
    public List<string> GroupIds { get; set; } = new();
}

public class WaFlowMetricsRequest
{
    public string FlowId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    /// <summary>e.g. ENDPOINT_REQUEST_COUNT, ENDPOINT_REQUEST_ERROR, ENDPOINT_REQUEST_ERROR_RATE, ENDPOINT_REQUEST_LATENCY_SECONDS_CEIL, ENDPOINT_AVAILABILITY.</summary>
    public string MetricName { get; set; } = "ENDPOINT_REQUEST_COUNT";
    /// <summary>DAY, HOUR or LIFETIME.</summary>
    public string Granularity { get; set; } = "DAY";
    public string? Since { get; set; }
    public string? Until { get; set; }
}

// ─── Groups ───────────────────────────────────────────────────────────────────
public class WaCreateGroupRequest : WaPhoneRequest
{
    public string Subject { get; set; } = string.Empty;
    public string? Description { get; set; }
    /// <summary>approval_required or auto_approve (default).</summary>
    public string? JoinApprovalMode { get; set; }
}

public class WaUpdateGroupRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string? Description { get; set; }
    /// <summary>Square JPEG ≤ 5 MB, at least 192×192, base64-encoded.</summary>
    public string? ProfilePictureBase64 { get; set; }
}

public class WaGroupParticipantsRequest
{
    public string AccessToken { get; set; } = string.Empty;
    /// <summary>Up to 8 phone numbers or WhatsApp IDs.</summary>
    public List<string> Participants { get; set; } = new();
}

public class WaJoinRequestsRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public List<string> JoinRequests { get; set; } = new();
}

public class WaGroupMessageRequest : WaPhoneRequest
{
    public string Type { get; set; } = "text";
    /// <summary>Object for the message type, e.g. { "body": "Hello" } for text.</summary>
    public JObject Content { get; set; } = new();
}

public class WaGroupInviteTemplateRequest : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string TemplateName { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = "en_US";
}

// ─── Calling ──────────────────────────────────────────────────────────────────
public class WaCallSessionDto
{
    /// <summary>offer (connect) or answer (pre_accept / accept).</summary>
    public string SdpType { get; set; } = "offer";
    /// <summary>RFC 8866 SDP.</summary>
    public string Sdp { get; set; } = string.Empty;
}

public class WaCallActionRequest : WaPhoneRequest
{
    /// <summary>Required for every action except connect.</summary>
    public string? CallId { get; set; }
    /// <summary>connect only: phone number.</summary>
    public string? To { get; set; }
    /// <summary>connect only: BSUID.</summary>
    public string? Recipient { get; set; }
    public WaCallSessionDto? Session { get; set; }
    public string? BizOpaqueCallbackData { get; set; }
    /// <summary>{ "status": "ENABLED", "purpose": ..., "announcement_language": ... }</summary>
    public JObject? Recording { get; set; }
    public JObject? Transcription { get; set; }
}

public class WaCallPermissionRequestMessage : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
}

public class WaVoiceCallButtonMessage : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string DisplayText { get; set; } = "Call us";
    public int? TtlMinutes { get; set; }
    public string? Payload { get; set; }
}

// ─── Payments (India) ─────────────────────────────────────────────────────────
public class WaCreatePaymentConfigRequest : WaWabaRequest
{
    public string ConfigurationName { get; set; } = string.Empty;
    /// <summary>upi_vpa, razorpay, payu or zaakpay.</summary>
    public string ProviderName { get; set; } = string.Empty;
    public string? PurposeCode { get; set; }
    public string? MerchantCategoryCode { get; set; }
    public string? MerchantVpa { get; set; }
    public string? RedirectUrl { get; set; }
}

public class WaPaymentConfigDataEndpointRequest : WaWabaRequest
{
    public string ConfigurationName { get; set; } = string.Empty;
    public string? DataEndpointUrl { get; set; }
}

public class WaRegeneratePaymentOauthLinkRequest : WaWabaRequest
{
    public string ConfigurationName { get; set; } = string.Empty;
    public string? RedirectUrl { get; set; }
}

public class WaRefundRequest : WaPhoneRequest
{
    public string ReferenceId { get; set; } = string.Empty;
    public string PaymentConfigId { get; set; } = string.Empty;
    /// <summary>instant or normal.</summary>
    public string? Speed { get; set; }
    /// <summary>{ "value": 1000, "offset": 100, "currency": "INR" }</summary>
    public JObject? Amount { get; set; }
}

public class WaOrderDetailsMessageRequest : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? FooterText { get; set; }
    public JObject? Header { get; set; }
    /// <summary>action.parameters: reference_id, type, payment_settings, currency, total_amount, order …</summary>
    public JObject Parameters { get; set; } = new();
}

public class WaOrderStatusMessageRequest : WaPhoneRequest
{
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? FooterText { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
    /// <summary>processing, partially_shipped, shipped, completed or canceled.</summary>
    public string Status { get; set; } = "processing";
    public string? Description { get; set; }
}

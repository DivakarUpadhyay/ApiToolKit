namespace ApiToolKit.Application.DTOs.Facebook.WhatsApp;

// ─── Common ──────────────────────────────────────────────────────────────────
public class WhatsAppMessageResponse
{
    public string? MessagingProduct { get; set; }
    public List<WhatsAppContactDto>? Contacts { get; set; }
    public List<WhatsAppMsgIdDto>? Messages { get; set; }
}

public class WhatsAppContactDto
{
    public string? Input { get; set; }
    public string? WaId { get; set; }
}

public class WhatsAppMsgIdDto
{
    public string? Id { get; set; }
    public string? MessageStatus { get; set; }
}

public class WhatsAppSuccessResponse
{
    public bool Success { get; set; }
}

// ─── Text Message ─────────────────────────────────────────────────────────────
public class SendTextMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool PreviewUrl { get; set; } = false;
    public string? ReplyToMessageId { get; set; }
}

// ─── Template Message ─────────────────────────────────────────────────────────
public class SendTemplateMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string TemplateName { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = "en_US";
    public List<WaTemplateComponent>? Components { get; set; }
}

public class WaTemplateComponent
{
    public string Type { get; set; } = string.Empty;
    public string? SubType { get; set; }
    public int? Index { get; set; }
    public List<WaTemplateParam>? Parameters { get; set; }
}

public class WaTemplateParam
{
    public string Type { get; set; } = string.Empty;
    public string? Text { get; set; }
    public string? ImageLink { get; set; }
    public string? DocumentLink { get; set; }
    public string? VideoLink { get; set; }
}

// ─── Image Message ────────────────────────────────────────────────────────────
public class SendImageMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string? MediaId { get; set; }
    public string? Link { get; set; }
    public string? Caption { get; set; }
    public string? ReplyToMessageId { get; set; }
}

// ─── Video Message ────────────────────────────────────────────────────────────
public class SendVideoMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string? MediaId { get; set; }
    public string? Link { get; set; }
    public string? Caption { get; set; }
    public string? ReplyToMessageId { get; set; }
}

// ─── Audio Message ────────────────────────────────────────────────────────────
public class SendAudioMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string? MediaId { get; set; }
    public string? Link { get; set; }
}

// ─── Document Message ─────────────────────────────────────────────────────────
public class SendDocumentMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string? MediaId { get; set; }
    public string? Link { get; set; }
    public string? Caption { get; set; }
    public string? Filename { get; set; }
}

// ─── Sticker Message ──────────────────────────────────────────────────────────
public class SendStickerMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string? MediaId { get; set; }
    public string? Link { get; set; }
}

// ─── Location Message ─────────────────────────────────────────────────────────
public class SendLocationMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
}

// ─── Contact Message ──────────────────────────────────────────────────────────
public class SendContactMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

// ─── Reaction Message ─────────────────────────────────────────────────────────
public class SendReactionMessageRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
}

// ─── Interactive Buttons ──────────────────────────────────────────────────────
public class SendInteractiveButtonsRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? HeaderText { get; set; }
    public string? FooterText { get; set; }
    public List<WaInteractiveButton> Buttons { get; set; } = new();
}

public class WaInteractiveButton
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

// ─── Interactive List ─────────────────────────────────────────────────────────
public class SendInteractiveListRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? HeaderText { get; set; }
    public string? FooterText { get; set; }
    public string ButtonText { get; set; } = "Select";
    public List<WaListSection> Sections { get; set; } = new();
}

public class WaListSection
{
    public string? Title { get; set; }
    public List<WaListRow> Rows { get; set; } = new();
}

public class WaListRow
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

// ─── Interactive Product ──────────────────────────────────────────────────────
public class SendInteractiveProductRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string CatalogId { get; set; } = string.Empty;
    public string ProductRetailerId { get; set; } = string.Empty;
    public string? BodyText { get; set; }
    public string? FooterText { get; set; }
}

// ─── Interactive Catalog ──────────────────────────────────────────────────────
public class SendInteractiveCatalogRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string CatalogId { get; set; } = string.Empty;
    public string? BodyText { get; set; }
    public string? FooterText { get; set; }
    public string? ThumbnailProductRetailerId { get; set; }
}

// ─── Mark Read ────────────────────────────────────────────────────────────────
public class MarkMessageReadRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
}

// ─── Media ────────────────────────────────────────────────────────────────────
public class WaUploadMediaRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
}

public class WaUploadMediaResponse
{
    public string? Id { get; set; }
}

public class WaMediaResponse
{
    public string? MessagingProduct { get; set; }
    public string? Url { get; set; }
    public string? MimeType { get; set; }
    public string? Sha256 { get; set; }
    public string? FileSize { get; set; }
    public string? Id { get; set; }
}

// ─── Phone Numbers ────────────────────────────────────────────────────────────
public class WaPhoneNumberDto
{
    public string? Id { get; set; }
    public string? DisplayPhoneNumber { get; set; }
    public string? VerifiedName { get; set; }
    public string? QualityRating { get; set; }
    public string? Status { get; set; }
    public string? NameStatus { get; set; }
    public string? CodeVerificationStatus { get; set; }
    public string? PlatformType { get; set; }
}

public class WaPhoneNumbersResponse
{
    public List<WaPhoneNumberDto>? Data { get; set; }
}

public class RequestVerificationCodeRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string CodeMethod { get; set; } = "SMS";
    public string Language { get; set; } = "en";
}

public class VerifyCodeRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class RegisterPhoneRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
    public string? DataLocalizationRegion { get; set; }
}

public class DeregisterPhoneRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}

public class UpdatePinRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
}

// ─── Business Profile ─────────────────────────────────────────────────────────
public class WaBusinessProfileDto
{
    public string? About { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? MessagingProduct { get; set; }
    public List<string>? Websites { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Vertical { get; set; }
}

public class WaBusinessProfileResponse
{
    public List<WaBusinessProfileDto>? Data { get; set; }
}

public class UpdateBusinessProfileRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? About { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public List<string>? Websites { get; set; }
    public string? Vertical { get; set; }
}

// ─── Templates ────────────────────────────────────────────────────────────────
public class WaTemplateDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? Category { get; set; }
    public string? Language { get; set; }
    public List<WaTemplateComponentDto>? Components { get; set; }
}

public class WaTemplateComponentDto
{
    public string Type { get; set; } = string.Empty;
    public string? Format { get; set; }
    public string? Text { get; set; }
    public List<WaTemplateBtnDto>? Buttons { get; set; }
}

public class WaTemplateBtnDto
{
    public string Type { get; set; } = string.Empty;
    public string? Text { get; set; }
    public string? Url { get; set; }
    public string? PhoneNumber { get; set; }
}

public class WaTemplatesResponse
{
    public List<WaTemplateDto>? Data { get; set; }
}

public class CreateTemplateRequest
{
    public string WabaId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "UTILITY";
    public string Language { get; set; } = "en_US";
    public List<WaTemplateComponentDto> Components { get; set; } = new();
}

public class UpdateTemplateRequest
{
    public string TemplateId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public List<WaTemplateComponentDto> Components { get; set; } = new();
}

// ─── QR Codes ─────────────────────────────────────────────────────────────────
public class WaQrCodeDto
{
    public string? Code { get; set; }
    public string? PrefilledMessage { get; set; }
    public string? DeepLinkUrl { get; set; }
    public string? QrImageUrl { get; set; }
}

public class WaQrCodesResponse
{
    public List<WaQrCodeDto>? Data { get; set; }
}

public class CreateQrCodeRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string PrefilledMessage { get; set; } = string.Empty;
    public string ImageFormat { get; set; } = "PNG";
}

public class UpdateQrCodeRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string QrCodeId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string PrefilledMessage { get; set; } = string.Empty;
}

// ─── Webhooks ─────────────────────────────────────────────────────────────────
public class WaWebhookSubscribeRequest
{
    public string AppId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string CallbackUrl { get; set; } = string.Empty;
    public string VerifyToken { get; set; } = string.Empty;
}

public class WaWebhookPayload
{
    public string? Object { get; set; }
    public List<WaWebhookEntry>? Entry { get; set; }
}

public class WaWebhookEntry
{
    public string? Id { get; set; }
    public List<WaWebhookChange>? Changes { get; set; }
}

public class WaWebhookChange
{
    public object? Value { get; set; }
    public string? Field { get; set; }
}

// ─── Interactive Location Request ─────────────────────────────────────────────
public class SendInteractiveLocationRequestRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
}

// ─── Interactive CTA URL ──────────────────────────────────────────────────────
public class SendInteractiveCtaUrlRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? HeaderText { get; set; }
    public string? FooterText { get; set; }
    public string DisplayText { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

// ─── Interactive Flow ─────────────────────────────────────────────────────────
public class SendInteractiveFlowRequest
{
    public string PhoneNumberId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string? HeaderText { get; set; }
    public string? FooterText { get; set; }
    public string FlowId { get; set; } = string.Empty;
    public string FlowToken { get; set; } = string.Empty;
    public string FlowCta { get; set; } = string.Empty;
    public string FlowAction { get; set; } = "navigate";
    public string Mode { get; set; } = "published";
}

// ─── WABA Info ────────────────────────────────────────────────────────────────
public class WabaInfoResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Currency { get; set; }
    public string? AccountReviewStatus { get; set; }
    public string? BusinessVerificationStatus { get; set; }
    public string? OwnershipType { get; set; }
}

// ─── Analytics ────────────────────────────────────────────────────────────────
public class WaAnalyticsRequest
{
    public string WabaId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public long Start { get; set; }
    public long End { get; set; }
    public string Granularity { get; set; } = "DAILY";
}

public class WaConversationAnalyticsRequest
{
    public string WabaId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public long Start { get; set; }
    public long End { get; set; }
    public string Granularity { get; set; } = "MONTHLY";
    public string Dimensions { get; set; } = "conversation_type,conversation_direction";
}

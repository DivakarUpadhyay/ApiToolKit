using ApiToolKit.Application.DTOs.Facebook.WhatsApp;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookWhatsAppService
{
    // Messages
    Task<Result<WhatsAppMessageResponse>> SendTextMessageAsync(SendTextMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendTemplateMessageAsync(SendTemplateMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendImageMessageAsync(SendImageMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendVideoMessageAsync(SendVideoMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendAudioMessageAsync(SendAudioMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendDocumentMessageAsync(SendDocumentMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendStickerMessageAsync(SendStickerMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendLocationMessageAsync(SendLocationMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendContactMessageAsync(SendContactMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendReactionMessageAsync(SendReactionMessageRequest request);
    Task<Result<WhatsAppMessageResponse>> SendInteractiveButtonsAsync(SendInteractiveButtonsRequest request);
    Task<Result<WhatsAppMessageResponse>> SendInteractiveListAsync(SendInteractiveListRequest request);
    Task<Result<WhatsAppMessageResponse>> SendInteractiveProductAsync(SendInteractiveProductRequest request);
    Task<Result<WhatsAppMessageResponse>> SendInteractiveCatalogAsync(SendInteractiveCatalogRequest request);
    Task<Result<bool>> MarkMessageReadAsync(MarkMessageReadRequest request);

    // Media
    Task<Result<WaUploadMediaResponse>> UploadMediaAsync(WaUploadMediaRequest request);
    Task<Result<WaMediaResponse>> GetMediaAsync(string mediaId, string accessToken);
    Task<Result<bool>> DeleteMediaAsync(string mediaId, string accessToken);

    // Phone Numbers
    Task<Result<WaPhoneNumbersResponse>> GetPhoneNumbersAsync(string wabaId, string accessToken);
    Task<Result<WaPhoneNumberDto>> GetPhoneNumberAsync(string phoneNumberId, string accessToken);
    Task<Result<bool>> RequestVerificationCodeAsync(RequestVerificationCodeRequest request);
    Task<Result<bool>> VerifyCodeAsync(VerifyCodeRequest request);
    Task<Result<bool>> RegisterPhoneNumberAsync(RegisterPhoneRequest request);
    Task<Result<bool>> DeregisterPhoneNumberAsync(DeregisterPhoneRequest request);
    Task<Result<bool>> UpdatePinAsync(UpdatePinRequest request);

    // Business Profile
    Task<Result<WaBusinessProfileResponse>> GetBusinessProfileAsync(string phoneNumberId, string accessToken);
    Task<Result<bool>> UpdateBusinessProfileAsync(UpdateBusinessProfileRequest request);

    // Templates
    Task<Result<WaTemplateDto>> CreateTemplateAsync(CreateTemplateRequest request);
    Task<Result<WaTemplatesResponse>> GetTemplatesAsync(string wabaId, string accessToken);
    Task<Result<WaTemplateDto>> GetTemplateAsync(string templateId, string accessToken);
    Task<Result<bool>> UpdateTemplateAsync(UpdateTemplateRequest request);
    Task<Result<bool>> DeleteTemplateAsync(string wabaId, string templateId, string templateName, string accessToken);

    // QR Codes
    Task<Result<WaQrCodeDto>> CreateQrCodeAsync(CreateQrCodeRequest request);
    Task<Result<WaQrCodesResponse>> GetQrCodesAsync(string phoneNumberId, string accessToken);
    Task<Result<WaQrCodeDto>> GetQrCodeAsync(string phoneNumberId, string qrCodeId, string accessToken);
    Task<Result<bool>> UpdateQrCodeAsync(UpdateQrCodeRequest request);
    Task<Result<bool>> DeleteQrCodeAsync(string phoneNumberId, string qrCodeId, string accessToken);

    // Webhooks
    Task<Result<bool>> SubscribeWebhookAsync(WaWebhookSubscribeRequest request);
    Task<Result<string>> VerifyWebhookAsync(string hubMode, string hubChallenge, string hubVerifyToken);
    Task<Result<bool>> ProcessWebhookPayloadAsync(WaWebhookPayload payload);

    // New interactive message types
    Task<Result<WhatsAppMessageResponse>> SendInteractiveLocationRequestAsync(SendInteractiveLocationRequestRequest request);
    Task<Result<WhatsAppMessageResponse>> SendInteractiveCtaUrlAsync(SendInteractiveCtaUrlRequest request);
    Task<Result<WhatsAppMessageResponse>> SendInteractiveFlowAsync(SendInteractiveFlowRequest request);

    // WABA info
    Task<Result<WabaInfoResponse>> GetWabaInfoAsync(string wabaId, string accessToken);

    // Analytics
    Task<Result<object>> GetAnalyticsAsync(WaAnalyticsRequest request);
    Task<Result<object>> GetConversationAnalyticsAsync(WaConversationAnalyticsRequest request);
}

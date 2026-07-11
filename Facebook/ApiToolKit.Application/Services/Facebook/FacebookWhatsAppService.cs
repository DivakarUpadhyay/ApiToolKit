using ApiToolKit.Application.DTOs.Facebook.WhatsApp;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppService : IFacebookWhatsAppService
{
    private readonly IWhatsAppApiClient _apiClient;
    private readonly IFacebookWhatsAppRepository _waRepo;
    private readonly IConfiguration _config;

    public FacebookWhatsAppService(IWhatsAppApiClient apiClient, IFacebookWhatsAppRepository waRepo, IConfiguration config)
    {
        _apiClient = apiClient;
        _waRepo = waRepo;
        _config = config;
    }

    private string ApiVersion => _config["WhatsApp:ApiVersion"] ?? "v21.0";
    private string WebhookVerifyToken => _config["WhatsApp:WebhookVerifyToken"] ?? string.Empty;

    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null)
    {
        await _waRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = apiName,
            RequestBody = request ?? string.Empty,
            ResponseBody = response ?? string.Empty,
            IsSuccess = success,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });
    }

    // ─── Messages ────────────────────────────────────────────────────────────

    public async Task<Result<WhatsAppMessageResponse>> SendTextMessageAsync(SendTextMessageRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "text",
            context = request.ReplyToMessageId != null ? new { message_id = request.ReplyToMessageId } : null,
            text = new { body = request.Body, preview_url = request.PreviewUrl }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendTextMessage", true, response: JsonConvert.SerializeObject(response));
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendTemplateMessageAsync(SendTemplateMessageRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "template",
            template = new
            {
                name = request.TemplateName,
                language = new { code = request.LanguageCode },
                components = request.Components
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendTemplateMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendImageMessageAsync(SendImageMessageRequest request)
    {
        object mediaObj = request.MediaId != null
            ? new { id = request.MediaId, caption = request.Caption }
            : new { link = request.Link, caption = request.Caption };

        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "image",
            context = request.ReplyToMessageId != null ? new { message_id = request.ReplyToMessageId } : null,
            image = mediaObj
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendImageMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendVideoMessageAsync(SendVideoMessageRequest request)
    {
        object mediaObj = request.MediaId != null
            ? new { id = request.MediaId, caption = request.Caption }
            : new { link = request.Link, caption = request.Caption };

        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "video",
            context = request.ReplyToMessageId != null ? new { message_id = request.ReplyToMessageId } : null,
            video = mediaObj
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendVideoMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendAudioMessageAsync(SendAudioMessageRequest request)
    {
        object mediaObj = request.MediaId != null
            ? (object)new { id = request.MediaId }
            : new { link = request.Link };

        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "audio",
            audio = mediaObj
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendAudioMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendDocumentMessageAsync(SendDocumentMessageRequest request)
    {
        object mediaObj = request.MediaId != null
            ? new { id = request.MediaId, caption = request.Caption, filename = request.Filename }
            : new { link = request.Link, caption = request.Caption, filename = request.Filename };

        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "document",
            document = mediaObj
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendDocumentMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendStickerMessageAsync(SendStickerMessageRequest request)
    {
        object mediaObj = request.MediaId != null
            ? (object)new { id = request.MediaId }
            : new { link = request.Link };

        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "sticker",
            sticker = mediaObj
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendStickerMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendLocationMessageAsync(SendLocationMessageRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "location",
            location = new
            {
                longitude = request.Longitude,
                latitude = request.Latitude,
                name = request.Name,
                address = request.Address
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendLocationMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendContactMessageAsync(SendContactMessageRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "contacts",
            contacts = new[]
            {
                new
                {
                    name = new { first_name = request.FirstName, last_name = request.LastName },
                    phones = request.Phone != null ? new[] { new { phone = request.Phone } } : Array.Empty<object>(),
                    emails = request.Email != null ? new[] { new { email = request.Email } } : Array.Empty<object>()
                }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendContactMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendReactionMessageAsync(SendReactionMessageRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "reaction",
            reaction = new { message_id = request.MessageId, emoji = request.Emoji }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendReactionMessage", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveButtonsAsync(SendInteractiveButtonsRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "button",
                header = request.HeaderText != null ? new { type = "text", text = request.HeaderText } : null,
                body = new { text = request.BodyText },
                footer = request.FooterText != null ? new { text = request.FooterText } : null,
                action = new
                {
                    buttons = request.Buttons.Select(b => new
                    {
                        type = "reply",
                        reply = new { id = b.Id, title = b.Title }
                    }).ToList()
                }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveButtons", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveListAsync(SendInteractiveListRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "list",
                header = request.HeaderText != null ? new { type = "text", text = request.HeaderText } : null,
                body = new { text = request.BodyText },
                footer = request.FooterText != null ? new { text = request.FooterText } : null,
                action = new
                {
                    button = request.ButtonText,
                    sections = request.Sections.Select(s => new
                    {
                        title = s.Title,
                        rows = s.Rows.Select(r => new { id = r.Id, title = r.Title, description = r.Description }).ToList()
                    }).ToList()
                }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveList", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveProductAsync(SendInteractiveProductRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "product",
                body = request.BodyText != null ? new { text = request.BodyText } : null,
                footer = request.FooterText != null ? new { text = request.FooterText } : null,
                action = new { catalog_id = request.CatalogId, product_retailer_id = request.ProductRetailerId }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveProduct", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveCatalogAsync(SendInteractiveCatalogRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "catalog_message",
                body = request.BodyText != null ? new { text = request.BodyText } : null,
                footer = request.FooterText != null ? new { text = request.FooterText } : null,
                action = new
                {
                    name = "catalog_message",
                    parameters = new
                    {
                        thumbnail_product_retailer_id = request.ThumbnailProductRetailerId
                    }
                }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveCatalog", true);
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<bool>> MarkMessageReadAsync(MarkMessageReadRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            status = "read",
            message_id = request.MessageId
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("MarkMessageRead", true);
        return Result<bool>.Ok(true);
    }

    // ─── Media ───────────────────────────────────────────────────────────────

    public async Task<Result<WaUploadMediaResponse>> UploadMediaAsync(WaUploadMediaRequest request)
    {
        using var httpClient = new System.Net.Http.HttpClient();
        var fileBytes = await httpClient.GetByteArrayAsync(request.FileUrl);

        var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(fileBytes), "file", "upload");
        content.Add(new System.Net.Http.StringContent(request.MimeType), "type");
        content.Add(new System.Net.Http.StringContent("whatsapp"), "messaging_product");

        var response = await _apiClient.PostMultipartAsync<dynamic>($"/{request.PhoneNumberId}/media", request.AccessToken, content);
        if (response == null) return Result<WaUploadMediaResponse>.Fail("No response from WhatsApp API.");

        await LogAsync("UploadMedia", true);
        return Result<WaUploadMediaResponse>.Ok(new WaUploadMediaResponse { Id = response?.id?.ToString() });
    }

    public async Task<Result<WaMediaResponse>> GetMediaAsync(string mediaId, string accessToken)
    {
        var response = await _apiClient.GetAsync<dynamic>($"/{mediaId}", accessToken);
        if (response == null) return Result<WaMediaResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaMediaResponse>(JsonConvert.SerializeObject(response)) ?? new WaMediaResponse();
        await LogAsync("GetMedia", true);
        return Result<WaMediaResponse>.Ok(result);
    }

    public async Task<Result<bool>> DeleteMediaAsync(string mediaId, string accessToken)
    {
        var response = await _apiClient.DeleteAsync<dynamic>($"/{mediaId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("DeleteMedia", true);
        return Result<bool>.Ok(true);
    }

    // ─── Phone Numbers ────────────────────────────────────────────────────────

    public async Task<Result<WaPhoneNumbersResponse>> GetPhoneNumbersAsync(string wabaId, string accessToken)
    {
        var response = await _apiClient.GetAsync<dynamic>($"/{wabaId}/phone_numbers", accessToken);
        if (response == null) return Result<WaPhoneNumbersResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaPhoneNumbersResponse>(JsonConvert.SerializeObject(response)) ?? new WaPhoneNumbersResponse();
        await LogAsync("GetPhoneNumbers", true);
        return Result<WaPhoneNumbersResponse>.Ok(result);
    }

    public async Task<Result<WaPhoneNumberDto>> GetPhoneNumberAsync(string phoneNumberId, string accessToken)
    {
        var queryParams = new Dictionary<string, string> { ["fields"] = "id,display_phone_number,verified_name,quality_rating,status,name_status,code_verification_status,platform_type" };
        var response = await _apiClient.GetAsync<dynamic>($"/{phoneNumberId}", accessToken, queryParams);
        if (response == null) return Result<WaPhoneNumberDto>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaPhoneNumberDto>(JsonConvert.SerializeObject(response)) ?? new WaPhoneNumberDto();
        await LogAsync("GetPhoneNumber", true);
        return Result<WaPhoneNumberDto>.Ok(result);
    }

    public async Task<Result<bool>> RequestVerificationCodeAsync(RequestVerificationCodeRequest request)
    {
        var body = new { code_method = request.CodeMethod, language = request.Language };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/request_code", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("RequestVerificationCode", true);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> VerifyCodeAsync(VerifyCodeRequest request)
    {
        var body = new { code = request.Code };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/verify_code", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("VerifyCode", true);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> RegisterPhoneNumberAsync(RegisterPhoneRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            pin = request.Pin,
            data_localization_region = request.DataLocalizationRegion
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/register", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("RegisterPhoneNumber", true);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeregisterPhoneNumberAsync(DeregisterPhoneRequest request)
    {
        var body = new { messaging_product = "whatsapp" };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/deregister", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("DeregisterPhoneNumber", true);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> UpdatePinAsync(UpdatePinRequest request)
    {
        var body = new { pin = request.Pin };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("UpdatePin", true);
        return Result<bool>.Ok(true);
    }

    // ─── Business Profile ─────────────────────────────────────────────────────

    public async Task<Result<WaBusinessProfileResponse>> GetBusinessProfileAsync(string phoneNumberId, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "about,address,description,email,profile_picture_url,websites,vertical,messaging_product"
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{phoneNumberId}/whatsapp_business_profile", accessToken, queryParams);
        if (response == null) return Result<WaBusinessProfileResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaBusinessProfileResponse>(JsonConvert.SerializeObject(response)) ?? new WaBusinessProfileResponse();
        await LogAsync("GetBusinessProfile", true);
        return Result<WaBusinessProfileResponse>.Ok(result);
    }

    public async Task<Result<bool>> UpdateBusinessProfileAsync(UpdateBusinessProfileRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            about = request.About,
            address = request.Address,
            description = request.Description,
            email = request.Email,
            websites = request.Websites,
            vertical = request.Vertical
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/whatsapp_business_profile", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("UpdateBusinessProfile", true);
        return Result<bool>.Ok(true);
    }

    // ─── Templates ────────────────────────────────────────────────────────────

    public async Task<Result<WaTemplateDto>> CreateTemplateAsync(CreateTemplateRequest request)
    {
        var body = new
        {
            name = request.Name,
            category = request.Category,
            language = request.Language,
            components = request.Components
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.WabaId}/message_templates", request.AccessToken, body);
        if (response == null) return Result<WaTemplateDto>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaTemplateDto>(JsonConvert.SerializeObject(response)) ?? new WaTemplateDto();
        await LogAsync("CreateTemplate", true);
        return Result<WaTemplateDto>.Ok(result);
    }

    public async Task<Result<WaTemplatesResponse>> GetTemplatesAsync(string wabaId, string accessToken)
    {
        var response = await _apiClient.GetAsync<dynamic>($"/{wabaId}/message_templates", accessToken);
        if (response == null) return Result<WaTemplatesResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaTemplatesResponse>(JsonConvert.SerializeObject(response)) ?? new WaTemplatesResponse();
        await LogAsync("GetTemplates", true);
        return Result<WaTemplatesResponse>.Ok(result);
    }

    public async Task<Result<WaTemplateDto>> GetTemplateAsync(string templateId, string accessToken)
    {
        var response = await _apiClient.GetAsync<dynamic>($"/{templateId}", accessToken);
        if (response == null) return Result<WaTemplateDto>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaTemplateDto>(JsonConvert.SerializeObject(response)) ?? new WaTemplateDto();
        await LogAsync("GetTemplate", true);
        return Result<WaTemplateDto>.Ok(result);
    }

    public async Task<Result<bool>> UpdateTemplateAsync(UpdateTemplateRequest request)
    {
        var body = new { components = request.Components };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.TemplateId}", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("UpdateTemplate", true);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteTemplateAsync(string templateId, string accessToken)
    {
        var response = await _apiClient.DeleteAsync<dynamic>($"/{templateId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("DeleteTemplate", true);
        return Result<bool>.Ok(true);
    }

    // ─── QR Codes ─────────────────────────────────────────────────────────────

    public async Task<Result<WaQrCodeDto>> CreateQrCodeAsync(CreateQrCodeRequest request)
    {
        var body = new { prefilled_message = request.PrefilledMessage, generate_qr_image = request.ImageFormat };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/message_qrdls", request.AccessToken, body);
        if (response == null) return Result<WaQrCodeDto>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaQrCodeDto>(JsonConvert.SerializeObject(response)) ?? new WaQrCodeDto();
        await LogAsync("CreateQrCode", true);
        return Result<WaQrCodeDto>.Ok(result);
    }

    public async Task<Result<WaQrCodesResponse>> GetQrCodesAsync(string phoneNumberId, string accessToken)
    {
        var response = await _apiClient.GetAsync<dynamic>($"/{phoneNumberId}/message_qrdls", accessToken);
        if (response == null) return Result<WaQrCodesResponse>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaQrCodesResponse>(JsonConvert.SerializeObject(response)) ?? new WaQrCodesResponse();
        await LogAsync("GetQrCodes", true);
        return Result<WaQrCodesResponse>.Ok(result);
    }

    public async Task<Result<WaQrCodeDto>> GetQrCodeAsync(string phoneNumberId, string qrCodeId, string accessToken)
    {
        var response = await _apiClient.GetAsync<dynamic>($"/{phoneNumberId}/message_qrdls/{qrCodeId}", accessToken);
        if (response == null) return Result<WaQrCodeDto>.Fail("No response from WhatsApp API.");

        var result = JsonConvert.DeserializeObject<WaQrCodeDto>(JsonConvert.SerializeObject(response)) ?? new WaQrCodeDto();
        await LogAsync("GetQrCode", true);
        return Result<WaQrCodeDto>.Ok(result);
    }

    public async Task<Result<bool>> UpdateQrCodeAsync(UpdateQrCodeRequest request)
    {
        var body = new { prefilled_message = request.PrefilledMessage };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/message_qrdls/{request.QrCodeId}", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("UpdateQrCode", true);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteQrCodeAsync(string phoneNumberId, string qrCodeId, string accessToken)
    {
        var response = await _apiClient.DeleteAsync<dynamic>($"/{phoneNumberId}/message_qrdls/{qrCodeId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("DeleteQrCode", true);
        return Result<bool>.Ok(true);
    }

    // ─── Webhooks ─────────────────────────────────────────────────────────────

    public async Task<Result<bool>> SubscribeWebhookAsync(WaWebhookSubscribeRequest request)
    {
        var body = new
        {
            object_type = "whatsapp_business_account",
            callback_url = request.CallbackUrl,
            verify_token = request.VerifyToken,
            fields = new[] { "messages" }
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.AppId}/subscriptions", request.AccessToken, body);
        if (response == null) return Result<bool>.Fail("No response from WhatsApp API.");

        await LogAsync("SubscribeWebhook", true);
        return Result<bool>.Ok(true);
    }

    public Task<Result<string>> VerifyWebhookAsync(string hubMode, string hubChallenge, string hubVerifyToken)
    {
        if (hubMode == "subscribe" && hubVerifyToken == WebhookVerifyToken)
            return Task.FromResult(Result<string>.Ok(hubChallenge));

        return Task.FromResult(Result<string>.Fail("Webhook verification failed."));
    }

    public async Task<Result<bool>> ProcessWebhookPayloadAsync(WaWebhookPayload payload)
    {
        await LogAsync("ProcessWebhookPayload", true, request: JsonConvert.SerializeObject(payload));
        return Result<bool>.Ok(true);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveLocationRequestAsync(SendInteractiveLocationRequestRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "location_request_message",
                body = new { text = request.BodyText },
                action = new { name = "send_location" }
            }
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");
        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveLocationRequest", true, response: JsonConvert.SerializeObject(response));
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveCtaUrlAsync(SendInteractiveCtaUrlRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "cta_url",
                header = request.HeaderText != null ? new { type = "text", text = request.HeaderText } : null,
                body = new { text = request.BodyText },
                footer = request.FooterText != null ? new { text = request.FooterText } : null,
                action = new
                {
                    name = "cta_url",
                    parameters = new { display_text = request.DisplayText, url = request.Url }
                }
            }
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");
        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveCtaUrl", true, response: JsonConvert.SerializeObject(response));
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WhatsAppMessageResponse>> SendInteractiveFlowAsync(SendInteractiveFlowRequest request)
    {
        var body = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = request.To,
            type = "interactive",
            interactive = new
            {
                type = "flow",
                header = request.HeaderText != null ? new { type = "text", text = request.HeaderText } : null,
                body = new { text = request.BodyText },
                footer = request.FooterText != null ? new { text = request.FooterText } : null,
                action = new
                {
                    name = "flow",
                    parameters = new
                    {
                        flow_message_version = "3",
                        flow_token = request.FlowToken,
                        flow_id = request.FlowId,
                        flow_cta = request.FlowCta,
                        flow_action = request.FlowAction,
                        mode = request.Mode
                    }
                }
            }
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PhoneNumberId}/messages", request.AccessToken, body);
        if (response == null) return Result<WhatsAppMessageResponse>.Fail("No response from WhatsApp API.");
        var result = JsonConvert.DeserializeObject<WhatsAppMessageResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppMessageResponse();
        await LogAsync("SendInteractiveFlow", true, response: JsonConvert.SerializeObject(response));
        return Result<WhatsAppMessageResponse>.Ok(result);
    }

    public async Task<Result<WabaInfoResponse>> GetWabaInfoAsync(string wabaId, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,currency,account_review_status,business_verification_status,ownership_type,primary_funding_id"
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{wabaId}", accessToken, queryParams);
        if (response == null) return Result<WabaInfoResponse>.Fail("No response from WhatsApp API.");
        var result = JsonConvert.DeserializeObject<WabaInfoResponse>(JsonConvert.SerializeObject(response)) ?? new WabaInfoResponse();
        await LogAsync("GetWabaInfo", true);
        return Result<WabaInfoResponse>.Ok(result);
    }

    public async Task<Result<object>> GetAnalyticsAsync(WaAnalyticsRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["start"] = request.Start.ToString(),
            ["end"] = request.End.ToString(),
            ["granularity"] = request.Granularity
        };
        var response = await _apiClient.GetAsync<object>($"/{request.WabaId}/analytics", request.AccessToken, queryParams);
        if (response == null) return Result<object>.Fail("No response from WhatsApp API.");
        await LogAsync("GetAnalytics", true);
        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetConversationAnalyticsAsync(WaConversationAnalyticsRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["start"] = request.Start.ToString(),
            ["end"] = request.End.ToString(),
            ["granularity"] = request.Granularity,
            ["dimensions"] = $"[\"{string.Join("\",\"", request.Dimensions.Split(','))}\"]"
        };
        var response = await _apiClient.GetAsync<object>($"/{request.WabaId}/conversation_analytics", request.AccessToken, queryParams);
        if (response == null) return Result<object>.Fail("No response from WhatsApp API.");
        await LogAsync("GetConversationAnalytics", true);
        return Result<object>.Ok(response);
    }
}

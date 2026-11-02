using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppMessagingService : WhatsAppServiceBase, IFacebookWhatsAppMessagingService
{
    public FacebookWhatsAppMessagingService(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo) : base(api, repo) { }

    // ─── Messages ────────────────────────────────────────────────────────────

    public Task<Result<object>> SendTypingIndicatorAsync(WaTypingIndicatorRequest r) =>
        RunAsync("SendTypingIndicator", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, new
        {
            messaging_product = "whatsapp",
            status = "read",
            message_id = r.MessageId,
            typing_indicator = new { type = "text" }
        }), r);

    public Task<Result<object>> SendRawMessageAsync(WaSendRawMessageRequest r)
    {
        var body = MessageBody(r.To, r.Recipient, r.Type, r.RecipientType);
        body[r.Type] = r.Content;
        if (!string.IsNullOrEmpty(r.ReplyToMessageId)) body["context"] = new { message_id = r.ReplyToMessageId };
        if (!string.IsNullOrEmpty(r.BizOpaqueCallbackData)) body["biz_opaque_callback_data"] = r.BizOpaqueCallbackData;
        return RunAsync("SendRawMessage", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }

    public Task<Result<object>> SendCarouselAsync(WaSendCarouselRequest r)
    {
        var body = MessageBody(r.To, null, "interactive");
        body["interactive"] = new
        {
            type = "carousel",
            body = new { text = r.BodyText },
            action = new { cards = r.Cards }
        };
        return RunAsync("SendCarousel", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }

    public Task<Result<object>> SendMultiProductAsync(WaSendMultiProductRequest r)
    {
        var interactive = new Dictionary<string, object?>
        {
            ["type"] = "product_list",
            ["header"] = new { type = "text", text = r.HeaderText },
            ["body"] = new { text = r.BodyText },
            ["action"] = new
            {
                catalog_id = r.CatalogId,
                sections = r.Sections.Select(s => new
                {
                    title = s.Title,
                    product_items = s.ProductRetailerIds.Select(id => new { product_retailer_id = id })
                })
            }
        };
        if (!string.IsNullOrEmpty(r.FooterText)) interactive["footer"] = new { text = r.FooterText };

        var body = MessageBody(r.To, null, "interactive");
        body["interactive"] = interactive;
        return RunAsync("SendMultiProduct", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }

    public Task<Result<object>> SendMarketingMessageAsync(WaSendMarketingMessageRequest r)
    {
        var body = MessageBody(r.To, r.Recipient, "template");
        body["template"] = new
        {
            name = r.TemplateName,
            language = new { code = r.LanguageCode },
            components = r.Components
        };
        if (r.ProductPolicy != null) body["product_policy"] = r.ProductPolicy;
        if (r.MessageActivitySharing.HasValue) body["message_activity_sharing"] = r.MessageActivitySharing.Value;
        return RunAsync("SendMarketingMessage", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/marketing_messages", r.AccessToken, body), r);
    }

    // ─── Media ───────────────────────────────────────────────────────────────

    public async Task<Result<WaMediaFile>> DownloadMediaAsync(string mediaId, string accessToken)
    {
        try
        {
            // Step 1: resolve the short-lived media URL; step 2: fetch it with the same token.
            var meta = await Api.GetAsync<JObject>($"/{mediaId}", accessToken);
            var url = meta?["url"]?.ToString();
            if (string.IsNullOrEmpty(url)) return Result<WaMediaFile>.Fail("Media URL not found.");

            var (content, contentType) = await Api.DownloadAsync(url, accessToken);
            var mime = meta?["mime_type"]?.ToString() ?? contentType;
            return Result<WaMediaFile>.Ok(new WaMediaFile { Content = content, ContentType = mime, FileName = $"{mediaId}{ExtensionFor(mime)}" });
        }
        catch (WhatsAppApiException ex) { return Result<WaMediaFile>.Fail(ex.Message, ex.Code); }
        catch (Exception ex) { return Result<WaMediaFile>.Fail(ex.Message); }
    }

    private static string ExtensionFor(string mime) => mime.Split(';')[0].Trim() switch
    {
        "image/jpeg" => ".jpg",
        "image/png" => ".png",
        "image/webp" => ".webp",
        "video/mp4" => ".mp4",
        "video/3gpp" => ".3gp",
        "audio/ogg" => ".ogg",
        "audio/mpeg" => ".mp3",
        "audio/mp4" => ".m4a",
        "audio/aac" => ".aac",
        "audio/amr" => ".amr",
        "application/pdf" => ".pdf",
        "text/plain" => ".txt",
        _ => string.Empty
    };

    // ─── Block users ─────────────────────────────────────────────────────────

    private static object BlockBody(WaUserListRequest r) => new
    {
        messaging_product = "whatsapp",
        block_users = r.Users.Select(u => new { user = u })
    };

    public Task<Result<object>> BlockUsersAsync(WaUserListRequest r) =>
        RunAsync("BlockUsers", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/block_users", r.AccessToken, BlockBody(r)), r);

    public Task<Result<object>> UnblockUsersAsync(WaUserListRequest r) =>
        RunAsync("UnblockUsers", () => Api.DeleteAsync<object>($"/{r.PhoneNumberId}/block_users", r.AccessToken, body: BlockBody(r)), r);

    public Task<Result<object>> GetBlockedUsersAsync(string phoneNumberId, string accessToken, int? limit, string? after, string? before) =>
        RunAsync("GetBlockedUsers", () => Api.GetAsync<object>($"/{phoneNumberId}/block_users", accessToken, WhatsAppQuery.Paging(limit, after, before)));

    // ─── Templates ───────────────────────────────────────────────────────────

    public Task<Result<object>> SearchTemplatesAsync(WaTemplateSearchQuery q)
    {
        var query = WhatsAppQuery.Paging(q.Limit, q.After, q.Before);
        if (!string.IsNullOrEmpty(q.Name)) query["name"] = q.Name;
        if (!string.IsNullOrEmpty(q.Status)) query["status"] = q.Status;
        if (!string.IsNullOrEmpty(q.Category)) query["category"] = q.Category;
        if (!string.IsNullOrEmpty(q.Language)) query["language"] = q.Language;
        if (!string.IsNullOrEmpty(q.Fields)) query["fields"] = q.Fields;
        return RunAsync("SearchTemplates", () => Api.GetAsync<object>($"/{q.WabaId}/message_templates", q.AccessToken, query));
    }

    public Task<Result<object>> GetTemplateLibraryAsync(WaTemplateLibraryQuery q)
    {
        var query = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(q.Search)) query["search"] = q.Search;
        if (!string.IsNullOrEmpty(q.Topic)) query["topic"] = q.Topic;
        if (!string.IsNullOrEmpty(q.Usecase)) query["usecase"] = q.Usecase;
        if (!string.IsNullOrEmpty(q.Industry)) query["industry"] = q.Industry;
        if (!string.IsNullOrEmpty(q.Language)) query["language"] = q.Language;
        if (!string.IsNullOrEmpty(q.Name)) query["name"] = q.Name;
        return RunAsync("GetTemplateLibrary", () => Api.GetAsync<object>("/message_template_library", q.AccessToken, query));
    }

    public Task<Result<object>> CreateTemplateFromLibraryAsync(WaCreateTemplateFromLibraryRequest r)
    {
        var body = new Dictionary<string, object?>
        {
            ["name"] = r.Name,
            ["language"] = r.Language,
            ["category"] = r.Category,
            ["library_template_name"] = r.LibraryTemplateName
        };
        // Meta expects the button inputs as a JSON *string*.
        if (r.LibraryTemplateButtonInputs != null) body["library_template_button_inputs"] = r.LibraryTemplateButtonInputs.ToString(Newtonsoft.Json.Formatting.None);
        if (r.LibraryTemplateBodyInputs != null) body["library_template_body_inputs"] = r.LibraryTemplateBodyInputs;
        return RunAsync("CreateTemplateFromLibrary", () => Api.PostAsync<object>($"/{r.WabaId}/message_templates", r.AccessToken, body), r);
    }

    public Task<Result<object>> DeleteTemplatesByNameAsync(string wabaId, string name, string accessToken) =>
        RunAsync("DeleteTemplatesByName", () => Api.DeleteAsync<object>($"/{wabaId}/message_templates", accessToken,
            new Dictionary<string, string> { ["name"] = name }), new { wabaId, name });

    public Task<Result<object>> BulkDeleteTemplatesAsync(string wabaId, List<string> templateIds, string accessToken)
    {
        if (templateIds.Count is 0 or > 100)
            return Task.FromResult(Result<object>.Fail("Provide between 1 and 100 template IDs."));
        return RunAsync("BulkDeleteTemplates", () => Api.DeleteAsync<object>($"/{wabaId}/message_templates", accessToken,
            new Dictionary<string, string> { ["hsm_ids"] = $"[{string.Join(",", templateIds)}]" }), new { wabaId, templateIds });
    }

    public Task<Result<object>> MigrateTemplatesAsync(WaMigrateTemplatesRequest r)
    {
        var body = new Dictionary<string, object?> { ["source_waba_id"] = r.SourceWabaId };
        if (r.PageNumber.HasValue) body["page_number"] = r.PageNumber.Value;
        if (r.Count.HasValue) body["count"] = r.Count.Value;
        if (r.TemplateIds is { Count: > 0 }) body["template_ids"] = r.TemplateIds;
        return RunAsync("MigrateTemplates", () => Api.PostAsync<object>($"/{r.DestinationWabaId}/migrate_message_templates", r.AccessToken, body), r);
    }
}

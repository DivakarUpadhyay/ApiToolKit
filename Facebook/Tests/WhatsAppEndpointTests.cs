using ApiToolKit.Application.DTOs.Facebook.WhatsApp;
using ApiToolKit.Application.DTOs.Facebook.WhatsAppCatalog;
using ApiToolKit.Application.DTOs.Facebook.WhatsAppFlows;
using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;
using static Facebook.WhatsAppTests.Harness;

namespace Facebook.WhatsAppTests;

/// <summary>
/// Every WhatsApp service method is called against a fake Graph server; the test checks the HTTP
/// method, the path + query (Graph v26.0), the Bearer token and — where shape matters — the body.
/// </summary>
public class WhatsAppEndpointTests
{
    public record Case(string Name, Func<Harness, Task<object>> Call, string Method, string PathAndQuery, Action<Harness>? Check = null)
    {
        public override string ToString() => Name;
    }

    private static Case C(string name, Func<Harness, Task<object>> call, string method, string path, Action<Harness>? check = null) =>
        new(name, call, method, path, check);

    private static Action<Harness> Json(Action<JObject> assert) => h => assert(h.LastJson);

    public static IEnumerable<object[]> Cases() => AllCases().Select(c => new object[] { c });

    private static IEnumerable<Case> AllCases()
    {
        const string P = "/v26.0";

        // ─── Existing: messages ──────────────────────────────────────────────
        yield return C("text", async h => await h.Main.SendTextMessageAsync(Req<SendTextMessageRequest>(r => r.ReplyToMessageId = "wamid.0")), "POST", $"{P}/PN1/messages",
            Json(j => { j["type"]!.ToString().Should().Be("text"); j["text"]!["body"]!.ToString().Should().Be("hi"); j["context"]!["message_id"]!.ToString().Should().Be("wamid.0"); }));
        yield return C("text omits null context", async h => await h.Main.SendTextMessageAsync(Req<SendTextMessageRequest>()), "POST", $"{P}/PN1/messages",
            Json(j => j.ContainsKey("context").Should().BeFalse()));
        yield return C("template", async h => await h.Main.SendTemplateMessageAsync(Req<SendTemplateMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("template")));
        yield return C("image", async h => await h.Main.SendImageMessageAsync(Req<SendImageMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("image")));
        yield return C("video", async h => await h.Main.SendVideoMessageAsync(Req<SendVideoMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("video")));
        yield return C("audio", async h => await h.Main.SendAudioMessageAsync(Req<SendAudioMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("audio")));
        yield return C("document", async h => await h.Main.SendDocumentMessageAsync(Req<SendDocumentMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("document")));
        yield return C("sticker", async h => await h.Main.SendStickerMessageAsync(Req<SendStickerMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("sticker")));
        yield return C("location", async h => await h.Main.SendLocationMessageAsync(Req<SendLocationMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("location")));
        yield return C("contact", async h => await h.Main.SendContactMessageAsync(Req<SendContactMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("contacts")));
        yield return C("reaction", async h => await h.Main.SendReactionMessageAsync(Req<SendReactionMessageRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["type"]!.ToString().Should().Be("reaction")));
        yield return C("buttons", async h => await h.Main.SendInteractiveButtonsAsync(Req<SendInteractiveButtonsRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("button")));
        yield return C("list", async h => await h.Main.SendInteractiveListAsync(Req<SendInteractiveListRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("list")));
        yield return C("product", async h => await h.Main.SendInteractiveProductAsync(Req<SendInteractiveProductRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("product")));
        yield return C("catalog", async h => await h.Main.SendInteractiveCatalogAsync(Req<SendInteractiveCatalogRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("catalog_message")));
        yield return C("location request", async h => await h.Main.SendInteractiveLocationRequestAsync(Req<SendInteractiveLocationRequestRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("location_request_message")));
        yield return C("cta url", async h => await h.Main.SendInteractiveCtaUrlAsync(Req<SendInteractiveCtaUrlRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("cta_url")));
        yield return C("flow message", async h => await h.Main.SendInteractiveFlowAsync(Req<SendInteractiveFlowRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["interactive"]!["type"]!.ToString().Should().Be("flow")));
        yield return C("mark read", async h => await h.Main.MarkMessageReadAsync(Req<MarkMessageReadRequest>()), "POST", $"{P}/PN1/messages", Json(j => j["status"]!.ToString().Should().Be("read")));

        // ─── Existing: media, phone numbers, profile ─────────────────────────
        yield return C("get media", async h => await h.Main.GetMediaAsync("MEDIA1", "TOKEN"), "GET", $"{P}/MEDIA1");
        yield return C("delete media", async h => await h.Main.DeleteMediaAsync("MEDIA1", "TOKEN"), "DELETE", $"{P}/MEDIA1");
        yield return C("phone numbers", async h => await h.Main.GetPhoneNumbersAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1/phone_numbers");
        yield return C("phone number", async h => await h.Main.GetPhoneNumberAsync("PN1", "TOKEN"), "GET", $"{P}/PN1?fields=");
        yield return C("request code", async h => await h.Main.RequestVerificationCodeAsync(Req<RequestVerificationCodeRequest>()), "POST", $"{P}/PN1/request_code");
        yield return C("verify code", async h => await h.Main.VerifyCodeAsync(Req<VerifyCodeRequest>()), "POST", $"{P}/PN1/verify_code");
        yield return C("register", async h => await h.Main.RegisterPhoneNumberAsync(Req<RegisterPhoneRequest>(r => r.Pin = "123456")), "POST", $"{P}/PN1/register", Json(j => j["pin"]!.ToString().Should().Be("123456")));
        yield return C("deregister", async h => await h.Main.DeregisterPhoneNumberAsync(Req<DeregisterPhoneRequest>()), "POST", $"{P}/PN1/deregister");
        yield return C("2fa pin", async h => await h.Main.UpdatePinAsync(Req<UpdatePinRequest>(r => r.Pin = "654321")), "POST", $"{P}/PN1", Json(j => j["pin"]!.ToString().Should().Be("654321")));
        yield return C("get business profile", async h => await h.Main.GetBusinessProfileAsync("PN1", "TOKEN"), "GET", $"{P}/PN1/whatsapp_business_profile?fields=");
        yield return C("update business profile", async h => await h.Main.UpdateBusinessProfileAsync(Req<UpdateBusinessProfileRequest>()), "POST", $"{P}/PN1/whatsapp_business_profile");
        yield return C("waba info", async h => await h.Main.GetWabaInfoAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1?fields=");

        // ─── Existing: templates (incl. delete fix) ──────────────────────────
        yield return C("create template", async h => await h.Main.CreateTemplateAsync(Req<CreateTemplateRequest>()), "POST", $"{P}/WABA1/message_templates");
        yield return C("list templates", async h => await h.Main.GetTemplatesAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1/message_templates");
        yield return C("get template", async h => await h.Main.GetTemplateAsync("TPL1", "TOKEN"), "GET", $"{P}/TPL1");
        yield return C("update template", async h => await h.Main.UpdateTemplateAsync(Req<UpdateTemplateRequest>()), "POST", $"{P}/TPL1");
        yield return C("FIX delete template via WABA edge", async h => await h.Main.DeleteTemplateAsync("WABA1", "TPL1", "order_update", "TOKEN"), "DELETE", $"{P}/WABA1/message_templates?hsm_id=TPL1&name=order_update");

        // ─── Existing: QR codes (incl. update fix) ───────────────────────────
        yield return C("create qr", async h => await h.Main.CreateQrCodeAsync(Req<CreateQrCodeRequest>(r => r.PrefilledMessage = "Hi")), "POST", $"{P}/PN1/message_qrdls",
            Json(j => j["generate_qr_image"]!.ToString().Should().Be("PNG")));
        yield return C("list qr", async h => await h.Main.GetQrCodesAsync("PN1", "TOKEN"), "GET", $"{P}/PN1/message_qrdls");
        yield return C("get qr", async h => await h.Main.GetQrCodeAsync("PN1", "QR1", "TOKEN"), "GET", $"{P}/PN1/message_qrdls/QR1");
        yield return C("FIX update qr posts code in body", async h => await h.Main.UpdateQrCodeAsync(Req<UpdateQrCodeRequest>(r => r.PrefilledMessage = "New")), "POST", $"{P}/PN1/message_qrdls",
            Json(j => { j["code"]!.ToString().Should().Be("QR1"); j["prefilled_message"]!.ToString().Should().Be("New"); }));
        yield return C("delete qr", async h => await h.Main.DeleteQrCodeAsync("PN1", "QR1", "TOKEN"), "DELETE", $"{P}/PN1/message_qrdls/QR1");

        // ─── Existing: webhook + analytics (fixes) ───────────────────────────
        yield return C("FIX app webhook subscription uses 'object'", async h => await h.Main.SubscribeWebhookAsync(Req<WaWebhookSubscribeRequest>(r => { r.CallbackUrl = "https://x/cb"; r.VerifyToken = "v"; r.Fields = new() { "messages", "message_template_status_update" }; })),
            "POST", $"{P}/APP1/subscriptions",
            Json(j => { j["object"]!.ToString().Should().Be("whatsapp_business_account"); j.ContainsKey("object_type").Should().BeFalse(); j["fields"]!.ToString().Should().Be("messages,message_template_status_update"); }));
        yield return C("FIX messaging analytics is a field", async h => await h.Main.GetAnalyticsAsync(Req<WaAnalyticsRequest>(r => { r.Start = 1; r.End = 2; r.PhoneNumbers = new() { "155" }; r.ProductTypes = new() { "0", "2" }; })),
            "GET", $"{P}/WABA1?fields=analytics.start(1).end(2).granularity(DAY).phone_numbers([\"155\"]).product_types([0,2])");
        yield return C("FIX conversation analytics is a field", async h => await h.Main.GetConversationAnalyticsAsync(Req<WaConversationAnalyticsRequest>(r => { r.Start = 1; r.End = 2; })),
            "GET", $"{P}/WABA1?fields=conversation_analytics.start(1).end(2).granularity(MONTHLY).dimensions([\"CONVERSATION_TYPE\",\"CONVERSATION_DIRECTION\"])");

        // ─── Catalog (fixes + new) ───────────────────────────────────────────
        yield return C("FIX catalogs connected to WABA", async h => await h.Catalog.GetCatalogsAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1/product_catalogs?fields=id,name,vertical");
        yield return C("FIX create catalog on business", async h => await h.Catalog.CreateCatalogAsync("BIZ1", new CreateWabaCatalogRequest("Shop", "commerce"), "TOKEN"), "POST", $"{P}/BIZ1/owned_product_catalogs");
        yield return C("business catalogs", async h => await h.Catalog.GetBusinessCatalogsAsync("BIZ1", "TOKEN"), "GET", $"{P}/BIZ1/owned_product_catalogs?fields=id,name,vertical");
        yield return C("connect catalog", async h => await h.Catalog.ConnectCatalogAsync("WABA1", "CAT1", "TOKEN"), "POST", $"{P}/WABA1/product_catalogs", Json(j => j["catalog_id"]!.ToString().Should().Be("CAT1")));
        yield return C("disconnect catalog", async h => await h.Catalog.DisconnectCatalogAsync("WABA1", "CAT1", "TOKEN"), "DELETE", $"{P}/WABA1/product_catalogs", Json(j => j["catalog_id"]!.ToString().Should().Be("CAT1")));
        yield return C("get catalog", async h => await h.Catalog.GetCatalogAsync("CAT1", "TOKEN"), "GET", $"{P}/CAT1?fields=id,name,vertical");
        yield return C("catalog products", async h => await h.Catalog.GetCatalogProductsAsync("CAT1", "TOKEN"), "GET", $"{P}/CAT1/products?fields=id,name,description,price,currency,availability,condition,image_url,retailer_id,url");
        yield return C("add product", async h => await h.Catalog.AddCatalogProductAsync("CAT1", new CreateWabaCatalogProductRequest("n", "d", "100", "INR", "in stock", "new"), "TOKEN"), "POST", $"{P}/CAT1/products");
        yield return C("update product", async h => await h.Catalog.UpdateCatalogProductAsync("PROD1", new UpdateWabaCatalogProductRequest(Price: "200"), "TOKEN"), "POST", $"{P}/PROD1", Json(j => j["price"]!.ToString().Should().Be("200")));
        yield return C("get product", async h => await h.Catalog.GetCatalogProductAsync("PROD1", "TOKEN"), "GET", $"{P}/PROD1?fields=id,name,description,price,currency,availability,condition,image_url,retailer_id,url");
        yield return C("delete product", async h => await h.Catalog.DeleteCatalogProductAsync("PROD1", "TOKEN"), "DELETE", $"{P}/PROD1");

        // ─── Flows (now on the WhatsApp client / v26) ────────────────────────
        yield return C("list flows", async h => await h.Flows.GetFlowsAsync(new GetWhatsAppFlowsRequest { WabaId = "WABA1" }, "TOKEN"), "GET", $"{P}/WABA1/flows?fields=id,name,status,categories,json_version,data_api_version,endpoint_uri,preview,whatsapp_business_account&limit=25");
        yield return C("get flow", async h => await h.Flows.GetFlowAsync("FLOW1", "TOKEN"), "GET", $"{P}/FLOW1?fields=id,name,status,categories,json_version,data_api_version,endpoint_uri,preview,whatsapp_business_account,validation_errors");
        yield return C("create flow", async h => await h.Flows.CreateFlowAsync(new CreateWhatsAppFlowRequest { WabaId = "WABA1", Name = "f", Categories = new() { "OTHER" } }, "TOKEN"), "POST", $"{P}/WABA1/flows");
        yield return C("update flow", async h => await h.Flows.UpdateFlowAsync(new UpdateWhatsAppFlowRequest { FlowId = "FLOW1", Name = "g" }, "TOKEN"), "POST", $"{P}/FLOW1");
        yield return C("delete flow", async h => await h.Flows.DeleteFlowAsync("FLOW1", "TOKEN"), "DELETE", $"{P}/FLOW1");
        yield return C("publish flow", async h => await h.Flows.PublishFlowAsync("FLOW1", "TOKEN"), "POST", $"{P}/FLOW1/publish");
        yield return C("deprecate flow", async h => await h.Flows.DeprecateFlowAsync("FLOW1", "TOKEN"), "POST", $"{P}/FLOW1/deprecate");
        yield return C("flow assets", async h => await h.Flows.GetFlowAssetsAsync("FLOW1", "TOKEN"), "GET", $"{P}/FLOW1/assets");
        yield return C("FIX upload flow json sends name + asset_type", async h => await h.Flows.UploadFlowJsonAsync(new UploadFlowJsonRequest { FlowId = "FLOW1", FlowJson = "{}" }, "TOKEN"), "POST", $"{P}/FLOW1/assets",
            h => { h.Last.Body.Should().Contain("FLOW_JSON").And.Contain("flow.json"); h.Last.ContentType.Should().Be("multipart/form-data"); });
        yield return C("flow preview", async h => await h.Flows.GetFlowPreviewAsync("FLOW1", "TOKEN"), "GET", $"{P}/FLOW1?fields=preview");

        // ─── NEW: messaging extras ───────────────────────────────────────────
        yield return C("typing indicator", async h => await h.Messaging.SendTypingIndicatorAsync(Req<WaTypingIndicatorRequest>()), "POST", $"{P}/PN1/messages",
            Json(j => { j["status"]!.ToString().Should().Be("read"); j["message_id"]!.ToString().Should().Be("wamid.1"); j["typing_indicator"]!["type"]!.ToString().Should().Be("text"); }));
        yield return C("raw message (address)", async h => await h.Messaging.SendRawMessageAsync(Req<WaSendRawMessageRequest>(r => { r.Type = "interactive"; r.Content = JObject.Parse("{\"type\":\"address_message\"}"); r.BizOpaqueCallbackData = "x"; })),
            "POST", $"{P}/PN1/messages", Json(j => { j["interactive"]!["type"]!.ToString().Should().Be("address_message"); j["biz_opaque_callback_data"]!.ToString().Should().Be("x"); }));
        yield return C("carousel", async h => await h.Messaging.SendCarouselAsync(Req<WaSendCarouselRequest>(r => { r.BodyText = "Pick"; r.Cards = JArray.Parse("[{\"card_index\":0},{\"card_index\":1}]"); })),
            "POST", $"{P}/PN1/messages", Json(j => { j["interactive"]!["type"]!.ToString().Should().Be("carousel"); ((JArray)j["interactive"]!["action"]!["cards"]!).Count.Should().Be(2); }));
        yield return C("multi-product", async h => await h.Messaging.SendMultiProductAsync(Req<WaSendMultiProductRequest>(r => { r.CatalogId = "CAT1"; r.HeaderText = "H"; r.BodyText = "B"; r.Sections = new() { new() { Title = "Top", ProductRetailerIds = new() { "SKU1", "SKU2" } } }; })),
            "POST", $"{P}/PN1/messages",
            Json(j => { j["interactive"]!["type"]!.ToString().Should().Be("product_list"); j["interactive"]!["action"]!["sections"]![0]!["product_items"]![1]!["product_retailer_id"]!.ToString().Should().Be("SKU2"); }));
        yield return C("marketing message", async h => await h.Messaging.SendMarketingMessageAsync(Req<WaSendMarketingMessageRequest>(r => { r.TemplateName = "promo"; r.ProductPolicy = "STRICT"; })),
            "POST", $"{P}/PN1/marketing_messages", Json(j => { j["type"]!.ToString().Should().Be("template"); j["product_policy"]!.ToString().Should().Be("STRICT"); }));
        yield return C("block users", async h => await h.Messaging.BlockUsersAsync(Req<WaUserListRequest>(r => r.Users = new() { "15551234567" })), "POST", $"{P}/PN1/block_users",
            Json(j => j["block_users"]![0]!["user"]!.ToString().Should().Be("15551234567")));
        yield return C("unblock users", async h => await h.Messaging.UnblockUsersAsync(Req<WaUserListRequest>(r => r.Users = new() { "15551234567" })), "DELETE", $"{P}/PN1/block_users",
            Json(j => j["messaging_product"]!.ToString().Should().Be("whatsapp")));
        yield return C("list blocked", async h => await h.Messaging.GetBlockedUsersAsync("PN1", "TOKEN", 10, "AFT", null), "GET", $"{P}/PN1/block_users?limit=10&after=AFT");
        yield return C("search templates", async h => await h.Messaging.SearchTemplatesAsync(new WaTemplateSearchQuery { WabaId = "WABA1", AccessToken = "TOKEN", Status = "APPROVED", Name = "promo", Limit = 5 }),
            "GET", $"{P}/WABA1/message_templates?limit=5&name=promo&status=APPROVED");
        yield return C("template library", async h => await h.Messaging.GetTemplateLibraryAsync(new WaTemplateLibraryQuery { AccessToken = "TOKEN", Search = "payments", Language = "en_US" }),
            "GET", $"{P}/message_template_library?search=payments&language=en_US");
        yield return C("create from library", async h => await h.Messaging.CreateTemplateFromLibraryAsync(Req<WaCreateTemplateFromLibraryRequest>(r => { r.Name = "my"; r.LibraryTemplateName = "delivery_update_1"; r.LibraryTemplateButtonInputs = JArray.Parse("[{\"type\":\"URL\"}]"); })),
            "POST", $"{P}/WABA1/message_templates",
            Json(j => { j["library_template_name"]!.ToString().Should().Be("delivery_update_1"); j["library_template_button_inputs"]!.Type.Should().Be(JTokenType.String); }));
        yield return C("delete templates by name", async h => await h.Messaging.DeleteTemplatesByNameAsync("WABA1", "promo", "TOKEN"), "DELETE", $"{P}/WABA1/message_templates?name=promo");
        yield return C("bulk delete templates", async h => await h.Messaging.BulkDeleteTemplatesAsync("WABA1", new() { "1", "2" }, "TOKEN"), "DELETE", $"{P}/WABA1/message_templates?hsm_ids=[1,2]");
        yield return C("migrate templates", async h => await h.Messaging.MigrateTemplatesAsync(new WaMigrateTemplatesRequest { DestinationWabaId = "WABA2", SourceWabaId = "WABA1", AccessToken = "TOKEN", PageNumber = 0 }),
            "POST", $"{P}/WABA2/migrate_message_templates", Json(j => j["source_waba_id"]!.ToString().Should().Be("WABA1")));

        // ─── NEW: account & settings ─────────────────────────────────────────
        yield return C("subscribe app to waba", async h => await h.Account.SubscribeAppToWabaAsync(Req<WaSubscribeAppRequest>(r => r.OverrideCallbackUri = "https://x/cb")), "POST", $"{P}/WABA1/subscribed_apps",
            Json(j => j["override_callback_uri"]!.ToString().Should().Be("https://x/cb")));
        yield return C("subscribed apps", async h => await h.Account.GetSubscribedAppsAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1/subscribed_apps");
        yield return C("unsubscribe app", async h => await h.Account.UnsubscribeAppFromWabaAsync("WABA1", "TOKEN"), "DELETE", $"{P}/WABA1/subscribed_apps");
        yield return C("owned wabas", async h => await h.Account.GetOwnedWabasAsync("BIZ1", "TOKEN"), "GET", $"{P}/BIZ1/owned_whatsapp_business_accounts?fields=");
        yield return C("client wabas", async h => await h.Account.GetClientWabasAsync("BIZ1", "TOKEN"), "GET", $"{P}/BIZ1/client_whatsapp_business_accounts?fields=");
        yield return C("add phone number", async h => await h.Account.AddPhoneNumberAsync(Req<WaAddPhoneNumberRequest>(r => { r.Cc = "91"; r.PhoneNumber = "9999999999"; r.VerifiedName = "Shop"; })), "POST", $"{P}/WABA1/phone_numbers",
            Json(j => j["verified_name"]!.ToString().Should().Be("Shop")));
        yield return C("display name", async h => await h.Account.UpdateDisplayNameAsync(Req<WaDisplayNameRequest>(r => r.NewDisplayName = "New Name")), "POST", $"{P}/PN1?new_display_name=New Name");
        yield return C("phone settings", async h => await h.Account.GetPhoneSettingsAsync("PN1", "TOKEN", false), "GET", $"{P}/PN1/settings");
        yield return C("update phone settings", async h => await h.Account.UpdatePhoneSettingsAsync(Req<WaRawSettingsRequest>(r => r.Settings = JObject.Parse("{\"storage_configuration\":{\"status\":\"IN_COUNTRY_STORAGE_ENABLED\"}}"))),
            "POST", $"{P}/PN1/settings", Json(j => j["storage_configuration"].Should().NotBeNull()));
        yield return C("get conversational automation", async h => await h.Account.GetConversationalAutomationAsync("PN1", "TOKEN"), "GET", $"{P}/PN1?fields=conversational_automation");
        yield return C("set conversational automation", async h => await h.Account.UpdateConversationalAutomationAsync(Req<WaConversationalAutomationRequest>(r => { r.Prompts = new() { "Book" }; r.Commands = new() { new() { CommandName = "help", CommandDescription = "Help" } }; })),
            "POST", $"{P}/PN1/conversational_automation", Json(j => j["commands"]![0]!["command_name"]!.ToString().Should().Be("help")));
        yield return C("get commerce settings", async h => await h.Account.GetCommerceSettingsAsync("PN1", "TOKEN"), "GET", $"{P}/PN1/whatsapp_commerce_settings");
        yield return C("set commerce settings", async h => await h.Account.UpdateCommerceSettingsAsync(Req<WaCommerceSettingsRequest>(r => { r.IsCartEnabled = true; r.IsCatalogVisible = false; })),
            "POST", $"{P}/PN1/whatsapp_commerce_settings?is_cart_enabled=true&is_catalog_visible=false");
        yield return C("get compliance", async h => await h.Account.GetBusinessComplianceAsync("PN1", "TOKEN"), "GET", $"{P}/PN1/business_compliance_info");
        yield return C("set compliance", async h => await h.Account.UpdateBusinessComplianceAsync(Req<WaRawSettingsRequest>(r => r.Settings = JObject.Parse("{\"entity_name\":\"X\"}"))), "POST", $"{P}/PN1/business_compliance_info");
        yield return C("health status", async h => await h.Account.GetHealthStatusAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1?fields=health_status");
        yield return C("marketing on cloud api", async h => await h.Account.SetMarketingOnCloudApiAsync(Req<WaMarketingOnCloudApiRequest>(r => r.Disable = true)), "POST", $"{P}/WABA1",
            Json(j => j["disable_marketing_messages_on_cloud_api"]!.Value<bool>().Should().BeTrue()));
        yield return C("template insights", async h => await h.Account.EnableTemplateInsightsAsync(Req<WaWabaRequest>()), "POST", $"{P}/WABA1?is_enabled_for_insights=true");
        yield return C("embedded signup code exchange", async h => await h.Account.ExchangeEmbeddedSignupCodeAsync(new WaExchangeCodeRequest { AppId = "APP1", AppSecret = "SECRET", Code = "C0DE" }),
            "GET", $"{P}/oauth/access_token?client_id=APP1&client_secret=SECRET&code=C0DE", h => h.Last.Authorization.Should().BeNull());

        // ─── NEW: analytics ──────────────────────────────────────────────────
        yield return C("pricing analytics", async h => await h.Analytics.GetPricingAnalyticsAsync(Req<WaPricingAnalyticsRequest>(r => { r.Start = 1; r.End = 2; r.Dimensions = new() { "PRICING_CATEGORY" }; })),
            "GET", $"{P}/WABA1?fields=pricing_analytics.start(1).end(2).granularity(DAILY).dimensions([\"PRICING_CATEGORY\"])");
        yield return C("call analytics", async h => await h.Analytics.GetCallAnalyticsAsync(Req<WaCallAnalyticsRequest>(r => { r.Start = 1; r.End = 2; r.MetricTypes = new() { "COUNT" }; })),
            "GET", $"{P}/WABA1?fields=call_analytics.start(1).end(2).granularity(DAILY).metric_types([\"COUNT\"])");
        yield return C("template analytics", async h => await h.Analytics.GetTemplateAnalyticsAsync(Req<WaTemplateAnalyticsRequest>(r => { r.Start = "1"; r.End = "2"; r.TemplateIds = new() { "T1" }; })),
            "GET", $"{P}/WABA1/template_analytics?start=1&end=2&granularity=DAILY&template_ids=[\"T1\"]");
        yield return C("template group analytics", async h => await h.Analytics.GetTemplateGroupAnalyticsAsync(Req<WaTemplateGroupAnalyticsRequest>(r => { r.Start = "1"; r.End = "2"; r.TemplateGroupIds = new() { "G1" }; })),
            "GET", $"{P}/WABA1/template_group_analytics?start=1&end=2&granularity=daily&template_group_ids=[\"G1\"]");
        yield return C("group analytics", async h => await h.Analytics.GetGroupAnalyticsAsync(Req<WaGroupAnalyticsRequest>(r => { r.Start = 1; r.End = 2; r.MetricTypes = new() { "SENT" }; r.GroupIds = new() { "GR1" }; })),
            "GET", $"{P}/WABA1/group_analytics?start=1&end=2&granularity=DAILY&metric_types=[\"SENT\"]&group_ids=[\"GR1\"]");
        yield return C("flow metrics", async h => await h.Analytics.GetFlowMetricsAsync(new WaFlowMetricsRequest { FlowId = "FLOW1", AccessToken = "TOKEN", Since = "2026-10-01" }),
            "GET", $"{P}/FLOW1?fields=metric.name(ENDPOINT_REQUEST_COUNT).granularity(DAY).since(2026-10-01)");

        // ─── NEW: groups ─────────────────────────────────────────────────────
        yield return C("create group", async h => await h.Groups.CreateGroupAsync(Req<WaCreateGroupRequest>(r => { r.Subject = "Team"; r.JoinApprovalMode = "approval_required"; })), "POST", $"{P}/PN1/groups",
            Json(j => { j["subject"]!.ToString().Should().Be("Team"); j["join_approval_mode"]!.ToString().Should().Be("approval_required"); }));
        yield return C("list groups", async h => await h.Groups.GetGroupsAsync("PN1", "TOKEN", 50, null, null), "GET", $"{P}/PN1/groups?limit=50");
        yield return C("get group", async h => await h.Groups.GetGroupAsync("GR1", "TOKEN", "subject"), "GET", $"{P}/GR1?fields=subject");
        yield return C("update group", async h => await h.Groups.UpdateGroupAsync("GR1", new WaUpdateGroupRequest { AccessToken = "TOKEN", Subject = "New" }), "POST", $"{P}/GR1", Json(j => j["subject"]!.ToString().Should().Be("New")));
        yield return C("update group picture (multipart)", async h => await h.Groups.UpdateGroupAsync("GR1", new WaUpdateGroupRequest { AccessToken = "TOKEN", ProfilePictureBase64 = Convert.ToBase64String(new byte[] { 1, 2, 3 }) }), "POST", $"{P}/GR1",
            h => { h.Last.ContentType.Should().Be("multipart/form-data"); h.Last.Body.Should().Contain("profile_picture_file"); });
        yield return C("delete group", async h => await h.Groups.DeleteGroupAsync("GR1", "TOKEN"), "DELETE", $"{P}/GR1");
        yield return C("get invite link", async h => await h.Groups.GetInviteLinkAsync("GR1", "TOKEN"), "GET", $"{P}/GR1/invite_link");
        yield return C("reset invite link", async h => await h.Groups.ResetInviteLinkAsync("GR1", "TOKEN"), "POST", $"{P}/GR1/invite_link");
        yield return C("remove participants", async h => await h.Groups.RemoveParticipantsAsync("GR1", new WaGroupParticipantsRequest { AccessToken = "TOKEN", Participants = new() { "1555" } }), "DELETE", $"{P}/GR1/participants",
            Json(j => j["participants"]![0]!["user"]!.ToString().Should().Be("1555")));
        yield return C("join requests", async h => await h.Groups.GetJoinRequestsAsync("GR1", "TOKEN"), "GET", $"{P}/GR1/join_requests");
        yield return C("approve join", async h => await h.Groups.ApproveJoinRequestsAsync("GR1", new WaJoinRequestsRequest { AccessToken = "TOKEN", JoinRequests = new() { "J1" } }), "POST", $"{P}/GR1/join_requests");
        yield return C("reject join", async h => await h.Groups.RejectJoinRequestsAsync("GR1", new WaJoinRequestsRequest { AccessToken = "TOKEN", JoinRequests = new() { "J1" } }), "DELETE", $"{P}/GR1/join_requests",
            Json(j => j["join_requests"]![0]!.ToString().Should().Be("J1")));
        yield return C("group message", async h => await h.Groups.SendGroupMessageAsync("GR1", Req<WaGroupMessageRequest>(r => r.Content = JObject.Parse("{\"body\":\"hello\"}"))), "POST", $"{P}/PN1/messages",
            Json(j => { j["recipient_type"]!.ToString().Should().Be("group"); j["to"]!.ToString().Should().Be("GR1"); j["text"]!["body"]!.ToString().Should().Be("hello"); }));
        yield return C("group invite template", async h => await h.Groups.SendGroupInviteTemplateAsync(Req<WaGroupInviteTemplateRequest>(r => { r.GroupId = "GR1"; r.TemplateName = "join_us"; })), "POST", $"{P}/PN1/messages",
            Json(j => j["template"]!["components"]![0]!["parameters"]![0]!["group_id"]!.ToString().Should().Be("GR1")));

        // ─── NEW: calling ────────────────────────────────────────────────────
        yield return C("call settings", async h => await h.Calling.GetCallSettingsAsync("PN1", "TOKEN", true), "GET", $"{P}/PN1/settings?include_sip_credentials=true");
        yield return C("update call settings", async h => await h.Calling.UpdateCallSettingsAsync(Req<WaRawSettingsRequest>(r => r.Settings = JObject.Parse("{\"calling\":{\"status\":\"ENABLED\"}}"))), "POST", $"{P}/PN1/settings",
            Json(j => j["calling"]!["status"]!.ToString().Should().Be("ENABLED")));
        yield return C("call connect", async h => await h.Calling.CallActionAsync("connect", Req<WaCallActionRequest>(r => r.Session = new() { SdpType = "offer", Sdp = "v=0" })), "POST", $"{P}/PN1/calls",
            Json(j => { j["action"]!.ToString().Should().Be("connect"); j["to"]!.ToString().Should().Be("15550001111"); j["session"]!["sdp"]!.ToString().Should().Be("v=0"); j.ContainsKey("call_id").Should().BeFalse(); }));
        yield return C("call pre-accept", async h => await h.Calling.CallActionAsync("pre-accept", Req<WaCallActionRequest>(r => { r.CallId = "CALL1"; r.Session = new() { SdpType = "answer", Sdp = "v=0" }; })), "POST", $"{P}/PN1/calls",
            Json(j => { j["action"]!.ToString().Should().Be("pre_accept"); j["call_id"]!.ToString().Should().Be("CALL1"); }));
        yield return C("call accept", async h => await h.Calling.CallActionAsync("accept", Req<WaCallActionRequest>(r => { r.CallId = "CALL1"; r.Session = new() { SdpType = "answer", Sdp = "v=0" }; })), "POST", $"{P}/PN1/calls");
        yield return C("call reject", async h => await h.Calling.CallActionAsync("reject", Req<WaCallActionRequest>(r => r.CallId = "CALL1")), "POST", $"{P}/PN1/calls", Json(j => j["action"]!.ToString().Should().Be("reject")));
        yield return C("call terminate", async h => await h.Calling.CallActionAsync("terminate", Req<WaCallActionRequest>(r => r.CallId = "CALL1")), "POST", $"{P}/PN1/calls", Json(j => j["action"]!.ToString().Should().Be("terminate")));
        yield return C("call permissions", async h => await h.Calling.GetCallPermissionsAsync("PN1", "TOKEN", "1555", null), "GET", $"{P}/PN1/call_permissions?user_wa_id=1555");
        yield return C("call permission request msg", async h => await h.Calling.SendCallPermissionRequestAsync(Req<WaCallPermissionRequestMessage>(r => r.BodyText = "May we call?")), "POST", $"{P}/PN1/messages",
            Json(j => j["interactive"]!["type"]!.ToString().Should().Be("call_permission_request")));
        yield return C("voice call button", async h => await h.Calling.SendVoiceCallButtonAsync(Req<WaVoiceCallButtonMessage>(r => { r.BodyText = "Call us"; r.TtlMinutes = 60; })), "POST", $"{P}/PN1/messages",
            Json(j => { j["interactive"]!["type"]!.ToString().Should().Be("voice_call"); j["interactive"]!["action"]!["parameters"]!["ttl_minutes"]!.Value<int>().Should().Be(60); }));

        // ─── NEW: payments (India) ───────────────────────────────────────────
        yield return C("payment configs", async h => await h.Payments.GetPaymentConfigurationsAsync("WABA1", "TOKEN"), "GET", $"{P}/WABA1/payment_configurations");
        yield return C("payment config", async h => await h.Payments.GetPaymentConfigurationAsync("WABA1", "upi-main", "TOKEN"), "GET", $"{P}/WABA1/payment_configuration/upi-main");
        yield return C("create payment config", async h => await h.Payments.CreatePaymentConfigurationAsync(Req<WaCreatePaymentConfigRequest>(r => { r.ConfigurationName = "upi-main"; r.ProviderName = "upi_vpa"; r.MerchantVpa = "shop@upi"; })),
            "POST", $"{P}/WABA1/payment_configuration", Json(j => j["provider_name"]!.ToString().Should().Be("upi_vpa")));
        yield return C("payment data endpoint", async h => await h.Payments.UpdatePaymentDataEndpointAsync(Req<WaPaymentConfigDataEndpointRequest>(r => { r.ConfigurationName = "rzp"; r.DataEndpointUrl = "https://x/data"; })),
            "POST", $"{P}/WABA1/payment_configuration/rzp", Json(j => j["data_endpoint_url"]!.ToString().Should().Be("https://x/data")));
        yield return C("regenerate oauth link", async h => await h.Payments.RegenerateOauthLinkAsync(Req<WaRegeneratePaymentOauthLinkRequest>(r => r.ConfigurationName = "rzp")), "POST", $"{P}/WABA1/generate_payment_configuration_oauth_link");
        yield return C("delete payment config", async h => await h.Payments.DeletePaymentConfigurationAsync("WABA1", "rzp", "TOKEN"), "DELETE", $"{P}/WABA1/payment_configuration",
            Json(j => j["configuration_name"]!.ToString().Should().Be("rzp")));
        yield return C("payment status", async h => await h.Payments.GetPaymentStatusAsync("PN1", "rzp", "ORDER-1", "TOKEN"), "GET", $"{P}/PN1/payments/rzp/ORDER-1");
        yield return C("refund", async h => await h.Payments.RefundAsync(Req<WaRefundRequest>(r => { r.ReferenceId = "ORDER-1"; r.PaymentConfigId = "rzp"; r.Speed = "instant"; })), "POST", $"{P}/PN1/payments_refund",
            Json(j => j["payment_config_id"]!.ToString().Should().Be("rzp")));
        yield return C("order details msg", async h => await h.Payments.SendOrderDetailsAsync(Req<WaOrderDetailsMessageRequest>(r => { r.BodyText = "Pay"; r.Parameters = JObject.Parse("{\"reference_id\":\"ORDER-1\"}"); })), "POST", $"{P}/PN1/messages",
            Json(j => { j["interactive"]!["type"]!.ToString().Should().Be("order_details"); j["interactive"]!["action"]!["name"]!.ToString().Should().Be("review_and_pay"); }));
        yield return C("order status msg", async h => await h.Payments.SendOrderStatusAsync(Req<WaOrderStatusMessageRequest>(r => { r.BodyText = "Shipped"; r.ReferenceId = "ORDER-1"; r.Status = "shipped"; })), "POST", $"{P}/PN1/messages",
            Json(j => j["interactive"]!["action"]!["parameters"]!["order"]!["status"]!.ToString().Should().Be("shipped")));
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public async Task Endpoint_calls_documented_graph_route(Case c)
    {
        var h = new Harness();

        dynamic result = await c.Call(h);

        ((bool)result.Success).Should().BeTrue($"{c.Name}: {(string?)result.Message}");
        h.Handler.Requests.Should().ContainSingle();
        h.Last.Method.Method.Should().Be(c.Method);
        if (c.PathAndQuery.EndsWith("?fields="))
            h.LastPathAndQuery.Should().StartWith(c.PathAndQuery, "the exact field list is not under test");
        else
            h.LastPathAndQuery.Should().Be(c.PathAndQuery);
        h.Last.Uri.Host.Should().Be("graph.facebook.com");
        if (!c.Name.StartsWith("embedded signup"))
            h.Last.Authorization.Should().Be("Bearer TOKEN");
        c.Check?.Invoke(h);
    }
}

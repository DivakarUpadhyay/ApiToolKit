using System.Net;
using ApiToolKit.Application.DTOs.Facebook.WhatsApp;
using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using FluentAssertions;
using Xunit;
using static Facebook.WhatsAppTests.Harness;

namespace Facebook.WhatsAppTests;

public class WhatsAppBehaviourTests
{
    private const string GraphError = "{\"error\":{\"message\":\"(#100) Invalid parameter\",\"type\":\"OAuthException\",\"code\":100,\"error_data\":{\"details\":\"to is required\"}}}";

    [Fact]
    public async Task Graph_error_is_reported_as_failure_by_existing_service()
    {
        var h = new Harness();
        h.Handler.Responder = _ => (HttpStatusCode.BadRequest, GraphError, "application/json");

        // Before the fix this returned Success = true with an empty DTO.
        await FluentActions.Awaiting(() => h.Main.SendTextMessageAsync(Req<SendTextMessageRequest>()))
            .Should().ThrowAsync<ApiToolKit.Application.Interfaces.Services.Facebook.WhatsAppApiException>()
            .Where(e => e.Code == 100 && e.Message.Contains("to is required"));
    }

    [Fact]
    public async Task Graph_error_is_reported_as_failure_by_new_services()
    {
        var h = new Harness();
        h.Handler.Responder = _ => (HttpStatusCode.BadRequest, GraphError, "application/json");

        var result = await h.Messaging.BlockUsersAsync(Req<WaUserListRequest>(r => r.Users = new() { "1" }));

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be(100);
        result.Message.Should().Contain("Invalid parameter");
        h.Repo.Logs.Should().ContainSingle(l => !l.IsSuccess && l.StatusCode == 400);
    }

    [Fact]
    public async Task Snake_case_responses_map_onto_existing_dtos()
    {
        var h = new Harness();
        h.Handler.Responder = _ => (HttpStatusCode.OK,
            "{\"messaging_product\":\"whatsapp\",\"url\":\"https://lookaside.fbsbx.com/x\",\"mime_type\":\"image/jpeg\",\"sha256\":\"abc\",\"file_size\":\"10\",\"id\":\"M1\"}", "application/json");

        var result = await h.Main.GetMediaAsync("M1", "TOKEN");

        result.Data!.MimeType.Should().Be("image/jpeg");
        result.Data.MessagingProduct.Should().Be("whatsapp");
        result.Data.FileSize.Should().Be("10");
    }

    [Fact]
    public async Task Get_qr_code_unwraps_data_array()
    {
        var h = new Harness();
        h.Handler.Responder = _ => (HttpStatusCode.OK,
            "{\"data\":[{\"code\":\"QR1\",\"prefilled_message\":\"Hi\",\"deep_link_url\":\"https://wa.me/message/QR1\"}]}", "application/json");

        var result = await h.Main.GetQrCodeAsync("PN1", "QR1", "TOKEN");

        result.Data!.Code.Should().Be("QR1");
        result.Data.DeepLinkUrl.Should().Be("https://wa.me/message/QR1");
    }

    [Fact]
    public async Task Media_download_resolves_url_then_fetches_file_with_token()
    {
        var h = new Harness();
        h.Handler.Responder = req => req.RequestUri!.Host == "graph.facebook.com"
            ? (HttpStatusCode.OK, "{\"url\":\"https://lookaside.fbsbx.com/whatsapp_business/attachments/?mid=1\",\"mime_type\":\"application/pdf\",\"id\":\"M1\"}", "application/json")
            : (HttpStatusCode.OK, "%PDF-1.7", "application/pdf");

        var result = await h.Messaging.DownloadMediaAsync("M1", "TOKEN");

        result.Success.Should().BeTrue();
        result.Data!.ContentType.Should().Be("application/pdf");
        result.Data.FileName.Should().Be("M1.pdf");
        System.Text.Encoding.UTF8.GetString(result.Data.Content).Should().Be("%PDF-1.7");
        h.Handler.Requests.Should().HaveCount(2);
        h.Handler.Requests[1].Authorization.Should().Be("Bearer TOKEN");
    }

    [Fact]
    public async Task Tokens_and_secrets_are_not_written_to_the_log_table()
    {
        var h = new Harness();
        h.Handler.Responder = _ => (HttpStatusCode.OK, "{\"access_token\":\"BUSINESS-TOKEN\",\"token_type\":\"bearer\"}", "application/json");

        var result = await h.Account.ExchangeEmbeddedSignupCodeAsync(new WaExchangeCodeRequest { AppId = "APP1", AppSecret = "SECRET", Code = "C0DE" });

        result.Success.Should().BeTrue();
        result.Data!.ToString().Should().Contain("BUSINESS-TOKEN", "the caller still gets the token");
        var log = h.Repo.Logs.Single();
        (log.RequestBody + log.ResponseBody).Should().NotContain("SECRET").And.NotContain("BUSINESS-TOKEN").And.NotContain("C0DE");
    }

    [Fact]
    public async Task Webhook_verification_checks_the_configured_token()
    {
        var h = new Harness();
        (await h.Main.VerifyWebhookAsync("subscribe", "12345", "verify-me")).Data.Should().Be("12345");
        (await h.Main.VerifyWebhookAsync("subscribe", "12345", "wrong")).Success.Should().BeFalse();
    }

    [Theory]
    [InlineData("dial")]
    [InlineData("accept")]   // missing CallId
    public async Task Invalid_call_actions_are_rejected_without_calling_meta(string action)
    {
        var h = new Harness();
        var result = await h.Calling.CallActionAsync(action, Req<WaCallActionRequest>());
        result.Success.Should().BeFalse();
        h.Handler.Requests.Should().BeEmpty();
    }

    [Fact]
    public async Task Limits_are_enforced_locally()
    {
        var h = new Harness();
        (await h.Analytics.GetTemplateAnalyticsAsync(Req<WaTemplateAnalyticsRequest>(r => r.TemplateIds = Enumerable.Range(0, 11).Select(i => i.ToString()).ToList()))).Success.Should().BeFalse();
        (await h.Groups.RemoveParticipantsAsync("GR1", new WaGroupParticipantsRequest { AccessToken = "TOKEN", Participants = Enumerable.Range(0, 9).Select(i => i.ToString()).ToList() })).Success.Should().BeFalse();
        (await h.Messaging.BulkDeleteTemplatesAsync("WABA1", new(), "TOKEN")).Success.Should().BeFalse();
        h.Handler.Requests.Should().BeEmpty();
    }
}

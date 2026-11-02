using System.Net;
using System.Text;
using ApiToolKit.Application.Services.Facebook;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.External.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Facebook.WhatsAppTests;

public record CapturedRequest(HttpMethod Method, Uri Uri, string? Body, string? ContentType, string? Authorization);

/// <summary>Records every outgoing request and answers with a canned Graph response.</summary>
public class FakeGraphHandler : HttpMessageHandler
{
    public List<CapturedRequest> Requests { get; } = new();
    public Func<HttpRequestMessage, (HttpStatusCode Status, string Body, string ContentType)> Responder { get; set; } =
        _ => (HttpStatusCode.OK, "{\"success\":true,\"id\":\"123\"}", "application/json");

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var body = request.Content != null ? await request.Content.ReadAsStringAsync(cancellationToken) : null;
        Requests.Add(new CapturedRequest(request.Method, request.RequestUri!, body,
            request.Content?.Headers.ContentType?.MediaType, request.Headers.Authorization?.ToString()));
        var (status, text, type) = Responder(request);
        return new HttpResponseMessage(status) { Content = new StringContent(text, Encoding.UTF8, type) };
    }
}

public class FakeLogRepo : IFacebookWhatsAppRepository, IFacebookWhatsAppCatalogRepository
{
    public List<FacebookApiLog> Logs { get; } = new();
    public Task<int> SaveApiLogAsync(FacebookApiLog log) { Logs.Add(log); return Task.FromResult(1); }
}

/// <summary>Real services wired to the real WhatsAppApiClient over a fake HTTP handler.</summary>
public class Harness
{
    public const string Base = "https://graph.facebook.com/v26.0";

    public FakeGraphHandler Handler { get; } = new();
    public FakeLogRepo Repo { get; } = new();
    public WhatsAppApiClient Client { get; }

    public FacebookWhatsAppService Main { get; }
    public FacebookWhatsAppCatalogService Catalog { get; }
    public FacebookWhatsAppFlowsService Flows { get; }
    public FacebookWhatsAppMessagingService Messaging { get; }
    public FacebookWhatsAppAccountService Account { get; }
    public FacebookWhatsAppAnalyticsService Analytics { get; }
    public FacebookWhatsAppGroupsService Groups { get; }
    public FacebookWhatsAppCallingService Calling { get; }
    public FacebookWhatsAppPaymentsService Payments { get; }

    public Harness()
    {
        // No WhatsApp:BaseUrl configured → must fall back to the v26.0 default.
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["WhatsApp:WebhookVerifyToken"] = "verify-me"
        }).Build();

        Client = new WhatsAppApiClient(new HttpClient(Handler), config);
        Main = new FacebookWhatsAppService(Client, Repo, config);
        Catalog = new FacebookWhatsAppCatalogService(Client, Repo);
        Flows = new FacebookWhatsAppFlowsService(null!, Client);
        Messaging = new FacebookWhatsAppMessagingService(Client, Repo);
        Account = new FacebookWhatsAppAccountService(Client, Repo);
        Analytics = new FacebookWhatsAppAnalyticsService(Client, Repo);
        Groups = new FacebookWhatsAppGroupsService(Client, Repo);
        Calling = new FacebookWhatsAppCallingService(Client, Repo);
        Payments = new FacebookWhatsAppPaymentsService(Client, Repo);
    }

    public CapturedRequest Last => Handler.Requests.Last();
    public JObject LastJson => JObject.Parse(Last.Body!);
    public string LastPathAndQuery => Uri.UnescapeDataString(Last.Uri.PathAndQuery);

    /// <summary>Creates a request DTO with the usual ID/token properties filled in.</summary>
    public static T Req<T>(Action<T>? configure = null) where T : new()
    {
        var obj = new T();
        var defaults = new Dictionary<string, string>
        {
            ["PhoneNumberId"] = "PN1", ["AccessToken"] = "TOKEN", ["WabaId"] = "WABA1", ["To"] = "15550001111",
            ["TemplateId"] = "TPL1", ["QrCodeId"] = "QR1", ["FlowId"] = "FLOW1", ["AppId"] = "APP1",
            ["MessageId"] = "wamid.1", ["Body"] = "hi", ["MimeType"] = "image/png"
        };
        foreach (var prop in typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string) && p.CanWrite))
            if (defaults.TryGetValue(prop.Name, out var value)) prop.SetValue(obj, value);
        configure?.Invoke(obj);
        return obj;
    }
}

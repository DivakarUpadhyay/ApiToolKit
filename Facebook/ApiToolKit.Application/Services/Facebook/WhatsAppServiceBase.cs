using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.Facebook;

/// <summary>
/// Shared plumbing for the pass-through WhatsApp services: runs the Graph call, logs it,
/// and turns Graph errors (thrown by <see cref="IWhatsAppApiClient"/>) into a failed Result.
/// </summary>
public abstract class WhatsAppServiceBase
{
    protected readonly IWhatsAppApiClient Api;
    private readonly IFacebookWhatsAppRepository _repo;

    protected WhatsAppServiceBase(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo)
    {
        Api = api;
        _repo = repo;
    }

    protected async Task<Result<object>> RunAsync(string apiName, Func<Task<object?>> call, object? request = null)
    {
        var requestJson = Redact(request);
        try
        {
            var response = await call();
            await LogAsync(apiName, true, 200, requestJson, Redact(response));
            return Result<object>.Ok(response ?? new { success = true });
        }
        catch (WhatsAppApiException ex)
        {
            await LogAsync(apiName, false, ex.StatusCode, requestJson, ex.Message);
            return Result<object>.Fail(ex.Message, ex.Code);
        }
        catch (Exception ex)
        {
            await LogAsync(apiName, false, 500, requestJson, ex.Message);
            return Result<object>.Fail(ex.Message);
        }
    }

    /// <summary>Base body for POST /{phone-number-id}/messages.</summary>
    protected static Dictionary<string, object?> MessageBody(string? to, string? recipient, string type, string recipientType = "individual")
    {
        var body = new Dictionary<string, object?>
        {
            ["messaging_product"] = "whatsapp",
            ["recipient_type"] = recipientType,
            ["type"] = type
        };
        // "to" is a phone number / group ID; "recipient" is a BSUID. Meta uses "to" if both are sent.
        if (!string.IsNullOrEmpty(to)) body["to"] = to;
        if (!string.IsNullOrEmpty(recipient)) body["recipient"] = recipient;
        return body;
    }

    private static readonly string[] SecretFields = { "AccessToken", "access_token", "AppSecret", "client_secret", "Pin", "Code" };

    // Requests carry tokens/secrets and the code exchange returns one; never write them to the API log table.
    private static string? Redact(object? request)
    {
        if (request == null) return null;
        // Clone so redaction never touches the response object returned to the caller.
        var token = request is JToken t ? t.DeepClone() : JToken.FromObject(request);
        if (token is JObject obj)
            foreach (var name in SecretFields)
                if (obj.Property(name, StringComparison.OrdinalIgnoreCase) is { } prop) prop.Value = "***";
        return token.ToString(Formatting.None);
    }

    // Best-effort: an unreachable log database must not turn a successful WhatsApp call into a failure.
    private async Task LogAsync(string apiName, bool success, int statusCode, string? request, string? response)
    {
        try
        {
            await _repo.SaveApiLogAsync(new FacebookApiLog
            {
                ApiName = apiName,
                RequestBody = request ?? string.Empty,
                ResponseBody = response ?? string.Empty,
                IsSuccess = success,
                StatusCode = statusCode,
                CreatedAt = DateTime.UtcNow
            });
        }
        catch
        {
            // ignored — see comment above
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Webhook;

public class SubscribeWebhookRequest
{
    [Required] public string ObjectType { get; set; } = string.Empty;
    [Required] public string CallbackUrl { get; set; } = string.Empty;
    [Required] public string VerifyToken { get; set; } = string.Empty;
    public List<string> Fields { get; set; } = new();
}

public class SubscribeWebhookResponse
{
    public bool Success { get; set; }
    public string? Id { get; set; }
}

public class WebhookSubscriptionResponse
{
    public string? Object { get; set; }
    public string? CallbackUrl { get; set; }
    public bool Active { get; set; }
    public List<string> Fields { get; set; } = new();
    public string? Id { get; set; }
}

public class GetWebhookSubscriptionsRequest
{
    [Required] public string AppId { get; set; } = string.Empty;
}

public class GetWebhookSubscriptionsResponse
{
    public List<WebhookSubscriptionResponse> Data { get; set; } = new();
}

public class DeleteWebhookRequest
{
    [Required] public string ObjectType { get; set; } = string.Empty;
    public List<string>? Fields { get; set; }
}

public class WebhookVerificationRequest
{
    public string? Mode { get; set; }
    public string? Token { get; set; }
    public string? Challenge { get; set; }
}

public class WebhookChange
{
    public string? Field { get; set; }
    public object? Value { get; set; }
}

public class WebhookEntry
{
    public string? Id { get; set; }
    public List<WebhookChange> Changes { get; set; } = new();
    public long Time { get; set; }
}

public class WebhookPayload
{
    public string? Object { get; set; }
    public List<WebhookEntry> EntryList { get; set; } = new();
}

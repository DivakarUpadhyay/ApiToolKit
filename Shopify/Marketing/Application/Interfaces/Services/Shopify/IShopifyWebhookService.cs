using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyWebhookService
{
    Task<Result<ListWebhooksResponse>> ListWebhooksAsync(string shopDomain, string? topic = null, int? limit = null);
    Task<Result<WebhookResponse>> GetWebhookAsync(string shopDomain, string webhookId);
    Task<Result<int>> CountWebhooksAsync(string shopDomain, string? topic = null);
    Task<Result<WebhookResponse>> CreateWebhookAsync(string shopDomain, CreateWebhookRequest request);
    Task<Result<WebhookResponse>> UpdateWebhookAsync(string shopDomain, string webhookId, UpdateWebhookRequest request);
    Task<Result<bool>> DeleteWebhookAsync(string shopDomain, string webhookId);
}

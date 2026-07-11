using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyEventService
{
    Task<Result<ListEventsResponse>> ListEventsAsync(string shopDomain, int? limit = null, string? sinceId = null, string? filter = null, string? verb = null);
    Task<Result<EventResponse>> GetEventAsync(string shopDomain, string eventId);
    Task<Result<int>> CountEventsAsync(string shopDomain, string? filter = null, string? verb = null);
}

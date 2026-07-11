using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.Capi;

public record CApiUserData(
    [property:JsonProperty("em")] string? Email = null,
    [property:JsonProperty("ph")] string? Phone = null,
    [property:JsonProperty("fn")] string? FirstName = null,
    [property:JsonProperty("ln")] string? LastName = null,
    [property:JsonProperty("ct")] string? City = null,
    [property:JsonProperty("st")] string? State = null,
    [property:JsonProperty("zp")] string? ZipCode = null,
    [property:JsonProperty("country")] string? Country = null,
    [property:JsonProperty("external_id")] string? ExternalId = null,
    [property:JsonProperty("client_ip_address")] string? ClientIpAddress = null,
    [property:JsonProperty("client_user_agent")] string? ClientUserAgent = null,
    [property:JsonProperty("fbc")] string? Fbc = null,
    [property:JsonProperty("fbp")] string? Fbp = null);

public record CApiCustomData(
    [property:JsonProperty("value")] decimal? Value = null,
    [property:JsonProperty("currency")] string? Currency = null,
    [property:JsonProperty("content_name")] string? ContentName = null,
    [property:JsonProperty("content_category")] string? ContentCategory = null,
    [property:JsonProperty("content_ids")] List<string>? ContentIds = null,
    [property:JsonProperty("content_type")] string? ContentType = null,
    [property:JsonProperty("num_items")] int? NumItems = null,
    [property:JsonProperty("order_id")] string? OrderId = null,
    [property:JsonProperty("predicted_ltv")] decimal? PredictedLtv = null);

public record CApiEvent(
    [property:JsonProperty("event_name")] string EventName,
    [property:JsonProperty("event_time")] long EventTime,
    [property:JsonProperty("event_source_url")] string? EventSourceUrl = null,
    [property:JsonProperty("event_id")] string? EventId = null,
    [property:JsonProperty("opt_out")] bool? OptOut = null,
    [property:JsonProperty("user_data")] CApiUserData? UserData = null,
    [property:JsonProperty("custom_data")] CApiCustomData? CustomData = null);

public record SendCApiEventsRequest(
    string PixelId,
    string AccessToken,
    List<CApiEvent> Data,
    string? TestEventCode = null);

public record CApiEventsResponse(
    [property:JsonProperty("events_received")] int? EventsReceived,
    [property:JsonProperty("messages_received")] List<string>? MessagesReceived,
    [property:JsonProperty("fbtrace_id")] string? FbtraceId);

public record PurchaseEventRequest(
    string PixelId, string AccessToken, long EventTime,
    decimal Value, string Currency,
    string? ContentName = null, List<string>? ContentIds = null, string? OrderId = null,
    string? Email = null, string? Phone = null,
    string? ClientIpAddress = null, string? ClientUserAgent = null,
    string? EventSourceUrl = null, string? TestEventCode = null);

public record AddToCartEventRequest(
    string PixelId, string AccessToken, long EventTime,
    decimal Value, string Currency,
    string? ContentName = null, List<string>? ContentIds = null,
    string? ClientIpAddress = null, string? ClientUserAgent = null,
    string? EventSourceUrl = null, string? TestEventCode = null);

public record PageViewEventRequest(
    string PixelId, string AccessToken, long EventTime,
    string? EventSourceUrl = null,
    string? ClientIpAddress = null, string? ClientUserAgent = null,
    string? TestEventCode = null);

public record LeadEventRequest(
    string PixelId, string AccessToken, long EventTime,
    string? ContentName = null, string? Email = null, string? Phone = null,
    string? ClientIpAddress = null, string? ClientUserAgent = null,
    string? EventSourceUrl = null, string? TestEventCode = null);

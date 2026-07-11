namespace ApiToolKit.Application.DTOs.Shopee.PushNotification;

public class SetAppPushConfigRequest
{
    public long ShopId { get; set; }
    public string callback_url { get; set; } = string.Empty;
    public int push_type { get; set; }
    public List<long> blocked_shop_id_list { get; set; } = new();
}

public class SetAppPushConfigResponse
{
    public string msg { get; set; } = string.Empty;
}

public class GetAppPushConfigRequest
{
    public long ShopId { get; set; }
}

public class GetAppPushConfigResponse
{
    public string callback_url { get; set; } = string.Empty;
    public int push_type { get; set; }
    public List<long> blocked_shop_id_list { get; set; } = new();
}

public class PushNotificationPayload
{
    public int code { get; set; }
    public long timestamp { get; set; }
    public long shop_id { get; set; }
    public object? data { get; set; }
}

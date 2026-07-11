namespace ApiToolKit.Application.DTOs.Shopee.FirstMile;

public class GenerateFirstMileTrackingNumberRequest
{
    public long ShopId { get; set; }
    public int quantity { get; set; }
    public long logistics_channel_id { get; set; }
}

public class GenerateFirstMileTrackingNumberResponse
{
    public List<string> first_mile_tracking_number_list { get; set; } = new();
}

public class GetFirstMileTrackingNumberRequest
{
    public long ShopId { get; set; }
    public string first_mile_tracking_number { get; set; } = string.Empty;
}

public class GetFirstMileTrackingNumberResponse
{
    public string first_mile_tracking_number { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public List<string> orders { get; set; } = new();
}

public class UploadFirstMileTrackingNumberRequest
{
    public long ShopId { get; set; }
    public string first_mile_tracking_number { get; set; } = string.Empty;
    public List<string> order_sn_list { get; set; } = new();
}

public class UploadFirstMileTrackingNumberResponse
{
    public string first_mile_tracking_number { get; set; } = string.Empty;
    public List<string> failed_list { get; set; } = new();
}

public class UnbindFirstMileTrackingNumberRequest
{
    public long ShopId { get; set; }
    public string first_mile_tracking_number { get; set; } = string.Empty;
}

public class UnbindFirstMileTrackingNumberResponse
{
    public string first_mile_tracking_number { get; set; } = string.Empty;
}

public class GetUnbindOrderListRequest
{
    public long ShopId { get; set; }
    public int page_no { get; set; }
    public int page_size { get; set; }
    public long pickup_time_from { get; set; }
    public long pickup_time_to { get; set; }
}

public class GetUnbindOrderListResponse
{
    public List<UnbindOrderInfo> order_list { get; set; } = new();
    public int total_count { get; set; }
    public bool has_next_page { get; set; }
}

public class UnbindOrderInfo
{
    public string order_sn { get; set; } = string.Empty;
    public long logistics_channel_id { get; set; }
    public long pickup_time { get; set; }
}

public class CreateChannelRequest
{
    public long ShopId { get; set; }
    public List<string> order_sn_list { get; set; } = new();
    public long logistics_channel_id { get; set; }
}

public class CreateChannelResponse
{
    public List<BatchShipResult> batch_ship_result { get; set; } = new();
}

public class BatchShipResult
{
    public string order_sn { get; set; } = string.Empty;
    public string fail_error { get; set; } = string.Empty;
    public string fail_message { get; set; } = string.Empty;
}

public class GetFirstMileChannelListRequest
{
    public long ShopId { get; set; }
    public string region { get; set; } = string.Empty;
}

public class GetFirstMileChannelListResponse
{
    public List<FirstMileChannelInfo> logistics_channel_list { get; set; } = new();
}

public class FirstMileChannelInfo
{
    public long logistics_channel_id { get; set; }
    public string logistics_channel_name { get; set; } = string.Empty;
    public bool cod_enabled { get; set; }
}

public class GetFirstMileTrackingListRequest
{
    public long ShopId { get; set; }
    public int page_no { get; set; }
    public int page_size { get; set; }
    public string status { get; set; } = string.Empty;
}

public class GetFirstMileTrackingListResponse
{
    public List<FirstMileTrackingInfo> tracking_list { get; set; } = new();
    public int total_count { get; set; }
    public bool has_next_page { get; set; }
}

public class FirstMileTrackingInfo
{
    public string first_mile_tracking_number { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public long create_time { get; set; }
    public int order_count { get; set; }
}

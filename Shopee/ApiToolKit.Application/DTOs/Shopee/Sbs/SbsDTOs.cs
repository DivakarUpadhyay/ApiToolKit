namespace ApiToolKit.Application.DTOs.Shopee.Sbs;

public class SbsGetItemListRequest
{
    public long ShopId { get; set; }
    public int page_no { get; set; }
    public int page_size { get; set; }
    public string item_status { get; set; } = string.Empty;
}

public class SbsGetItemListResponse
{
    public List<SbsItemInfo> item_list { get; set; } = new();
    public int total_count { get; set; }
    public bool has_next_page { get; set; }
}

public class SbsItemInfo
{
    public long item_id { get; set; }
    public string item_name { get; set; } = string.Empty;
    public string item_sku { get; set; } = string.Empty;
    public long price { get; set; }
    public int stock { get; set; }
    public string status { get; set; } = string.Empty;
}

public class SbsCreateItemRequest
{
    public long ShopId { get; set; }
    public long item_id { get; set; }
    public long price { get; set; }
    public int stock { get; set; }
    public long logistics_channel_id { get; set; }
}

public class SbsCreateItemResponse
{
    public long item_id { get; set; }
    public long price { get; set; }
    public int stock { get; set; }
}

public class SbsUpdateItemRequest
{
    public long ShopId { get; set; }
    public long item_id { get; set; }
    public long price { get; set; }
    public int stock { get; set; }
}

public class SbsUpdateItemResponse
{
    public long item_id { get; set; }
    public long price { get; set; }
    public int stock { get; set; }
}

public class SbsDeleteItemRequest
{
    public long ShopId { get; set; }
    public List<long> item_id_list { get; set; } = new();
}

public class SbsDeleteItemResponse
{
    public List<long> failed_list { get; set; } = new();
}

public class SbsGetItemRequest
{
    public long ShopId { get; set; }
    public long item_id { get; set; }
}

public class SbsGetItemResponse
{
    public long item_id { get; set; }
    public string item_name { get; set; } = string.Empty;
    public long price { get; set; }
    public int stock { get; set; }
    public string status { get; set; } = string.Empty;
    public long logistics_channel_id { get; set; }
}

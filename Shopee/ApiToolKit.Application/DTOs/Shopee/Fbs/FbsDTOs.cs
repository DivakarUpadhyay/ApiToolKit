namespace ApiToolKit.Application.DTOs.Shopee.Fbs;

public class FbsGetItemListRequest
{
    public long ShopId { get; set; }
    public int page_no { get; set; }
    public int page_size { get; set; }
}

public class FbsGetItemListResponse
{
    public List<FbsItemInfo> item_list { get; set; } = new();
    public int total_count { get; set; }
    public bool has_next_page { get; set; }
}

public class FbsItemInfo
{
    public long item_id { get; set; }
    public string item_name { get; set; } = string.Empty;
    public List<string> sku_list { get; set; } = new();
    public int stock { get; set; }
    public string status { get; set; } = string.Empty;
}

public class FbsCreateItemRequest
{
    public long ShopId { get; set; }
    public long item_id { get; set; }
    public List<string> sku_list { get; set; } = new();
}

public class FbsCreateItemResponse
{
    public long item_id { get; set; }
}

public class FbsUpdateItemRequest
{
    public long ShopId { get; set; }
    public long item_id { get; set; }
    public List<string> sku_list { get; set; } = new();
}

public class FbsUpdateItemResponse
{
    public long item_id { get; set; }
}

public class FbsDeleteItemRequest
{
    public long ShopId { get; set; }
    public List<long> item_id_list { get; set; } = new();
}

public class FbsDeleteItemResponse
{
    public List<long> failed_list { get; set; } = new();
}

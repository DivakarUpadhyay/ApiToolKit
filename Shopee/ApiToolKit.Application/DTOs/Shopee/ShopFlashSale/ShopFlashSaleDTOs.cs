namespace ApiToolKit.Application.DTOs.Shopee.ShopFlashSale;

public class GetTimeSlotIdRequest
{
    public long StartTime { get; set; }
    public long EndTime { get; set; }
}

public class GetTimeSlotIdResponse
{
    public long TimeslotId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class CreateFlashSaleRequest
{
    public long TimeslotId { get; set; }
}

public class CreateFlashSaleResponse
{
    public long FlashSaleId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class FlashSaleItem
{
    public long ItemId { get; set; }
    public int PurchaseLimit { get; set; }
    public long OriginalPrice { get; set; }
    public long Price { get; set; }
    public int Stock { get; set; }
}

public class AddFlashSaleItemsRequest
{
    public long FlashSaleId { get; set; }
    public List<FlashSaleItem> Items { get; set; } = new();
}

public class AddFlashSaleItemsResponse
{
    public long FlashSaleId { get; set; }
    public List<FlashSaleItem> ItemList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class UpdateFlashSaleRequest
{
    public long FlashSaleId { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateFlashSaleResponse
{
    public long FlashSaleId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class DeleteFlashSaleRequest
{
    public long FlashSaleId { get; set; }
}

public class DeleteFlashSaleResponse
{
    public long FlashSaleId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetFlashSaleListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string Filter { get; set; } = string.Empty;
}

public class FlashSaleInfo
{
    public long FlashSaleId { get; set; }
    public long TimeslotId { get; set; }
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
}

public class GetFlashSaleListResponse
{
    public List<FlashSaleInfo> FlashSaleList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetFlashSaleRequest
{
    public long FlashSaleId { get; set; }
}

public class GetFlashSaleResponse
{
    public long FlashSaleId { get; set; }
    public long TimeslotId { get; set; }
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public int ItemCount { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class UpdateFlashSaleItemsRequest
{
    public long FlashSaleId { get; set; }
    public List<FlashSaleItem> Items { get; set; } = new();
}

public class UpdateFlashSaleItemsResponse
{
    public long FlashSaleId { get; set; }
    public List<FlashSaleItem> ItemList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class DeleteFlashSaleItemsRequest
{
    public long FlashSaleId { get; set; }
    public List<long> ItemIdList { get; set; } = new();
}

public class DeleteFlashSaleItemsResponse
{
    public long FlashSaleId { get; set; }
    public List<long> FailedList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetFlashSaleItemsRequest
{
    public long FlashSaleId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetFlashSaleItemsResponse
{
    public long FlashSaleId { get; set; }
    public List<FlashSaleItem> ItemList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetItemCriteriaRequest
{
    public List<long> ItemIdList { get; set; } = new();
}

public class GetItemCriteriaResponse
{
    public List<object> ItemCriteriaList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

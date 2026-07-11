namespace ApiToolKit.Application.DTOs.Shopee.ShopCategory;

public class AddShopCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public int SortWeight { get; set; }
}

public class AddShopCategoryResponse
{
    public long ShopCategoryId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetShopCategoryListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class ShopCategoryInfo
{
    public long ShopCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortWeight { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetShopCategoryListResponse
{
    public List<ShopCategoryInfo> ShopCategoryList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class DeleteShopCategoryRequest
{
    public long ShopCategoryId { get; set; }
}

public class DeleteShopCategoryResponse
{
    public long ShopCategoryId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class UpdateShopCategoryRequest
{
    public long ShopCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortWeight { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateShopCategoryResponse
{
    public long ShopCategoryId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class AddItemsOfShopCategoryRequest
{
    public long ShopCategoryId { get; set; }
    public List<long> ItemIdList { get; set; } = new();
}

public class AddItemsOfShopCategoryResponse
{
    public long ShopCategoryId { get; set; }
    public List<long> FailedList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetItemsOfShopCategoryRequest
{
    public long ShopCategoryId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetItemsOfShopCategoryResponse
{
    public List<long> ItemList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class DeleteItemsOfShopCategoryRequest
{
    public long ShopCategoryId { get; set; }
    public List<long> ItemIdList { get; set; } = new();
}

public class DeleteItemsOfShopCategoryResponse
{
    public long ShopCategoryId { get; set; }
    public List<long> FailedList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

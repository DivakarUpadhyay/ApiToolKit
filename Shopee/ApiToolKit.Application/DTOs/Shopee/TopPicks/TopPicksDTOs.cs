namespace ApiToolKit.Application.DTOs.Shopee.TopPicks;

public class GetTopPicksListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class TopPicksInfo
{
    public long TopPicksId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActivated { get; set; }
    public List<long> ItemList { get; set; } = new();
}

public class GetTopPicksListResponse
{
    public List<TopPicksInfo> TopPicksList { get; set; } = new();
    public int TotalCount { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class AddTopPicksRequest
{
    public string Name { get; set; } = string.Empty;
    public List<long> ItemIdList { get; set; } = new();
}

public class AddTopPicksResponse
{
    public long TopPicksId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class UpdateTopPicksRequest
{
    public long TopPicksId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<long> ItemIdList { get; set; } = new();
    public bool IsActivated { get; set; }
}

public class UpdateTopPicksResponse
{
    public long TopPicksId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class DeleteTopPicksRequest
{
    public long TopPicksId { get; set; }
}

public class DeleteTopPicksResponse
{
    public long TopPicksId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

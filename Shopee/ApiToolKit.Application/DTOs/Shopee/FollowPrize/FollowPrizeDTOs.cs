using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.FollowPrize;

public class GetFollowPrizeInfoRequest
{
    [Required] public long ShopId { get; set; }
}

public class GetFollowPrizeInfoResponse
{
    public long FollowPrizeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public int PrizeType { get; set; }
    public int PrizeQuantity { get; set; }
    public long PrizeItemId { get; set; }
}

public class CreateFollowPrizeRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public long StartTime { get; set; }
    [Required] public long EndTime { get; set; }
    [Required] public int PrizeType { get; set; }
    [Required] public int PrizeQuantity { get; set; }
    [Required] public long PrizeItemId { get; set; }
}

public class CreateFollowPrizeResponse
{
    public long FollowPrizeId { get; set; }
}

public class UpdateFollowPrizeRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long FollowPrizeId { get; set; }
    public string? Name { get; set; }
    public long? StartTime { get; set; }
    public long? EndTime { get; set; }
}

public class UpdateFollowPrizeResponse
{
    public long FollowPrizeId { get; set; }
}

public class EndFollowPrizeRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long FollowPrizeId { get; set; }
}

public class EndFollowPrizeResponse
{
    public long FollowPrizeId { get; set; }
}

public class GetFollowPrizeParticipantListRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long FollowPrizeId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetFollowPrizeParticipantListResponse
{
    public List<ParticipantInfo> ParticipantList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class ParticipantInfo
{
    public long UserId { get; set; }
    public long FollowTime { get; set; }
    public bool IsWinner { get; set; }
    public string PrizeStatus { get; set; } = string.Empty;
}

public class GetFollowPrizeWinnerListRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long FollowPrizeId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetFollowPrizeWinnerListResponse
{
    public List<WinnerInfo> WinnerList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class WinnerInfo
{
    public long UserId { get; set; }
    public long WinTime { get; set; }
    public string PrizeStatus { get; set; } = string.Empty;
    public string OrderSn { get; set; } = string.Empty;
}

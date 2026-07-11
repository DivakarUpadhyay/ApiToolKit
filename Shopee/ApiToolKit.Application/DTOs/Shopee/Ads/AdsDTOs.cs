namespace ApiToolKit.Application.DTOs.Shopee.Ads;

public class GetRecommendKeywordRequest
{
    public long ItemId { get; set; }
}

public class GetRecommendKeywordResponse
{
    public List<KeywordInfo> KeywordList { get; set; } = new();
}

public class KeywordInfo
{
    public string Keyword { get; set; } = string.Empty;
    public string MatchType { get; set; } = string.Empty;
    public long Bid { get; set; }
    public long CpcBid { get; set; }
    public bool IsRecommend { get; set; }
}

public class CreateCampaignRequest
{
    public string Name { get; set; } = string.Empty;
    public int CampaignType { get; set; }
    public int BudgetType { get; set; }
    public long Budget { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
}

public class CreateCampaignResponse
{
    public long CampaignId { get; set; }
}

public class GetCampaignListRequest
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetCampaignListResponse
{
    public List<CampaignInfo> CampaignList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class CampaignInfo
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int BudgetType { get; set; }
    public long Budget { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
}

public class GetCampaignRequest
{
    public long CampaignId { get; set; }
}

public class GetCampaignResponse
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int BudgetType { get; set; }
    public long Budget { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public long TotalImpression { get; set; }
    public long TotalClick { get; set; }
    public long TotalCost { get; set; }
}

public class UpdateCampaignRequest
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Budget { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateCampaignResponse
{
    public long CampaignId { get; set; }
}

public class DeleteCampaignRequest
{
    public List<long> CampaignIdList { get; set; } = new();
}

public class DeleteCampaignResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class CreateAdGroupRequest
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BidType { get; set; }
    public long Bid { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateAdGroupResponse
{
    public long AdgroupId { get; set; }
}

public class GetAdGroupListRequest
{
    public long CampaignId { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
}

public class GetAdGroupListResponse
{
    public List<AdGroupInfo> AdgroupList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class AdGroupInfo
{
    public long AdgroupId { get; set; }
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BidType { get; set; }
    public long Bid { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateAdGroupRequest
{
    public long AdgroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Bid { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateAdGroupResponse
{
    public long AdgroupId { get; set; }
}

public class CreateAdRequest
{
    public long AdgroupId { get; set; }
    public long ItemId { get; set; }
    public long ModelId { get; set; }
    public long Bid { get; set; }
}

public class CreateAdResponse
{
    public long AdId { get; set; }
}

public class GetAdListRequest
{
    public long AdgroupId { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
}

public class GetAdListResponse
{
    public List<AdInfo> AdList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class AdInfo
{
    public long AdId { get; set; }
    public long AdgroupId { get; set; }
    public long ItemId { get; set; }
    public long ModelId { get; set; }
    public long Bid { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateAdRequest
{
    public long AdId { get; set; }
    public long Bid { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateAdResponse
{
    public long AdId { get; set; }
}

public class GetAdDailyPerformanceRequest
{
    public long AdId { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
}

public class GetAdDailyPerformanceResponse
{
    public long AdId { get; set; }
    public List<AdPerformance> PerformanceList { get; set; } = new();
}

public class AdPerformance
{
    public string Date { get; set; } = string.Empty;
    public long Impression { get; set; }
    public long Click { get; set; }
    public long Cost { get; set; }
    public long Order { get; set; }
    public long Gmv { get; set; }
}

public class GetCampaignDailyPerformanceRequest
{
    public long CampaignId { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
}

public class GetCampaignDailyPerformanceResponse
{
    public long CampaignId { get; set; }
    public List<AdPerformance> PerformanceList { get; set; } = new();
}

public class AddKeywordRequest
{
    public long AdgroupId { get; set; }
    public List<KeywordInfo> KeywordList { get; set; } = new();
}

public class AddKeywordResponse
{
    public long AdgroupId { get; set; }
    public List<KeywordInfo> KeywordList { get; set; } = new();
}

public class GetKeywordListRequest
{
    public long AdgroupId { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
}

public class GetKeywordListResponse
{
    public List<KeywordInfo> KeywordList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class UpdateKeywordRequest
{
    public long AdgroupId { get; set; }
    public List<KeywordInfo> KeywordList { get; set; } = new();
}

public class UpdateKeywordResponse
{
    public long AdgroupId { get; set; }
    public List<KeywordInfo> KeywordList { get; set; } = new();
}

public class DeleteKeywordRequest
{
    public long AdgroupId { get; set; }
    public List<string> KeywordList { get; set; } = new();
}

public class DeleteKeywordResponse
{
    public long AdgroupId { get; set; }
    public List<string> FailedList { get; set; } = new();
}

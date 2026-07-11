namespace ApiToolKit.Application.DTOs.Shopee.Ams;

public class GetAffiliateCampaignListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string Status { get; set; } = string.Empty;
}

public class GetAffiliateCampaignListResponse
{
    public List<AffiliateCampaignInfo> CampaignList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class AffiliateCampaignInfo
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CampaignType { get; set; }
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public double CommissionRate { get; set; }
    public long Budget { get; set; }
    public long TotalClick { get; set; }
    public long TotalOrder { get; set; }
    public long TotalGmv { get; set; }
}

public class CreateAffiliateCampaignRequest
{
    public string Name { get; set; } = string.Empty;
    public int CampaignType { get; set; }
    public double CommissionRate { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public long Budget { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class CreateAffiliateCampaignResponse
{
    public long CampaignId { get; set; }
}

public class GetAffiliateCampaignRequest
{
    public long CampaignId { get; set; }
}

public class GetAffiliateCampaignResponse
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CampaignType { get; set; }
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public double CommissionRate { get; set; }
    public long Budget { get; set; }
    public string Description { get; set; } = string.Empty;
    public long TotalClick { get; set; }
    public long TotalOrder { get; set; }
    public long TotalGmv { get; set; }
}

public class UpdateAffiliateCampaignRequest
{
    public long CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double CommissionRate { get; set; }
    public long Budget { get; set; }
    public long EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UpdateAffiliateCampaignResponse
{
    public long CampaignId { get; set; }
}

public class DeleteAffiliateCampaignRequest
{
    public List<long> CampaignIdList { get; set; } = new();
}

public class DeleteAffiliateCampaignResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class AddItemToCampaignRequest
{
    public long CampaignId { get; set; }
    public List<long> ItemIdList { get; set; } = new();
}

public class AddItemToCampaignResponse
{
    public long CampaignId { get; set; }
    public List<long> FailedList { get; set; } = new();
}

public class RemoveItemFromCampaignRequest
{
    public long CampaignId { get; set; }
    public List<long> ItemIdList { get; set; } = new();
}

public class RemoveItemFromCampaignResponse
{
    public long CampaignId { get; set; }
    public List<long> FailedList { get; set; } = new();
}

public class GetCampaignItemListRequest
{
    public long CampaignId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetCampaignItemListResponse
{
    public long CampaignId { get; set; }
    public List<CampaignItemInfo> ItemList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class CampaignItemInfo
{
    public long ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ItemSku { get; set; } = string.Empty;
    public long Price { get; set; }
    public double CommissionRate { get; set; }
    public long Click { get; set; }
    public long Order { get; set; }
    public long Gmv { get; set; }
}

public class GetAffiliateOrderListRequest
{
    public long CampaignId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
}

public class GetAffiliateOrderListResponse
{
    public List<AffiliateOrderInfo> OrderList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class AffiliateOrderInfo
{
    public string OrderId { get; set; } = string.Empty;
    public long CampaignId { get; set; }
    public long AffiliateId { get; set; }
    public long ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public long OrderAmount { get; set; }
    public long Commission { get; set; }
    public string Status { get; set; } = string.Empty;
    public long CreateTime { get; set; }
}

public class GetAffiliateListRequest
{
    public long CampaignId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string Status { get; set; } = string.Empty;
}

public class GetAffiliateListResponse
{
    public List<AffiliateInfo> AffiliateList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class AffiliateInfo
{
    public long AffiliateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long TotalClick { get; set; }
    public long TotalOrder { get; set; }
    public long TotalGmv { get; set; }
    public long TotalCommission { get; set; }
}

public class GetAffiliateRequest
{
    public long AffiliateId { get; set; }
}

public class GetAffiliateResponse
{
    public long AffiliateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long ApprovedTime { get; set; }
    public long TotalClick { get; set; }
    public long TotalOrder { get; set; }
    public long TotalGmv { get; set; }
    public long TotalCommission { get; set; }
}

public class ApproveAffiliateRequest
{
    public List<long> AffiliateIdList { get; set; } = new();
}

public class ApproveAffiliateResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class RejectAffiliateRequest
{
    public List<long> AffiliateIdList { get; set; } = new();
    public string Reason { get; set; } = string.Empty;
}

public class RejectAffiliateResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class GetAffiliateLinkListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public long CampaignId { get; set; }
}

public class GetAffiliateLinkListResponse
{
    public List<AffiliateLinkInfo> LinkList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class AffiliateLinkInfo
{
    public long LinkId { get; set; }
    public long CampaignId { get; set; }
    public long AffiliateId { get; set; }
    public long ItemId { get; set; }
    public string LinkUrl { get; set; } = string.Empty;
    public string ShortLink { get; set; } = string.Empty;
    public long Click { get; set; }
    public long Order { get; set; }
    public long Gmv { get; set; }
    public long CreateTime { get; set; }
}

public class GetCampaignPerformanceRequest
{
    public long CampaignId { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
}

public class GetCampaignPerformanceResponse
{
    public long CampaignId { get; set; }
    public List<CampaignDailyPerformance> PerformanceList { get; set; } = new();
}

public class CampaignDailyPerformance
{
    public string Date { get; set; } = string.Empty;
    public long Click { get; set; }
    public long Order { get; set; }
    public long Gmv { get; set; }
    public long Commission { get; set; }
    public long UniqueClick { get; set; }
}

public class GetCommissionSettlementListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetCommissionSettlementListResponse
{
    public List<CommissionSettlement> SettlementList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class CommissionSettlement
{
    public long SettlementId { get; set; }
    public long AffiliateId { get; set; }
    public long Commission { get; set; }
    public string Status { get; set; } = string.Empty;
    public long SettleTime { get; set; }
    public int OrderCount { get; set; }
}

public class GetAmsConfigRequest { }

public class GetAmsConfigResponse
{
    public bool IsEnabled { get; set; }
    public double DefaultCommissionRate { get; set; }
    public double MaxCommissionRate { get; set; }
    public int CampaignCount { get; set; }
}

public class UpdateAmsConfigRequest
{
    public double DefaultCommissionRate { get; set; }
}

public class UpdateAmsConfigResponse
{
    public bool IsEnabled { get; set; }
    public double DefaultCommissionRate { get; set; }
}

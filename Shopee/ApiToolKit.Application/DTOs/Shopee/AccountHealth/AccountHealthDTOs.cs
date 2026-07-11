namespace ApiToolKit.Application.DTOs.Shopee.AccountHealth;

public class GetShopPenaltyRequest
{
    public long ShopId { get; set; }
}

public class GetShopPenaltyResponse
{
    public int penalty_point { get; set; }
    public List<ViolationInfo> violation_list { get; set; } = new();
}

public class ViolationInfo
{
    public string violation_type { get; set; } = string.Empty;
    public long violation_time { get; set; }
    public int penalty_point { get; set; }
    public string order_sn { get; set; } = string.Empty;
    public int offense_count { get; set; }
}

public class GetMetricListRequest
{
    public long ShopId { get; set; }
    public List<string> metric_list { get; set; } = new();
}

public class GetMetricListResponse
{
    public List<MetricData> metric_data { get; set; } = new();
}

public class MetricData
{
    public string metric_key { get; set; } = string.Empty;
    public double metric_value { get; set; }
    public double threshold { get; set; }
    public double score { get; set; }
    public string unit { get; set; } = string.Empty;
}

public class GetShopBriefPenaltyRequest
{
    public long ShopId { get; set; }
}

public class GetShopBriefPenaltyResponse
{
    public List<TierVariation> tier_variation_list { get; set; } = new();
}

public class TierVariation
{
    public string tier_variation_type { get; set; } = string.Empty;
    public int tier_variation_point { get; set; }
}

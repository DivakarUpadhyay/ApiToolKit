using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Ad;

public class CallToAction
{
    public string Type { get; set; } = string.Empty;
    public object? Value { get; set; }
}

public class LinkData
{
    public string? Link { get; set; }
    public string? Message { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public CallToAction? CallToAction { get; set; }
}

public class VideoData
{
    public string? VideoId { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? ImageUrl { get; set; }
    public CallToAction? CallToAction { get; set; }
}

public class ObjectStorySpec
{
    public string? PageId { get; set; }
    public LinkData? LinkData { get; set; }
    public VideoData? VideoData { get; set; }
}

public class AdCreativeInline
{
    public string? CreativeId { get; set; }
    public ObjectStorySpec? ObjectStorySpec { get; set; }
}

public class CreateAdRequest
{
    [Required] public string AdSetId { get; set; } = string.Empty;
    [Required] public string AccountId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = "PAUSED";
    public AdCreativeInline? Creative { get; set; }
}

public class CreateAdResponse
{
    public string Id { get; set; } = string.Empty;
}

public class AdResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? AdSetId { get; set; }
    public string? CampaignId { get; set; }
    public string? Status { get; set; }
    public string? CreativeId { get; set; }
    public string? CreatedTime { get; set; }
    public string? UpdatedTime { get; set; }
    public string? BidAmount { get; set; }
    public string? BidType { get; set; }
}

public class UpdateAdRequest
{
    [Required] public string AdId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Status { get; set; }
}

public class GetAdsRequest
{
    [Required] public string AdSetId { get; set; } = string.Empty;
    public string? Status { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,name,adset_id,campaign_id,status,creative,created_time,updated_time";
}

public class AdPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetAdsResponse
{
    public List<AdResponse> Data { get; set; } = new();
    public AdPaging? Paging { get; set; }
}

public class AdCreativeRequest
{
    [Required] public string AccountId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public ObjectStorySpec? ObjectStorySpec { get; set; }
}

public class AdCreativeResponse
{
    public string Id { get; set; } = string.Empty;
}

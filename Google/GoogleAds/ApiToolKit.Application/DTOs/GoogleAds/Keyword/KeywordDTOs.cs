using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Keyword;

public class ListKeywordsRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    public string? AdGroupResourceName { get; set; }
    public int PageSize { get; set; } = 50;
}

public class KeywordResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string AdGroupResourceName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string MatchType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long CpcBidMicros { get; set; }
    public int? QualityScore { get; set; }
}

public class KeywordListResponse
{
    public List<KeywordResponse> Keywords { get; set; } = new();
    public string? NextPageToken { get; set; }
}

public class CreateKeywordRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string AdGroupResourceName { get; set; } = string.Empty;
    [Required] public string Text { get; set; } = string.Empty;
    public string MatchType { get; set; } = "BROAD";
    public long CpcBidMicros { get; set; }
}

public class UpdateKeywordRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string ResourceName { get; set; } = string.Empty;
    public string? Status { get; set; }
    public long? CpcBidMicros { get; set; }
}

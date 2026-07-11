using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.LeadGen;

public class LeadGenFormQuestion
{
    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? Key { get; set; }
    public List<string>? Options { get; set; }
}

public class LeadGenPrivacyPolicy
{
    [Newtonsoft.Json.JsonProperty("url")] public string? Url { get; set; }
    [Newtonsoft.Json.JsonProperty("link_type")] public string? LinkType { get; set; } = "PRIVACY_POLICY";
}

public class LeadGenThankYouPage
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    [Newtonsoft.Json.JsonProperty("button_text")] public string? ButtonText { get; set; }
    [Newtonsoft.Json.JsonProperty("website_url")] public string? WebsiteUrl { get; set; }
}

public class CreateLeadGenFormRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public List<LeadGenFormQuestion> Questions { get; set; } = new();
    public LeadGenPrivacyPolicy? PrivacyPolicy { get; set; }
    public LeadGenThankYouPage? ThankYouPage { get; set; }
    public string? Locale { get; set; }
}

public class LeadGenFormResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    [Newtonsoft.Json.JsonProperty("created_time")] public string? CreatedTime { get; set; }
    [Newtonsoft.Json.JsonProperty("leads_count")] public int LeadsCount { get; set; }
    [Newtonsoft.Json.JsonProperty("is_continued_flow")] public bool IsContinuedFlow { get; set; }
    public List<LeadGenFormQuestion>? Questions { get; set; }
    [Newtonsoft.Json.JsonProperty("privacy_policy")] public LeadGenPrivacyPolicy? PrivacyPolicy { get; set; }
    [Newtonsoft.Json.JsonProperty("thank_you_page")] public LeadGenThankYouPage? ThankYouPage { get; set; }
}

public class GetLeadGenFormsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class GetLeadsRequest
{
    [Required] public string FormId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string? FilterStartTime { get; set; }
    public string? FilterEndTime { get; set; }
}

public class LeadFieldData
{
    public string? Name { get; set; }
    public List<string>? Values { get; set; }
}

public class LeadResponse
{
    public string? Id { get; set; }
    [Newtonsoft.Json.JsonProperty("created_time")] public string? CreatedTime { get; set; }
    [Newtonsoft.Json.JsonProperty("field_data")] public List<LeadFieldData>? FieldData { get; set; }
    [Newtonsoft.Json.JsonProperty("ad_id")] public string? AdId { get; set; }
    [Newtonsoft.Json.JsonProperty("ad_name")] public string? AdName { get; set; }
    [Newtonsoft.Json.JsonProperty("campaign_id")] public string? CampaignId { get; set; }
    [Newtonsoft.Json.JsonProperty("campaign_name")] public string? CampaignName { get; set; }
    [Newtonsoft.Json.JsonProperty("form_id")] public string? FormId { get; set; }
}

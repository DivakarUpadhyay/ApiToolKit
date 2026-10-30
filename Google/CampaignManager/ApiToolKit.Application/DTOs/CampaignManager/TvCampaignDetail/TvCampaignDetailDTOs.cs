using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.TvCampaignDetail;

/// <summary>Gets one TvCampaignDetail by ID.</summary>
public class GetTvCampaignDetailRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Required.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>TvCampaignDetailId (Required.)</summary>
    [Required] public string TvCampaignDetailId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? CountryDartId { get; set; }
    /// <summary>Required.</summary>
    public string? AccountId { get; set; }
    /// <summary>Optional.</summary>
    public string? TvDataProvider { get; set; }
}

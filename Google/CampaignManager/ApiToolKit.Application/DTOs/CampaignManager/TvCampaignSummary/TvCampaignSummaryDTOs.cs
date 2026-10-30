using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.TvCampaignSummary;

/// <summary>Retrieves a list of TV campaign summaries.</summary>
public class ListTvCampaignSummariesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Required.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? CountryDartId { get; set; }
    /// <summary>Required.</summary>
    public string? Name { get; set; }
    /// <summary>Required.</summary>
    public string? AccountId { get; set; }
    /// <summary>Optional.</summary>
    public string? TvDataProvider { get; set; }
}

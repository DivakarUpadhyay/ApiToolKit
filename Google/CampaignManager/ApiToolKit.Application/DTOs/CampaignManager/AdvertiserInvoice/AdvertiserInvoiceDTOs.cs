using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.AdvertiserInvoice;

/// <summary>Retrieves a list of invoices for a particular issue month.</summary>
public class ListAdvertiserInvoicesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserId (User profile ID associated with this request.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Month for which invoices are needed in the format YYYYMM.</summary>
    public string? IssueMonth { get; set; }
}

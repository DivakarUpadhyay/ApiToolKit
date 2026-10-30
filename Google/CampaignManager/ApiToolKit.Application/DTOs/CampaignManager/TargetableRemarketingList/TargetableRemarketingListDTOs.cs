using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.TargetableRemarketingList;

/// <summary>Retrieves a list of targetable remarketing lists, possibly filtered.</summary>
public class ListTargetableRemarketingListsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? Name { get; set; }
    /// <summary>Select only active or only inactive targetable remarketing lists.</summary>
    public bool? Active { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
}

/// <summary>Gets one remarketing list by ID.</summary>
public class GetTargetableRemarketingListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>TargetableRemarketingListId (User profile ID associated with this request.)</summary>
    [Required] public string TargetableRemarketingListId { get; set; } = string.Empty;
}

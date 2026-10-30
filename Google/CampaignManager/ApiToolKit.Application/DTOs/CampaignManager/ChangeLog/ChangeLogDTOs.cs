using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.ChangeLog;

/// <summary>Gets one change log by ID.</summary>
public class GetChangeLogRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>ChangeLogId (User profile ID associated with this request.)</summary>
    [Required] public string ChangeLogId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of change logs.</summary>
public class ListChangeLogsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only change logs whose change time is before the specified maxChangeTime.The time should be formatted as an RFC3339 date/time string.</summary>
    public string? MaxChangeTime { get; set; }
    /// <summary>Select only change logs with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only change logs with these object IDs.</summary>
    public List<string>? ObjectIds { get; set; }
    /// <summary>Select only change logs whose change time is after the specified minChangeTime.The time should be formatted as an RFC3339 date/time string.</summary>
    public string? MinChangeTime { get; set; }
    /// <summary>Select only change logs with these user profile IDs.</summary>
    public List<string>? UserProfileIds { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only change logs with the specified action.</summary>
    public string? Action { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only change logs whose object ID, user name, old or new values match the search string.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only change logs with the specified object type.</summary>
    public string? ObjectType { get; set; }
}

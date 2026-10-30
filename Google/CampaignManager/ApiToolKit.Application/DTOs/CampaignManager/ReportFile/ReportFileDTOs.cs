using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.ReportFile;

/// <summary>Lists files for a report.</summary>
public class ListFilesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the parent report.)</summary>
    [Required] public string ReportId { get; set; } = string.Empty;
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>The field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>The value of the nextToken from the previous result page.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Retrieves a report file by its report ID and file ID.</summary>
public class GetFileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the report.)</summary>
    [Required] public string ReportId { get; set; } = string.Empty;
    /// <summary>FileId (The ID of the report file.)</summary>
    [Required] public string FileId { get; set; } = string.Empty;
}

/// <summary>Download the file contents of: Retrieves a report file by its report ID and file ID.</summary>
public class GetFileContentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the report.)</summary>
    [Required] public string ReportId { get; set; } = string.Empty;
    /// <summary>FileId (The ID of the report file.)</summary>
    [Required] public string FileId { get; set; } = string.Empty;
}

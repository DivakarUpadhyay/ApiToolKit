using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Report;

/// <summary>Runs a report.</summary>
public class RunReportRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the report.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ReportId { get; set; } = string.Empty;
    /// <summary>If set and true, tries to run the report synchronously.</summary>
    public bool? Synchronous { get; set; }
}

/// <summary>Deletes a report by its ID.</summary>
public class DeleteReportRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the report.)</summary>
    [Required] public string ReportId { get; set; } = string.Empty;
}

/// <summary>Retrieves list of reports.</summary>
public class ListReportsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>The value of the nextToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>The scope that defines which results are returned.</summary>
    public string? Scope { get; set; }
    /// <summary>The field by which to sort the list.</summary>
    public string? SortField { get; set; }
}

/// <summary>Retrieves a report by its ID.</summary>
public class GetReportRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the report.)</summary>
    [Required] public string ReportId { get; set; } = string.Empty;
}

/// <summary>Updates a report.</summary>
public class UpdateReportRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProfileId { get; set; } = string.Empty;
    /// <summary>ReportId (The ID of the report.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ReportId { get; set; } = string.Empty;
    /// <summary>Report JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a report.</summary>
public class InsertReportRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProfileId { get; set; } = string.Empty;
    /// <summary>Report JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

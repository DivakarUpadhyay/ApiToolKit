using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSource;

/// <summary>Performs the data fetch immediately (even outside fetch schedule) on a data source from your Merchant Center Account.</summary>
public class FetchDataSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>DataSourceId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string DataSourceId { get; set; } = string.Empty;
    /// <summary>FetchDataSourceRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/datasources_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes a data source from your Merchant Center account.</summary>
public class DeleteDataSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>DataSourceId (Required.)</summary>
    [Required] public string DataSourceId { get; set; } = string.Empty;
}

/// <summary>Creates the new data source configuration for the given account.</summary>
public class CreateDataSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>DataSource JSON object as documented at https://developers.google.com/merchant/api/reference/rest/datasources_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Lists the configurations for data sources for the given account.</summary>
public class ListDataSourcesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Updates the existing data source configuration.</summary>
public class UpdateDataSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>DataSourceId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string DataSourceId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>DataSource JSON object as documented at https://developers.google.com/merchant/api/reference/rest/datasources_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves the data source configuration for the given account.</summary>
public class GetDataSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>DataSourceId (Required.)</summary>
    [Required] public string DataSourceId { get; set; } = string.Empty;
}

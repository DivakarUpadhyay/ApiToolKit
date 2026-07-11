using ApiToolKit.Application.DTOs.BigQuery.Job;
using ApiToolKit.Application.DTOs.BigQuery.Table;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.BigQuery;

public class BigQueryTableService : IBigQueryTableService
{
    private readonly IBigQueryApiClient _apiClient;
    private readonly IBigQueryTableRepository _tableRepo;
    private const string BaseUrl = "https://bigquery.googleapis.com/bigquery/v2";

    public BigQueryTableService(IBigQueryApiClient apiClient, IBigQueryTableRepository tableRepo)
    {
        _apiClient = apiClient;
        _tableRepo = tableRepo;
    }

    public async Task<Result<ListTablesResponse>> ListTablesAsync(ListTablesRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets/{Uri.EscapeDataString(request.DatasetId)}/tables" +
                  $"?maxResults={request.MaxResults}";
        if (!string.IsNullOrEmpty(request.PageToken))
            url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";

        var response = await _apiClient.GetAsync<ListTablesResponse>(url, accessToken);
        if (response == null)
            return Result<ListTablesResponse>.Fail("No response from BigQuery API.");

        return Result<ListTablesResponse>.Ok(response);
    }

    public async Task<Result<TableResponse>> GetTableAsync(string projectId, string datasetId, string tableId, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(projectId)}/datasets/{Uri.EscapeDataString(datasetId)}/tables/{Uri.EscapeDataString(tableId)}";
        var response = await _apiClient.GetAsync<TableResponse>(url, accessToken);
        if (response == null)
            return Result<TableResponse>.Fail("Table not found or no response from BigQuery API.");

        return Result<TableResponse>.Ok(response);
    }

    public async Task<Result<TableResponse>> CreateTableAsync(CreateTableRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets/{Uri.EscapeDataString(request.DatasetId)}/tables";

        var body = new
        {
            tableReference = new
            {
                projectId = request.ProjectId,
                datasetId = request.DatasetId,
                tableId = request.TableId
            },
            friendlyName = request.FriendlyName,
            description = request.Description,
            schema = request.Schema,
            timePartitioning = request.TimePartitioning,
            rangePartitioning = request.RangePartitioning,
            clustering = request.Clustering,
            labels = request.Labels,
            expirationTime = request.ExpirationTime?.ToString(),
            requirePartitionFilter = request.RequirePartitionFilter,
            encryptionConfiguration = request.EncryptionConfiguration,
            view = request.View,
            materializedView = request.MaterializedView
        };

        var response = await _apiClient.PostAsync<TableResponse>(url, body, accessToken);
        if (response == null)
            return Result<TableResponse>.Fail("Failed to create table.");

        var table = new BigQueryTableInfo
        {
            TableId = request.TableId,
            DatasetId = request.DatasetId,
            ProjectId = request.ProjectId,
            Type = response.Type,
            FriendlyName = request.FriendlyName,
            Description = request.Description,
            NumRows = response.NumRows,
            NumBytes = response.NumBytes,
            CreatedTime = response.CreationTime,
            LastModifiedTime = response.LastModifiedTime,
            LocalCreatedAt = DateTime.UtcNow
        };
        await _tableRepo.SaveTableAsync(table);

        return Result<TableResponse>.Ok(response);
    }

    public async Task<Result<TableResponse>> UpdateTableAsync(UpdateTableRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets/{Uri.EscapeDataString(request.DatasetId)}/tables/{Uri.EscapeDataString(request.TableId)}";

        var body = new
        {
            friendlyName = request.FriendlyName,
            description = request.Description,
            schema = request.Schema,
            labels = request.Labels,
            expirationTime = request.ExpirationTime?.ToString(),
            requirePartitionFilter = request.RequirePartitionFilter
        };

        var response = await _apiClient.PatchAsync<TableResponse>(url, body, accessToken);
        if (response == null)
            return Result<TableResponse>.Fail("Failed to update table.");

        var existing = (await _tableRepo.GetByDatasetIdAsync(request.DatasetId, request.ProjectId))
            .FirstOrDefault(t => t.TableId == request.TableId);

        if (existing != null)
        {
            existing.FriendlyName = request.FriendlyName;
            existing.Description = request.Description;
            existing.LastModifiedTime = response.LastModifiedTime;
            await _tableRepo.UpdateTableAsync(existing);
        }

        return Result<TableResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteTableAsync(DeleteTableRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets/{Uri.EscapeDataString(request.DatasetId)}/tables/{Uri.EscapeDataString(request.TableId)}";
        await _apiClient.DeleteAsync(url, accessToken);
        await _tableRepo.DeleteTableAsync(request.TableId, request.DatasetId, request.ProjectId);

        return Result<bool>.Ok(true, "Table deleted successfully.");
    }

    public async Task<Result<JobResponse>> CopyTableAsync(CopyTableRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/jobs";

        var body = new
        {
            configuration = new
            {
                copy = new
                {
                    sourceTables = new[]
                    {
                        new { projectId = request.ProjectId, datasetId = request.SourceDatasetId, tableId = request.SourceTableId }
                    },
                    destinationTable = new
                    {
                        projectId = request.ProjectId,
                        datasetId = request.DestDatasetId,
                        tableId = request.DestTableId
                    },
                    writeDisposition = request.WriteDisposition,
                    createDisposition = request.CreateDisposition
                }
            }
        };

        var response = await _apiClient.PostAsync<JobResponse>(url, body, accessToken);
        if (response == null)
            return Result<JobResponse>.Fail("Failed to copy table.");

        return Result<JobResponse>.Ok(response);
    }
}

using ApiToolKit.Application.DTOs.BigQuery.Dataset;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.BigQuery;

public class BigQueryDatasetService : IBigQueryDatasetService
{
    private readonly IBigQueryApiClient _apiClient;
    private readonly IBigQueryDatasetRepository _datasetRepo;
    private readonly IBigQueryAuthRepository _authRepo;
    private const string BaseUrl = "https://bigquery.googleapis.com/bigquery/v2";

    public BigQueryDatasetService(
        IBigQueryApiClient apiClient,
        IBigQueryDatasetRepository datasetRepo,
        IBigQueryAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _datasetRepo = datasetRepo;
        _authRepo = authRepo;
    }

    public async Task<Result<ListDatasetsResponse>> ListDatasetsAsync(ListDatasetsRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets" +
                  $"?maxResults={request.MaxResults}&all={request.All.ToString().ToLower()}";
        if (!string.IsNullOrEmpty(request.PageToken))
            url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.Filter))
            url += $"&filter={Uri.EscapeDataString(request.Filter)}";

        var response = await _apiClient.GetAsync<ListDatasetsResponse>(url, accessToken);
        if (response == null)
            return Result<ListDatasetsResponse>.Fail("No response from BigQuery API.");

        return Result<ListDatasetsResponse>.Ok(response);
    }

    public async Task<Result<DatasetResponse>> GetDatasetAsync(string projectId, string datasetId, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(projectId)}/datasets/{Uri.EscapeDataString(datasetId)}";
        var response = await _apiClient.GetAsync<DatasetResponse>(url, accessToken);
        if (response == null)
            return Result<DatasetResponse>.Fail("Dataset not found or no response from BigQuery API.");

        return Result<DatasetResponse>.Ok(response);
    }

    public async Task<Result<DatasetResponse>> CreateDatasetAsync(CreateDatasetRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets";

        var body = new
        {
            datasetReference = new { datasetId = request.DatasetId, projectId = request.ProjectId },
            friendlyName = request.FriendlyName,
            description = request.Description,
            location = request.Location,
            defaultTableExpirationMs = request.DefaultTableExpirationMs?.ToString(),
            labels = request.Labels
        };

        var response = await _apiClient.PostAsync<DatasetResponse>(url, body, accessToken);
        if (response == null)
            return Result<DatasetResponse>.Fail("Failed to create dataset.");

        var dataset = new BigQueryDataset
        {
            DatasetId = request.DatasetId,
            ProjectId = request.ProjectId,
            FriendlyName = request.FriendlyName,
            Description = request.Description,
            Location = request.Location,
            CreatedAt = response.CreationTime,
            LastModifiedTime = response.LastModifiedTime,
            LocalCreatedAt = DateTime.UtcNow
        };
        await _datasetRepo.SaveDatasetAsync(dataset);

        return Result<DatasetResponse>.Ok(response);
    }

    public async Task<Result<DatasetResponse>> UpdateDatasetAsync(UpdateDatasetRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets/{Uri.EscapeDataString(request.DatasetId)}";

        var body = new
        {
            friendlyName = request.FriendlyName,
            description = request.Description,
            defaultTableExpirationMs = request.DefaultTableExpirationMs?.ToString(),
            access = request.Access,
            labels = request.Labels
        };

        var response = await _apiClient.PatchAsync<DatasetResponse>(url, body, accessToken);
        if (response == null)
            return Result<DatasetResponse>.Fail("Failed to update dataset.");

        var existing = (await _datasetRepo.GetByProjectIdAsync(request.ProjectId))
            .FirstOrDefault(d => d.DatasetId == request.DatasetId);

        if (existing != null)
        {
            existing.FriendlyName = request.FriendlyName;
            existing.Description = request.Description;
            existing.LastModifiedTime = response.LastModifiedTime;
            await _datasetRepo.UpdateDatasetAsync(existing);
        }

        return Result<DatasetResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteDatasetAsync(DeleteDatasetRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/datasets/{Uri.EscapeDataString(request.DatasetId)}" +
                  $"?deleteContents={request.DeleteContents.ToString().ToLower()}";

        await _apiClient.DeleteAsync(url, accessToken);
        await _datasetRepo.DeleteDatasetAsync(request.DatasetId, request.ProjectId);

        return Result<bool>.Ok(true, "Dataset deleted successfully.");
    }
}

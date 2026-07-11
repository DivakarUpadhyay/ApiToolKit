using ApiToolKit.Application.DTOs.BigQuery.Job;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.BigQuery;

public class BigQueryJobService : IBigQueryJobService
{
    private readonly IBigQueryApiClient _apiClient;
    private readonly IBigQueryJobRepository _jobRepo;
    private readonly IBigQueryAuthRepository _authRepo;
    private const string BaseUrl = "https://bigquery.googleapis.com/bigquery/v2";

    public BigQueryJobService(
        IBigQueryApiClient apiClient,
        IBigQueryJobRepository jobRepo,
        IBigQueryAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _jobRepo = jobRepo;
        _authRepo = authRepo;
    }

    public async Task<Result<QueryResponse>> QueryAsync(QueryRequest request)
    {
        var token = await _authRepo.GetTokenByProjectIdAsync(request.ProjectId);
        if (token == null)
            return Result<QueryResponse>.Fail("Access token not found for project.");

        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/queries";

        var body = new
        {
            query = request.Query,
            maxResults = request.MaxResults,
            defaultDataset = request.DefaultDataset,
            location = request.Location,
            useLegacySql = request.UseLegacySql,
            queryParameters = request.QueryParameters,
            timeoutMs = request.TimeoutMs,
            createSession = request.CreateSession
        };

        var response = await _apiClient.PostAsync<QueryResponse>(url, body, token.AccessToken);
        if (response == null)
            return Result<QueryResponse>.Fail("No response from BigQuery API.");

        if (response.JobReference?.JobId != null)
        {
            var job = new BigQueryJob
            {
                JobId = response.JobReference.JobId,
                ProjectId = request.ProjectId,
                JobType = "QUERY",
                Status = response.JobComplete ? "DONE" : "RUNNING",
                TotalRows = response.TotalRows,
                TotalBytesProcessed = response.TotalBytesProcessed,
                Query = request.Query,
                LocalCreatedAt = DateTime.UtcNow
            };
            await _jobRepo.SaveJobAsync(job);
        }

        return Result<QueryResponse>.Ok(response);
    }

    public async Task<Result<InsertAllResponse>> InsertAllAsync(InsertAllRequest request)
    {
        var token = await _authRepo.GetTokenByProjectIdAsync(request.ProjectId);
        if (token == null)
            return Result<InsertAllResponse>.Fail("Access token not found for project.");

        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}" +
                  $"/datasets/{Uri.EscapeDataString(request.DatasetId)}" +
                  $"/tables/{Uri.EscapeDataString(request.TableId)}/insertAll";

        var body = new
        {
            rows = request.Rows,
            skipInvalidRows = request.SkipInvalidRows,
            ignoreUnknownValues = request.IgnoreUnknownValues,
            templateSuffix = request.TemplateSuffix
        };

        var response = await _apiClient.PostAsync<InsertAllResponse>(url, body, token.AccessToken);
        if (response == null)
            return Result<InsertAllResponse>.Fail("No response from BigQuery API.");

        return Result<InsertAllResponse>.Ok(response);
    }

    public async Task<Result<ListRowsResponse>> ListRowsAsync(ListRowsRequest request)
    {
        var token = await _authRepo.GetTokenByProjectIdAsync(request.ProjectId);
        if (token == null)
            return Result<ListRowsResponse>.Fail("Access token not found for project.");

        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}" +
                  $"/datasets/{Uri.EscapeDataString(request.DatasetId)}" +
                  $"/tables/{Uri.EscapeDataString(request.TableId)}/data" +
                  $"?maxResults={request.MaxResults}";

        if (!string.IsNullOrEmpty(request.PageToken))
            url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.SelectedFields))
            url += $"&selectedFields={Uri.EscapeDataString(request.SelectedFields)}";

        var response = await _apiClient.GetAsync<ListRowsResponse>(url, token.AccessToken);
        if (response == null)
            return Result<ListRowsResponse>.Fail("No response from BigQuery API.");

        return Result<ListRowsResponse>.Ok(response);
    }

    public async Task<Result<JobResponse>> CreateJobAsync(CreateJobRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/jobs";

        object configuration;
        string jobType;

        if (request.QueryConfig != null)
        {
            jobType = "QUERY";
            configuration = new { query = request.QueryConfig };
        }
        else if (request.LoadConfig != null)
        {
            jobType = "LOAD";
            configuration = new { load = request.LoadConfig };
        }
        else if (request.ExtractConfig != null)
        {
            jobType = "EXTRACT";
            configuration = new { extract = request.ExtractConfig };
        }
        else
        {
            return Result<JobResponse>.Fail("At least one job configuration must be provided.");
        }

        var jobRef = string.IsNullOrEmpty(request.JobId)
            ? (object)new { projectId = request.ProjectId, location = request.Location }
            : new { projectId = request.ProjectId, jobId = request.JobId, location = request.Location };

        var body = new
        {
            jobReference = jobRef,
            configuration
        };

        var response = await _apiClient.PostAsync<JobResponse>(url, body, accessToken);
        if (response == null)
            return Result<JobResponse>.Fail("Failed to create job.");

        if (response.JobReference?.JobId != null)
        {
            var job = new BigQueryJob
            {
                JobId = response.JobReference.JobId,
                ProjectId = request.ProjectId,
                JobType = jobType,
                Status = response.Status?.State ?? "RUNNING",
                CreationTime = response.Statistics?.CreationTime ?? 0,
                StartTime = response.Statistics?.StartTime ?? 0,
                EndTime = response.Statistics?.EndTime ?? 0,
                TotalBytesProcessed = response.Statistics?.TotalBytesProcessed ?? 0,
                Query = request.QueryConfig?.Query,
                LocalCreatedAt = DateTime.UtcNow
            };
            await _jobRepo.SaveJobAsync(job);
        }

        return Result<JobResponse>.Ok(response);
    }

    public async Task<Result<JobResponse>> GetJobAsync(GetJobRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/jobs/{Uri.EscapeDataString(request.JobId)}";
        if (!string.IsNullOrEmpty(request.Location))
            url += $"?location={Uri.EscapeDataString(request.Location)}";

        var response = await _apiClient.GetAsync<JobResponse>(url, accessToken);
        if (response == null)
            return Result<JobResponse>.Fail("Job not found or no response from BigQuery API.");

        return Result<JobResponse>.Ok(response);
    }

    public async Task<Result<ListJobsResponse>> ListJobsAsync(ListJobsRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/jobs" +
                  $"?maxResults={request.MaxResults}&allUsers={request.AllUsers.ToString().ToLower()}&projection={request.Projection}";

        if (!string.IsNullOrEmpty(request.PageToken))
            url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.StateFilter))
            url += $"&stateFilter={Uri.EscapeDataString(request.StateFilter)}";
        if (request.MinCreationTime.HasValue)
            url += $"&minCreationTime={request.MinCreationTime}";
        if (request.MaxCreationTime.HasValue)
            url += $"&maxCreationTime={request.MaxCreationTime}";
        if (!string.IsNullOrEmpty(request.ParentJobId))
            url += $"&parentJobId={Uri.EscapeDataString(request.ParentJobId)}";

        var response = await _apiClient.GetAsync<ListJobsResponse>(url, accessToken);
        if (response == null)
            return Result<ListJobsResponse>.Fail("No response from BigQuery API.");

        return Result<ListJobsResponse>.Ok(response);
    }

    public async Task<Result<JobResponse>> CancelJobAsync(CancelJobRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}/jobs/{Uri.EscapeDataString(request.JobId)}/cancel";
        if (!string.IsNullOrEmpty(request.Location))
            url += $"?location={Uri.EscapeDataString(request.Location)}";

        var response = await _apiClient.PostAsync<dynamic>(url, new { }, accessToken);

        var jobs = await _jobRepo.GetJobsByProjectIdAsync(request.ProjectId);
        var existing = jobs.FirstOrDefault(j => j.JobId == request.JobId);
        if (existing != null)
        {
            existing.Status = "CANCELLED";
            await _jobRepo.UpdateJobAsync(existing);
        }

        return Result<JobResponse>.Ok(new JobResponse
        {
            JobReference = new JobReference { ProjectId = request.ProjectId, JobId = request.JobId },
            Status = new JobStatus { State = "DONE" }
        }, "Job cancelled successfully.");
    }

    public async Task<Result<QueryResponse>> GetQueryResultsAsync(string projectId, string jobId, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(projectId)}/queries/{Uri.EscapeDataString(jobId)}";

        var response = await _apiClient.GetAsync<QueryResponse>(url, accessToken);
        if (response == null)
            return Result<QueryResponse>.Fail("No response from BigQuery API.");

        return Result<QueryResponse>.Ok(response);
    }
}

using ApiToolKit.Application.DTOs.BigQuery.Job;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BigQuery;

public interface IBigQueryJobService
{
    Task<Result<QueryResponse>> QueryAsync(QueryRequest request);
    Task<Result<InsertAllResponse>> InsertAllAsync(InsertAllRequest request);
    Task<Result<ListRowsResponse>> ListRowsAsync(ListRowsRequest request);
    Task<Result<JobResponse>> CreateJobAsync(CreateJobRequest request, string accessToken);
    Task<Result<JobResponse>> GetJobAsync(GetJobRequest request, string accessToken);
    Task<Result<ListJobsResponse>> ListJobsAsync(ListJobsRequest request, string accessToken);
    Task<Result<JobResponse>> CancelJobAsync(CancelJobRequest request, string accessToken);
    Task<Result<QueryResponse>> GetQueryResultsAsync(string projectId, string jobId, string accessToken);
}

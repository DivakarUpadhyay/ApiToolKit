using ApiToolKit.Application.DTOs.BigQuery.Job;
using ApiToolKit.Application.DTOs.BigQuery.Table;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BigQuery;

public interface IBigQueryTableService
{
    Task<Result<ListTablesResponse>> ListTablesAsync(ListTablesRequest request, string accessToken);
    Task<Result<TableResponse>> GetTableAsync(string projectId, string datasetId, string tableId, string accessToken);
    Task<Result<TableResponse>> CreateTableAsync(CreateTableRequest request, string accessToken);
    Task<Result<TableResponse>> UpdateTableAsync(UpdateTableRequest request, string accessToken);
    Task<Result<bool>> DeleteTableAsync(DeleteTableRequest request, string accessToken);
    Task<Result<JobResponse>> CopyTableAsync(CopyTableRequest request, string accessToken);
}

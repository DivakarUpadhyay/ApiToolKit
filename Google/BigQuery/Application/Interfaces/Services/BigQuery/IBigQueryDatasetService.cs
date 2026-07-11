using ApiToolKit.Application.DTOs.BigQuery.Dataset;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BigQuery;

public interface IBigQueryDatasetService
{
    Task<Result<ListDatasetsResponse>> ListDatasetsAsync(ListDatasetsRequest request, string accessToken);
    Task<Result<DatasetResponse>> GetDatasetAsync(string projectId, string datasetId, string accessToken);
    Task<Result<DatasetResponse>> CreateDatasetAsync(CreateDatasetRequest request, string accessToken);
    Task<Result<DatasetResponse>> UpdateDatasetAsync(UpdateDatasetRequest request, string accessToken);
    Task<Result<bool>> DeleteDatasetAsync(DeleteDatasetRequest request, string accessToken);
}

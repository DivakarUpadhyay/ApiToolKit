using ApiToolKit.Domain.Entities.BigQuery;

namespace ApiToolKit.Domain.Interfaces.Repositories.BigQuery;

public interface IBigQueryDatasetRepository
{
    Task SaveDatasetAsync(BigQueryDataset dataset);
    Task UpdateDatasetAsync(BigQueryDataset dataset);
    Task DeleteDatasetAsync(string datasetId, string projectId);
    Task<IEnumerable<BigQueryDataset>> GetByProjectIdAsync(string projectId);
}

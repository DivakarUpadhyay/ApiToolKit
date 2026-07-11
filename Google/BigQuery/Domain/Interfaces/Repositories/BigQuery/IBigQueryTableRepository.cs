using ApiToolKit.Domain.Entities.BigQuery;

namespace ApiToolKit.Domain.Interfaces.Repositories.BigQuery;

public interface IBigQueryTableRepository
{
    Task SaveTableAsync(BigQueryTableInfo table);
    Task UpdateTableAsync(BigQueryTableInfo table);
    Task DeleteTableAsync(string tableId, string datasetId, string projectId);
    Task<IEnumerable<BigQueryTableInfo>> GetByDatasetIdAsync(string datasetId, string projectId);
}

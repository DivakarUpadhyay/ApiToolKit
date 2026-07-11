using ApiToolKit.Domain.Entities.BigQuery;

namespace ApiToolKit.Domain.Interfaces.Repositories.BigQuery;

public interface IBigQueryJobRepository
{
    Task SaveJobAsync(BigQueryJob job);
    Task UpdateJobAsync(BigQueryJob job);
    Task<IEnumerable<BigQueryJob>> GetJobsByProjectIdAsync(string projectId);
}

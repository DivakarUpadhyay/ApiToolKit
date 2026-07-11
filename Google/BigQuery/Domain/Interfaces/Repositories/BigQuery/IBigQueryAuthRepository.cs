using ApiToolKit.Domain.Entities.BigQuery;

namespace ApiToolKit.Domain.Interfaces.Repositories.BigQuery;

public interface IBigQueryAuthRepository
{
    Task<BigQueryConfig?> GetConfigAsync();
    Task<BigQueryToken?> GetTokenByProjectIdAsync(string projectId);
    Task SaveTokenAsync(BigQueryToken token);
    Task UpdateTokenAsync(BigQueryToken token);
    Task SaveApiLogAsync(BigQueryApiLog log);
}

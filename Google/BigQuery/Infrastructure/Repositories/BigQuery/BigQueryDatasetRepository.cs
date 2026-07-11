using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.BigQuery;

public class BigQueryDatasetRepository : IBigQueryDatasetRepository
{
    private readonly DapperContext _context;

    public BigQueryDatasetRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveDatasetAsync(BigQueryDataset dataset)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_SaveDataset",
            new
            {
                dataset.DatasetId,
                dataset.ProjectId,
                dataset.FriendlyName,
                dataset.Description,
                dataset.Location,
                dataset.CreatedAt,
                dataset.LastModifiedTime
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateDatasetAsync(BigQueryDataset dataset)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_UpdateDataset",
            new
            {
                dataset.Id,
                dataset.FriendlyName,
                dataset.Description,
                dataset.LastModifiedTime
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteDatasetAsync(string datasetId, string projectId)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_DeleteDataset",
            new { DatasetId = datasetId, ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<BigQueryDataset>> GetByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<BigQueryDataset>(
            "sp_BigQuery_GetDatasetsByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

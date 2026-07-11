using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.BigQuery;

public class BigQueryTableRepository : IBigQueryTableRepository
{
    private readonly DapperContext _context;

    public BigQueryTableRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveTableAsync(BigQueryTableInfo table)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_SaveTable",
            new
            {
                table.TableId,
                table.DatasetId,
                table.ProjectId,
                table.Type,
                table.FriendlyName,
                table.Description,
                table.NumRows,
                table.NumBytes,
                table.CreatedTime,
                table.LastModifiedTime
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateTableAsync(BigQueryTableInfo table)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_UpdateTable",
            new
            {
                table.Id,
                table.FriendlyName,
                table.Description,
                table.NumRows,
                table.NumBytes,
                table.LastModifiedTime
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteTableAsync(string tableId, string datasetId, string projectId)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_DeleteTable",
            new { TableId = tableId, DatasetId = datasetId, ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<BigQueryTableInfo>> GetByDatasetIdAsync(string datasetId, string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<BigQueryTableInfo>(
            "sp_BigQuery_GetTablesByDatasetId",
            new { DatasetId = datasetId, ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

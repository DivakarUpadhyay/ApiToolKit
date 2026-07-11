using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.BigQuery;

public class BigQueryJobRepository : IBigQueryJobRepository
{
    private readonly DapperContext _context;

    public BigQueryJobRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveJobAsync(BigQueryJob job)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_SaveJob",
            new
            {
                job.JobId,
                job.ProjectId,
                job.JobType,
                job.Status,
                job.CreationTime,
                job.StartTime,
                job.EndTime,
                job.TotalRows,
                job.TotalBytesProcessed,
                job.Query
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateJobAsync(BigQueryJob job)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_UpdateJob",
            new
            {
                job.Id,
                job.Status,
                job.EndTime,
                job.TotalRows,
                job.TotalBytesProcessed
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<BigQueryJob>> GetJobsByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<BigQueryJob>(
            "sp_BigQuery_GetJobsByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

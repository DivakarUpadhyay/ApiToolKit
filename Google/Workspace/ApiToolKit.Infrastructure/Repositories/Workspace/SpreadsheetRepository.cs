using ApiToolKit.Domain.Entities.Workspace.Sheets;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class SpreadsheetRepository : ISpreadsheetRepository
{
    private readonly DapperContext _context;

    public SpreadsheetRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveSpreadsheetAsync(SpreadsheetInfo spreadsheet)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Sheets_SaveSpreadsheet",
            new
            {
                spreadsheet.SpreadsheetId,
                spreadsheet.UserId,
                spreadsheet.Title,
                spreadsheet.SpreadsheetUrl
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<SpreadsheetInfo>> GetSpreadsheetsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<SpreadsheetInfo>(
            "sp_Workspace_Sheets_GetSpreadsheetsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

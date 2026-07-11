using ApiToolKit.Domain.Entities.Workspace.Sheets;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface ISpreadsheetRepository
{
    Task SaveSpreadsheetAsync(SpreadsheetInfo spreadsheet);
    Task<List<SpreadsheetInfo>> GetSpreadsheetsByUserIdAsync(string userId);
}

using ApiToolKit.Application.DTOs.Workspace.Sheets;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface ISheetsService
{
    Task<Result<SpreadsheetResponse>> CreateSpreadsheetAsync(string userId, CreateSpreadsheetRequest request);
    Task<Result<SpreadsheetResponse>> GetSpreadsheetAsync(string userId, GetSpreadsheetRequest request);
    Task<Result<ValuesResponse>> GetValuesAsync(string userId, GetValuesRequest request);
    Task<Result<UpdateValuesResponse>> UpdateValuesAsync(string userId, UpdateValuesRequest request);
    Task<Result<object>> AppendValuesAsync(string userId, AppendValuesRequest request);
    Task<Result<object>> ClearValuesAsync(string userId, ClearValuesRequest request);
    Task<Result<BatchGetValuesResponse>> BatchGetValuesAsync(string userId, BatchGetValuesRequest request);
    Task<Result<object>> BatchUpdateValuesAsync(string userId, BatchUpdateValuesRequest request);
    Task<Result<SpreadsheetResponse>> AddSheetAsync(string userId, AddSheetRequest request);
    Task<Result<SpreadsheetResponse>> DeleteSheetAsync(string userId, DeleteSheetRequest request);
    Task<Result<SpreadsheetResponse>> RenameSheetAsync(string userId, RenameSheetRequest request);
    Task<Result<SheetPropertiesDto>> CopySheetToAsync(string userId, CopyToRequest request);
}

using ApiToolKit.Application.DTOs.Workspace.Sheets;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Sheets;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;

namespace ApiToolKit.Application.Services.Workspace.Sheets;

public class SheetsService : ISheetsService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly ISpreadsheetRepository _spreadsheetRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://sheets.googleapis.com/v4/spreadsheets";

    public SheetsService(IWorkspaceAuthService authService, ISpreadsheetRepository spreadsheetRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _spreadsheetRepo = spreadsheetRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<SpreadsheetResponse>> CreateSpreadsheetAsync(string userId, CreateSpreadsheetRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpreadsheetResponse>.Fail("Access token not found.");

        var sheets = request.Sheets?.Select(s => new
        {
            properties = new { title = s.Title, gridProperties = new { rowCount = s.RowCount, columnCount = s.ColumnCount } }
        }).ToList();

        var body = new { properties = new { title = request.Title }, sheets };
        var response = await _apiClient.PostAsync<SpreadsheetResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<SpreadsheetResponse>.Fail("Failed to create spreadsheet.");

        await _spreadsheetRepo.SaveSpreadsheetAsync(new SpreadsheetInfo
        {
            SpreadsheetId = response.SpreadsheetId ?? string.Empty,
            UserId = userId,
            Title = response.Properties?.Title ?? request.Title,
            SpreadsheetUrl = response.SpreadsheetUrl,
            CreatedAt = DateTime.UtcNow
        });

        return Result<SpreadsheetResponse>.Ok(response);
    }

    public async Task<Result<SpreadsheetResponse>> GetSpreadsheetAsync(string userId, GetSpreadsheetRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpreadsheetResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpreadsheetId}?includeGridData={request.IncludeGridData.ToString().ToLower()}";
        if (request.Ranges?.Any() == true)
            foreach (var range in request.Ranges) url += $"&ranges={Uri.EscapeDataString(range)}";

        var response = await _apiClient.GetAsync<SpreadsheetResponse>(url, accessToken);
        if (response == null) return Result<SpreadsheetResponse>.Fail("Spreadsheet not found.");

        return Result<SpreadsheetResponse>.Ok(response);
    }

    public async Task<Result<ValuesResponse>> GetValuesAsync(string userId, GetValuesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ValuesResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpreadsheetId}/values/{Uri.EscapeDataString(request.Range)}?majorDimension={request.MajorDimension}&valueRenderOption={request.ValueRenderOption}&dateTimeRenderOption={request.DateTimeRenderOption}";
        var response = await _apiClient.GetAsync<ValuesResponse>(url, accessToken);
        if (response == null) return Result<ValuesResponse>.Fail("No response from Sheets API.");

        return Result<ValuesResponse>.Ok(response);
    }

    public async Task<Result<UpdateValuesResponse>> UpdateValuesAsync(string userId, UpdateValuesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<UpdateValuesResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpreadsheetId}/values/{Uri.EscapeDataString(request.Range)}?valueInputOption={request.ValueInputOption}";
        var body = new { range = request.Range, majorDimension = request.MajorDimension, values = request.Values };
        var response = await _apiClient.PutAsync<UpdateValuesResponse>(url, body, accessToken);
        if (response == null) return Result<UpdateValuesResponse>.Fail("Failed to update values.");

        return Result<UpdateValuesResponse>.Ok(response);
    }

    public async Task<Result<object>> AppendValuesAsync(string userId, AppendValuesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpreadsheetId}/values/{Uri.EscapeDataString(request.Range)}:append?valueInputOption={request.ValueInputOption}&insertDataOption={request.InsertDataOption}";
        var body = new { range = request.Range, values = request.Values };
        var response = await _apiClient.PostAsync<object>(url, body, accessToken);
        if (response == null) return Result<object>.Fail("Failed to append values.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> ClearValuesAsync(string userId, ClearValuesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpreadsheetId}/values/{Uri.EscapeDataString(request.Range)}:clear";
        var response = await _apiClient.PostAsync<object>(url, new { }, accessToken);
        if (response == null) return Result<object>.Fail("Failed to clear values.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<BatchGetValuesResponse>> BatchGetValuesAsync(string userId, BatchGetValuesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<BatchGetValuesResponse>.Fail("Access token not found.");

        var rangesParam = string.Join("&", request.Ranges.Select(r => $"ranges={Uri.EscapeDataString(r)}"));
        var url = $"{BaseUrl}/{request.SpreadsheetId}/values:batchGet?{rangesParam}&majorDimension={request.MajorDimension}";
        var response = await _apiClient.GetAsync<BatchGetValuesResponse>(url, accessToken);
        if (response == null) return Result<BatchGetValuesResponse>.Fail("No response from Sheets API.");

        return Result<BatchGetValuesResponse>.Ok(response);
    }

    public async Task<Result<object>> BatchUpdateValuesAsync(string userId, BatchUpdateValuesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var body = new { valueInputOption = request.ValueInputOption, data = request.Data };
        var url = $"{BaseUrl}/{request.SpreadsheetId}/values:batchUpdate";
        var response = await _apiClient.PostAsync<object>(url, body, accessToken);
        if (response == null) return Result<object>.Fail("Failed to batch update values.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<SpreadsheetResponse>> AddSheetAsync(string userId, AddSheetRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpreadsheetResponse>.Fail("Access token not found.");

        var body = new { requests = new[] { new { addSheet = new { properties = new { title = request.Title } } } } };
        var response = await _apiClient.PostAsync<SpreadsheetResponse>($"{BaseUrl}/{request.SpreadsheetId}:batchUpdate", body, accessToken);
        if (response == null) return Result<SpreadsheetResponse>.Fail("Failed to add sheet.");

        return Result<SpreadsheetResponse>.Ok(response);
    }

    public async Task<Result<SpreadsheetResponse>> DeleteSheetAsync(string userId, DeleteSheetRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpreadsheetResponse>.Fail("Access token not found.");

        var body = new { requests = new[] { new { deleteSheet = new { sheetId = request.SheetId } } } };
        var response = await _apiClient.PostAsync<SpreadsheetResponse>($"{BaseUrl}/{request.SpreadsheetId}:batchUpdate", body, accessToken);
        if (response == null) return Result<SpreadsheetResponse>.Fail("Failed to delete sheet.");

        return Result<SpreadsheetResponse>.Ok(response);
    }

    public async Task<Result<SpreadsheetResponse>> RenameSheetAsync(string userId, RenameSheetRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpreadsheetResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new { updateSheetProperties = new { properties = new { sheetId = request.SheetId, title = request.Title }, fields = "title" } }
            }
        };
        var response = await _apiClient.PostAsync<SpreadsheetResponse>($"{BaseUrl}/{request.SpreadsheetId}:batchUpdate", body, accessToken);
        if (response == null) return Result<SpreadsheetResponse>.Fail("Failed to rename sheet.");

        return Result<SpreadsheetResponse>.Ok(response);
    }

    public async Task<Result<SheetPropertiesDto>> CopySheetToAsync(string userId, CopyToRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SheetPropertiesDto>.Fail("Access token not found.");

        var body = new { destinationSpreadsheetId = request.DestinationSpreadsheetId };
        var response = await _apiClient.PostAsync<SheetPropertiesDto>($"{BaseUrl}/{request.SpreadsheetId}/sheets/{request.SheetId}:copyTo", body, accessToken);
        if (response == null) return Result<SheetPropertiesDto>.Fail("Failed to copy sheet.");

        return Result<SheetPropertiesDto>.Ok(response);
    }
}

using ApiToolKit.Application.DTOs.Workspace.Forms;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Forms;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Workspace.Forms;

public class FormResponseService : IFormResponseService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IFormRepository _formRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://forms.googleapis.com/v1/forms";

    public FormResponseService(IWorkspaceAuthService authService, IFormRepository formRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _formRepo = formRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListFormResponsesResponse>> ListResponsesAsync(string userId, ListFormResponsesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListFormResponsesResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.FormId}/responses?pageSize={request.PageSize}";
        if (!string.IsNullOrEmpty(request.PageToken)) url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.Filter)) url += $"&filter={Uri.EscapeDataString(request.Filter)}";

        var response = await _apiClient.GetAsync<ListFormResponsesResponse>(url, accessToken);
        if (response == null) return Result<ListFormResponsesResponse>.Fail("No response from Forms API.");

        return Result<ListFormResponsesResponse>.Ok(response);
    }

    public async Task<Result<FormResponseDto>> GetResponseAsync(string userId, string formId, string responseId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FormResponseDto>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<FormResponseDto>($"{BaseUrl}/{formId}/responses/{responseId}", accessToken);
        if (response == null) return Result<FormResponseDto>.Fail("Response not found.");

        await _formRepo.SaveResponseAsync(new FormResponse
        {
            ResponseId = response.ResponseId ?? string.Empty,
            FormId = formId,
            UserId = userId,
            CreateTime = response.CreateTime,
            LastSubmittedTime = response.LastSubmittedTime,
            RespondentEmail = response.RespondentEmail,
            TotalScore = response.TotalScore,
            Answers = response.Answers != null ? JsonConvert.SerializeObject(response.Answers) : null,
            CreatedAt = DateTime.UtcNow
        });

        return Result<FormResponseDto>.Ok(response);
    }
}

using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Gmail;

public class GmailSettingsService : IGmailSettingsService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://gmail.googleapis.com/gmail/v1/users/me/settings";

    public GmailSettingsService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<VacationSettingsResponse>> GetVacationSettingsAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<VacationSettingsResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<VacationSettingsResponse>($"{BaseUrl}/vacation", accessToken);
        if (response == null) return Result<VacationSettingsResponse>.Fail("No response from Gmail API.");

        return Result<VacationSettingsResponse>.Ok(response);
    }

    public async Task<Result<VacationSettingsResponse>> UpdateVacationSettingsAsync(string userId, UpdateVacationRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<VacationSettingsResponse>.Fail("Access token not found.");

        var body = new
        {
            enableAutoReply = request.EnableAutoReply,
            responseSubject = request.ResponseSubject,
            responseBodyPlainText = request.ResponseBodyPlainText,
            responseBodyHtml = request.ResponseBodyHtml,
            startTime = request.StartTime,
            endTime = request.EndTime
        };

        var response = await _apiClient.PutAsync<VacationSettingsResponse>($"{BaseUrl}/vacation", body, accessToken);
        if (response == null) return Result<VacationSettingsResponse>.Fail("Failed to update vacation settings.");

        return Result<VacationSettingsResponse>.Ok(response);
    }

    public async Task<Result<List<FilterResponse>>> ListFiltersAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<List<FilterResponse>>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<dynamic>($"{BaseUrl}/filters", accessToken);
        if (response == null) return Result<List<FilterResponse>>.Fail("No response from Gmail API.");

        return Result<List<FilterResponse>>.Ok(new List<FilterResponse>());
    }

    public async Task<Result<FilterResponse>> CreateFilterAsync(string userId, CreateFilterRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FilterResponse>.Fail("Access token not found.");

        var body = new
        {
            criteria = new { from = request.From, to = request.To, subject = request.Subject, query = request.Query, hasAttachment = request.HasAttachment },
            action = new { addLabelIds = request.AddLabelIds, removeLabelIds = request.RemoveLabelIds, forward = request.Forward }
        };

        var response = await _apiClient.PostAsync<FilterResponse>($"{BaseUrl}/filters", body, accessToken);
        if (response == null) return Result<FilterResponse>.Fail("Failed to create filter.");

        return Result<FilterResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteFilterAsync(string userId, string filterId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/filters/{filterId}", accessToken);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<List<ForwardingAddressResponse>>> ListForwardingAddressesAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<List<ForwardingAddressResponse>>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<dynamic>($"{BaseUrl}/forwardingAddresses", accessToken);
        if (response == null) return Result<List<ForwardingAddressResponse>>.Fail("No response from Gmail API.");

        return Result<List<ForwardingAddressResponse>>.Ok(new List<ForwardingAddressResponse>());
    }

    public async Task<Result<ForwardingAddressResponse>> CreateForwardingAddressAsync(string userId, CreateForwardingRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ForwardingAddressResponse>.Fail("Access token not found.");

        var body = new { forwardingEmail = request.ForwardingEmail };
        var response = await _apiClient.PostAsync<ForwardingAddressResponse>($"{BaseUrl}/forwardingAddresses", body, accessToken);
        if (response == null) return Result<ForwardingAddressResponse>.Fail("Failed to create forwarding address.");

        return Result<ForwardingAddressResponse>.Ok(response);
    }

    public async Task<Result<ImapSettings>> GetImapSettingsAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ImapSettings>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ImapSettings>($"{BaseUrl}/imap", accessToken);
        if (response == null) return Result<ImapSettings>.Fail("No response from Gmail API.");

        return Result<ImapSettings>.Ok(response);
    }

    public async Task<Result<ImapSettings>> UpdateImapSettingsAsync(string userId, ImapSettings settings)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ImapSettings>.Fail("Access token not found.");

        var response = await _apiClient.PutAsync<ImapSettings>($"{BaseUrl}/imap", settings, accessToken);
        if (response == null) return Result<ImapSettings>.Fail("Failed to update IMAP settings.");

        return Result<ImapSettings>.Ok(response);
    }

    public async Task<Result<PopSettings>> GetPopSettingsAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PopSettings>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<PopSettings>($"{BaseUrl}/pop", accessToken);
        if (response == null) return Result<PopSettings>.Fail("No response from Gmail API.");

        return Result<PopSettings>.Ok(response);
    }
}

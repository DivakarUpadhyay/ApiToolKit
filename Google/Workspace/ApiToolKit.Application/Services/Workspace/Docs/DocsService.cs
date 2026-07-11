using ApiToolKit.Application.DTOs.Workspace.Docs;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Docs;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;

namespace ApiToolKit.Application.Services.Workspace.Docs;

public class DocsService : IDocsService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IDocumentRepository _documentRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://docs.googleapis.com/v1/documents";

    public DocsService(IWorkspaceAuthService authService, IDocumentRepository documentRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _documentRepo = documentRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<DocumentResponse>> CreateDocumentAsync(string userId, CreateDocumentRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new { title = request.Title };
        var response = await _apiClient.PostAsync<DocumentResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to create document.");

        await _documentRepo.SaveDocumentAsync(new DocumentInfo
        {
            DocumentId = response.DocumentId ?? string.Empty,
            UserId = userId,
            Title = response.Title ?? request.Title,
            DocumentUrl = $"https://docs.google.com/document/d/{response.DocumentId}/edit",
            RevisionId = response.RevisionId,
            CreatedAt = DateTime.UtcNow
        });

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> GetDocumentAsync(string userId, GetDocumentRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}", accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Document not found.");

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> InsertTextAsync(string userId, InsertTextRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new { requests = new[] { new { insertText = new { text = request.Text, location = new { index = request.Index } } } } };
        var response = await _apiClient.PostAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}:batchUpdate", body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to insert text.");

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> ReplaceTextAsync(string userId, ReplaceTextRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new
                {
                    replaceAllText = new
                    {
                        containsText = new { text = request.OldText, matchCase = request.MatchCase },
                        replaceText = request.NewText
                    }
                }
            }
        };

        var response = await _apiClient.PostAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}:batchUpdate", body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to replace text.");

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> InsertTableAsync(string userId, InsertTableRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new { insertTable = new { rows = request.Rows, columns = request.Columns, location = new { index = request.Index } } }
            }
        };

        var response = await _apiClient.PostAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}:batchUpdate", body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to insert table.");

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> DeleteContentRangeAsync(string userId, DeleteContentRangeRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new { deleteContentRange = new { range = new { startIndex = request.StartIndex, endIndex = request.EndIndex } } }
            }
        };

        var response = await _apiClient.PostAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}:batchUpdate", body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to delete content range.");

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> InsertPageBreakAsync(string userId, InsertPageBreakRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new { insertPageBreak = new { location = new { index = request.Index ?? 1 } } }
            }
        };

        var response = await _apiClient.PostAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}:batchUpdate", body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to insert page break.");

        return Result<DocumentResponse>.Ok(response);
    }

    public async Task<Result<DocumentResponse>> ApplyParagraphStyleAsync(string userId, ParagraphStyleRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DocumentResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new
                {
                    updateParagraphStyle = new
                    {
                        range = new { startIndex = request.StartIndex, endIndex = request.EndIndex },
                        paragraphStyle = new { namedStyleType = request.NamedStyleType },
                        fields = "namedStyleType"
                    }
                }
            }
        };

        var response = await _apiClient.PostAsync<DocumentResponse>($"{BaseUrl}/{request.DocumentId}:batchUpdate", body, accessToken);
        if (response == null) return Result<DocumentResponse>.Fail("Failed to apply paragraph style.");

        return Result<DocumentResponse>.Ok(response);
    }
}

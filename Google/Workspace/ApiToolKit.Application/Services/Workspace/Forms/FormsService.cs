using ApiToolKit.Application.DTOs.Workspace.Forms;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using DomainFormInfo = ApiToolKit.Domain.Entities.Workspace.Forms.FormInfo;

namespace ApiToolKit.Application.Services.Workspace.Forms;

public class FormsService : IFormsService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IFormRepository _formRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://forms.googleapis.com/v1/forms";

    public FormsService(IWorkspaceAuthService authService, IFormRepository formRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _formRepo = formRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<FormResponse2>> CreateFormAsync(string userId, CreateFormRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FormResponse2>.Fail("Access token not found.");

        var body = new { info = new { title = request.Title, documentTitle = request.DocumentTitle ?? request.Title } };
        var response = await _apiClient.PostAsync<FormResponse2>(BaseUrl, body, accessToken);
        if (response == null) return Result<FormResponse2>.Fail("Failed to create form.");

        await _formRepo.SaveFormAsync(new DomainFormInfo
        {
            FormId = response.FormId ?? string.Empty,
            UserId = userId,
            Title = response.Info?.Title ?? request.Title,
            DocumentTitle = response.Info?.DocumentTitle,
            RevisionId = response.RevisionId,
            ResponderUri = response.ResponderUri,
            CreatedAt = DateTime.UtcNow
        });

        return Result<FormResponse2>.Ok(response);
    }

    public async Task<Result<FormResponse2>> GetFormAsync(string userId, GetFormRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FormResponse2>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<FormResponse2>($"{BaseUrl}/{request.FormId}", accessToken);
        if (response == null) return Result<FormResponse2>.Fail("Form not found.");

        return Result<FormResponse2>.Ok(response);
    }

    public async Task<Result<FormResponse2>> UpdateFormAsync(string userId, UpdateFormRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FormResponse2>.Fail("Access token not found.");

        var requests = new List<object>();
        if (!string.IsNullOrEmpty(request.Title) || !string.IsNullOrEmpty(request.Description))
        {
            requests.Add(new
            {
                updateFormInfo = new
                {
                    info = new { title = request.Title, description = request.Description },
                    updateMask = "title,description"
                }
            });
        }
        if (request.IsQuiz.HasValue)
        {
            requests.Add(new
            {
                updateSettings = new
                {
                    settings = new { quizSettings = new { isQuiz = request.IsQuiz.Value } },
                    updateMask = "quizSettings.isQuiz"
                }
            });
        }

        var body = new { requests };
        var response = await _apiClient.PostAsync<FormResponse2>($"{BaseUrl}/{request.FormId}:batchUpdate", body, accessToken);
        if (response == null) return Result<FormResponse2>.Fail("Failed to update form.");

        return Result<FormResponse2>.Ok(response);
    }

    public async Task<Result<FormResponse2>> AddItemAsync(string userId, AddItemRequest2 request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FormResponse2>.Fail("Access token not found.");

        object questionSpec = request.ItemType switch
        {
            "MULTIPLE_CHOICE" => new { choiceQuestion = new { type = "RADIO", options = (request.Choices ?? new List<string>()).Select(c => new { value = c }).ToList(), shuffle = false } },
            "CHECKBOX" => new { choiceQuestion = new { type = "CHECKBOX", options = (request.Choices ?? new List<string>()).Select(c => new { value = c }).ToList(), shuffle = false } },
            "DROPDOWN" => new { choiceQuestion = new { type = "DROP_DOWN", options = (request.Choices ?? new List<string>()).Select(c => new { value = c }).ToList(), shuffle = false } },
            "SCALE" => new { scaleQuestion = new { low = request.ScaleMin, high = request.ScaleMax, lowLabel = request.LowLabel, highLabel = request.HighLabel } },
            "DATE" => new { dateQuestion = new { includeTime = false, includeYear = true } },
            "TIME" => new { timeQuestion = new { duration = false } },
            "PARAGRAPH" => new { textQuestion = new { paragraph = true } },
            _ => new { textQuestion = new { paragraph = false } }
        };

        var itemBody = new
        {
            requests = new[]
            {
                new
                {
                    createItem = new
                    {
                        item = new
                        {
                            title = request.ItemTitle,
                            description = request.ItemDescription,
                            questionItem = new { question = new { required = request.Required } }
                        },
                        location = request.InsertAtEnd ? (object)new { } : new { index = 0 }
                    }
                }
            }
        };

        var response = await _apiClient.PostAsync<FormResponse2>($"{BaseUrl}/{request.FormId}:batchUpdate", itemBody, accessToken);
        if (response == null) return Result<FormResponse2>.Fail("Failed to add item.");

        return Result<FormResponse2>.Ok(response);
    }

    public async Task<Result<bool>> DeleteItemAsync(string userId, DeleteItemRequest2 request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { requests = new[] { new { deleteItem = new { location = new { index = 0 } } } } };
        await _apiClient.PostAsync<object>($"{BaseUrl}/{request.FormId}:batchUpdate", body, accessToken);
        return Result<bool>.Ok(true);
    }
}

using ApiToolKit.Application.DTOs.Workspace.Slides;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Slides;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;

namespace ApiToolKit.Application.Services.Workspace.Slides;

public class SlidesService : ISlidesService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IPresentationRepository _presentationRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://slides.googleapis.com/v1/presentations";

    public SlidesService(IWorkspaceAuthService authService, IPresentationRepository presentationRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _presentationRepo = presentationRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<PresentationResponse>> CreatePresentationAsync(string userId, CreatePresentationRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var body = new { title = request.Title };
        var response = await _apiClient.PostAsync<PresentationResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to create presentation.");

        await _presentationRepo.SavePresentationAsync(new PresentationInfo
        {
            PresentationId = response.PresentationId ?? string.Empty,
            UserId = userId,
            Title = response.Title ?? request.Title,
            PresentationUrl = $"https://docs.google.com/presentation/d/{response.PresentationId}/edit",
            RevisionId = response.RevisionId,
            CreatedAt = DateTime.UtcNow
        });

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> GetPresentationAsync(string userId, GetPresentationRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}", accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Presentation not found.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> AddSlideAsync(string userId, AddSlideRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var insertSlide = new
        {
            insertionIndex = request.InsertionIndex,
            slideLayoutReference = request.SlideLayoutReference != null
                ? new { predefinedLayout = request.SlideLayoutReference.PredefinedLayout }
                : new { predefinedLayout = "BLANK" }
        };

        var body = new { requests = new[] { new { createSlide = insertSlide } } };
        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to add slide.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> DeleteSlideAsync(string userId, DeleteSlideRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var body = new { requests = new[] { new { deleteObject = new { objectId = request.SlideObjectId } } } };
        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to delete slide.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> InsertTextAsync(string userId, SlidesInsertTextRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new { insertText = new { objectId = request.ObjectId, insertionIndex = request.InsertionIndex, text = request.Text } }
            }
        };

        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to insert text.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> ReplaceTextAsync(string userId, SlidesReplaceTextRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var replaceRequest = new
        {
            replaceAllText = new
            {
                containsText = new { text = request.OldText, matchCase = request.MatchCase },
                replaceText = request.NewText,
                pageObjectIds = request.PageObjectIds
            }
        };

        var body = new { requests = new[] { replaceRequest } };
        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to replace text.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> InsertImageAsync(string userId, InsertImageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new
                {
                    createImage = new
                    {
                        url = request.Url,
                        elementProperties = new
                        {
                            pageObjectId = request.SlideObjectId,
                            size = new
                            {
                                width = new { magnitude = request.Width, unit = "EMU" },
                                height = new { magnitude = request.Height, unit = "EMU" }
                            },
                            transform = new
                            {
                                scaleX = 1, scaleY = 1,
                                translateX = request.Left, translateY = request.Top,
                                unit = "EMU"
                            }
                        }
                    }
                }
            }
        };

        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to insert image.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> DuplicateSlideAsync(string userId, DuplicateSlideRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var body = new { requests = new[] { new { duplicateObject = new { objectId = request.SlideObjectId } } } };
        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to duplicate slide.");

        return Result<PresentationResponse>.Ok(response);
    }

    public async Task<Result<PresentationResponse>> ReorderSlidesAsync(string userId, ReorderSlidesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PresentationResponse>.Fail("Access token not found.");

        var body = new
        {
            requests = new[]
            {
                new { updateSlidesPosition = new { slideObjectIds = request.SlideObjectIds, insertionIndex = request.InsertionIndex } }
            }
        };

        var response = await _apiClient.PostAsync<PresentationResponse>($"{BaseUrl}/{request.PresentationId}:batchUpdate", body, accessToken);
        if (response == null) return Result<PresentationResponse>.Fail("Failed to reorder slides.");

        return Result<PresentationResponse>.Ok(response);
    }
}

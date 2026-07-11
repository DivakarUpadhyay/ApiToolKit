using ApiToolKit.Application.DTOs.Cloud.Shared;
using ApiToolKit.Application.DTOs.Cloud.Vision;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Cloud;

public class VisionService : IVisionService
{
    private readonly ICloudAuthRepository _authRepo;
    private readonly IVisionRepository _visionRepo;
    private readonly ICloudApiClient _apiClient;

    private const string VisionBaseUrl = "https://vision.googleapis.com/v1/";

    public VisionService(ICloudAuthRepository authRepo, IVisionRepository visionRepo, ICloudApiClient apiClient)
    {
        _authRepo = authRepo;
        _visionRepo = visionRepo;
        _apiClient = apiClient;
    }

    private async Task<(string? accessToken, string? projectId)> GetTokenAsync(string projectId)
    {
        var token = await _authRepo.GetTokenByProjectIdAsync(projectId);
        return (token?.AccessToken, token?.ProjectId);
    }

    public async Task<Result<ImageAnnotationResponse>> AnnotateImageAsync(AnnotateImageRequest request)
    {
        var (accessToken, _) = await GetTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<ImageAnnotationResponse>.Fail("Access token not found for project.");

        var body = new
        {
            requests = new[]
            {
                new
                {
                    image = new
                    {
                        content = request.Image.Content,
                        source = (request.Image.GcsUri != null || request.Image.ImageUri != null)
                            ? new { gcsImageUri = request.Image.GcsUri, imageUri = request.Image.ImageUri }
                            : null
                    },
                    features = request.Features.Select(f => new { type = f.Type, maxResults = f.MaxResults }),
                    imageContext = request.ImageContext
                }
            }
        };

        var url = $"{VisionBaseUrl}images:annotate";
        var apiResponse = await _apiClient.PostAsync<dynamic>(url, body, accessToken);

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = request.ProjectId,
            Product = "Vision",
            ApiName = "AnnotateImage",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(apiResponse),
            IsSuccess = apiResponse != null,
            StatusCode = apiResponse != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (apiResponse == null) return Result<ImageAnnotationResponse>.Fail("No response from Vision API.");

        var responsesArray = apiResponse?.responses;
        if (responsesArray == null) return Result<ImageAnnotationResponse>.Fail("Invalid response from Vision API.");

        var responseJson = JsonConvert.SerializeObject(responsesArray[0]);
        var result = JsonConvert.DeserializeObject<ImageAnnotationResponse>(responseJson);
        if (result == null) return Result<ImageAnnotationResponse>.Fail("Failed to parse Vision API response.");

        if (result.Error != null)
            return Result<ImageAnnotationResponse>.Fail(result.Error.Message ?? "Vision API error.", result.Error.Code);

        var requestId = Guid.NewGuid().ToString();
        var imageSource = request.Image.GcsUri ?? request.Image.ImageUri ?? "base64";
        var annotationType = request.Features.FirstOrDefault()?.Type ?? "UNKNOWN";
        var confidence = result.LabelAnnotations?.FirstOrDefault()?.Score ?? 0.0;

        await _visionRepo.SaveAnnotationAsync(new VisionAnnotation
        {
            RequestId = requestId,
            ProjectId = request.ProjectId,
            ImageSource = imageSource,
            AnnotationType = annotationType,
            AnnotationData = responseJson,
            Confidence = confidence,
            CreatedAt = DateTime.UtcNow
        });

        return Result<ImageAnnotationResponse>.Ok(result);
    }

    public async Task<Result<BatchAnnotateImagesResponse>> BatchAnnotateImagesAsync(BatchAnnotateImagesRequest request)
    {
        if (!request.Requests.Any())
            return Result<BatchAnnotateImagesResponse>.Fail("Requests list cannot be empty.");

        var projectId = request.ProjectId;
        var (accessToken, _) = await GetTokenAsync(projectId);
        if (accessToken == null) return Result<BatchAnnotateImagesResponse>.Fail("Access token not found for project.");

        var body = new
        {
            requests = request.Requests.Select(r => new
            {
                image = new
                {
                    content = r.Image.Content,
                    source = (r.Image.GcsUri != null || r.Image.ImageUri != null)
                        ? new { gcsImageUri = r.Image.GcsUri, imageUri = r.Image.ImageUri }
                        : null
                },
                features = r.Features.Select(f => new { type = f.Type, maxResults = f.MaxResults }),
                imageContext = r.ImageContext
            })
        };

        var url = $"{VisionBaseUrl}images:annotate";
        var apiResponse = await _apiClient.PostAsync<BatchAnnotateImagesResponse>(url, body, accessToken);

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = projectId,
            Product = "Vision",
            ApiName = "BatchAnnotateImages",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(apiResponse),
            IsSuccess = apiResponse != null,
            StatusCode = apiResponse != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (apiResponse == null) return Result<BatchAnnotateImagesResponse>.Fail("No response from Vision API.");
        return Result<BatchAnnotateImagesResponse>.Ok(apiResponse);
    }

    public async Task<Result<OperationResponse>> AsyncAnnotateFileAsync(AsyncAnnotateFileRequest request)
    {
        var (accessToken, _) = await GetTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<OperationResponse>.Fail("Access token not found for project.");

        var body = new
        {
            requests = new[]
            {
                new
                {
                    inputConfig = request.InputConfig,
                    features = request.Features.Select(f => new { type = f.Type, maxResults = f.MaxResults }),
                    outputConfig = request.OutputConfig
                }
            }
        };

        var url = $"{VisionBaseUrl}files:asyncBatchAnnotate";
        var apiResponse = await _apiClient.PostAsync<OperationResponse>(url, body, accessToken);

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = request.ProjectId,
            Product = "Vision",
            ApiName = "AsyncAnnotateFile",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(apiResponse),
            IsSuccess = apiResponse != null,
            StatusCode = apiResponse != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (apiResponse == null) return Result<OperationResponse>.Fail("No response from Vision API.");
        return Result<OperationResponse>.Ok(apiResponse);
    }

    public async Task<Result<OperationResponse>> GetOperationAsync(GetOperationRequest request)
    {
        var (accessToken, _) = await GetTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<OperationResponse>.Fail("Access token not found for project.");

        var url = $"https://vision.googleapis.com/v1/{request.OperationName}";
        var apiResponse = await _apiClient.GetAsync<OperationResponse>(url, accessToken);

        if (apiResponse == null) return Result<OperationResponse>.Fail("No response from Vision API.");
        return Result<OperationResponse>.Ok(apiResponse);
    }
}

using ApiToolKit.Application.DTOs.Cloud.NaturalLanguage;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Cloud;

public class LanguageService : ILanguageService
{
    private readonly ICloudAuthRepository _authRepo;
    private readonly ILanguageRepository _languageRepo;
    private readonly ICloudApiClient _apiClient;

    private const string NlpBaseUrl = "https://language.googleapis.com/v1/";

    public LanguageService(ICloudAuthRepository authRepo, ILanguageRepository languageRepo, ICloudApiClient apiClient)
    {
        _authRepo = authRepo;
        _languageRepo = languageRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(string projectId)
    {
        var token = await _authRepo.GetTokenByProjectIdAsync(projectId);
        return token?.AccessToken;
    }

    private object BuildDocumentBody(TextDocument doc) => new
    {
        type = doc.Type,
        content = doc.Content,
        gcsContentUri = doc.GcsContentUri,
        language = doc.Language
    };

    private async Task LogAsync(string projectId, string apiName, object request, object? response, bool success)
    {
        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = projectId,
            Product = "Language",
            ApiName = apiName,
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = success,
            StatusCode = success ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<Result<AnalyzeEntitiesResponse>> AnalyzeEntitiesAsync(AnalyzeEntitiesRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<AnalyzeEntitiesResponse>.Fail("Access token not found for project.");

        var body = new { document = BuildDocumentBody(request.Document), encodingType = request.EncodingType };
        var url = $"{NlpBaseUrl}documents:analyzeEntities";
        var apiResponse = await _apiClient.PostAsync<AnalyzeEntitiesResponse>(url, body, accessToken);

        await LogAsync(request.ProjectId, "AnalyzeEntities", request, apiResponse, apiResponse != null);

        if (apiResponse == null) return Result<AnalyzeEntitiesResponse>.Fail("No response from Language API.");

        await _languageRepo.SaveAnalysisAsync(new LanguageAnalysis
        {
            RequestId = Guid.NewGuid().ToString(),
            ProjectId = request.ProjectId,
            Text = request.Document.Content,
            AnalysisType = "Entities",
            ResultData = JsonConvert.SerializeObject(apiResponse),
            LanguageCode = apiResponse.Language,
            Confidence = apiResponse.Entities?.FirstOrDefault()?.Salience ?? 0.0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<AnalyzeEntitiesResponse>.Ok(apiResponse);
    }

    public async Task<Result<AnalyzeSentimentResponse>> AnalyzeSentimentAsync(AnalyzeSentimentRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<AnalyzeSentimentResponse>.Fail("Access token not found for project.");

        var body = new { document = BuildDocumentBody(request.Document), encodingType = request.EncodingType };
        var url = $"{NlpBaseUrl}documents:analyzeSentiment";
        var apiResponse = await _apiClient.PostAsync<AnalyzeSentimentResponse>(url, body, accessToken);

        await LogAsync(request.ProjectId, "AnalyzeSentiment", request, apiResponse, apiResponse != null);

        if (apiResponse == null) return Result<AnalyzeSentimentResponse>.Fail("No response from Language API.");

        await _languageRepo.SaveAnalysisAsync(new LanguageAnalysis
        {
            RequestId = Guid.NewGuid().ToString(),
            ProjectId = request.ProjectId,
            Text = request.Document.Content,
            AnalysisType = "Sentiment",
            ResultData = JsonConvert.SerializeObject(apiResponse),
            LanguageCode = apiResponse.Language,
            Confidence = apiResponse.DocumentSentiment?.Score ?? 0.0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<AnalyzeSentimentResponse>.Ok(apiResponse);
    }

    public async Task<Result<AnalyzeSyntaxResponse>> AnalyzeSyntaxAsync(AnalyzeSyntaxRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<AnalyzeSyntaxResponse>.Fail("Access token not found for project.");

        var body = new { document = BuildDocumentBody(request.Document), encodingType = request.EncodingType };
        var url = $"{NlpBaseUrl}documents:analyzeSyntax";
        var apiResponse = await _apiClient.PostAsync<AnalyzeSyntaxResponse>(url, body, accessToken);

        await LogAsync(request.ProjectId, "AnalyzeSyntax", request, apiResponse, apiResponse != null);

        if (apiResponse == null) return Result<AnalyzeSyntaxResponse>.Fail("No response from Language API.");

        await _languageRepo.SaveAnalysisAsync(new LanguageAnalysis
        {
            RequestId = Guid.NewGuid().ToString(),
            ProjectId = request.ProjectId,
            Text = request.Document.Content,
            AnalysisType = "Syntax",
            ResultData = JsonConvert.SerializeObject(apiResponse),
            LanguageCode = apiResponse.Language,
            Confidence = 0.0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<AnalyzeSyntaxResponse>.Ok(apiResponse);
    }

    public async Task<Result<ClassifyTextResponse>> ClassifyTextAsync(ClassifyTextRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<ClassifyTextResponse>.Fail("Access token not found for project.");

        var body = new
        {
            document = BuildDocumentBody(request.Document),
            classificationModelOptions = request.ClassificationModelOptions
        };
        var url = $"{NlpBaseUrl}documents:classifyText";
        var apiResponse = await _apiClient.PostAsync<ClassifyTextResponse>(url, body, accessToken);

        await LogAsync(request.ProjectId, "ClassifyText", request, apiResponse, apiResponse != null);

        if (apiResponse == null) return Result<ClassifyTextResponse>.Fail("No response from Language API.");

        await _languageRepo.SaveAnalysisAsync(new LanguageAnalysis
        {
            RequestId = Guid.NewGuid().ToString(),
            ProjectId = request.ProjectId,
            Text = request.Document.Content,
            AnalysisType = "Classification",
            ResultData = JsonConvert.SerializeObject(apiResponse),
            LanguageCode = null,
            Confidence = apiResponse.Categories?.FirstOrDefault()?.Confidence ?? 0.0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<ClassifyTextResponse>.Ok(apiResponse);
    }

    public async Task<Result<ModerateTextResponse>> ModerateTextAsync(ModerateTextRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<ModerateTextResponse>.Fail("Access token not found for project.");

        var body = new
        {
            document = BuildDocumentBody(request.Document),
            modelVersion = request.ModelVersion
        };
        var url = $"{NlpBaseUrl}documents:moderateText";
        var apiResponse = await _apiClient.PostAsync<ModerateTextResponse>(url, body, accessToken);

        await LogAsync(request.ProjectId, "ModerateText", request, apiResponse, apiResponse != null);

        if (apiResponse == null) return Result<ModerateTextResponse>.Fail("No response from Language API.");

        await _languageRepo.SaveAnalysisAsync(new LanguageAnalysis
        {
            RequestId = Guid.NewGuid().ToString(),
            ProjectId = request.ProjectId,
            Text = request.Document.Content,
            AnalysisType = "Moderation",
            ResultData = JsonConvert.SerializeObject(apiResponse),
            LanguageCode = null,
            Confidence = apiResponse.ModerationCategories?.FirstOrDefault()?.Confidence ?? 0.0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<ModerateTextResponse>.Ok(apiResponse);
    }

    public async Task<Result<AnnotateTextResponse>> AnnotateTextAsync(AnnotateTextRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<AnnotateTextResponse>.Fail("Access token not found for project.");

        var body = new
        {
            document = BuildDocumentBody(request.Document),
            features = new
            {
                extractSyntax = request.Features.ExtractSyntax,
                extractEntities = request.Features.ExtractEntities,
                extractDocumentSentiment = request.Features.ExtractDocumentSentiment,
                classifyText = request.Features.ClassifyText,
                moderateText = request.Features.ModerateText,
                extractEntitySentiment = request.Features.ExtractEntitySentiment
            },
            encodingType = request.EncodingType
        };
        var url = $"{NlpBaseUrl}documents:annotateText";
        var apiResponse = await _apiClient.PostAsync<AnnotateTextResponse>(url, body, accessToken);

        await LogAsync(request.ProjectId, "AnnotateText", request, apiResponse, apiResponse != null);

        if (apiResponse == null) return Result<AnnotateTextResponse>.Fail("No response from Language API.");

        await _languageRepo.SaveAnalysisAsync(new LanguageAnalysis
        {
            RequestId = Guid.NewGuid().ToString(),
            ProjectId = request.ProjectId,
            Text = request.Document.Content,
            AnalysisType = "Annotate",
            ResultData = JsonConvert.SerializeObject(apiResponse),
            LanguageCode = apiResponse.Language,
            Confidence = apiResponse.DocumentSentiment?.Score ?? 0.0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<AnnotateTextResponse>.Ok(apiResponse);
    }
}

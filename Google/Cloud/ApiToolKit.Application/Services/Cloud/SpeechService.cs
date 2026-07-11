using ApiToolKit.Application.DTOs.Cloud.Shared;
using ApiToolKit.Application.DTOs.Cloud.Speech;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Cloud;

public class SpeechService : ISpeechService
{
    private readonly ICloudAuthRepository _authRepo;
    private readonly ISpeechRepository _speechRepo;
    private readonly ICloudApiClient _apiClient;

    private const string SpeechBaseUrl = "https://speech.googleapis.com/v1/";

    public SpeechService(ICloudAuthRepository authRepo, ISpeechRepository speechRepo, ICloudApiClient apiClient)
    {
        _authRepo = authRepo;
        _speechRepo = speechRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(string projectId)
    {
        var token = await _authRepo.GetTokenByProjectIdAsync(projectId);
        return token?.AccessToken;
    }

    public async Task<Result<RecognizeResponse>> RecognizeAsync(RecognizeRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<RecognizeResponse>.Fail("Access token not found for project.");

        var body = new
        {
            config = new
            {
                encoding = request.Config.Encoding,
                sampleRateHertz = request.Config.SampleRateHertz,
                audioChannelCount = request.Config.AudioChannelCount,
                languageCode = request.Config.LanguageCode,
                maxAlternatives = request.Config.MaxAlternatives,
                profanityFilter = request.Config.ProfanityFilter,
                speechContexts = request.Config.SpeechContexts,
                enableWordTimeOffsets = request.Config.EnableWordTimeOffsets,
                enableWordConfidence = request.Config.EnableWordConfidence,
                enableAutomaticPunctuation = request.Config.EnableAutomaticPunctuation,
                enableSpeakerDiarization = request.Config.EnableSpeakerDiarization,
                diarizationSpeakerCount = request.Config.DiarizationSpeakerCount,
                model = request.Config.Model,
                useEnhanced = request.Config.UseEnhanced
            },
            audio = new
            {
                content = request.Audio.Content,
                uri = request.Audio.Uri
            }
        };

        var url = $"{SpeechBaseUrl}speech:recognize";
        var apiResponse = await _apiClient.PostAsync<RecognizeResponse>(url, body, accessToken);

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = request.ProjectId,
            Product = "Speech",
            ApiName = "Recognize",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(apiResponse),
            IsSuccess = apiResponse != null,
            StatusCode = apiResponse != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (apiResponse == null) return Result<RecognizeResponse>.Fail("No response from Speech API.");

        var requestId = Guid.NewGuid().ToString();
        var firstResult = apiResponse.Results?.FirstOrDefault();
        var firstAlternative = firstResult?.Alternatives?.FirstOrDefault();
        var transcript = firstAlternative?.Transcript ?? string.Empty;
        var confidence = firstAlternative?.Confidence ?? 0.0;
        var wordCount = transcript.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

        await _speechRepo.SaveRecognitionAsync(new Domain.Entities.Cloud.SpeechRecognition
        {
            RequestId = requestId,
            ProjectId = request.ProjectId,
            AudioSource = request.Audio.Uri ?? "base64",
            LanguageCode = request.Config.LanguageCode,
            Transcript = transcript,
            Confidence = confidence,
            WordCount = wordCount,
            Duration = 0,
            CreatedAt = DateTime.UtcNow
        });

        return Result<RecognizeResponse>.Ok(apiResponse);
    }

    public async Task<Result<OperationResponse>> LongRunningRecognizeAsync(LongRunningRecognizeRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<OperationResponse>.Fail("Access token not found for project.");

        var body = new
        {
            config = new
            {
                encoding = request.Config.Encoding,
                sampleRateHertz = request.Config.SampleRateHertz,
                audioChannelCount = request.Config.AudioChannelCount,
                languageCode = request.Config.LanguageCode,
                maxAlternatives = request.Config.MaxAlternatives,
                profanityFilter = request.Config.ProfanityFilter,
                speechContexts = request.Config.SpeechContexts,
                enableWordTimeOffsets = request.Config.EnableWordTimeOffsets,
                enableWordConfidence = request.Config.EnableWordConfidence,
                enableAutomaticPunctuation = request.Config.EnableAutomaticPunctuation,
                enableSpeakerDiarization = request.Config.EnableSpeakerDiarization,
                diarizationSpeakerCount = request.Config.DiarizationSpeakerCount,
                model = request.Config.Model,
                useEnhanced = request.Config.UseEnhanced
            },
            audio = new
            {
                content = request.Audio.Content,
                uri = request.Audio.Uri
            },
            outputConfig = request.OutputConfig
        };

        var url = $"{SpeechBaseUrl}speech:longrunningrecognize";
        var apiResponse = await _apiClient.PostAsync<OperationResponse>(url, body, accessToken);

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = request.ProjectId,
            Product = "Speech",
            ApiName = "LongRunningRecognize",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(apiResponse),
            IsSuccess = apiResponse != null,
            StatusCode = apiResponse != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (apiResponse == null) return Result<OperationResponse>.Fail("No response from Speech API.");
        return Result<OperationResponse>.Ok(apiResponse);
    }

    public async Task<Result<OperationResponse>> GetOperationAsync(GetOperationRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ProjectId);
        if (accessToken == null) return Result<OperationResponse>.Fail("Access token not found for project.");

        var url = $"https://speech.googleapis.com/v1/operations/{Uri.EscapeDataString(request.OperationName)}";
        var apiResponse = await _apiClient.GetAsync<OperationResponse>(url, accessToken);

        if (apiResponse == null) return Result<OperationResponse>.Fail("No response from Speech API.");
        return Result<OperationResponse>.Ok(apiResponse);
    }
}

using ApiToolKit.Application.DTOs.Cloud.Shared;
using ApiToolKit.Application.DTOs.Cloud.Translation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Cloud;

public interface ITranslationService
{
    Task<Result<TranslateTextResponse>> TranslateTextAsync(TranslateTextRequest request);
    Task<Result<DetectLanguageResponse>> DetectLanguageAsync(DetectLanguageRequest request);
    Task<Result<GetSupportedLanguagesResponse>> GetSupportedLanguagesAsync(GetSupportedLanguagesRequest request);
    Task<Result<RomanizeTextResponse>> RomanizeTextAsync(RomanizeTextRequest request);
    Task<Result<OperationResponse>> BatchTranslateTextAsync(BatchTranslateTextRequest request);
    Task<Result<GlossaryResponse>> CreateGlossaryAsync(CreateGlossaryRequest request);
    Task<Result<ListGlossariesResponse>> ListGlossariesAsync(string projectId, string accessToken);
    Task<Result<OperationResponse>> DeleteGlossaryAsync(DeleteGlossaryRequest request);
}

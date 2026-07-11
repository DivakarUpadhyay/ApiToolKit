using ApiToolKit.Application.DTOs.Cloud.NaturalLanguage;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Cloud;

public interface ILanguageService
{
    Task<Result<AnalyzeEntitiesResponse>> AnalyzeEntitiesAsync(AnalyzeEntitiesRequest request);
    Task<Result<AnalyzeSentimentResponse>> AnalyzeSentimentAsync(AnalyzeSentimentRequest request);
    Task<Result<AnalyzeSyntaxResponse>> AnalyzeSyntaxAsync(AnalyzeSyntaxRequest request);
    Task<Result<ClassifyTextResponse>> ClassifyTextAsync(ClassifyTextRequest request);
    Task<Result<ModerateTextResponse>> ModerateTextAsync(ModerateTextRequest request);
    Task<Result<AnnotateTextResponse>> AnnotateTextAsync(AnnotateTextRequest request);
}

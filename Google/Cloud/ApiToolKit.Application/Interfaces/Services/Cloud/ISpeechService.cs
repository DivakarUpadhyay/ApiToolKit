using ApiToolKit.Application.DTOs.Cloud.Shared;
using ApiToolKit.Application.DTOs.Cloud.Speech;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Cloud;

public interface ISpeechService
{
    Task<Result<RecognizeResponse>> RecognizeAsync(RecognizeRequest request);
    Task<Result<OperationResponse>> LongRunningRecognizeAsync(LongRunningRecognizeRequest request);
    Task<Result<OperationResponse>> GetOperationAsync(GetOperationRequest request);
}

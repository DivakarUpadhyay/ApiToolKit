using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithm;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCustomBiddingAlgorithmService
{
    Task<Result<object>> UploadRulesCustomBiddingAlgorithmAsync(UploadRulesCustomBiddingAlgorithmRequest request);
    Task<Result<object>> ListCustomBiddingAlgorithmsAsync(ListCustomBiddingAlgorithmsRequest request);
    Task<Result<object>> GetCustomBiddingAlgorithmAsync(GetCustomBiddingAlgorithmRequest request);
    Task<Result<object>> UploadScriptCustomBiddingAlgorithmAsync(UploadScriptCustomBiddingAlgorithmRequest request);
    Task<Result<object>> CreateCustomBiddingAlgorithmAsync(CreateCustomBiddingAlgorithmRequest request);
    Task<Result<object>> UpdateCustomBiddingAlgorithmAsync(UpdateCustomBiddingAlgorithmRequest request);
}

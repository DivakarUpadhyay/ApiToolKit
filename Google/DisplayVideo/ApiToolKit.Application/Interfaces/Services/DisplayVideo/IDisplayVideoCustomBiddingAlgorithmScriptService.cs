using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmScript;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCustomBiddingAlgorithmScriptService
{
    Task<Result<object>> ListScriptsAsync(ListScriptsRequest request);
    Task<Result<object>> GetScriptAsync(GetScriptRequest request);
    Task<Result<object>> CreateScriptAsync(CreateScriptRequest request);
}

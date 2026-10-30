using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmRule;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCustomBiddingAlgorithmRuleService
{
    Task<Result<object>> CreateRuleAsync(CreateRuleRequest request);
    Task<Result<object>> GetRuleAsync(GetRuleRequest request);
    Task<Result<object>> ListRulesAsync(ListRulesRequest request);
}

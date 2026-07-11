using ApiToolKit.Application.DTOs.Shopee.AccountHealth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeAccountHealthService
{
    Task<Result<GetShopPenaltyResponse>> GetShopPenaltyAsync(GetShopPenaltyRequest request);
    Task<Result<GetMetricListResponse>> GetMetricListAsync(GetMetricListRequest request);
    Task<Result<GetShopBriefPenaltyResponse>> GetShopBriefPenaltyAsync(GetShopBriefPenaltyRequest request);
}

using ApiToolKit.Application.DTOs.CampaignManager.DynamicFeed;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerDynamicFeedService
{
    Task<Result<object>> GetDynamicFeedAsync(GetDynamicFeedRequest request);
    Task<Result<object>> RetransformDynamicFeedAsync(RetransformDynamicFeedRequest request);
    Task<Result<object>> InsertDynamicFeedAsync(InsertDynamicFeedRequest request);
    Task<Result<object>> UpdateDynamicFeedAsync(UpdateDynamicFeedRequest request);
}

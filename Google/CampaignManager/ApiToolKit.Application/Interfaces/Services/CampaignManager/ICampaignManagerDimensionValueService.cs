using ApiToolKit.Application.DTOs.CampaignManager.DimensionValue;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerDimensionValueService
{
    Task<Result<object>> QueryDimensionValueAsync(QueryDimensionValueRequest request);
}

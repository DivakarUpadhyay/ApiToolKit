using ApiToolKit.Application.DTOs.CampaignManager.DynamicTargetingKey;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerDynamicTargetingKeyService
{
    Task<Result<object>> DeleteDynamicTargetingKeyAsync(DeleteDynamicTargetingKeyRequest request);
    Task<Result<object>> InsertDynamicTargetingKeyAsync(InsertDynamicTargetingKeyRequest request);
    Task<Result<object>> ListDynamicTargetingKeysAsync(ListDynamicTargetingKeysRequest request);
}

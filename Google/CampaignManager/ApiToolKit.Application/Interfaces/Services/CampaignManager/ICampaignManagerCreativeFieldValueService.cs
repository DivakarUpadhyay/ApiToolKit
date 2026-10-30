using ApiToolKit.Application.DTOs.CampaignManager.CreativeFieldValue;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCreativeFieldValueService
{
    Task<Result<object>> GetCreativeFieldValueAsync(GetCreativeFieldValueRequest request);
    Task<Result<object>> UpdateCreativeFieldValueAsync(UpdateCreativeFieldValueRequest request);
    Task<Result<object>> InsertCreativeFieldValueAsync(InsertCreativeFieldValueRequest request);
    Task<Result<object>> UpdateCreativeFieldValueAltAsync(UpdateCreativeFieldValueAltRequest request);
    Task<Result<object>> ListCreativeFieldValuesAsync(ListCreativeFieldValuesRequest request);
    Task<Result<object>> DeleteCreativeFieldValueAsync(DeleteCreativeFieldValueRequest request);
}

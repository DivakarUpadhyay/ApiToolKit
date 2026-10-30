using ApiToolKit.Application.DTOs.CampaignManager.CreativeField;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCreativeFieldService
{
    Task<Result<object>> UpdateCreativeFieldAsync(UpdateCreativeFieldRequest request);
    Task<Result<object>> GetCreativeFieldAsync(GetCreativeFieldRequest request);
    Task<Result<object>> InsertCreativeFieldAsync(InsertCreativeFieldRequest request);
    Task<Result<object>> UpdateCreativeFieldAltAsync(UpdateCreativeFieldAltRequest request);
    Task<Result<object>> DeleteCreativeFieldAsync(DeleteCreativeFieldRequest request);
    Task<Result<object>> ListCreativeFieldsAsync(ListCreativeFieldsRequest request);
}

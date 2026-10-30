using ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivity;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerFloodlightActivityService
{
    Task<Result<object>> UpdateFloodlightActivityAsync(UpdateFloodlightActivityRequest request);
    Task<Result<object>> UpdateFloodlightActivityAltAsync(UpdateFloodlightActivityAltRequest request);
    Task<Result<object>> DeleteFloodlightActivityAsync(DeleteFloodlightActivityRequest request);
    Task<Result<object>> ListFloodlightActivitiesAsync(ListFloodlightActivitiesRequest request);
    Task<Result<object>> GetFloodlightActivityAsync(GetFloodlightActivityRequest request);
    Task<Result<object>> GeneratetagFloodlightActivityAsync(GeneratetagFloodlightActivityRequest request);
    Task<Result<object>> InsertFloodlightActivityAsync(InsertFloodlightActivityRequest request);
}

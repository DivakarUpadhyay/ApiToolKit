using ApiToolKit.Application.DTOs.CampaignManager.FloodlightConfiguration;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerFloodlightConfigurationService
{
    Task<Result<object>> GetFloodlightConfigurationAsync(GetFloodlightConfigurationRequest request);
    Task<Result<object>> UpdateFloodlightConfigurationAsync(UpdateFloodlightConfigurationRequest request);
    Task<Result<object>> ListFloodlightConfigurationsAsync(ListFloodlightConfigurationsRequest request);
    Task<Result<object>> PatchFloodlightConfigurationAsync(PatchFloodlightConfigurationRequest request);
}

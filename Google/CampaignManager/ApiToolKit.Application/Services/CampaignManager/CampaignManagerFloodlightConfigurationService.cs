using ApiToolKit.Application.DTOs.CampaignManager.FloodlightConfiguration;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerFloodlightConfigurationService : CampaignManagerServiceBase, ICampaignManagerFloodlightConfigurationService
{
    public CampaignManagerFloodlightConfigurationService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetFloodlightConfigurationAsync(GetFloodlightConfigurationRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightConfigurations/{Esc(r.FloodlightConfigurationId)}";
        return await SendAsync(r.AccountKey, "GetFloodlightConfiguration", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateFloodlightConfigurationAsync(UpdateFloodlightConfigurationRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightConfigurations";
        return await SendAsync(r.AccountKey, "UpdateFloodlightConfiguration", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> ListFloodlightConfigurationsAsync(ListFloodlightConfigurationsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightConfigurations";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "ids", r.Ids);
        return await SendAsync(r.AccountKey, "ListFloodlightConfigurations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> PatchFloodlightConfigurationAsync(PatchFloodlightConfigurationRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightConfigurations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchFloodlightConfiguration", HttpMethod.Patch, url, query, r.Body);
    }
}

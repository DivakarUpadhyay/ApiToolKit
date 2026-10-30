using ApiToolKit.Application.DTOs.CampaignManager.CampaignCreativeAssociation;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCampaignCreativeAssociationService : CampaignManagerServiceBase, ICampaignManagerCampaignCreativeAssociationService
{
    public CampaignManagerCampaignCreativeAssociationService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertCampaignCreativeAssociationAsync(InsertCampaignCreativeAssociationRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns/{Esc(r.CampaignId)}/campaignCreativeAssociations";
        return await SendAsync(r.AccountKey, "InsertCampaignCreativeAssociation", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListCampaignCreativeAssociationsAsync(ListCampaignCreativeAssociationsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns/{Esc(r.CampaignId)}/campaignCreativeAssociations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "maxResults", r.MaxResults?.ToString());
        return await SendAsync(r.AccountKey, "ListCampaignCreativeAssociations", HttpMethod.Get, url, query);
    }
}

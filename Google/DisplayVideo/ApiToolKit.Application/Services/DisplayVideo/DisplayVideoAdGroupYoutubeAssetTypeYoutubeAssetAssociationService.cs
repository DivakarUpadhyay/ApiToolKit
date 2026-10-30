using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupYoutubeAssetTypeYoutubeAssetAssociation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService : DisplayVideoServiceBase, IDisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService
{
    public DisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListYoutubeAssetAssociationsAsync(ListYoutubeAssetAssociationsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups/{Esc(r.AdGroupId)}/youtubeAssetTypes/{Esc(r.YoutubeAssetTypeId)}/youtubeAssetAssociations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "linkedEntity.lineItemId", r.LinkedEntityLineItemId);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListYoutubeAssetAssociations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateYoutubeAssetAssociationAsync(CreateYoutubeAssetAssociationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups/{Esc(r.AdGroupId)}/youtubeAssetTypes/{Esc(r.YoutubeAssetTypeId)}/youtubeAssetAssociations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "linkedEntity.lineItemId", r.LinkedEntityLineItemId);
        return await SendAsync(r.AccountKey, "CreateYoutubeAssetAssociation", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteYoutubeAssetAssociationAsync(DeleteYoutubeAssetAssociationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/adGroups/{Esc(r.AdGroupId)}/youtubeAssetTypes/{Esc(r.YoutubeAssetTypeId)}/youtubeAssetAssociations/{Esc(r.YoutubeAssetAssociationId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "linkedEntity.lineItemId", r.LinkedEntityLineItemId);
        return await SendAsync(r.AccountKey, "DeleteYoutubeAssetAssociation", HttpMethod.Delete, url, query);
    }
}

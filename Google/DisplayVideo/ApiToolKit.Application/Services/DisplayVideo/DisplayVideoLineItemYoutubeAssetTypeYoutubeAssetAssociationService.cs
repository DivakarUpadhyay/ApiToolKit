using ApiToolKit.Application.DTOs.DisplayVideo.LineItemYoutubeAssetTypeYoutubeAssetAssociation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService : DisplayVideoServiceBase, IDisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService
{
    public DisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListYoutubeAssetAssociationsAsync(ListYoutubeAssetAssociationsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}/youtubeAssetTypes/{Esc(r.YoutubeAssetTypeId)}/youtubeAssetAssociations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "linkedEntity.adGroupId", r.LinkedEntityAdGroupId);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListYoutubeAssetAssociations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateYoutubeAssetAssociationAsync(CreateYoutubeAssetAssociationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}/youtubeAssetTypes/{Esc(r.YoutubeAssetTypeId)}/youtubeAssetAssociations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "linkedEntity.adGroupId", r.LinkedEntityAdGroupId);
        return await SendAsync(r.AccountKey, "CreateYoutubeAssetAssociation", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteYoutubeAssetAssociationAsync(DeleteYoutubeAssetAssociationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/lineItems/{Esc(r.LineItemId)}/youtubeAssetTypes/{Esc(r.YoutubeAssetTypeId)}/youtubeAssetAssociations/{Esc(r.YoutubeAssetAssociationId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "linkedEntity.adGroupId", r.LinkedEntityAdGroupId);
        return await SendAsync(r.AccountKey, "DeleteYoutubeAssetAssociation", HttpMethod.Delete, url, query);
    }
}

using ApiToolKit.Application.DTOs.BusinessProfile.Media;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileMediaService : BusinessProfileServiceBase, IBusinessProfileMediaService
{
    public BusinessProfileMediaService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListMediaAsync(ListMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListMedia", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetMediaAsync(GetMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media/{r.MediaId}";
        return await SendAsync(r.AccountKey, "GetMedia", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateMediaAsync(CreateMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media";
        return await SendAsync(r.AccountKey, "CreateMedia", HttpMethod.Post, url, null, new { mediaFormat = r.MediaFormat, sourceUrl = r.SourceUrl, locationAssociation = r.Category == null ? null : new { category = r.Category }, description = r.Description });
    }

    public async Task<Result<object>> UpdateMediaAsync(UpdateMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media/{r.MediaId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateMedia", HttpMethod.Patch, url, query, new { description = r.Description, locationAssociation = r.Category == null ? null : new { category = r.Category } });
    }

    public async Task<Result<object>> DeleteMediaAsync(DeleteMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media/{r.MediaId}";
        return await SendAsync(r.AccountKey, "DeleteMedia", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> StartMediaUploadAsync(StartMediaUploadRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media:startUpload";
        return await SendAsync(r.AccountKey, "StartMediaUpload", HttpMethod.Post, url, null, new { });
    }

    public async Task<Result<object>> ListCustomerMediaAsync(ListCustomerMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media/customers";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListCustomerMedia", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetCustomerMediaAsync(GetCustomerMediaRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/media/customers/{r.MediaId}";
        return await SendAsync(r.AccountKey, "GetCustomerMedia", HttpMethod.Get, url);
    }
}

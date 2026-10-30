using ApiToolKit.Application.DTOs.BusinessProfile.LocalPost;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileLocalPostService : BusinessProfileServiceBase, IBusinessProfileLocalPostService
{
    public BusinessProfileLocalPostService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListLocalPostsAsync(ListLocalPostsRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/localPosts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListLocalPosts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateLocalPostAsync(CreateLocalPostRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/localPosts";
        return await SendAsync(r.AccountKey, "CreateLocalPost", HttpMethod.Post, url, null, new { languageCode = r.LanguageCode, summary = r.Summary, topicType = r.TopicType ?? "STANDARD", callToAction = r.CallToActionType == null ? null : new { actionType = r.CallToActionType, url = r.CallToActionUrl }, media = r.MediaUrl == null ? null : new[] { new { mediaFormat = "PHOTO", sourceUrl = r.MediaUrl } }, @event = r.Event, offer = r.Offer });
    }

    public async Task<Result<object>> DeleteLocalPostAsync(DeleteLocalPostRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/localPosts/{r.PostId}";
        return await SendAsync(r.AccountKey, "DeleteLocalPost", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetLocalPostAsync(GetLocalPostRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/localPosts/{r.PostId}";
        return await SendAsync(r.AccountKey, "GetLocalPost", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateLocalPostAsync(UpdateLocalPostRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/localPosts/{r.PostId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateLocalPost", HttpMethod.Patch, url, query, new { summary = r.Summary, @event = r.Event, offer = r.Offer, callToAction = r.CallToAction });
    }

    public async Task<Result<object>> GetLocalPostInsightsAsync(GetLocalPostInsightsRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/localPosts:reportInsights";
        return await SendAsync(r.AccountKey, "GetLocalPostInsights", HttpMethod.Post, url, null, new { localPostNames = r.LocalPostNames, basicRequest = r.BasicRequest });
    }
}

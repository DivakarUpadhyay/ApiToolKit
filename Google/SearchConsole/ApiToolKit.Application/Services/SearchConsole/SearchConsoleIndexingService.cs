using ApiToolKit.Application.DTOs.SearchConsole.Indexing;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;

namespace ApiToolKit.Application.Services.SearchConsole;

public class SearchConsoleIndexingService : SearchConsoleServiceBase, ISearchConsoleIndexingService
{
    public SearchConsoleIndexingService(ISearchConsoleApiClient apiClient, ISearchConsoleAuthService authService, ISearchConsoleAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> PublishUrlNotificationAsync(PublishUrlNotificationRequest r)
    {
        var url = "https://indexing.googleapis.com/v3/urlNotifications:publish";
        return await SendAsync(r.AccountKey, "PublishUrlNotification", HttpMethod.Post, url, null, new { url = r.Url, type = r.Type });
    }

    public async Task<Result<object>> GetUrlNotificationMetadataAsync(GetUrlNotificationMetadataRequest r)
    {
        var url = "https://indexing.googleapis.com/v3/urlNotifications/metadata";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "url", r.Url);
        return await SendAsync(r.AccountKey, "GetUrlNotificationMetadata", HttpMethod.Get, url, query);
    }
}

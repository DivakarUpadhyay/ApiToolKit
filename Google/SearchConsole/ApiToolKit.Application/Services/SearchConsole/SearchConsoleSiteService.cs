using ApiToolKit.Application.DTOs.SearchConsole.Site;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;

namespace ApiToolKit.Application.Services.SearchConsole;

public class SearchConsoleSiteService : SearchConsoleServiceBase, ISearchConsoleSiteService
{
    public SearchConsoleSiteService(ISearchConsoleApiClient apiClient, ISearchConsoleAuthService authService, ISearchConsoleAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListSitesAsync(ListSitesRequest r)
    {
        var url = "https://www.googleapis.com/webmasters/v3/sites";
        return await SendAsync(r.AccountKey, "ListSites", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetSiteAsync(GetSiteRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}";
        return await SendAsync(r.AccountKey, "GetSite", HttpMethod.Get, url);
    }

    public async Task<Result<object>> AddSiteAsync(AddSiteRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}";
        return await SendAsync(r.AccountKey, "AddSite", HttpMethod.Put, url);
    }

    public async Task<Result<object>> DeleteSiteAsync(DeleteSiteRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}";
        return await SendAsync(r.AccountKey, "DeleteSite", HttpMethod.Delete, url);
    }
}

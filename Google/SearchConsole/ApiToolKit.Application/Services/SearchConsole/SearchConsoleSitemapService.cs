using ApiToolKit.Application.DTOs.SearchConsole.Sitemap;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;

namespace ApiToolKit.Application.Services.SearchConsole;

public class SearchConsoleSitemapService : SearchConsoleServiceBase, ISearchConsoleSitemapService
{
    public SearchConsoleSitemapService(ISearchConsoleApiClient apiClient, ISearchConsoleAuthService authService, ISearchConsoleAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListSitemapsAsync(ListSitemapsRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/sitemaps";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sitemapIndex", r.SitemapIndex);
        return await SendAsync(r.AccountKey, "ListSitemaps", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetSitemapAsync(GetSitemapRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/sitemaps/{Esc(r.Feedpath)}";
        return await SendAsync(r.AccountKey, "GetSitemap", HttpMethod.Get, url);
    }

    public async Task<Result<object>> SubmitSitemapAsync(SubmitSitemapRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/sitemaps/{Esc(r.Feedpath)}";
        return await SendAsync(r.AccountKey, "SubmitSitemap", HttpMethod.Put, url);
    }

    public async Task<Result<object>> DeleteSitemapAsync(DeleteSitemapRequest r)
    {
        var url = $"https://www.googleapis.com/webmasters/v3/sites/{Esc(r.SiteUrl)}/sitemaps/{Esc(r.Feedpath)}";
        return await SendAsync(r.AccountKey, "DeleteSitemap", HttpMethod.Delete, url);
    }
}

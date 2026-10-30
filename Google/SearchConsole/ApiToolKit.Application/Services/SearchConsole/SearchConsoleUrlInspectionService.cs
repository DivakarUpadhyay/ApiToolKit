using ApiToolKit.Application.DTOs.SearchConsole.UrlInspection;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;

namespace ApiToolKit.Application.Services.SearchConsole;

public class SearchConsoleUrlInspectionService : SearchConsoleServiceBase, ISearchConsoleUrlInspectionService
{
    public SearchConsoleUrlInspectionService(ISearchConsoleApiClient apiClient, ISearchConsoleAuthService authService, ISearchConsoleAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InspectUrlAsync(InspectUrlRequest r)
    {
        var url = "https://searchconsole.googleapis.com/v1/urlInspection/index:inspect";
        return await SendAsync(r.AccountKey, "InspectUrl", HttpMethod.Post, url, null, new { inspectionUrl = r.InspectionUrl, siteUrl = r.SiteUrl, languageCode = r.LanguageCode });
    }
}

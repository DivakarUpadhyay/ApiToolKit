using ApiToolKit.Application.DTOs.SearchConsole.Site;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleSiteService
{
    Task<Result<object>> ListSitesAsync(ListSitesRequest request);
    Task<Result<object>> GetSiteAsync(GetSiteRequest request);
    Task<Result<object>> AddSiteAsync(AddSiteRequest request);
    Task<Result<object>> DeleteSiteAsync(DeleteSiteRequest request);
}

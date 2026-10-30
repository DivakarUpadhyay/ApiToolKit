using ApiToolKit.Application.DTOs.SearchConsole.Sitemap;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleSitemapService
{
    Task<Result<object>> ListSitemapsAsync(ListSitemapsRequest request);
    Task<Result<object>> GetSitemapAsync(GetSitemapRequest request);
    Task<Result<object>> SubmitSitemapAsync(SubmitSitemapRequest request);
    Task<Result<object>> DeleteSitemapAsync(DeleteSitemapRequest request);
}

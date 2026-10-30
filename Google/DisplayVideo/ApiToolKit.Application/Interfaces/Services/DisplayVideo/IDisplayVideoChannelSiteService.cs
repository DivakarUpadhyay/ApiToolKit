using ApiToolKit.Application.DTOs.DisplayVideo.ChannelSite;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoChannelSiteService
{
    Task<Result<object>> CreateSiteAsync(CreateSiteRequest request);
    Task<Result<object>> ListSitesAsync(ListSitesRequest request);
    Task<Result<object>> ReplaceSiteAsync(ReplaceSiteRequest request);
    Task<Result<object>> BulkEditSiteAsync(BulkEditSiteRequest request);
    Task<Result<object>> DeleteSiteAsync(DeleteSiteRequest request);
}

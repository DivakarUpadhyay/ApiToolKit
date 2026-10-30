using ApiToolKit.Application.DTOs.DisplayVideo.PartnerChannelSite;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoPartnerChannelSiteService
{
    Task<Result<object>> BulkEditSiteAsync(BulkEditSiteRequest request);
    Task<Result<object>> CreateSiteAsync(CreateSiteRequest request);
    Task<Result<object>> ReplaceSiteAsync(ReplaceSiteRequest request);
    Task<Result<object>> ListSitesAsync(ListSitesRequest request);
    Task<Result<object>> DeleteSiteAsync(DeleteSiteRequest request);
}

using ApiToolKit.Application.DTOs.CampaignManager.Site;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerSiteService
{
    Task<Result<object>> ListSitesAsync(ListSitesRequest request);
    Task<Result<object>> InsertSiteAsync(InsertSiteRequest request);
    Task<Result<object>> UpdateSiteAsync(UpdateSiteRequest request);
    Task<Result<object>> GetSiteAsync(GetSiteRequest request);
    Task<Result<object>> UpdateSiteAltAsync(UpdateSiteAltRequest request);
}

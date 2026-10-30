using ApiToolKit.Application.DTOs.CampaignManager.DirectorySite;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerDirectorySiteService
{
    Task<Result<object>> ListDirectorySitesAsync(ListDirectorySitesRequest request);
    Task<Result<object>> InsertDirectorySiteAsync(InsertDirectorySiteRequest request);
    Task<Result<object>> GetDirectorySiteAsync(GetDirectorySiteRequest request);
}

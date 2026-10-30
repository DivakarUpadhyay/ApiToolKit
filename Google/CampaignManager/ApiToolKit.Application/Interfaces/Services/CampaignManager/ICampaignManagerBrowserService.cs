using ApiToolKit.Application.DTOs.CampaignManager.Browser;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerBrowserService
{
    Task<Result<object>> ListBrowsersAsync(ListBrowsersRequest request);
}

using ApiToolKit.Application.DTOs.CampaignManager.OperatingSystemVersion;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerOperatingSystemVersionService
{
    Task<Result<object>> GetOperatingSystemVersionAsync(GetOperatingSystemVersionRequest request);
    Task<Result<object>> ListOperatingSystemVersionsAsync(ListOperatingSystemVersionsRequest request);
}

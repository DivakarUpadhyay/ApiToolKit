using ApiToolKit.Application.DTOs.CampaignManager.OperatingSystem;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerOperatingSystemService
{
    Task<Result<object>> GetOperatingSystemAsync(GetOperatingSystemRequest request);
    Task<Result<object>> ListOperatingSystemsAsync(ListOperatingSystemsRequest request);
}

using ApiToolKit.Application.DTOs.CampaignManager.PlatformType;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerPlatformTypeService
{
    Task<Result<object>> GetPlatformTypeAsync(GetPlatformTypeRequest request);
    Task<Result<object>> ListPlatformTypesAsync(ListPlatformTypesRequest request);
}

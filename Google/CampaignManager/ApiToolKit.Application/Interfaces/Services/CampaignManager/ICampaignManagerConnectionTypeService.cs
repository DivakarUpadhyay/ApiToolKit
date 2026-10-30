using ApiToolKit.Application.DTOs.CampaignManager.ConnectionType;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerConnectionTypeService
{
    Task<Result<object>> ListConnectionTypesAsync(ListConnectionTypesRequest request);
    Task<Result<object>> GetConnectionTypeAsync(GetConnectionTypeRequest request);
}

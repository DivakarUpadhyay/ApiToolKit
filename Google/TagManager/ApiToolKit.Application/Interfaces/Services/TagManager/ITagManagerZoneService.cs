using ApiToolKit.Application.DTOs.TagManager.Zone;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerZoneService
{
    Task<Result<object>> ListZonesAsync(ListZonesRequest request);
    Task<Result<object>> GetZoneAsync(GetZoneRequest request);
    Task<Result<object>> CreateZoneAsync(CreateZoneRequest request);
    Task<Result<object>> UpdateZoneAsync(UpdateZoneRequest request);
    Task<Result<object>> DeleteZoneAsync(DeleteZoneRequest request);
    Task<Result<object>> RevertZoneAsync(RevertZoneRequest request);
}

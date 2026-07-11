using ApiToolKit.Application.DTOs.Maps.StaticMap;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsStaticMapService
{
    Task<Result<StaticMapResponse>> GetStaticMapUrlAsync(StaticMapRequest request);
}

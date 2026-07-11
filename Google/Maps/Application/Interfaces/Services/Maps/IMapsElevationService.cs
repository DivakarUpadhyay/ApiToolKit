using ApiToolKit.Application.DTOs.Maps.Elevation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsElevationService
{
    Task<Result<ElevationResponse>> GetElevationAsync(ElevationRequest request);
    Task<Result<ElevationResponse>> GetElevationAlongPathAsync(ElevationPathRequest request);
}

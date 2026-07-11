using ApiToolKit.Application.DTOs.Maps.Directions;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsDirectionsService
{
    Task<Result<DirectionsResponse>> GetDirectionsAsync(DirectionsRequest request);
}

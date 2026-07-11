using ApiToolKit.Application.DTOs.Maps.Roads;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsRoadsService
{
    Task<Result<SnapToRoadsResponse>> SnapToRoadsAsync(SnapToRoadsRequest request);
}

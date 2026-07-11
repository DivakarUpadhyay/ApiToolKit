using ApiToolKit.Application.DTOs.Maps.TimeZone;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsTimeZoneService
{
    Task<Result<TimeZoneResponse>> GetTimeZoneAsync(TimeZoneRequest request);
}

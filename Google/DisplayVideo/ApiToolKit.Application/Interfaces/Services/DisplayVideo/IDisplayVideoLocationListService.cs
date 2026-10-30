using ApiToolKit.Application.DTOs.DisplayVideo.LocationList;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoLocationListService
{
    Task<Result<object>> UpdateLocationListAsync(UpdateLocationListRequest request);
    Task<Result<object>> GetLocationListAsync(GetLocationListRequest request);
    Task<Result<object>> ListLocationListsAsync(ListLocationListsRequest request);
    Task<Result<object>> CreateLocationListAsync(CreateLocationListRequest request);
}

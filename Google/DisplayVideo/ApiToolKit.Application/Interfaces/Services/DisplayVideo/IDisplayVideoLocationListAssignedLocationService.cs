using ApiToolKit.Application.DTOs.DisplayVideo.LocationListAssignedLocation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoLocationListAssignedLocationService
{
    Task<Result<object>> ListAssignedLocationsAsync(ListAssignedLocationsRequest request);
    Task<Result<object>> CreateAssignedLocationAsync(CreateAssignedLocationRequest request);
    Task<Result<object>> DeleteAssignedLocationAsync(DeleteAssignedLocationRequest request);
    Task<Result<object>> BulkEditAssignedLocationAsync(BulkEditAssignedLocationRequest request);
}

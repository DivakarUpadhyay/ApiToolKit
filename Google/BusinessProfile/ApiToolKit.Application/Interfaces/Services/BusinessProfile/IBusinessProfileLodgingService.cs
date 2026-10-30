using ApiToolKit.Application.DTOs.BusinessProfile.Lodging;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileLodgingService
{
    Task<Result<object>> GetLodgingAsync(GetLodgingRequest request);
    Task<Result<object>> UpdateLodgingAsync(UpdateLodgingRequest request);
    Task<Result<object>> GetGoogleUpdatedLodgingAsync(GetGoogleUpdatedLodgingRequest request);
}

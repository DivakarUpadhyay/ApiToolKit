using ApiToolKit.Application.DTOs.BusinessProfile.PlaceAction;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfilePlaceActionService
{
    Task<Result<object>> ListPlaceActionLinksAsync(ListPlaceActionLinksRequest request);
    Task<Result<object>> GetPlaceActionLinkAsync(GetPlaceActionLinkRequest request);
    Task<Result<object>> CreatePlaceActionLinkAsync(CreatePlaceActionLinkRequest request);
    Task<Result<object>> UpdatePlaceActionLinkAsync(UpdatePlaceActionLinkRequest request);
    Task<Result<object>> DeletePlaceActionLinkAsync(DeletePlaceActionLinkRequest request);
    Task<Result<object>> ListPlaceActionTypesAsync(ListPlaceActionTypesRequest request);
}

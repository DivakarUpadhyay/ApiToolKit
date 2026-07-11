using ApiToolKit.Application.DTOs.Flipkart.HyperLocal;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Flipkart;

public interface IFlipkartHyperLocalService
{
    Task<Result<HyperLocalOperationResponse>> CreateHyperLocalListingAsync(CreateHyperLocalListingRequest request);
    Task<Result<HyperLocalOperationResponse>> UpdateHyperLocalListingAsync(UpdateHyperLocalListingRequest request);
    Task<Result<object>> GetHyperLocalListingAsync(GetHLListingRequest request);
    Task<Result<HyperLocalOperationResponse>> UpdateHLPriceAsync(UpdateHLPriceRequest request);
    Task<Result<HyperLocalOperationResponse>> UpdateHLInventoryAsync(UpdateHLInventoryRequest request);
}

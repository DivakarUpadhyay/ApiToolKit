using ApiToolKit.Application.DTOs.Flipkart.HyperLocal;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Flipkart;

public class FlipkartHyperLocalService : IFlipkartHyperLocalService
{
    private readonly IFlipkartApiClient _api;
    public FlipkartHyperLocalService(IFlipkartApiClient api) => _api = api;

    public async Task<Result<HyperLocalOperationResponse>> CreateHyperLocalListingAsync(CreateHyperLocalListingRequest request)
    {
        var result = await _api.PostAsync<HyperLocalOperationResponse>("/listings/v3/hyperlocal", request.Listings);
        return result != null ? Result<HyperLocalOperationResponse>.Ok(result) : Result<HyperLocalOperationResponse>.Fail("No response.");
    }

    public async Task<Result<HyperLocalOperationResponse>> UpdateHyperLocalListingAsync(UpdateHyperLocalListingRequest request)
    {
        var result = await _api.PostAsync<HyperLocalOperationResponse>("/listings/v3/hyperlocal/update", request.Listings);
        return result != null ? Result<HyperLocalOperationResponse>.Ok(result) : Result<HyperLocalOperationResponse>.Fail("No response.");
    }

    public async Task<Result<object>> GetHyperLocalListingAsync(GetHLListingRequest request)
    {
        var result = await _api.GetAsync<object>($"/listings/v3/{Uri.EscapeDataString(request.SkuIds)}");
        return result != null ? Result<object>.Ok(result) : Result<object>.Fail("No response.");
    }

    public async Task<Result<HyperLocalOperationResponse>> UpdateHLPriceAsync(UpdateHLPriceRequest request)
    {
        var result = await _api.PostAsync<HyperLocalOperationResponse>("/listings/v3/update/price", request.Prices);
        return result != null ? Result<HyperLocalOperationResponse>.Ok(result) : Result<HyperLocalOperationResponse>.Fail("No response.");
    }

    public async Task<Result<HyperLocalOperationResponse>> UpdateHLInventoryAsync(UpdateHLInventoryRequest request)
    {
        var result = await _api.PostAsync<HyperLocalOperationResponse>("/listings/v3/update/inventory", request.Inventories);
        return result != null ? Result<HyperLocalOperationResponse>.Ok(result) : Result<HyperLocalOperationResponse>.Fail("No response.");
    }
}

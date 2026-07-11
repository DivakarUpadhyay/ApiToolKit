using ApiToolKit.Application.DTOs.Flipkart.Listing;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Flipkart;

public class FlipkartListingService : IFlipkartListingService
{
    private readonly IFlipkartApiClient _api;
    public FlipkartListingService(IFlipkartApiClient api) => _api = api;

    public async Task<Result<ListingOperationResponse>> CreateListingAsync(CreateListingRequest request)
    {
        var result = await _api.PostAsync<ListingOperationResponse>("/listings/v3", request.Listings);
        return result != null ? Result<ListingOperationResponse>.Ok(result) : Result<ListingOperationResponse>.Fail("No response from Flipkart API.");
    }

    public async Task<Result<ListingOperationResponse>> UpdateListingAsync(UpdateListingRequest request)
    {
        var result = await _api.PostAsync<ListingOperationResponse>("/listings/v3/update", request.Listings);
        return result != null ? Result<ListingOperationResponse>.Ok(result) : Result<ListingOperationResponse>.Fail("No response from Flipkart API.");
    }

    public async Task<Result<GetListingsResponse>> GetListingsBySkuAsync(GetListingsBySkuRequest request)
    {
        var result = await _api.GetAsync<GetListingsResponse>($"/listings/v3/{Uri.EscapeDataString(request.SkuIds)}");
        return result != null ? Result<GetListingsResponse>.Ok(result) : Result<GetListingsResponse>.Fail("No response.");
    }

    public async Task<Result<GetListingsResponse>> GetListingDetailsAsync(GetListingDetailsRequest request)
    {
        var result = await _api.PostAsync<GetListingsResponse>("/listings/v3/details", new { sku_ids = request.SkuIds });
        return result != null ? Result<GetListingsResponse>.Ok(result) : Result<GetListingsResponse>.Fail("No response.");
    }

    public async Task<Result<SearchListingsResponse>> SearchListingsAsync(SearchListingsRequest request)
    {
        var result = await _api.PostAsync<SearchListingsResponse>("/listings/v3/product/search", new { batchNo = request.BatchNo, internalState = request.InternalState, searchText = request.SearchText });
        return result != null ? Result<SearchListingsResponse>.Ok(result) : Result<SearchListingsResponse>.Fail("No response.");
    }

    public async Task<Result<ListingOperationResponse>> UpdatePriceAsync(UpdatePriceRequest request)
    {
        var result = await _api.PostAsync<ListingOperationResponse>("/listings/v3/update/price", request.Prices);
        return result != null ? Result<ListingOperationResponse>.Ok(result) : Result<ListingOperationResponse>.Fail("No response.");
    }

    public async Task<Result<ListingOperationResponse>> UpdateInventoryAsync(UpdateInventoryRequest request)
    {
        var result = await _api.PostAsync<ListingOperationResponse>("/listings/v3/update/inventory", request.Inventories);
        return result != null ? Result<ListingOperationResponse>.Ok(result) : Result<ListingOperationResponse>.Fail("No response.");
    }

    public async Task<Result<GetAllListingsResponse>> GetAllListingsAsync(GetAllListingsRequest request)
    {
        var body = new { filters = new { listing_status = request.ListingStatus }, page_id = request.PageId };
        var result = await _api.PostAsync<GetAllListingsResponse>("/listings/v3/search", body);
        return result != null ? Result<GetAllListingsResponse>.Ok(result) : Result<GetAllListingsResponse>.Fail("No response.");
    }

    public async Task<Result<SearchListingsResponse>> ProductSearchAsync(ProductSearchRequest request)
    {
        var result = await _api.PostAsync<SearchListingsResponse>("/listings/v3/product/search", new { searchText = request.SearchText, batchNo = request.BatchNo });
        return result != null ? Result<SearchListingsResponse>.Ok(result) : Result<SearchListingsResponse>.Fail("No response.");
    }
}

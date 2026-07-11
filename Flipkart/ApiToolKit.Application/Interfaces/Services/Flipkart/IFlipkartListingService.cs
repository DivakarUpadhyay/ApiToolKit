using ApiToolKit.Application.DTOs.Flipkart.Listing;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Flipkart;

public interface IFlipkartListingService
{
    Task<Result<ListingOperationResponse>> CreateListingAsync(CreateListingRequest request);
    Task<Result<ListingOperationResponse>> UpdateListingAsync(UpdateListingRequest request);
    Task<Result<GetListingsResponse>> GetListingsBySkuAsync(GetListingsBySkuRequest request);
    Task<Result<GetListingsResponse>> GetListingDetailsAsync(GetListingDetailsRequest request);
    Task<Result<SearchListingsResponse>> SearchListingsAsync(SearchListingsRequest request);
    Task<Result<ListingOperationResponse>> UpdatePriceAsync(UpdatePriceRequest request);
    Task<Result<ListingOperationResponse>> UpdateInventoryAsync(UpdateInventoryRequest request);
    Task<Result<GetAllListingsResponse>> GetAllListingsAsync(GetAllListingsRequest request);
    Task<Result<SearchListingsResponse>> ProductSearchAsync(ProductSearchRequest request);
}

using ApiToolKit.Application.DTOs.Shopee.GlobalProduct;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeGlobalProductService
{
    Task<Result<GetGlobalCategoryListResponse>> GetGlobalCategoryListAsync(GetGlobalCategoryListRequest request);
    Task<Result<GetCategoryAttributeResponse>> GetCategoryAttributeAsync(GetCategoryAttributeRequest request);
    Task<Result<GetBrandListResponse>> GetBrandListAsync(GetBrandListRequest request);
    Task<Result<AddGlobalItemResponse>> AddGlobalItemAsync(AddGlobalItemRequest request);
    Task<Result<CopyItemResponse>> CopyItemAsync(CopyItemRequest request);
    Task<Result<DeleteGlobalItemResponse>> DeleteGlobalItemAsync(DeleteGlobalItemRequest request);
    Task<Result<GetGlobalItemListResponse>> GetGlobalItemListAsync(GetGlobalItemListRequest request);
    Task<Result<GetGlobalItemDetailResponse>> GetGlobalItemDetailAsync(GetGlobalItemDetailRequest request);
    Task<Result<UpdateGlobalItemResponse>> UpdateGlobalItemAsync(UpdateGlobalItemRequest request);
    Task<Result<SearchGlobalItemResponse>> SearchGlobalItemAsync(SearchGlobalItemRequest request);
    Task<Result<InitTierVariationResponse>> InitTierVariationAsync(InitTierVariationRequest request);
    Task<Result<UpdateTierVariationResponse>> UpdateTierVariationAsync(UpdateTierVariationRequest request);
    Task<Result<GetModelListResponse>> GetModelListAsync(GetModelListRequest request);
    Task<Result<UpdateModelResponse>> UpdateModelAsync(UpdateModelRequest request);
    Task<Result<AddGlobalModelResponse>> AddGlobalModelAsync(AddGlobalModelRequest request);
    Task<Result<DeleteGlobalModelResponse>> DeleteGlobalModelAsync(DeleteGlobalModelRequest request);
    Task<Result<GetGlobalItemInfoResponse>> GetGlobalItemInfoAsync(GetGlobalItemInfoRequest request);
    Task<Result<UpdateGlobalItemStatusResponse>> UpdateGlobalItemStatusAsync(UpdateGlobalItemStatusRequest request);
    Task<Result<GetShopItemInfoResponse>> GetShopItemInfoAsync(GetShopItemInfoRequest request);
    Task<Result<GetChannelListResponse>> GetChannelListAsync(GetChannelListRequest request);
    Task<Result<UpdateGlobalItemLogisticResponse>> UpdateGlobalItemLogisticAsync(UpdateGlobalItemLogisticRequest request);
    Task<Result<BatchUpdatePriceResponse>> BatchUpdatePriceAsync(BatchUpdatePriceRequest request);
    Task<Result<BatchUpdateStockResponse>> BatchUpdateStockAsync(BatchUpdateStockRequest request);
}

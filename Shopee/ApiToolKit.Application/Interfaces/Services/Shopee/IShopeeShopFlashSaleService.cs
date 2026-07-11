using ApiToolKit.Application.DTOs.Shopee.ShopFlashSale;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeShopFlashSaleService
{
    Task<Result<GetTimeSlotIdResponse>> GetTimeSlotIdAsync(GetTimeSlotIdRequest request);
    Task<Result<CreateFlashSaleResponse>> CreateFlashSaleAsync(CreateFlashSaleRequest request);
    Task<Result<AddFlashSaleItemsResponse>> AddFlashSaleItemsAsync(AddFlashSaleItemsRequest request);
    Task<Result<UpdateFlashSaleResponse>> UpdateFlashSaleAsync(UpdateFlashSaleRequest request);
    Task<Result<DeleteFlashSaleResponse>> DeleteFlashSaleAsync(DeleteFlashSaleRequest request);
    Task<Result<GetFlashSaleListResponse>> GetFlashSaleListAsync(GetFlashSaleListRequest request);
    Task<Result<GetFlashSaleResponse>> GetFlashSaleAsync(GetFlashSaleRequest request);
    Task<Result<UpdateFlashSaleItemsResponse>> UpdateFlashSaleItemsAsync(UpdateFlashSaleItemsRequest request);
    Task<Result<DeleteFlashSaleItemsResponse>> DeleteFlashSaleItemsAsync(DeleteFlashSaleItemsRequest request);
    Task<Result<GetFlashSaleItemsResponse>> GetFlashSaleItemsAsync(GetFlashSaleItemsRequest request);
    Task<Result<GetItemCriteriaResponse>> GetItemCriteriaAsync(GetItemCriteriaRequest request);
}

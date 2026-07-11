using ApiToolKit.Application.DTOs.Shopee.Sbs;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeSbsService
{
    Task<Result<SbsGetItemListResponse>> GetItemListAsync(SbsGetItemListRequest request);
    Task<Result<SbsCreateItemResponse>> CreateItemAsync(SbsCreateItemRequest request);
    Task<Result<SbsUpdateItemResponse>> UpdateItemAsync(SbsUpdateItemRequest request);
    Task<Result<SbsDeleteItemResponse>> DeleteItemAsync(SbsDeleteItemRequest request);
    Task<Result<SbsGetItemResponse>> GetItemAsync(SbsGetItemRequest request);
}

using ApiToolKit.Application.DTOs.Shopee.Fbs;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeFbsService
{
    Task<Result<FbsGetItemListResponse>> GetItemListAsync(FbsGetItemListRequest request);
    Task<Result<FbsCreateItemResponse>> CreateItemAsync(FbsCreateItemRequest request);
    Task<Result<FbsUpdateItemResponse>> UpdateItemAsync(FbsUpdateItemRequest request);
    Task<Result<FbsDeleteItemResponse>> DeleteItemAsync(FbsDeleteItemRequest request);
}

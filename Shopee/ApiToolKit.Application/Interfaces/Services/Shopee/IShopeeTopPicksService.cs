using ApiToolKit.Application.DTOs.Shopee.TopPicks;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeTopPicksService
{
    Task<Result<GetTopPicksListResponse>> GetTopPicksListAsync(GetTopPicksListRequest request);
    Task<Result<AddTopPicksResponse>> AddTopPicksAsync(AddTopPicksRequest request);
    Task<Result<UpdateTopPicksResponse>> UpdateTopPicksAsync(UpdateTopPicksRequest request);
    Task<Result<DeleteTopPicksResponse>> DeleteTopPicksAsync(DeleteTopPicksRequest request);
}

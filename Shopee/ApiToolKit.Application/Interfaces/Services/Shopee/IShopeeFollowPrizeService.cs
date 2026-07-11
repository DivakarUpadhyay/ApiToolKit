using ApiToolKit.Application.DTOs.Shopee.FollowPrize;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeFollowPrizeService
{
    Task<Result<GetFollowPrizeInfoResponse>> GetFollowPrizeInfoAsync(GetFollowPrizeInfoRequest request);
    Task<Result<CreateFollowPrizeResponse>> CreateFollowPrizeAsync(CreateFollowPrizeRequest request);
    Task<Result<UpdateFollowPrizeResponse>> UpdateFollowPrizeAsync(UpdateFollowPrizeRequest request);
    Task<Result<EndFollowPrizeResponse>> EndFollowPrizeAsync(EndFollowPrizeRequest request);
    Task<Result<GetFollowPrizeParticipantListResponse>> GetFollowPrizeParticipantListAsync(GetFollowPrizeParticipantListRequest request);
    Task<Result<GetFollowPrizeWinnerListResponse>> GetFollowPrizeWinnerListAsync(GetFollowPrizeWinnerListRequest request);
}

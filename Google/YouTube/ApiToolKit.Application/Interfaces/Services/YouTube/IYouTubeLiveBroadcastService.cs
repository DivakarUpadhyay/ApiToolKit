using ApiToolKit.Application.DTOs.YouTube.LiveBroadcast;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeLiveBroadcastService
{
    Task<Result<LiveBroadcastListResponse>> ListLiveBroadcastsAsync(ListLiveBroadcastsRequest request, string accessToken);
    Task<Result<LiveBroadcastResponse>> CreateLiveBroadcastAsync(CreateLiveBroadcastRequest request, string accessToken);
    Task<Result<LiveBroadcastResponse>> UpdateLiveBroadcastAsync(UpdateLiveBroadcastRequest request, string accessToken);
    Task<Result<LiveBroadcastResponse>> TransitionBroadcastAsync(TransitionBroadcastRequest request, string accessToken);
    Task<Result<LiveBroadcastResponse>> BindBroadcastAsync(BindBroadcastRequest request, string accessToken);
    Task<Result<bool>> DeleteLiveBroadcastAsync(string broadcastId, string accessToken);
}

using ApiToolKit.Application.DTOs.YouTube.LiveStream;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeLiveStreamService
{
    Task<Result<List<LiveStreamResponse>>> ListLiveStreamsAsync(ListLiveStreamsRequest request, string accessToken);
    Task<Result<LiveStreamResponse>> CreateLiveStreamAsync(CreateLiveStreamRequest request, string accessToken);
    Task<Result<LiveStreamResponse>> UpdateLiveStreamAsync(UpdateLiveStreamRequest request, string accessToken);
    Task<Result<bool>> DeleteLiveStreamAsync(string streamId, string accessToken);
}

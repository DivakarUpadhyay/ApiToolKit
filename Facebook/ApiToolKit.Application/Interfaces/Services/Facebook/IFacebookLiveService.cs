using ApiToolKit.Application.DTOs.Facebook.Live;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookLiveService
{
    Task<Result<LiveVideo>> CreateLiveVideoAsync(string pageId, CreateLiveVideoRequest request, string accessToken);
    Task<Result<LiveVideosResponse>> GetLiveVideosAsync(string pageId, string accessToken);
    Task<Result<LiveVideo>> GetLiveVideoAsync(string liveVideoId, string accessToken);
    Task<Result<bool>> UpdateLiveVideoAsync(string liveVideoId, UpdateLiveVideoRequest request, string accessToken);
    Task<Result<bool>> EndLiveVideoAsync(string liveVideoId, string accessToken);
    Task<Result<bool>> DeleteLiveVideoAsync(string liveVideoId, string accessToken);
    Task<Result<LiveVideoErrorsResponse>> GetLiveVideoErrorsAsync(string liveVideoId, string accessToken);
    Task<Result<LiveVideoBlockedUsersResponse>> GetBlockedUsersAsync(string liveVideoId, string accessToken);
}

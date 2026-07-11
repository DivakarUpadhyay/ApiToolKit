using ApiToolKit.Application.DTOs.YouTube.Playlist;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubePlaylistService
{
    Task<Result<PlaylistListResponse>> ListPlaylistsAsync(ListPlaylistsRequest request, string accessToken);
    Task<Result<PlaylistResponse>> GetPlaylistAsync(string playlistId, string accessToken);
    Task<Result<PlaylistResponse>> CreatePlaylistAsync(CreatePlaylistRequest request, string accessToken);
    Task<Result<PlaylistResponse>> UpdatePlaylistAsync(UpdatePlaylistRequest request, string accessToken);
    Task<Result<bool>> DeletePlaylistAsync(DeletePlaylistRequest request, string accessToken);
}

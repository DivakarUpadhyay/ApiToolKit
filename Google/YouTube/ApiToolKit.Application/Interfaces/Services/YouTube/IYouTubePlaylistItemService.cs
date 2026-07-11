using ApiToolKit.Application.DTOs.YouTube.PlaylistItem;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubePlaylistItemService
{
    Task<Result<PlaylistItemListResponse>> ListPlaylistItemsAsync(ListPlaylistItemsRequest request, string accessToken);
    Task<Result<PlaylistItemResponse>> AddPlaylistItemAsync(AddPlaylistItemRequest request, string accessToken);
    Task<Result<PlaylistItemResponse>> UpdatePlaylistItemAsync(UpdatePlaylistItemRequest request, string accessToken);
    Task<Result<bool>> DeletePlaylistItemAsync(DeletePlaylistItemRequest request, string accessToken);
}

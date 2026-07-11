using ApiToolKit.Application.DTOs.Facebook.Photo;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookPhotoService
{
    Task<Result<UploadPhotoResponse>> UploadPhotoAsync(UploadPhotoRequest request, string accessToken);
    Task<Result<PhotoResponse>> GetPhotoAsync(string photoId, string accessToken);
    Task<Result<GetPhotosResponse>> GetPhotosAsync(GetPhotosRequest request, string accessToken);
    Task<Result<bool>> DeletePhotoAsync(string photoId, string accessToken);
    Task<Result<object>> GetAlbumsAsync(string pageId, string accessToken, int limit);
    Task<Result<CreateAlbumResponse>> CreateAlbumAsync(CreateAlbumRequest request, string accessToken);
    Task<Result<GetPhotosResponse>> GetAlbumPhotosAsync(string albumId, string accessToken, int limit);
}

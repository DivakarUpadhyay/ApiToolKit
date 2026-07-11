using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookPhotoRepository : IFacebookPhotoRepository
{
    private readonly DapperContext _context;

    public FacebookPhotoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SavePhotoAsync(FacebookPhoto photo)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Photo_Save",
            new
            {
                photo.PhotoId,
                photo.PageId,
                photo.UserId,
                photo.AlbumId,
                Caption = photo.Name,
                photo.Source,
                photo.Link,
                photo.Width,
                photo.Height
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookPhoto?> GetPhotoByIdAsync(string photoId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookPhoto>(
            "sp_Facebook_Photo_GetById",
            new { PhotoId = photoId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookPhoto>> GetPhotosByPageIdAsync(string pageId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookPhoto>(
            "sp_Facebook_Photo_GetByPageId",
            new { PageId = pageId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeletePhotoAsync(string photoId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Photo_Delete",
            new { PhotoId = photoId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Photo_SaveApiLog",
            new
            {
                log.UserId,
                log.PageId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookUserRepository : IFacebookUserRepository
{
    private readonly DapperContext _context;

    public FacebookUserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveUserAsync(string userId, string name, string? email, string? picture)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_User_Save",
            new { UserId = userId, Name = name, Email = email, PictureUrl = picture },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetUserByIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync(
            "sp_Facebook_User_GetById",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_User_SaveApiLog",
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

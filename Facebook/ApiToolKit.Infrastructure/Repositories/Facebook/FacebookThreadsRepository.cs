using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;
using System.Data;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookThreadsRepository : IFacebookThreadsRepository
{
    private readonly DapperContext _context;

    public FacebookThreadsRepository(DapperContext context) => _context = context;

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var connection = _context.CreateConnection();
        var parameters = new { log.ApiName, log.RequestBody, log.ResponseBody, log.IsSuccess, log.StatusCode };
        return await connection.ExecuteAsync("sp_Facebook_Auth_SaveApiLog", parameters, commandType: CommandType.StoredProcedure);
    }
}

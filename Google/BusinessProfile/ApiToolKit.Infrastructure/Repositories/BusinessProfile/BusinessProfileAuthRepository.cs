using ApiToolKit.Domain.Entities.BusinessProfile;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.BusinessProfile;

public class BusinessProfileAuthRepository : IBusinessProfileAuthRepository
{
    private readonly DapperContext _context;

    public BusinessProfileAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<BusinessProfileToken?> GetTokenAsync(string accountKey)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<BusinessProfileToken>(
            "sp_BusinessProfile_GetToken",
            new { AccountKey = accountKey },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(BusinessProfileToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BusinessProfile_SaveToken",
            new
            {
                token.AccountKey,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveApiLogAsync(BusinessProfileApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BusinessProfile_SaveApiLog",
            new
            {
                log.AccountKey,
                log.ApiName,
                log.HttpMethod,
                log.Url,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

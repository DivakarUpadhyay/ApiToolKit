using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookAdAccountRepository : IFacebookAdAccountRepository
{
    private readonly DapperContext _context;

    public FacebookAdAccountRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAdAccountAsync(FacebookAdAccount account)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_AdAccount_Save",
            new
            {
                account.AccountId,
                AccountName = account.Name,
                account.Currency,
                account.TimezoneName,
                account.Balance,
                AccountStatus = account.Status,
                account.BusinessId
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookAdAccount?> GetAdAccountByIdAsync(string accountId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookAdAccount>(
            "sp_Facebook_AdAccount_GetById",
            new { AccountId = accountId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookAdAccount>> GetAdAccountsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookAdAccount>(
            "sp_Facebook_AdAccount_GetByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_AdAccount_SaveApiLog",
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

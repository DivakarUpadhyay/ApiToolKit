using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookWebhookRepository : IFacebookWebhookRepository
{
    private readonly DapperContext _context;

    public FacebookWebhookRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveWebhookAsync(FacebookWebhook webhook)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Webhook_Save",
            new
            {
                webhook.ObjectType,
                SubscribedFields = webhook.Fields,
                webhook.CallbackUrl,
                webhook.VerifyToken,
                webhook.IsActive
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookWebhook?> GetWebhookByIdAsync(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookWebhook>(
            "sp_Facebook_Webhook_GetById",
            new { Id = id },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookWebhook>> GetWebhooksByObjectTypeAsync(string objectType)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookWebhook>(
            "sp_Facebook_Webhook_GetByObjectType",
            new { ObjectType = objectType },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteWebhookAsync(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Webhook_Delete",
            new { Id = id },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateWebhookAsync(FacebookWebhook webhook)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Webhook_Update",
            new
            {
                webhook.Id,
                webhook.ObjectType,
                SubscribedFields = webhook.Fields,
                webhook.CallbackUrl,
                webhook.VerifyToken,
                webhook.IsActive
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Webhook_SaveApiLog",
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

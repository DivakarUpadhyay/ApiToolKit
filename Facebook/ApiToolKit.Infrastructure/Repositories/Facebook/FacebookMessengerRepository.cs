using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookMessengerRepository : IFacebookMessengerRepository
{
    private readonly DapperContext _context;

    public FacebookMessengerRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveMessageAsync(FacebookMessenger message)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Messenger_SaveMessage",
            new
            {
                message.SenderId,
                message.RecipientId,
                message.MessageId,
                message.MessageText,
                message.MessageType,
                message.Direction
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookMessenger>> GetMessagesByConversationAsync(string senderId, string recipientId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookMessenger>(
            "sp_Facebook_Messenger_GetMessages",
            new { SenderId = senderId, RecipientId = recipientId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Messenger_SaveApiLog",
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

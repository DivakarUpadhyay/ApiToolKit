using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Firebase;

public class FcmRepository : IFcmRepository
{
    private readonly DapperContext _context;

    public FcmRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveMessageAsync(FcmMessage message)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_SaveFcmMessage",
            new
            {
                message.MessageId,
                message.ProjectId,
                message.Title,
                message.Body,
                message.Topic,
                message.Token,
                message.Condition,
                message.Data,
                message.Platform,
                message.SendStatus,
                message.SentAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateMessageStatusAsync(string messageId, string status)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_UpdateFcmMessageStatus",
            new { MessageId = messageId, Status = status },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<FcmMessage>> GetMessagesByProjectIdAsync(string projectId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<FcmMessage>(
            "sp_Firebase_GetFcmMessagesByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

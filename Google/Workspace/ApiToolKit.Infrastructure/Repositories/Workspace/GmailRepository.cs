using ApiToolKit.Domain.Entities.Workspace.Gmail;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class GmailRepository : IGmailRepository
{
    private readonly DapperContext _context;

    public GmailRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveMessageAsync(GmailMessage message)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Gmail_SaveMessage",
            new
            {
                message.MessageId,
                message.ThreadId,
                message.UserId,
                message.Subject,
                message.From,
                message.To,
                message.Cc,
                message.Snippet,
                message.Body,
                message.LabelIds,
                message.IsRead,
                message.IsStarred,
                message.InternalDate,
                message.SizeEstimate
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<GmailMessage?> GetMessageByIdAsync(string messageId, string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GmailMessage>(
            "sp_Workspace_Gmail_GetMessageById",
            new { MessageId = messageId, UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveLabelAsync(GmailLabel label)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Gmail_SaveLabel",
            new
            {
                label.LabelId,
                label.UserId,
                label.Name,
                label.Type,
                label.MessageListVisibility,
                label.LabelListVisibility,
                label.MessagesTotal,
                label.MessagesUnread,
                label.ThreadsTotal,
                label.ThreadsUnread
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<GmailLabel>> GetLabelsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<GmailLabel>(
            "sp_Workspace_Gmail_GetLabelsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

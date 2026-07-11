using ApiToolKit.Domain.Entities.Workspace.Chat;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class ChatRepository : IChatRepository
{
    private readonly DapperContext _context;

    public ChatRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveMessageAsync(ChatMessage message)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Chat_SaveMessage",
            new
            {
                message.MessageName,
                message.SpaceName,
                message.UserId,
                message.Text,
                message.CreateTime,
                message.LastUpdateTime,
                message.FormattedText
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<ChatMessage>> GetMessagesBySpaceAsync(string spaceName)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<ChatMessage>(
            "sp_Workspace_Chat_GetMessagesBySpace",
            new { SpaceName = spaceName },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

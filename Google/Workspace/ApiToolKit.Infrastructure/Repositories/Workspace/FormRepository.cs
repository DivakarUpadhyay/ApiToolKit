using ApiToolKit.Domain.Entities.Workspace.Forms;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class FormRepository : IFormRepository
{
    private readonly DapperContext _context;

    public FormRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveFormAsync(FormInfo form)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Forms_SaveForm",
            new
            {
                form.FormId,
                form.UserId,
                form.Title,
                form.DocumentTitle,
                form.RevisionId,
                form.ResponderUri
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveResponseAsync(FormResponse response)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Forms_SaveResponse",
            new
            {
                response.ResponseId,
                response.FormId,
                response.UserId,
                response.CreateTime,
                response.LastSubmittedTime,
                response.RespondentEmail,
                response.TotalScore,
                response.Answers
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<FormInfo>> GetFormsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<FormInfo>(
            "sp_Workspace_Forms_GetFormsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

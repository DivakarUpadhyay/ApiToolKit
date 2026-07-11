using ApiToolKit.Domain.Entities.Workspace.Docs;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class DocumentRepository : IDocumentRepository
{
    private readonly DapperContext _context;

    public DocumentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveDocumentAsync(DocumentInfo document)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Docs_SaveDocument",
            new
            {
                document.DocumentId,
                document.UserId,
                document.Title,
                document.DocumentUrl,
                document.RevisionId
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<DocumentInfo>> GetDocumentsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<DocumentInfo>(
            "sp_Workspace_Docs_GetDocumentsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

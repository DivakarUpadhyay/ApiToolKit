using ApiToolKit.Domain.Entities.Workspace.Docs;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IDocumentRepository
{
    Task SaveDocumentAsync(DocumentInfo document);
    Task<List<DocumentInfo>> GetDocumentsByUserIdAsync(string userId);
}

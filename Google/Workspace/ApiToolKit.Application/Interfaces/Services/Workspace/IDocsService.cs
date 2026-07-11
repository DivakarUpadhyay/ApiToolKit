using ApiToolKit.Application.DTOs.Workspace.Docs;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IDocsService
{
    Task<Result<DocumentResponse>> CreateDocumentAsync(string userId, CreateDocumentRequest request);
    Task<Result<DocumentResponse>> GetDocumentAsync(string userId, GetDocumentRequest request);
    Task<Result<DocumentResponse>> InsertTextAsync(string userId, InsertTextRequest request);
    Task<Result<DocumentResponse>> ReplaceTextAsync(string userId, ReplaceTextRequest request);
    Task<Result<DocumentResponse>> InsertTableAsync(string userId, InsertTableRequest request);
    Task<Result<DocumentResponse>> DeleteContentRangeAsync(string userId, DeleteContentRangeRequest request);
    Task<Result<DocumentResponse>> InsertPageBreakAsync(string userId, InsertPageBreakRequest request);
    Task<Result<DocumentResponse>> ApplyParagraphStyleAsync(string userId, ParagraphStyleRequest request);
}

using ApiToolKit.Application.DTOs.Search.KnowledgeGraph;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Search;

public interface IKnowledgeGraphService
{
    Task<Result<KnowledgeGraphResponse>> SearchAsync(KnowledgeGraphRequest request);
}

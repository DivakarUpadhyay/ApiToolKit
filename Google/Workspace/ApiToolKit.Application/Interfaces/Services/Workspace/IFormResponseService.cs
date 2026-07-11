using ApiToolKit.Application.DTOs.Workspace.Forms;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IFormResponseService
{
    Task<Result<ListFormResponsesResponse>> ListResponsesAsync(string userId, ListFormResponsesRequest request);
    Task<Result<FormResponseDto>> GetResponseAsync(string userId, string formId, string responseId);
}

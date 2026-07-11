using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IGmailLabelService
{
    Task<Result<ListLabelsResponse>> ListLabelsAsync(string userId);
    Task<Result<LabelResponse>> GetLabelAsync(string userId, string labelId);
    Task<Result<LabelResponse>> CreateLabelAsync(string userId, CreateLabelRequest request);
    Task<Result<LabelResponse>> UpdateLabelAsync(string userId, UpdateLabelRequest request);
    Task<Result<bool>> DeleteLabelAsync(string userId, DeleteLabelRequest request);
}

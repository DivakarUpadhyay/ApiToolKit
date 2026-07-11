using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IMeetSpaceService
{
    Task<Result<SpaceResponse>> CreateSpaceAsync(string userId, CreateSpaceRequest request);
    Task<Result<SpaceResponse>> GetSpaceAsync(string userId, string spaceName);
    Task<Result<bool>> EndSpaceAsync(string userId, EndSpaceRequest request);
}

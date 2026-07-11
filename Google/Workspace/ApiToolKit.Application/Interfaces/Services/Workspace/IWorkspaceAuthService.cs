using ApiToolKit.Application.DTOs.Workspace.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IWorkspaceAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string[] scopes, string state = "");
    Task<Result<WorkspaceTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<WorkspaceTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetAccessTokenAsync(string userId);
}

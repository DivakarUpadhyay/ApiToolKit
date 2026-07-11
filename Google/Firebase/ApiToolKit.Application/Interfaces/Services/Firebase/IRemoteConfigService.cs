using ApiToolKit.Application.DTOs.Firebase.RemoteConfig;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Firebase;

public interface IRemoteConfigService
{
    Task<Result<GetRemoteConfigResponse>> GetRemoteConfigAsync(GetRemoteConfigRequest request);
    Task<Result<GetRemoteConfigResponse>> UpdateRemoteConfigAsync(UpdateRemoteConfigRequest request);
    Task<Result<object>> ListVersionsAsync(ListRemoteConfigVersionsRequest request);
    Task<Result<GetRemoteConfigResponse>> RollbackAsync(RollbackRemoteConfigRequest request);
}

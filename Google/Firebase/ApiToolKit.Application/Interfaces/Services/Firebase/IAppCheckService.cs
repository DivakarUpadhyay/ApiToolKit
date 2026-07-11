using ApiToolKit.Application.DTOs.Firebase.AppCheck;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Firebase;

public interface IAppCheckService
{
    Task<Result<VerifyAppCheckTokenResponse>> VerifyTokenAsync(VerifyAppCheckTokenRequest request);
    Task<Result<List<AppCheckConfig>>> BatchGetConfigsAsync(BatchGetAppCheckConfigsRequest request);
    Task<Result<List<AppCheckConfig>>> BatchUpdateConfigsAsync(BatchUpdateAppCheckConfigsRequest request);
}

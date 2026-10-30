using ApiToolKit.Application.DTOs.TagManager.Environment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerEnvironmentService
{
    Task<Result<object>> ListEnvironmentsAsync(ListEnvironmentsRequest request);
    Task<Result<object>> GetEnvironmentAsync(GetEnvironmentRequest request);
    Task<Result<object>> CreateEnvironmentAsync(CreateEnvironmentRequest request);
    Task<Result<object>> UpdateEnvironmentAsync(UpdateEnvironmentRequest request);
    Task<Result<object>> DeleteEnvironmentAsync(DeleteEnvironmentRequest request);
    Task<Result<object>> ReauthorizeEnvironmentAsync(ReauthorizeEnvironmentRequest request);
}

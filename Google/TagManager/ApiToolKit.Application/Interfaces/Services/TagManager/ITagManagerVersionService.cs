using ApiToolKit.Application.DTOs.TagManager.Version;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerVersionService
{
    Task<Result<object>> GetLiveVersionAsync(GetLiveVersionRequest request);
    Task<Result<object>> ListVersionHeadersAsync(ListVersionHeadersRequest request);
    Task<Result<object>> GetLatestVersionHeaderAsync(GetLatestVersionHeaderRequest request);
    Task<Result<object>> GetVersionAsync(GetVersionRequest request);
    Task<Result<object>> PublishVersionAsync(PublishVersionRequest request);
    Task<Result<object>> DeleteVersionAsync(DeleteVersionRequest request);
    Task<Result<object>> UpdateVersionAsync(UpdateVersionRequest request);
    Task<Result<object>> SetLatestVersionAsync(SetLatestVersionRequest request);
    Task<Result<object>> UndeleteVersionAsync(UndeleteVersionRequest request);
}

using ApiToolKit.Application.DTOs.TagManager.Tag;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerTagService
{
    Task<Result<object>> ListTagsAsync(ListTagsRequest request);
    Task<Result<object>> GetTagAsync(GetTagRequest request);
    Task<Result<object>> CreateTagAsync(CreateTagRequest request);
    Task<Result<object>> UpdateTagAsync(UpdateTagRequest request);
    Task<Result<object>> DeleteTagAsync(DeleteTagRequest request);
    Task<Result<object>> RevertTagAsync(RevertTagRequest request);
}

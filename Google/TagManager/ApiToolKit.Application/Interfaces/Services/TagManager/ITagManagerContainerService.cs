using ApiToolKit.Application.DTOs.TagManager.Container;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerContainerService
{
    Task<Result<object>> ListContainersAsync(ListContainersRequest request);
    Task<Result<object>> GetContainerAsync(GetContainerRequest request);
    Task<Result<object>> CreateContainerAsync(CreateContainerRequest request);
    Task<Result<object>> UpdateContainerAsync(UpdateContainerRequest request);
    Task<Result<object>> DeleteContainerAsync(DeleteContainerRequest request);
    Task<Result<object>> GetContainerSnippetAsync(GetContainerSnippetRequest request);
    Task<Result<object>> LookupContainerAsync(LookupContainerRequest request);
    Task<Result<object>> CombineContainersAsync(CombineContainersRequest request);
    Task<Result<object>> MoveTagIdAsync(MoveTagIdRequest request);
}

using ApiToolKit.Application.DTOs.TagManager.Folder;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerFolderService
{
    Task<Result<object>> ListFoldersAsync(ListFoldersRequest request);
    Task<Result<object>> GetFolderAsync(GetFolderRequest request);
    Task<Result<object>> CreateFolderAsync(CreateFolderRequest request);
    Task<Result<object>> UpdateFolderAsync(UpdateFolderRequest request);
    Task<Result<object>> DeleteFolderAsync(DeleteFolderRequest request);
    Task<Result<object>> RevertFolderAsync(RevertFolderRequest request);
    Task<Result<object>> GetFolderEntitiesAsync(GetFolderEntitiesRequest request);
    Task<Result<object>> MoveEntitiesToFolderAsync(MoveEntitiesToFolderRequest request);
}

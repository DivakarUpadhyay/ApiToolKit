using ApiToolKit.Domain.Entities.Workspace.Drive;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IDriveRepository
{
    Task SaveFileAsync(DriveFile file);
    Task UpdateFileAsync(DriveFile file);
    Task DeleteFileAsync(string fileId, string userId);
    Task<List<DriveFile>> GetFilesByUserIdAsync(string userId);
    Task SavePermissionAsync(DrivePermission permission);
}

using ApiToolKit.Domain.Entities.Workspace.Drive;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class DriveRepository : IDriveRepository
{
    private readonly DapperContext _context;

    public DriveRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveFileAsync(DriveFile file)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Drive_SaveFile",
            new
            {
                file.FileId,
                file.UserId,
                file.Name,
                file.MimeType,
                file.Description,
                file.Size,
                file.ModifiedTime,
                file.CreatedTime,
                file.WebViewLink,
                file.WebContentLink,
                file.Parents,
                file.IsShared,
                file.IsStarred,
                file.IsTrashed,
                file.Version
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateFileAsync(DriveFile file)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Drive_UpdateFile",
            new
            {
                file.FileId,
                file.UserId,
                file.Name,
                file.Description,
                file.IsStarred,
                file.IsTrashed,
                file.Version
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteFileAsync(string fileId, string userId)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Drive_DeleteFile",
            new { FileId = fileId, UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<DriveFile>> GetFilesByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<DriveFile>(
            "sp_Workspace_Drive_GetFilesByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task SavePermissionAsync(DrivePermission permission)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Drive_SavePermission",
            new
            {
                permission.PermissionId,
                permission.FileId,
                permission.UserId,
                permission.Type,
                permission.Role,
                permission.EmailAddress,
                permission.DisplayName,
                permission.ExpirationTime
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

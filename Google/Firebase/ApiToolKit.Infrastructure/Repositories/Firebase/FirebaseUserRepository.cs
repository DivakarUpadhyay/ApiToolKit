using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Firebase;

public class FirebaseUserRepository : IFirebaseUserRepository
{
    private readonly DapperContext _context;

    public FirebaseUserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveUserAsync(FirebaseUser user)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_SaveUser",
            new
            {
                user.Uid,
                user.ProjectId,
                user.Email,
                user.EmailVerified,
                user.DisplayName,
                user.PhotoUrl,
                user.PhoneNumber,
                user.Disabled,
                user.CreationTime,
                user.LastSignInTime,
                user.ProviderData
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateUserAsync(FirebaseUser user)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_UpdateUser",
            new
            {
                user.Uid,
                user.ProjectId,
                user.Email,
                user.EmailVerified,
                user.DisplayName,
                user.PhotoUrl,
                user.PhoneNumber,
                user.Disabled
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteUserAsync(string uid, string projectId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_DeleteUser",
            new { Uid = uid, ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<FirebaseUser>> GetUsersByProjectIdAsync(string projectId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<FirebaseUser>(
            "sp_Firebase_GetUsersByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

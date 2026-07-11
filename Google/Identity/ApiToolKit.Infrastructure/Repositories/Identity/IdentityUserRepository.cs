using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Identity;

public class IdentityUserRepository : IIdentityUserRepository
{
    private readonly DapperContext _context;

    public IdentityUserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveUserAsync(IdentityUser user)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_SaveUser",
            new
            {
                user.LocalId,
                user.UserId,
                user.Email,
                user.EmailVerified,
                user.DisplayName,
                user.PhotoUrl,
                user.PhoneNumber,
                user.ProviderUserInfo,
                user.CreatedAt,
                user.LastLoginAt,
                user.LastRefreshAt,
                user.Disabled
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateUserAsync(IdentityUser user)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_UpdateUser",
            new
            {
                user.LocalId,
                user.Email,
                user.EmailVerified,
                user.DisplayName,
                user.PhotoUrl,
                user.PhoneNumber,
                user.Disabled
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteUserAsync(string localId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_DeleteUser",
            new { LocalId = localId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IdentityUser?> GetUserByEmailAsync(string email)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<IdentityUser>(
            "sp_Identity_GetUserByEmail",
            new { Email = email },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}

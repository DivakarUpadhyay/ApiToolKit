using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Identity;

public class ContactGroupRepository : IContactGroupRepository
{
    private readonly DapperContext _context;

    public ContactGroupRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveGroupAsync(ContactGroup group)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_SaveContactGroup",
            new
            {
                group.GroupResourceName,
                group.UserId,
                group.Name,
                group.FormattedName,
                group.GroupType,
                group.MemberCount
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteGroupAsync(string resourceName, string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_DeleteContactGroup",
            new { ResourceName = resourceName, UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<ContactGroup>> GetGroupsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<ContactGroup>(
            "sp_Identity_GetContactGroupsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

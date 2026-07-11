using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Identity;

public class ContactRepository : IContactRepository
{
    private readonly DapperContext _context;

    public ContactRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveContactAsync(ContactPerson contact)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_SaveContact",
            new
            {
                contact.ResourceName,
                contact.UserId,
                contact.DisplayName,
                contact.GivenName,
                contact.FamilyName,
                contact.Email,
                contact.PhoneNumber,
                contact.PhotoUrl,
                contact.Organization,
                contact.JobTitle,
                contact.Notes,
                contact.Etag
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateContactAsync(ContactPerson contact)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_UpdateContact",
            new
            {
                contact.ResourceName,
                contact.UserId,
                contact.DisplayName,
                contact.GivenName,
                contact.FamilyName,
                contact.Email,
                contact.PhoneNumber,
                contact.PhotoUrl,
                contact.Organization,
                contact.JobTitle,
                contact.Notes,
                contact.Etag
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteContactAsync(string resourceName, string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_DeleteContact",
            new { ResourceName = resourceName, UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<ContactPerson>> GetContactsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<ContactPerson>(
            "sp_Identity_GetContactsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}

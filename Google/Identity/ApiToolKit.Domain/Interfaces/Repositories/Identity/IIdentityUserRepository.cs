using ApiToolKit.Domain.Entities.Identity;

namespace ApiToolKit.Domain.Interfaces.Repositories.Identity;

public interface IIdentityUserRepository
{
    Task<int> SaveUserAsync(IdentityUser user);
    Task<int> UpdateUserAsync(IdentityUser user);
    Task<int> DeleteUserAsync(string localId);
    Task<IdentityUser?> GetUserByEmailAsync(string email);
}

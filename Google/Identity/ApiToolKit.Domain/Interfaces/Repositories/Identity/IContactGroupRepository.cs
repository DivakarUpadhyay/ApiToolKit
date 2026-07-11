using ApiToolKit.Domain.Entities.Identity;

namespace ApiToolKit.Domain.Interfaces.Repositories.Identity;

public interface IContactGroupRepository
{
    Task<int> SaveGroupAsync(ContactGroup group);
    Task<int> DeleteGroupAsync(string resourceName, string userId);
    Task<List<ContactGroup>> GetGroupsByUserIdAsync(string userId);
}

using ApiToolKit.Domain.Entities.Identity;

namespace ApiToolKit.Domain.Interfaces.Repositories.Identity;

public interface IContactRepository
{
    Task<int> SaveContactAsync(ContactPerson contact);
    Task<int> UpdateContactAsync(ContactPerson contact);
    Task<int> DeleteContactAsync(string resourceName, string userId);
    Task<List<ContactPerson>> GetContactsByUserIdAsync(string userId);
}

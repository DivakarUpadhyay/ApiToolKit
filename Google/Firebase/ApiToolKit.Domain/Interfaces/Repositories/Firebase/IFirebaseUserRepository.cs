using ApiToolKit.Domain.Entities.Firebase;

namespace ApiToolKit.Domain.Interfaces.Repositories.Firebase;

public interface IFirebaseUserRepository
{
    Task<int> SaveUserAsync(FirebaseUser user);
    Task<int> UpdateUserAsync(FirebaseUser user);
    Task<int> DeleteUserAsync(string uid, string projectId);
    Task<List<FirebaseUser>> GetUsersByProjectIdAsync(string projectId);
}

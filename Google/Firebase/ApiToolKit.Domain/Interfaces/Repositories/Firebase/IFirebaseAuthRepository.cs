using ApiToolKit.Domain.Entities.Firebase;

namespace ApiToolKit.Domain.Interfaces.Repositories.Firebase;

public interface IFirebaseAuthRepository
{
    Task<FirebaseConfig?> GetConfigAsync();
    Task<FirebaseToken?> GetTokenByProjectIdAsync(string projectId);
    Task<int> SaveTokenAsync(FirebaseToken token);
    Task<int> UpdateTokenAsync(FirebaseToken token);
    Task<int> SaveApiLogAsync(FirebaseApiLog log);
}

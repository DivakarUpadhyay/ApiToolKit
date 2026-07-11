using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookCatalogRepository
{
    Task<int> SaveCatalogAsync(FacebookCatalog catalog);
    Task<FacebookCatalog?> GetCatalogByIdAsync(string catalogId);
    Task<IEnumerable<FacebookCatalog>> GetCatalogsByBusinessIdAsync(string businessId);
    Task<int> DeleteCatalogAsync(string catalogId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}

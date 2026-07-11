using ApiToolKit.Domain.Entities.Maps;

namespace ApiToolKit.Domain.Interfaces.Repositories.Maps;

public interface IMapsConfigRepository
{
    Task<MapsConfig?> GetConfigAsync();
}

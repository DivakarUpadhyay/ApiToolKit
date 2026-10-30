using ApiToolKit.Domain.Entities.CampaignManager;

namespace ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

public interface ICampaignManagerAuthRepository
{
    Task<CampaignManagerToken?> GetTokenAsync(string accountKey);
    Task SaveTokenAsync(CampaignManagerToken token);
    Task SaveApiLogAsync(CampaignManagerApiLog log);
}

using ApiToolKit.Application.DTOs.CampaignManager.Language;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerLanguageService
{
    Task<Result<object>> ListLanguagesAsync(ListLanguagesRequest request);
}

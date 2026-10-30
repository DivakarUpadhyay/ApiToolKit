using ApiToolKit.Application.DTOs.Merchant.AutomaticImprovement;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantAutomaticImprovementService
{
    Task<Result<object>> GetAutomaticImprovementsAsync(GetAutomaticImprovementsRequest request);
    Task<Result<object>> UpdateAutomaticImprovementsAsync(UpdateAutomaticImprovementsRequest request);
}

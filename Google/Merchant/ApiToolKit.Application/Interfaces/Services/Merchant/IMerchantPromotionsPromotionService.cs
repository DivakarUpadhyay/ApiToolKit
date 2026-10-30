using ApiToolKit.Application.DTOs.Merchant.PromotionsPromotion;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantPromotionsPromotionService
{
    Task<Result<object>> ListPromotionsAsync(ListPromotionsRequest request);
    Task<Result<object>> GetPromotionAsync(GetPromotionRequest request);
    Task<Result<object>> InsertPromotionAsync(InsertPromotionRequest request);
}

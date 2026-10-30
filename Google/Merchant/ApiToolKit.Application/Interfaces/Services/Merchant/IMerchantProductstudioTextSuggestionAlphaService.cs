using ApiToolKit.Application.DTOs.Merchant.ProductstudioTextSuggestionAlpha;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantProductstudioTextSuggestionAlphaService
{
    Task<Result<object>> GenerateProductTextSuggestionsAsync(GenerateProductTextSuggestionsRequest request);
}

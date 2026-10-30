using ApiToolKit.Application.DTOs.Merchant.ProductstudioTextSuggestionAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantProductstudioTextSuggestionAlphaService : MerchantServiceBase, IMerchantProductstudioTextSuggestionAlphaService
{
    public MerchantProductstudioTextSuggestionAlphaService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GenerateProductTextSuggestionsAsync(GenerateProductTextSuggestionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/productstudio/v1alpha/accounts/{Esc(r.AccountId)}:generateProductTextSuggestions";
        return await SendAsync(r.AccountKey, "GenerateProductTextSuggestions", HttpMethod.Post, url, null, r.Body);
    }
}

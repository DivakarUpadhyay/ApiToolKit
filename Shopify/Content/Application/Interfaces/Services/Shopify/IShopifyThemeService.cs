using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyThemeService
{
    Task<Result<ListThemesResponse>> ListThemesAsync(string shopDomain);
    Task<Result<ThemeResponse>> GetThemeAsync(string shopDomain, string themeId);
    Task<Result<ThemeResponse>> CreateThemeAsync(string shopDomain, CreateThemeRequest request);
    Task<Result<ThemeResponse>> UpdateThemeAsync(string shopDomain, string themeId, UpdateThemeRequest request);
    Task<Result<bool>> DeleteThemeAsync(string shopDomain, string themeId);
}

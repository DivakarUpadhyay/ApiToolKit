using ApiToolKit.Application.DTOs.Facebook.Pixel;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookPixelService {
    Task<Result<MetaPixel>> CreatePixelAsync(string adAccountId, CreatePixelRequest request, string accessToken);
    Task<Result<PixelsResponse>> GetPixelsAsync(string adAccountId, string accessToken);
    Task<Result<MetaPixel>> GetPixelAsync(string pixelId, string accessToken);
    Task<Result<bool>> UpdatePixelAsync(string pixelId, UpdatePixelRequest request, string accessToken);
    Task<Result<PixelStatsResponse>> GetPixelStatsAsync(string pixelId, string accessToken, string? startTime = null, string? endTime = null);
    Task<Result<PixelAudiencesResponse>> GetPixelAudiencesAsync(string pixelId, string accessToken);
    Task<Result<bool>> SharePixelAsync(string pixelId, SharePixelRequest request, string accessToken);
    Task<Result<dynamic>> GetPixelDiagnosticsAsync(string pixelId, string accessToken);
}

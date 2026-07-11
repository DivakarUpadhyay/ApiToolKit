using ApiToolKit.Application.DTOs.YouTube.Caption;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeCaptionService
{
    Task<Result<List<CaptionResponse>>> ListCaptionsAsync(ListCaptionsRequest request, string accessToken);
    Task<Result<CaptionResponse>> CreateCaptionAsync(CreateCaptionRequest request, string accessToken);
    Task<Result<CaptionResponse>> UpdateCaptionAsync(UpdateCaptionRequest request, string accessToken);
    Task<Result<bool>> DeleteCaptionAsync(DeleteCaptionRequest request, string accessToken);
}

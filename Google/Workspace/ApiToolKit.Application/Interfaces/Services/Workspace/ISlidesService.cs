using ApiToolKit.Application.DTOs.Workspace.Slides;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface ISlidesService
{
    Task<Result<PresentationResponse>> CreatePresentationAsync(string userId, CreatePresentationRequest request);
    Task<Result<PresentationResponse>> GetPresentationAsync(string userId, GetPresentationRequest request);
    Task<Result<PresentationResponse>> AddSlideAsync(string userId, AddSlideRequest request);
    Task<Result<PresentationResponse>> DeleteSlideAsync(string userId, DeleteSlideRequest request);
    Task<Result<PresentationResponse>> InsertTextAsync(string userId, SlidesInsertTextRequest request);
    Task<Result<PresentationResponse>> ReplaceTextAsync(string userId, SlidesReplaceTextRequest request);
    Task<Result<PresentationResponse>> InsertImageAsync(string userId, InsertImageRequest request);
    Task<Result<PresentationResponse>> DuplicateSlideAsync(string userId, DuplicateSlideRequest request);
    Task<Result<PresentationResponse>> ReorderSlidesAsync(string userId, ReorderSlidesRequest request);
}

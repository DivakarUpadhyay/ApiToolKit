using ApiToolKit.Application.DTOs.TagManager.Transformation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerTransformationService
{
    Task<Result<object>> ListTransformationsAsync(ListTransformationsRequest request);
    Task<Result<object>> GetTransformationAsync(GetTransformationRequest request);
    Task<Result<object>> CreateTransformationAsync(CreateTransformationRequest request);
    Task<Result<object>> UpdateTransformationAsync(UpdateTransformationRequest request);
    Task<Result<object>> DeleteTransformationAsync(DeleteTransformationRequest request);
    Task<Result<object>> RevertTransformationAsync(RevertTransformationRequest request);
}

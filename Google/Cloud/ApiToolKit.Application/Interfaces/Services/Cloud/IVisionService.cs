using ApiToolKit.Application.DTOs.Cloud.Shared;
using ApiToolKit.Application.DTOs.Cloud.Vision;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Cloud;

public interface IVisionService
{
    Task<Result<ImageAnnotationResponse>> AnnotateImageAsync(AnnotateImageRequest request);
    Task<Result<BatchAnnotateImagesResponse>> BatchAnnotateImagesAsync(BatchAnnotateImagesRequest request);
    Task<Result<OperationResponse>> AsyncAnnotateFileAsync(AsyncAnnotateFileRequest request);
    Task<Result<OperationResponse>> GetOperationAsync(GetOperationRequest request);
}

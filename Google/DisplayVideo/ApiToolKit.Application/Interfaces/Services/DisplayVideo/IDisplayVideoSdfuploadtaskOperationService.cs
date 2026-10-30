using ApiToolKit.Application.DTOs.DisplayVideo.SdfuploadtaskOperation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoSdfuploadtaskOperationService
{
    Task<Result<object>> GetOperationAsync(GetOperationRequest request);
}

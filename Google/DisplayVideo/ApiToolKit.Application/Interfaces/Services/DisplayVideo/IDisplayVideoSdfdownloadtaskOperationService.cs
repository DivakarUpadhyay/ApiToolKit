using ApiToolKit.Application.DTOs.DisplayVideo.SdfdownloadtaskOperation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoSdfdownloadtaskOperationService
{
    Task<Result<object>> GetOperationAsync(GetOperationRequest request);
}

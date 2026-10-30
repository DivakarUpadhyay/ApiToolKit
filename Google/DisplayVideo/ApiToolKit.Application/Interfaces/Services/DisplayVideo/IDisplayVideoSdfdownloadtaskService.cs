using ApiToolKit.Application.DTOs.DisplayVideo.Sdfdownloadtask;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoSdfdownloadtaskService
{
    Task<Result<object>> CreateSdfdownloadtaskAsync(CreateSdfdownloadtaskRequest request);
}

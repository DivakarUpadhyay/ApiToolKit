using ApiToolKit.Application.DTOs.CampaignManager.ReportFile;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerReportFileService
{
    Task<Result<object>> ListFilesAsync(ListFilesRequest request);
    Task<Result<object>> GetFileAsync(GetFileRequest request);
    Task<Result<object>> GetFileContentAsync(GetFileContentRequest request);
}

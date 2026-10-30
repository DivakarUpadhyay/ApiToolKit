using ApiToolKit.Application.DTOs.CampaignManager.ChangeLog;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerChangeLogService
{
    Task<Result<object>> GetChangeLogAsync(GetChangeLogRequest request);
    Task<Result<object>> ListChangeLogsAsync(ListChangeLogsRequest request);
}

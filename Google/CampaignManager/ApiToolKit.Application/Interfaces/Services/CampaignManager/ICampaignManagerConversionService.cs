using ApiToolKit.Application.DTOs.CampaignManager.Conversion;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerConversionService
{
    Task<Result<object>> BatchupdateConversionAsync(BatchupdateConversionRequest request);
    Task<Result<object>> BatchinsertConversionAsync(BatchinsertConversionRequest request);
}

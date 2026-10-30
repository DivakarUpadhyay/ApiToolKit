using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserInvoice;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAdvertiserInvoiceService
{
    Task<Result<object>> ListAdvertiserInvoicesAsync(ListAdvertiserInvoicesRequest request);
}

using ApiToolKit.Application.DTOs.DisplayVideo.Invoice;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoInvoiceService
{
    Task<Result<object>> LookupInvoiceCurrencyAsync(LookupInvoiceCurrencyRequest request);
    Task<Result<object>> ListInvoicesAsync(ListInvoicesRequest request);
}

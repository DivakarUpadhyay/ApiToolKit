using ApiToolKit.Application.DTOs.SearchConsole.Indexing;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleIndexingService
{
    Task<Result<object>> PublishUrlNotificationAsync(PublishUrlNotificationRequest request);
    Task<Result<object>> GetUrlNotificationMetadataAsync(GetUrlNotificationMetadataRequest request);
}

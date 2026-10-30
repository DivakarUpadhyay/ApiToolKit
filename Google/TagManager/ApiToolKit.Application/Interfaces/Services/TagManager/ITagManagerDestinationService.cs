using ApiToolKit.Application.DTOs.TagManager.Destination;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerDestinationService
{
    Task<Result<object>> ListDestinationsAsync(ListDestinationsRequest request);
    Task<Result<object>> GetDestinationAsync(GetDestinationRequest request);
    Task<Result<object>> LinkDestinationAsync(LinkDestinationRequest request);
}

using ApiToolKit.Application.DTOs.DisplayVideo.CustomList;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCustomListService
{
    Task<Result<object>> ListCustomListsAsync(ListCustomListsRequest request);
    Task<Result<object>> GetCustomListAsync(GetCustomListRequest request);
}

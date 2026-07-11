using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface ICalendarFreeBusyService
{
    Task<Result<FreeBusyResponse>> QueryFreeBusyAsync(string userId, FreeBusyRequest request);
}

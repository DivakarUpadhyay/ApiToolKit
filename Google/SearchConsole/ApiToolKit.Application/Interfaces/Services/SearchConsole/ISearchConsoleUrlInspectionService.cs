using ApiToolKit.Application.DTOs.SearchConsole.UrlInspection;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleUrlInspectionService
{
    Task<Result<object>> InspectUrlAsync(InspectUrlRequest request);
}

using ApiToolKit.Application.DTOs.BigQuery.Routine;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BigQuery;

public interface IBigQueryRoutineService
{
    Task<Result<ListRoutinesResponse>> ListRoutinesAsync(ListRoutinesRequest request, string accessToken);
    Task<Result<RoutineResponse>> CreateRoutineAsync(CreateRoutineRequest request, string accessToken);
    Task<Result<RoutineResponse>> GetRoutineAsync(string projectId, string datasetId, string routineId, string accessToken);
    Task<Result<bool>> DeleteRoutineAsync(DeleteRoutineRequest request, string accessToken);
}

using ApiToolKit.Application.DTOs.BigQuery.Routine;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.BigQuery;

public class BigQueryRoutineService : IBigQueryRoutineService
{
    private readonly IBigQueryApiClient _apiClient;
    private const string BaseUrl = "https://bigquery.googleapis.com/bigquery/v2";

    public BigQueryRoutineService(IBigQueryApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<ListRoutinesResponse>> ListRoutinesAsync(ListRoutinesRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}" +
                  $"/datasets/{Uri.EscapeDataString(request.DatasetId)}/routines" +
                  $"?maxResults={request.MaxResults}";

        if (!string.IsNullOrEmpty(request.PageToken))
            url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.Filter))
            url += $"&filter={Uri.EscapeDataString(request.Filter)}";

        var response = await _apiClient.GetAsync<ListRoutinesResponse>(url, accessToken);
        if (response == null)
            return Result<ListRoutinesResponse>.Fail("No response from BigQuery API.");

        return Result<ListRoutinesResponse>.Ok(response);
    }

    public async Task<Result<RoutineResponse>> CreateRoutineAsync(CreateRoutineRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}" +
                  $"/datasets/{Uri.EscapeDataString(request.DatasetId)}/routines";

        var body = new
        {
            routineReference = new
            {
                projectId = request.ProjectId,
                datasetId = request.DatasetId,
                routineId = request.RoutineId
            },
            routineType = request.RoutineType,
            language = request.Language,
            arguments = request.Arguments,
            returnType = request.ReturnType,
            definitionBody = request.DefinitionBody,
            description = request.Description
        };

        var response = await _apiClient.PostAsync<RoutineResponse>(url, body, accessToken);
        if (response == null)
            return Result<RoutineResponse>.Fail("Failed to create routine.");

        return Result<RoutineResponse>.Ok(response);
    }

    public async Task<Result<RoutineResponse>> GetRoutineAsync(string projectId, string datasetId, string routineId, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(projectId)}" +
                  $"/datasets/{Uri.EscapeDataString(datasetId)}/routines/{Uri.EscapeDataString(routineId)}";

        var response = await _apiClient.GetAsync<RoutineResponse>(url, accessToken);
        if (response == null)
            return Result<RoutineResponse>.Fail("Routine not found or no response from BigQuery API.");

        return Result<RoutineResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteRoutineAsync(DeleteRoutineRequest request, string accessToken)
    {
        var url = $"{BaseUrl}/projects/{Uri.EscapeDataString(request.ProjectId)}" +
                  $"/datasets/{Uri.EscapeDataString(request.DatasetId)}/routines/{Uri.EscapeDataString(request.RoutineId)}";

        await _apiClient.DeleteAsync(url, accessToken);

        return Result<bool>.Ok(true, "Routine deleted successfully.");
    }
}

using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IMeetRecordingService
{
    Task<Result<ListRecordingsResponse>> ListRecordingsAsync(string userId, ListRecordingsRequest request);
    Task<Result<RecordingResponse>> GetRecordingAsync(string userId, string recordingName);
    Task<Result<ListConferenceRecordsResponse>> ListConferenceRecordsAsync(string userId, ListConferenceRecordsRequest request);
    Task<Result<ConferenceRecordResponse>> GetConferenceRecordAsync(string userId, string conferenceRecordName);
}

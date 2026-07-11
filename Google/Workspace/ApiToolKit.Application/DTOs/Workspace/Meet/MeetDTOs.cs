using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Meet;

public class SpaceConfig
{
    public string? AccessType { get; set; }
    public string? EntryPointAccess { get; set; }
}

public class ActiveConference
{
    public string? ConferenceRecord { get; set; }
    public DateTime? ExpireTime { get; set; }
}

public class SpaceResponse
{
    public string? Name { get; set; }
    public string? MeetingUri { get; set; }
    public string? MeetingCode { get; set; }
    public SpaceConfig? Config { get; set; }
    public ActiveConference? ActiveConference { get; set; }
}

public class ListSpacesResponse
{
    public List<SpaceResponse>? Spaces { get; set; }
    public string? NextPageToken { get; set; }
}

public class CreateSpaceRequest
{
    public string Config_AccessType { get; set; } = "OPEN";
    public string InitialConfig_EntryPointAccess { get; set; } = "ALL";
}

public class EndSpaceRequest
{
    [Required]
    public string SpaceName { get; set; } = string.Empty;
}

public class DriveDestination
{
    public string? File { get; set; }
    public string? ExportUri { get; set; }
}

public class RecordingResponse
{
    public string? Name { get; set; }
    public string? State { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DriveDestination? DriveDestination { get; set; }
}

public class ListRecordingsRequest
{
    [Required]
    public string ConferenceRecord { get; set; } = string.Empty;
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
}

public class ListRecordingsResponse
{
    public List<RecordingResponse>? Recordings { get; set; }
    public string? NextPageToken { get; set; }
}

public class ParticipantResponse
{
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public DateTime? EarliestStartTime { get; set; }
    public DateTime? LatestEndTime { get; set; }
}

public class ListParticipantsRequest
{
    [Required]
    public string ConferenceRecord { get; set; } = string.Empty;
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
}

public class ListParticipantsResponse
{
    public List<ParticipantResponse>? Participants { get; set; }
    public string? NextPageToken { get; set; }
}

public class ConferenceRecordResponse
{
    public string? Name { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Space { get; set; }
    public DateTime? ExpireTime { get; set; }
}

public class ListConferenceRecordsRequest
{
    public string? Filter { get; set; }
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
}

public class ListConferenceRecordsResponse
{
    public List<ConferenceRecordResponse>? ConferenceRecords { get; set; }
    public string? NextPageToken { get; set; }
}

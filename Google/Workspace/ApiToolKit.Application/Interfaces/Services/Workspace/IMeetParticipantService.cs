using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IMeetParticipantService
{
    Task<Result<ListParticipantsResponse>> ListParticipantsAsync(string userId, ListParticipantsRequest request);
    Task<Result<ParticipantResponse>> GetParticipantAsync(string userId, string participantName);
}

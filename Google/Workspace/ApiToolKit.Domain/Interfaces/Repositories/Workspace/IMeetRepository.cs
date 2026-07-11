using ApiToolKit.Domain.Entities.Workspace.Meet;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IMeetRepository
{
    Task SaveSpaceAsync(MeetSpace space);
    Task<List<MeetSpace>> GetSpacesByUserIdAsync(string userId);
}

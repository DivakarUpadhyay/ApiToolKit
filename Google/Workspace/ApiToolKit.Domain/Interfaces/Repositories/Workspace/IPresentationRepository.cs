using ApiToolKit.Domain.Entities.Workspace.Slides;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IPresentationRepository
{
    Task SavePresentationAsync(PresentationInfo presentation);
    Task<List<PresentationInfo>> GetPresentationsByUserIdAsync(string userId);
}

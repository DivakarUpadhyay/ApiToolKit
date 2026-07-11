using ApiToolKit.Domain.Entities.Workspace.Forms;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IFormRepository
{
    Task SaveFormAsync(FormInfo form);
    Task SaveResponseAsync(FormResponse response);
    Task<List<FormInfo>> GetFormsByUserIdAsync(string userId);
}

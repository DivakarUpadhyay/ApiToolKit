using ApiToolKit.Application.DTOs.TagManager.Client;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerClientService
{
    Task<Result<object>> ListClientsAsync(ListClientsRequest request);
    Task<Result<object>> GetClientAsync(GetClientRequest request);
    Task<Result<object>> CreateClientAsync(CreateClientRequest request);
    Task<Result<object>> UpdateClientAsync(UpdateClientRequest request);
    Task<Result<object>> DeleteClientAsync(DeleteClientRequest request);
    Task<Result<object>> RevertClientAsync(RevertClientRequest request);
}

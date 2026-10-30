using ApiToolKit.Application.DTOs.TagManager.Variable;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerVariableService
{
    Task<Result<object>> ListVariablesAsync(ListVariablesRequest request);
    Task<Result<object>> GetVariableAsync(GetVariableRequest request);
    Task<Result<object>> CreateVariableAsync(CreateVariableRequest request);
    Task<Result<object>> UpdateVariableAsync(UpdateVariableRequest request);
    Task<Result<object>> DeleteVariableAsync(DeleteVariableRequest request);
    Task<Result<object>> RevertVariableAsync(RevertVariableRequest request);
}

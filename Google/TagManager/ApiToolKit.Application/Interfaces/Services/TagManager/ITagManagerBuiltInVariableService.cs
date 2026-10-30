using ApiToolKit.Application.DTOs.TagManager.BuiltInVariable;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerBuiltInVariableService
{
    Task<Result<object>> ListBuiltInVariablesAsync(ListBuiltInVariablesRequest request);
    Task<Result<object>> EnableBuiltInVariablesAsync(EnableBuiltInVariablesRequest request);
    Task<Result<object>> DisableBuiltInVariablesAsync(DisableBuiltInVariablesRequest request);
    Task<Result<object>> RevertBuiltInVariableAsync(RevertBuiltInVariableRequest request);
}

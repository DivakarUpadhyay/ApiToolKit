using ApiToolKit.Application.DTOs.Workspace.Forms;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IFormsService
{
    Task<Result<FormResponse2>> CreateFormAsync(string userId, CreateFormRequest request);
    Task<Result<FormResponse2>> GetFormAsync(string userId, GetFormRequest request);
    Task<Result<FormResponse2>> UpdateFormAsync(string userId, UpdateFormRequest request);
    Task<Result<FormResponse2>> AddItemAsync(string userId, AddItemRequest2 request);
    Task<Result<bool>> DeleteItemAsync(string userId, DeleteItemRequest2 request);
}

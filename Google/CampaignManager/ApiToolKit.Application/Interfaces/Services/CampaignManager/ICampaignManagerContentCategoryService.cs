using ApiToolKit.Application.DTOs.CampaignManager.ContentCategory;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerContentCategoryService
{
    Task<Result<object>> DeleteContentCategoryAsync(DeleteContentCategoryRequest request);
    Task<Result<object>> ListContentCategoriesAsync(ListContentCategoriesRequest request);
    Task<Result<object>> UpdateContentCategoryAsync(UpdateContentCategoryRequest request);
    Task<Result<object>> GetContentCategoryAsync(GetContentCategoryRequest request);
    Task<Result<object>> InsertContentCategoryAsync(InsertContentCategoryRequest request);
    Task<Result<object>> PatchContentCategoryAsync(PatchContentCategoryRequest request);
}

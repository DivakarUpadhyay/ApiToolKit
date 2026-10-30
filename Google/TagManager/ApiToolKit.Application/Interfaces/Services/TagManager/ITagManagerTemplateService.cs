using ApiToolKit.Application.DTOs.TagManager.Template;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerTemplateService
{
    Task<Result<object>> ListTemplatesAsync(ListTemplatesRequest request);
    Task<Result<object>> GetTemplateAsync(GetTemplateRequest request);
    Task<Result<object>> CreateTemplateAsync(CreateTemplateRequest request);
    Task<Result<object>> UpdateTemplateAsync(UpdateTemplateRequest request);
    Task<Result<object>> DeleteTemplateAsync(DeleteTemplateRequest request);
    Task<Result<object>> RevertTemplateAsync(RevertTemplateRequest request);
    Task<Result<object>> ImportTemplateFromGalleryAsync(ImportTemplateFromGalleryRequest request);
}

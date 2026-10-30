using ApiToolKit.Application.DTOs.TagManager.Template;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerTemplateService : TagManagerServiceBase, ITagManagerTemplateService
{
    public TagManagerTemplateService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListTemplatesAsync(ListTemplatesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListTemplates", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetTemplateAsync(GetTemplateRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates/{r.TemplateId}";
        return await SendAsync(r.AccountKey, "GetTemplate", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateTemplateAsync(CreateTemplateRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates";
        return await SendAsync(r.AccountKey, "CreateTemplate", HttpMethod.Post, url, null, new { name = r.Name, templateData = r.TemplateData });
    }

    public async Task<Result<object>> UpdateTemplateAsync(UpdateTemplateRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates/{r.TemplateId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateTemplate", HttpMethod.Put, url, query, new { name = r.Name, templateData = r.TemplateData });
    }

    public async Task<Result<object>> DeleteTemplateAsync(DeleteTemplateRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates/{r.TemplateId}";
        return await SendAsync(r.AccountKey, "DeleteTemplate", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertTemplateAsync(RevertTemplateRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates/{r.TemplateId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertTemplate", HttpMethod.Post, url, query, new { });
    }

    public async Task<Result<object>> ImportTemplateFromGalleryAsync(ImportTemplateFromGalleryRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/templates:import_from_gallery";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "galleryOwner", r.GalleryOwner);
        Add(query, "galleryRepository", r.GalleryRepository);
        Add(query, "gallerySha256Hash", r.GallerySha256Hash);
        Add(query, "acknowledgePermissions", r.AcknowledgePermissions?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "ImportTemplateFromGallery", HttpMethod.Post, url, query, new { });
    }
}

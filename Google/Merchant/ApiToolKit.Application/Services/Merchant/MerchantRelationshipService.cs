using ApiToolKit.Application.DTOs.Merchant.Relationship;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantRelationshipService : MerchantServiceBase, IMerchantRelationshipService
{
    public MerchantRelationshipService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListRelationshipsAsync(ListRelationshipsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/relationships";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListRelationships", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateRelationshipAsync(UpdateRelationshipRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/relationships/{Esc(r.RelationshipId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateRelationship", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetRelationshipAsync(GetRelationshipRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/relationships/{Esc(r.RelationshipId)}";
        return await SendAsync(r.AccountKey, "GetRelationship", HttpMethod.Get, url);
    }
}

using ApiToolKit.Application.DTOs.Merchant.Relationship;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantRelationshipService
{
    Task<Result<object>> ListRelationshipsAsync(ListRelationshipsRequest request);
    Task<Result<object>> UpdateRelationshipAsync(UpdateRelationshipRequest request);
    Task<Result<object>> GetRelationshipAsync(GetRelationshipRequest request);
}

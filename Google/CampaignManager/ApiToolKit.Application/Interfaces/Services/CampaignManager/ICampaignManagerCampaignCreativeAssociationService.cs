using ApiToolKit.Application.DTOs.CampaignManager.CampaignCreativeAssociation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCampaignCreativeAssociationService
{
    Task<Result<object>> InsertCampaignCreativeAssociationAsync(InsertCampaignCreativeAssociationRequest request);
    Task<Result<object>> ListCampaignCreativeAssociationsAsync(ListCampaignCreativeAssociationsRequest request);
}

using ApiToolKit.Application.DTOs.DisplayVideo.LineItemYoutubeAssetTypeYoutubeAssetAssociation;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService
{
    Task<Result<object>> ListYoutubeAssetAssociationsAsync(ListYoutubeAssetAssociationsRequest request);
    Task<Result<object>> CreateYoutubeAssetAssociationAsync(CreateYoutubeAssetAssociationRequest request);
    Task<Result<object>> DeleteYoutubeAssetAssociationAsync(DeleteYoutubeAssetAssociationRequest request);
}

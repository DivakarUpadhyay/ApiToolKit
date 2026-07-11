using ApiToolKit.Application.DTOs.Facebook.Reaction;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookReactionService
{
    Task<Result<bool>> CreateReactionAsync(CreateReactionRequest request, string accessToken);
    Task<Result<GetReactionsResponse>> GetReactionsAsync(GetReactionsRequest request, string accessToken);
    Task<Result<bool>> DeleteReactionAsync(string objectId, string accessToken);
}

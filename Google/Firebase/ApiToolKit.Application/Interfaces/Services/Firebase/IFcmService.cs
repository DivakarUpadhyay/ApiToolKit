using ApiToolKit.Application.DTOs.Firebase.Fcm;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Firebase;

public interface IFcmService
{
    Task<Result<FcmMessageResponse>> SendToTokenAsync(SendToTokenRequest request, string accessToken);
    Task<Result<FcmMessageResponse>> SendToTopicAsync(SendToTopicRequest request, string accessToken);
    Task<Result<FcmMessageResponse>> SendToConditionAsync(SendToConditionRequest request, string accessToken);
    Task<Result<MulticastResponse>> SendMulticastAsync(MulticastRequest request, string accessToken);
    Task<Result<SubscribeToTopicResponse>> SubscribeToTopicAsync(SubscribeToTopicRequest request, string accessToken);
    Task<Result<SubscribeToTopicResponse>> UnsubscribeFromTopicAsync(UnsubscribeFromTopicRequest request, string accessToken);
}

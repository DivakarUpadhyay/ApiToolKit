using ApiToolKit.Application.DTOs.YouTube.Channel;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeChannelService
{
    Task<Result<ListMyChannelsResponse>> GetMyChannelsAsync(string accessToken);
    Task<Result<ChannelResponse>> GetChannelAsync(GetChannelRequest request, string accessToken);
    Task<Result<ChannelResponse>> UpdateChannelAsync(UpdateChannelRequest request, string accessToken);
}

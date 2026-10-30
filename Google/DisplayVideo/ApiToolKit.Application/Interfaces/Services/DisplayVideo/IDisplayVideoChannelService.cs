using ApiToolKit.Application.DTOs.DisplayVideo.Channel;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoChannelService
{
    Task<Result<object>> ListChannelsAsync(ListChannelsRequest request);
    Task<Result<object>> UpdateChannelAsync(UpdateChannelRequest request);
    Task<Result<object>> CreateChannelAsync(CreateChannelRequest request);
    Task<Result<object>> GetChannelAsync(GetChannelRequest request);
}

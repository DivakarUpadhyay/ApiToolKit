using ApiToolKit.Application.DTOs.DisplayVideo.PartnerChannel;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoPartnerChannelService
{
    Task<Result<object>> ListChannelsAsync(ListChannelsRequest request);
    Task<Result<object>> GetChannelAsync(GetChannelRequest request);
    Task<Result<object>> CreateChannelAsync(CreateChannelRequest request);
    Task<Result<object>> UpdateChannelAsync(UpdateChannelRequest request);
}

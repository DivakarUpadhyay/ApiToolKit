using ApiToolKit.Application.DTOs.DisplayVideo.FirstPartyAndPartnerAudience;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoFirstPartyAndPartnerAudienceService
{
    Task<Result<object>> EditCustomerMatchMembersFirstPartyAndPartnerAudienceAsync(EditCustomerMatchMembersFirstPartyAndPartnerAudienceRequest request);
    Task<Result<object>> ListFirstPartyAndPartnerAudiencesAsync(ListFirstPartyAndPartnerAudiencesRequest request);
    Task<Result<object>> GetFirstPartyAndPartnerAudienceAsync(GetFirstPartyAndPartnerAudienceRequest request);
    Task<Result<object>> UpdateFirstPartyAndPartnerAudienceAsync(UpdateFirstPartyAndPartnerAudienceRequest request);
    Task<Result<object>> CreateFirstPartyAndPartnerAudienceAsync(CreateFirstPartyAndPartnerAudienceRequest request);
}

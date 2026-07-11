using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IGmailSettingsService
{
    Task<Result<VacationSettingsResponse>> GetVacationSettingsAsync(string userId);
    Task<Result<VacationSettingsResponse>> UpdateVacationSettingsAsync(string userId, UpdateVacationRequest request);
    Task<Result<List<FilterResponse>>> ListFiltersAsync(string userId);
    Task<Result<FilterResponse>> CreateFilterAsync(string userId, CreateFilterRequest request);
    Task<Result<bool>> DeleteFilterAsync(string userId, string filterId);
    Task<Result<List<ForwardingAddressResponse>>> ListForwardingAddressesAsync(string userId);
    Task<Result<ForwardingAddressResponse>> CreateForwardingAddressAsync(string userId, CreateForwardingRequest request);
    Task<Result<ImapSettings>> GetImapSettingsAsync(string userId);
    Task<Result<ImapSettings>> UpdateImapSettingsAsync(string userId, ImapSettings settings);
    Task<Result<PopSettings>> GetPopSettingsAsync(string userId);
}

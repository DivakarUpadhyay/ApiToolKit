using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookWhatsAppMessagingService
{
    Task<Result<object>> SendTypingIndicatorAsync(WaTypingIndicatorRequest request);
    Task<Result<object>> SendRawMessageAsync(WaSendRawMessageRequest request);
    Task<Result<object>> SendCarouselAsync(WaSendCarouselRequest request);
    Task<Result<object>> SendMultiProductAsync(WaSendMultiProductRequest request);
    Task<Result<object>> SendMarketingMessageAsync(WaSendMarketingMessageRequest request);
    Task<Result<WaMediaFile>> DownloadMediaAsync(string mediaId, string accessToken);
    Task<Result<object>> BlockUsersAsync(WaUserListRequest request);
    Task<Result<object>> UnblockUsersAsync(WaUserListRequest request);
    Task<Result<object>> GetBlockedUsersAsync(string phoneNumberId, string accessToken, int? limit, string? after, string? before);
    Task<Result<object>> SearchTemplatesAsync(WaTemplateSearchQuery query);
    Task<Result<object>> GetTemplateLibraryAsync(WaTemplateLibraryQuery query);
    Task<Result<object>> CreateTemplateFromLibraryAsync(WaCreateTemplateFromLibraryRequest request);
    Task<Result<object>> DeleteTemplatesByNameAsync(string wabaId, string name, string accessToken);
    Task<Result<object>> BulkDeleteTemplatesAsync(string wabaId, List<string> templateIds, string accessToken);
    Task<Result<object>> MigrateTemplatesAsync(WaMigrateTemplatesRequest request);
}

public interface IFacebookWhatsAppAccountService
{
    Task<Result<object>> SubscribeAppToWabaAsync(WaSubscribeAppRequest request);
    Task<Result<object>> GetSubscribedAppsAsync(string wabaId, string accessToken);
    Task<Result<object>> UnsubscribeAppFromWabaAsync(string wabaId, string accessToken);
    Task<Result<object>> GetOwnedWabasAsync(string businessId, string accessToken);
    Task<Result<object>> GetClientWabasAsync(string businessId, string accessToken);
    Task<Result<object>> AddPhoneNumberAsync(WaAddPhoneNumberRequest request);
    Task<Result<object>> UpdateDisplayNameAsync(WaDisplayNameRequest request);
    Task<Result<object>> GetPhoneSettingsAsync(string phoneNumberId, string accessToken, bool includeSipCredentials);
    Task<Result<object>> UpdatePhoneSettingsAsync(WaRawSettingsRequest request);
    Task<Result<object>> GetConversationalAutomationAsync(string phoneNumberId, string accessToken);
    Task<Result<object>> UpdateConversationalAutomationAsync(WaConversationalAutomationRequest request);
    Task<Result<object>> GetCommerceSettingsAsync(string phoneNumberId, string accessToken);
    Task<Result<object>> UpdateCommerceSettingsAsync(WaCommerceSettingsRequest request);
    Task<Result<object>> GetBusinessComplianceAsync(string phoneNumberId, string accessToken);
    Task<Result<object>> UpdateBusinessComplianceAsync(WaRawSettingsRequest request);
    Task<Result<object>> GetHealthStatusAsync(string nodeId, string accessToken);
    Task<Result<object>> SetMarketingOnCloudApiAsync(WaMarketingOnCloudApiRequest request);
    Task<Result<object>> EnableTemplateInsightsAsync(WaWabaRequest request);
    Task<Result<object>> ExchangeEmbeddedSignupCodeAsync(WaExchangeCodeRequest request);
}

public interface IFacebookWhatsAppAnalyticsService
{
    Task<Result<object>> GetPricingAnalyticsAsync(WaPricingAnalyticsRequest request);
    Task<Result<object>> GetCallAnalyticsAsync(WaCallAnalyticsRequest request);
    Task<Result<object>> GetTemplateAnalyticsAsync(WaTemplateAnalyticsRequest request);
    Task<Result<object>> GetTemplateGroupAnalyticsAsync(WaTemplateGroupAnalyticsRequest request);
    Task<Result<object>> GetGroupAnalyticsAsync(WaGroupAnalyticsRequest request);
    Task<Result<object>> GetFlowMetricsAsync(WaFlowMetricsRequest request);
}

public interface IFacebookWhatsAppGroupsService
{
    Task<Result<object>> CreateGroupAsync(WaCreateGroupRequest request);
    Task<Result<object>> GetGroupsAsync(string phoneNumberId, string accessToken, int? limit, string? after, string? before);
    Task<Result<object>> GetGroupAsync(string groupId, string accessToken, string? fields);
    Task<Result<object>> UpdateGroupAsync(string groupId, WaUpdateGroupRequest request);
    Task<Result<object>> DeleteGroupAsync(string groupId, string accessToken);
    Task<Result<object>> GetInviteLinkAsync(string groupId, string accessToken);
    Task<Result<object>> ResetInviteLinkAsync(string groupId, string accessToken);
    Task<Result<object>> RemoveParticipantsAsync(string groupId, WaGroupParticipantsRequest request);
    Task<Result<object>> GetJoinRequestsAsync(string groupId, string accessToken);
    Task<Result<object>> ApproveJoinRequestsAsync(string groupId, WaJoinRequestsRequest request);
    Task<Result<object>> RejectJoinRequestsAsync(string groupId, WaJoinRequestsRequest request);
    Task<Result<object>> SendGroupMessageAsync(string groupId, WaGroupMessageRequest request);
    Task<Result<object>> SendGroupInviteTemplateAsync(WaGroupInviteTemplateRequest request);
}

public interface IFacebookWhatsAppCallingService
{
    Task<Result<object>> GetCallSettingsAsync(string phoneNumberId, string accessToken, bool includeSipCredentials);
    Task<Result<object>> UpdateCallSettingsAsync(WaRawSettingsRequest request);
    Task<Result<object>> CallActionAsync(string action, WaCallActionRequest request);
    Task<Result<object>> GetCallPermissionsAsync(string phoneNumberId, string accessToken, string? userWaId, string? recipient);
    Task<Result<object>> SendCallPermissionRequestAsync(WaCallPermissionRequestMessage request);
    Task<Result<object>> SendVoiceCallButtonAsync(WaVoiceCallButtonMessage request);
}

public interface IFacebookWhatsAppPaymentsService
{
    Task<Result<object>> GetPaymentConfigurationsAsync(string wabaId, string accessToken);
    Task<Result<object>> GetPaymentConfigurationAsync(string wabaId, string configurationName, string accessToken);
    Task<Result<object>> CreatePaymentConfigurationAsync(WaCreatePaymentConfigRequest request);
    Task<Result<object>> UpdatePaymentDataEndpointAsync(WaPaymentConfigDataEndpointRequest request);
    Task<Result<object>> RegenerateOauthLinkAsync(WaRegeneratePaymentOauthLinkRequest request);
    Task<Result<object>> DeletePaymentConfigurationAsync(string wabaId, string configurationName, string accessToken);
    Task<Result<object>> GetPaymentStatusAsync(string phoneNumberId, string paymentConfiguration, string referenceId, string accessToken);
    Task<Result<object>> RefundAsync(WaRefundRequest request);
    Task<Result<object>> SendOrderDetailsAsync(WaOrderDetailsMessageRequest request);
    Task<Result<object>> SendOrderStatusAsync(WaOrderStatusMessageRequest request);
}

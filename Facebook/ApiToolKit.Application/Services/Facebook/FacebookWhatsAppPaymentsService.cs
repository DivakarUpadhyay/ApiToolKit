using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;

namespace ApiToolKit.Application.Services.Facebook;

/// <summary>WhatsApp Payments API (India): payment configurations, status lookup, refunds and order messages.</summary>
public class FacebookWhatsAppPaymentsService : WhatsAppServiceBase, IFacebookWhatsAppPaymentsService
{
    public FacebookWhatsAppPaymentsService(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo) : base(api, repo) { }

    // ─── Payment configurations ─────────────────────────────────────────────

    public Task<Result<object>> GetPaymentConfigurationsAsync(string wabaId, string accessToken) =>
        RunAsync("GetPaymentConfigurations", () => Api.GetAsync<object>($"/{wabaId}/payment_configurations", accessToken));

    public Task<Result<object>> GetPaymentConfigurationAsync(string wabaId, string configurationName, string accessToken) =>
        RunAsync("GetPaymentConfiguration", () => Api.GetAsync<object>($"/{wabaId}/payment_configuration/{Uri.EscapeDataString(configurationName)}", accessToken));

    public Task<Result<object>> CreatePaymentConfigurationAsync(WaCreatePaymentConfigRequest r)
    {
        var body = new Dictionary<string, object?>
        {
            ["configuration_name"] = r.ConfigurationName,
            ["provider_name"] = r.ProviderName
        };
        if (!string.IsNullOrEmpty(r.PurposeCode)) body["purpose_code"] = r.PurposeCode;
        if (!string.IsNullOrEmpty(r.MerchantCategoryCode)) body["merchant_category_code"] = r.MerchantCategoryCode;
        if (!string.IsNullOrEmpty(r.MerchantVpa)) body["merchant_vpa"] = r.MerchantVpa;
        if (!string.IsNullOrEmpty(r.RedirectUrl)) body["redirect_url"] = r.RedirectUrl;
        return RunAsync("CreatePaymentConfiguration", () => Api.PostAsync<object>($"/{r.WabaId}/payment_configuration", r.AccessToken, body), r);
    }

    public Task<Result<object>> UpdatePaymentDataEndpointAsync(WaPaymentConfigDataEndpointRequest r) =>
        RunAsync("UpdatePaymentDataEndpoint", () => Api.PostAsync<object>(
            $"/{r.WabaId}/payment_configuration/{Uri.EscapeDataString(r.ConfigurationName)}", r.AccessToken,
            new { data_endpoint_url = r.DataEndpointUrl }), r);

    public Task<Result<object>> RegenerateOauthLinkAsync(WaRegeneratePaymentOauthLinkRequest r)
    {
        var body = new Dictionary<string, object?> { ["configuration_name"] = r.ConfigurationName };
        if (!string.IsNullOrEmpty(r.RedirectUrl)) body["redirect_url"] = r.RedirectUrl;
        return RunAsync("RegeneratePaymentOauthLink", () => Api.PostAsync<object>($"/{r.WabaId}/generate_payment_configuration_oauth_link", r.AccessToken, body), r);
    }

    public Task<Result<object>> DeletePaymentConfigurationAsync(string wabaId, string configurationName, string accessToken) =>
        RunAsync("DeletePaymentConfiguration", () => Api.DeleteAsync<object>($"/{wabaId}/payment_configuration", accessToken,
            body: new { configuration_name = configurationName }), new { wabaId, configurationName });

    // ─── Payments ───────────────────────────────────────────────────────────

    public Task<Result<object>> GetPaymentStatusAsync(string phoneNumberId, string paymentConfiguration, string referenceId, string accessToken) =>
        RunAsync("GetPaymentStatus", () => Api.GetAsync<object>(
            $"/{phoneNumberId}/payments/{Uri.EscapeDataString(paymentConfiguration)}/{Uri.EscapeDataString(referenceId)}", accessToken));

    public Task<Result<object>> RefundAsync(WaRefundRequest r)
    {
        var body = new Dictionary<string, object?>
        {
            ["reference_id"] = r.ReferenceId,
            ["payment_config_id"] = r.PaymentConfigId
        };
        if (!string.IsNullOrEmpty(r.Speed)) body["speed"] = r.Speed;
        if (r.Amount != null) body["amount"] = r.Amount;
        return RunAsync("RefundPayment", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/payments_refund", r.AccessToken, body), r);
    }

    // ─── Order messages ─────────────────────────────────────────────────────

    public Task<Result<object>> SendOrderDetailsAsync(WaOrderDetailsMessageRequest r)
    {
        var interactive = new Dictionary<string, object?>
        {
            ["type"] = "order_details",
            ["body"] = new { text = r.BodyText },
            ["action"] = new { name = "review_and_pay", parameters = r.Parameters }
        };
        if (r.Header != null) interactive["header"] = r.Header;
        if (!string.IsNullOrEmpty(r.FooterText)) interactive["footer"] = new { text = r.FooterText };

        var body = MessageBody(r.To, null, "interactive");
        body["interactive"] = interactive;
        return RunAsync("SendOrderDetails", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }

    public Task<Result<object>> SendOrderStatusAsync(WaOrderStatusMessageRequest r)
    {
        var order = new Dictionary<string, object?> { ["status"] = r.Status };
        if (!string.IsNullOrEmpty(r.Description)) order["description"] = r.Description;

        var interactive = new Dictionary<string, object?>
        {
            ["type"] = "order_status",
            ["body"] = new { text = r.BodyText },
            ["action"] = new { name = "review_order", parameters = new { reference_id = r.ReferenceId, order } }
        };
        if (!string.IsNullOrEmpty(r.FooterText)) interactive["footer"] = new { text = r.FooterText };

        var body = MessageBody(r.To, null, "interactive");
        body["interactive"] = interactive;
        return RunAsync("SendOrderStatus", () => Api.PostAsync<object>($"/{r.PhoneNumberId}/messages", r.AccessToken, body), r);
    }
}

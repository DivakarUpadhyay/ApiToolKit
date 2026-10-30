using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Invoice;

/// <summary>Retrieves the invoice currency used by an advertiser in a given month.</summary>
public class LookupInvoiceCurrencyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Month for which the currency is needed.</summary>
    public string? InvoiceMonth { get; set; }
}

/// <summary>Lists invoices posted for an advertiser in a given month.</summary>
public class ListInvoicesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Select type of invoice to retrieve for Loi Sapin advertisers.</summary>
    public string? LoiSapinInvoiceType { get; set; }
    /// <summary>The month to list the invoices for.</summary>
    public string? IssueMonth { get; set; }
}

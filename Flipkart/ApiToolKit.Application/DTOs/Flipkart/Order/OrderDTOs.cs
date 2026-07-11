using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Flipkart.Order;

// ── Filter Shipments ──────────────────────────────────────────────────────────

public class FilterShipmentsRequest
{
    public ShipmentFilter? Filter { get; set; }
    public ShipmentPagination? Pagination { get; set; }
    public ShipmentSort? Sort { get; set; }
}

public class ShipmentFilter
{
    public string? Type { get; set; }
    public List<string>? States { get; set; }
    public bool? Hold { get; set; }
    public List<string>? Sku { get; set; }
    public string? ServiceProfiles { get; set; }
    public bool? Mps { get; set; }
    public string? LocationId { get; set; }
    public string? CancellationType { get; set; }
    public List<string>? ShipmentTypes { get; set; }
    public DateRange? OrderDate { get; set; }
    public DateRange? DispatchAfterDate { get; set; }
    public DateRange? DispatchByDate { get; set; }
    public DateRange? CancellationDate { get; set; }
}

public class DateRange { public string? From { get; set; } public string? To { get; set; } }
public class ShipmentPagination { public int PageSize { get; set; } = 10; }
public class ShipmentSort { public string? Field { get; set; } public string? Order { get; set; } }

// ── Get Shipments ─────────────────────────────────────────────────────────────

public class GetShipmentsByIdsRequest { [Required] public string ShipmentIds { get; set; } = string.Empty; }
public class GetShipmentsByOrderItemIdsRequest { [Required] public string OrderItemIds { get; set; } = string.Empty; }
public class GetShipmentsByOrderIdsRequest { [Required] public string OrderIds { get; set; } = string.Empty; }

// ── Labels ────────────────────────────────────────────────────────────────────

public class GenerateLabelsRequest
{
    [Required] public List<ShipmentLabelData> Shipments { get; set; } = new();
}

public class ShipmentLabelData
{
    [Required] public string ShipmentId { get; set; } = string.Empty;
    public string? LocationId { get; set; }
    public List<TaxItem>? TaxItems { get; set; }
    public List<SerialNumberItem>? SerialNumbers { get; set; }
    public List<SubShipment>? SubShipments { get; set; }
    public List<ShipmentInvoice>? Invoices { get; set; }
}

public class TaxItem { public string? OrderItemId { get; set; } public decimal PurchasePrice { get; set; } public decimal TaxRate { get; set; } public int Quantity { get; set; } }
public class SerialNumberItem { public string? OrderItemId { get; set; } public List<List<string>>? SerialNumbers { get; set; } }
public class SubShipment { public string? SubShipmentId { get; set; } public ShipmentDimensions? Dimensions { get; set; } }
public class ShipmentDimensions { public decimal Length { get; set; } public decimal Breadth { get; set; } public decimal Height { get; set; } public decimal Width { get; set; } }
public class ShipmentInvoice { public string? OrderId { get; set; } public string? InvoiceNumber { get; set; } public string? InvoiceDate { get; set; } }

public class DownloadLabelsRequest { [Required] public string ShipmentIds { get; set; } = string.Empty; public bool Reprint { get; set; } }

// ── Dispatch ──────────────────────────────────────────────────────────────────

public class DispatchShipmentsRequest
{
    [Required] public List<string> ShipmentIds { get; set; } = new();
    public string? LocationId { get; set; }
}

// ── Cancel ────────────────────────────────────────────────────────────────────

public class CancelShipmentsRequest
{
    [Required] public List<CancelShipmentItem> Shipments { get; set; } = new();
}

public class CancelShipmentItem
{
    [Required] public string ShipmentId { get; set; } = string.Empty;
    public string? LocationId { get; set; }
    public List<string>? CancellationGroupIds { get; set; }
    public string? Reason { get; set; }
}

// ── Handover / Manifest ───────────────────────────────────────────────────────

public class GetHandoverCountsRequest { [Required] public string LocationId { get; set; } = string.Empty; }

public class GenerateManifestRequest
{
    [Required] public List<ManifestParam> Params { get; set; } = new();
}

public class ManifestParam
{
    public string? VendorGroupCode { get; set; }
    public string? LocationId { get; set; }
    public bool IsMps { get; set; }
    public DateRange? PickUpDate { get; set; }
}

// ── Self-Ship ─────────────────────────────────────────────────────────────────

public class SelfShipDispatchRequest
{
    [Required] public string ShipmentId { get; set; } = string.Empty;
    public string? TentativeDeliveryDate { get; set; }
    public string? DispatchDate { get; set; }
    public string? DeliveryPartner { get; set; }
    public string? DeliveryPartnerCode { get; set; }
    public string? TrackingId { get; set; }
    public string? LocationId { get; set; }
    public SelfShipInvoice? Invoice { get; set; }
    public SelfShipDispatchLocation? DispatchLocation { get; set; }
}

public class SelfShipInvoice { public string? InvoiceNumber { get; set; } public string? InvoiceDate { get; set; } public List<SelfShipInvoiceItem>? Items { get; set; } }
public class SelfShipInvoiceItem { public string? OrderItemId { get; set; } public int Quantity { get; set; } public List<List<string>>? SerialNumbers { get; set; } }
public class SelfShipDispatchLocation { public string? LocationId { get; set; } public DispatchAddress? Address { get; set; } }
public class DispatchAddress { public string? Address1 { get; set; } public string? Address2 { get; set; } public string? Landmark { get; set; } public string? City { get; set; } public string? State { get; set; } public string? Pincode { get; set; } }

public class SelfShipDeliveryRequest
{
    [Required] public string ShipmentId { get; set; } = string.Empty;
    [Required] public string DeliveryDate { get; set; } = string.Empty;
    public string? LocationId { get; set; }
}

// ── OTC ───────────────────────────────────────────────────────────────────────

public class GetOtcDetailsRequest { [Required] public string LocationId { get; set; } = string.Empty; public string? RequestType { get; set; } }

// ── Services ──────────────────────────────────────────────────────────────────

public class ServiceAttemptRequest
{
    [Required] public List<ServiceAttemptItem> Services { get; set; } = new();
}

public class ServiceAttemptItem
{
    public string? DeliveryDate { get; set; }
    public string? NewDeliveryDate { get; set; }
    public string? Reason { get; set; }
    public string? SubReason { get; set; }
    public string? Comments { get; set; }
    [Required] public string ServiceFulfilmentId { get; set; } = string.Empty;
}

public class ServiceCompleteRequest
{
    [Required] public List<ServiceCompleteItem> Services { get; set; } = new();
}

public class ServiceCompleteItem { [Required] public string ServiceFulfilmentId { get; set; } = string.Empty; public string? DeliveryDate { get; set; } }

// ── Generic Response ─────────────────────────────────────────────────────────

public class ShipmentResponse { public object? Data { get; set; } public string? Status { get; set; } }

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopify.Fulfillment;

public class TrackingInfo
{
    public string? Number { get; set; }
    public string? Url { get; set; }
    public string? Company { get; set; }
}

public class FulfillmentLineItemDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public string? Sku { get; set; }
}

public class FulfillmentDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
    public string? Status { get; set; }
    public string? TrackingCompany { get; set; }
    public string? TrackingNumber { get; set; }
    public string? TrackingUrl { get; set; }
    public List<string>? TrackingNumbers { get; set; }
    public List<string>? TrackingUrls { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public List<FulfillmentLineItemDto>? LineItems { get; set; }
}

public class ListFulfillmentsResponse
{
    public List<FulfillmentDto>? Fulfillments { get; set; }
}

public class FulfillmentResponse
{
    public FulfillmentDto? Fulfillment { get; set; }
}

public class FulfillmentOrderLineItemDto
{
    public string? Id { get; set; }
    public int? Quantity { get; set; }
}

public class CreateFulfillmentRequest
{
    [Required]
    public List<FulfillmentOrderDto2>? FulfillmentOrderLineItems { get; set; }
    public TrackingInfo? TrackingInfo { get; set; }
    public bool? NotifyCustomer { get; set; }
}

public class FulfillmentOrderDto2
{
    [Required]
    public string FulfillmentOrderId { get; set; } = string.Empty;
    public List<FulfillmentOrderLineItemDto>? FulfillmentOrderLineItems { get; set; }
}

public class UpdateTrackingRequest
{
    [Required]
    public TrackingInfo TrackingInfo { get; set; } = new();
    public bool? NotifyCustomer { get; set; }
}

public class FulfillmentOrderDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
    public string? AssignedLocationId { get; set; }
    public string? Status { get; set; }
    public string? RequestStatus { get; set; }
    public string? FulfillAt { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class ListFulfillmentOrdersResponse
{
    public List<FulfillmentOrderDto>? FulfillmentOrders { get; set; }
}

public class FulfillmentOrderResponse
{
    public FulfillmentOrderDto? FulfillmentOrder { get; set; }
}

public class FulfillmentServiceDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Handle { get; set; }
    public bool? InventoryManagement { get; set; }
    public bool? TrackingSupport { get; set; }
    public bool? RequiresShippingMethod { get; set; }
    public string? CallbackUrl { get; set; }
    public string? Format { get; set; }
    public string? ServiceName { get; set; }
}

public class ListFulfillmentServicesResponse
{
    public List<FulfillmentServiceDto>? FulfillmentServices { get; set; }
}

public class FulfillmentServiceResponse
{
    public FulfillmentServiceDto? FulfillmentService { get; set; }
}

public class CreateFulfillmentServiceRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string CallbackUrl { get; set; } = string.Empty;
    public bool? InventoryManagement { get; set; }
    public bool? TrackingSupport { get; set; }
    public bool? RequiresShippingMethod { get; set; }
    public string? Format { get; set; }
}

public class UpdateFulfillmentServiceRequest
{
    public string? Name { get; set; }
    public string? CallbackUrl { get; set; }
    public bool? InventoryManagement { get; set; }
    public bool? TrackingSupport { get; set; }
}

public class ShippingZoneDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ProfileId { get; set; }
    public List<string>? Countries { get; set; }
}

public class ListShippingZonesResponse
{
    public List<ShippingZoneDto>? ShippingZones { get; set; }
}

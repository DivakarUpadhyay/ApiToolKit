using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Logistics;

public class GetLogisticInfoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
}

public class InitLogisticsRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    [Required] public string PackageNumber { get; set; } = string.Empty;
    public PickupRequest? Pickup { get; set; }
    public DropoffRequest? Dropoff { get; set; }
    public NonIntegratedRequest? NonIntegrated { get; set; }
}

public class PickupRequest
{
    public long AddressId { get; set; }
    public string PickupTimeId { get; set; } = string.Empty;
}

public class DropoffRequest
{
    public long BranchId { get; set; }
    public string SenderRealName { get; set; } = string.Empty;
}

public class NonIntegratedRequest
{
    public string TrackingNumber { get; set; } = string.Empty;
}

public class ShipOrderRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    public string? PackageNumber { get; set; }
    public PickupRequest? Pickup { get; set; }
    public DropoffRequest? Dropoff { get; set; }
    public NonIntegratedRequest? NonIntegrated { get; set; }
}

public class GetTrackingNumberRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    public string? PackageNumber { get; set; }
}

public class GetAddressListRequest
{
    [Required] public long ShopId { get; set; }
}

public class UpdateLogisticsRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    [Required] public string TrackingNumber { get; set; } = string.Empty;
    public string HandoverMethod { get; set; } = string.Empty;
    public long BranchId { get; set; }
    public long SenderAddressId { get; set; }
    public string PickupTimeId { get; set; } = string.Empty;
}

public class LogisticsInfoResponse
{
    public long LogisticId { get; set; }
    public string LogisticName { get; set; } = string.Empty;
    public bool HasMap { get; set; }
    public bool Cod { get; set; }
    public bool Enabled { get; set; }
    public decimal FeeType { get; set; }
    public decimal ShippingFee { get; set; }
    public string Status { get; set; } = string.Empty;
    public long MinSendDays { get; set; }
    public long MaxSendDays { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class TrackingNumberResponse
{
    public string TrackingNumber { get; set; } = string.Empty;
    public string PlpNumber { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class AddressListResponse
{
    public List<AddressResponse> AddressList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class AddressResponse
{
    public long AddressId { get; set; }
    public string Region { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Town { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public List<string> AddressType { get; set; } = new();
}

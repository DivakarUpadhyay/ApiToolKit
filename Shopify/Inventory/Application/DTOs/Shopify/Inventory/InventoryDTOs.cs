using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopify.Inventory;

// ---- Inventory Item DTOs ----
public class InventoryItemDto
{
    public string? Id { get; set; }
    public string? Sku { get; set; }
    public bool? Tracked { get; set; }
    public string? Cost { get; set; }
    public string? CountryCodeOfOrigin { get; set; }
    public string? ProvinceCodeOfOrigin { get; set; }
    public string? HarmonizedSystemCode { get; set; }
    public bool? RequiresShipping { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}
public class ListInventoryItemsResponse { public List<InventoryItemDto>? InventoryItems { get; set; } }
public class InventoryItemResponse { public InventoryItemDto? InventoryItem { get; set; } }
public class UpdateInventoryItemRequest { public bool? Tracked { get; set; } public string? Cost { get; set; } public string? CountryCodeOfOrigin { get; set; } public string? HarmonizedSystemCode { get; set; } }

// ---- Inventory Level DTOs ----
public class InventoryLevelDto
{
    public string? InventoryItemId { get; set; }
    public string? LocationId { get; set; }
    public int? Available { get; set; }
    public string? UpdatedAt { get; set; }
}
public class ListInventoryLevelsResponse { public List<InventoryLevelDto>? InventoryLevels { get; set; } }
public class InventoryLevelResponse { public InventoryLevelDto? InventoryLevel { get; set; } }
public class AdjustInventoryLevelRequest { [Required] public string InventoryItemId { get; set; } = string.Empty; [Required] public string LocationId { get; set; } = string.Empty; [Required] public int AvailableAdjustment { get; set; } }
public class ConnectInventoryLevelRequest { [Required] public string InventoryItemId { get; set; } = string.Empty; [Required] public string LocationId { get; set; } = string.Empty; public bool? RelocateIfNecessary { get; set; } }
public class SetInventoryLevelRequest { [Required] public string InventoryItemId { get; set; } = string.Empty; [Required] public string LocationId { get; set; } = string.Empty; [Required] public int Available { get; set; } }

// ---- Location DTOs ----
public class LocationDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? Zip { get; set; }
    public string? Phone { get; set; }
    public bool? Active { get; set; }
    public bool? Legacy { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}
public class ListLocationsResponse { public List<LocationDto>? Locations { get; set; } }
public class LocationResponse { public LocationDto? Location { get; set; } }

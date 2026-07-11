using System.ComponentModel.DataAnnotations;
namespace ApiToolKit.Application.DTOs.Shopee.AddOnDeal;
public class AddAddOnDealRequest { [Required] public long ShopId { get; set; } [Required] public string AddOnDealName { get; set; } = string.Empty; [Required] public long StartTime { get; set; } [Required] public long EndTime { get; set; } }
public class UpdateAddOnDealRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } public string? AddOnDealName { get; set; } public long? StartTime { get; set; } public long? EndTime { get; set; } }
public class GetAddOnDealRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } }
public class GetAddOnDealListRequest { [Required] public long ShopId { get; set; } public string Status { get; set; } = "ongoing"; public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; }
public class AddOnDealMainItemRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } [Required] public List<AddOnMainItem> ItemList { get; set; } = new(); }
public class AddOnMainItem { public long ItemId { get; set; } public long ModelId { get; set; } public decimal MinPurchaseQty { get; set; } }
public class AddOnDealSubItemRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } [Required] public List<AddOnSubItem> ItemList { get; set; } = new(); }
public class AddOnSubItem { public long ItemId { get; set; } public long ModelId { get; set; } public decimal AddOnPrice { get; set; } public int Stock { get; set; } public int PurchaseLimit { get; set; } }
public class DeleteAddOnDealRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } }
public class EndAddOnDealRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } }
public class DeleteAddOnItemRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } [Required] public List<long> ItemIdList { get; set; } = new(); }
public class UpdateAddOnSubItemRequest { [Required] public long ShopId { get; set; } [Required] public long AddOnDealId { get; set; } [Required] public List<AddOnSubItem> ItemList { get; set; } = new(); }
public class AddOnDealResponse { public long AddOnDealId { get; set; } public string AddOnDealName { get; set; } = string.Empty; public long StartTime { get; set; } public long EndTime { get; set; } public string Status { get; set; } = string.Empty; public string Error { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; }
public class AddOnDealListResponse { public List<AddOnDealResponse> AddOnDealList { get; set; } = new(); public bool More { get; set; } public int TotalCount { get; set; } public string Error { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; }

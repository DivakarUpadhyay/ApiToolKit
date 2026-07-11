using System.ComponentModel.DataAnnotations;
namespace ApiToolKit.Application.DTOs.Shopee.BundleDeal;
public class AddBundleDealRequest { [Required] public long ShopId { get; set; } [Required] public string BundleDealName { get; set; } = string.Empty; [Required] public long StartTime { get; set; } [Required] public long EndTime { get; set; } [Required] public List<BundleDealItemRequest> ItemList { get; set; } = new(); }
public class BundleDealItemRequest { public long ItemId { get; set; } public decimal BundlePrice { get; set; } public long ModelId { get; set; } }
public class UpdateBundleDealRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } public string? BundleDealName { get; set; } public long? StartTime { get; set; } public long? EndTime { get; set; } }
public class GetBundleDealRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } }
public class GetBundleDealListRequest { [Required] public long ShopId { get; set; } public string Status { get; set; } = "ongoing"; public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; }
public class DeleteBundleDealRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } }
public class EndBundleDealRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } }
public class AddBundleDealItemRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } [Required] public List<BundleDealItemRequest> ItemList { get; set; } = new(); }
public class DeleteBundleDealItemRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } [Required] public List<long> ItemIdList { get; set; } = new(); }
public class UpdateBundleDealItemRequest { [Required] public long ShopId { get; set; } [Required] public long BundleDealId { get; set; } [Required] public List<BundleDealItemRequest> ItemList { get; set; } = new(); }
public class BundleDealResponse { public long BundleDealId { get; set; } public string BundleDealName { get; set; } = string.Empty; public long StartTime { get; set; } public long EndTime { get; set; } public string Status { get; set; } = string.Empty; public string Error { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; }
public class BundleDealListResponse { public List<BundleDealResponse> BundleDealList { get; set; } = new(); public bool More { get; set; } public int TotalCount { get; set; } public string Error { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; }

using System.ComponentModel.DataAnnotations;
namespace ApiToolKit.Application.DTOs.Shopee.Return;

public class GetReturnListRequest { [Required] public long ShopId { get; set; } public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; public long CreateTimeFrom { get; set; } public long CreateTimeTo { get; set; } public string Status { get; set; } = string.Empty; }
public class GetReturnDetailRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; }
public class ConfirmReturnRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; }
public class DisputeReturnRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; [Required] public string DisputeReason { get; set; } = string.Empty; public string DisputeTextReason { get; set; } = string.Empty; public List<string> ImageList { get; set; } = new(); }
public class OfferReturnRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; [Required] public string OfferType { get; set; } = string.Empty; public decimal Amount { get; set; } }
public class AcceptOfferRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; }
public class CancelDisputeRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; }
public class UploadProofRequest { [Required] public long ShopId { get; set; } [Required] public string ReturnSn { get; set; } = string.Empty; public string Text { get; set; } = string.Empty; public List<string> ImageList { get; set; } = new(); public List<string> VideoList { get; set; } = new(); }
public class ReturnResponse { public string ReturnSn { get; set; } = string.Empty; public string OrderSn { get; set; } = string.Empty; public string Status { get; set; } = string.Empty; public string Reason { get; set; } = string.Empty; public string TextReason { get; set; } = string.Empty; public decimal RefundAmount { get; set; } public long CreateTime { get; set; } public long UpdateTime { get; set; } public string BuyerUserId { get; set; } = string.Empty; public string Error { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; }
public class ReturnListResponse { public List<ReturnResponse> ReturnList { get; set; } = new(); public bool More { get; set; } public int TotalCount { get; set; } public string Error { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; }
public class GetReturnDisputeReasonRequest { [Required] public long ShopId { get; set; } }

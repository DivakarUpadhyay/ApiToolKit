using System.ComponentModel.DataAnnotations;
namespace ApiToolKit.Application.DTOs.Shopee.Payment;
public class GetEscrowListRequest { [Required] public long ShopId { get; set; } public long ReleaseTimeFrom { get; set; } public long ReleaseTimeTo { get; set; } public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; }
public class GetEscrowDetailRequest { [Required] public long ShopId { get; set; } [Required] public string OrderSn { get; set; } = string.Empty; }
public class GetWalletTransactionListRequest { [Required] public long ShopId { get; set; } public long CreateTimeFrom { get; set; } public long CreateTimeTo { get; set; } public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; public string WalletType { get; set; } = string.Empty; }
public class GetPayoutDetailRequest { [Required] public long ShopId { get; set; } public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; }
public class GetPaymentMethodListRequest { [Required] public long ShopId { get; set; } }
public class GetInstallmentStatusRequest { [Required] public long ShopId { get; set; } }
public class SetInstallmentStatusRequest { [Required] public long ShopId { get; set; } [Required] public bool InstallmentStatus { get; set; } }
public class GetItemInstallmentRequest { [Required] public long ShopId { get; set; } [Required] public List<long> ItemIdList { get; set; } = new(); }
public class SetItemInstallmentRequest { [Required] public long ShopId { get; set; } [Required] public long ItemId { get; set; } [Required] public bool InstallmentStatus { get; set; } }
public class GenerateIncomeReportRequest { [Required] public long ShopId { get; set; } [Required] public string StartDate { get; set; } = string.Empty; [Required] public string EndDate { get; set; } = string.Empty; }
public class GetIncomeReportRequest { [Required] public long ShopId { get; set; } [Required] public string ReportType { get; set; } = string.Empty; [Required] public string StartDate { get; set; } = string.Empty; [Required] public string EndDate { get; set; } = string.Empty; }
public class GetBillingTransactionRequest { [Required] public long ShopId { get; set; } public long CreateTimeFrom { get; set; } public long CreateTimeTo { get; set; } public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; }
public class GetPayoutInfoRequest { [Required] public long ShopId { get; set; } public int PageNo { get; set; } = 1; public int PageSize { get; set; } = 20; }

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Voucher;

public class CreateVoucherRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string VoucherCode { get; set; } = string.Empty;
    [Required] public int VoucherType { get; set; } // 1=Shop, 2=Product
    [Required] public int DiscountType { get; set; } // 1=Fixed, 2=Percentage
    [Required] public decimal RewardValue { get; set; }
    [Required] public decimal MinimumSpend { get; set; }
    [Required] public long StartTime { get; set; }
    [Required] public long EndTime { get; set; }
    [Required] public int UsageQuantity { get; set; }
    public int DisplayChannelList { get; set; } = 0;
}

public class UpdateVoucherRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long VoucherId { get; set; }
    public string? VoucherCode { get; set; }
    public decimal? RewardValue { get; set; }
    public decimal? MinimumSpend { get; set; }
    public long? StartTime { get; set; }
    public long? EndTime { get; set; }
    public int? UsageQuantity { get; set; }
}

public class GetVoucherRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long VoucherId { get; set; }
}

public class GetVoucherListRequest
{
    [Required] public long ShopId { get; set; }
    public int Status { get; set; } = 1; // 1=Upcoming, 2=Ongoing, 3=Expired
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class EndVoucherRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long VoucherId { get; set; }
}

public class DeleteVoucherRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long VoucherId { get; set; }
}

public class VoucherResponse
{
    public long VoucherId { get; set; }
    public string VoucherCode { get; set; } = string.Empty;
    public string VoucherType { get; set; } = string.Empty;
    public string DiscountType { get; set; } = string.Empty;
    public decimal RewardValue { get; set; }
    public decimal MinimumSpend { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public int UsageQuantity { get; set; }
    public int TotalUsageCount { get; set; }
    public string VoucherStatus { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class VoucherListResponse
{
    public List<VoucherResponse> VoucherList { get; set; } = new();
    public bool More { get; set; }
    public int TotalCount { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

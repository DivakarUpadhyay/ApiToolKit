using System.ComponentModel.DataAnnotations;
namespace ApiToolKit.Application.DTOs.Shopify.Analytics;

// ---- Report DTOs ----
public class ReportDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? ShopifyqlQuery { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}
public class ListReportsResponse { public List<ReportDto>? Reports { get; set; } }
public class ReportResponse { public ReportDto? Report { get; set; } }
public class CreateReportRequest { [Required] public string Name { get; set; } = string.Empty; [Required] public string ShopifyqlQuery { get; set; } = string.Empty; }
public class UpdateReportRequest { public string? Name { get; set; } public string? ShopifyqlQuery { get; set; } }

// ---- Payout DTOs ----
public class PayoutDto
{
    public string? Id { get; set; }
    public string? Status { get; set; }
    public string? Date { get; set; }
    public string? Currency { get; set; }
    public string? Amount { get; set; }
}
public class ListPayoutsResponse { public List<PayoutDto>? Payouts { get; set; } }
public class PayoutResponse { public PayoutDto? Payout { get; set; } }

// ---- Dispute DTOs ----
public class DisputeEvidenceDto { public string? AccessedByMerchantOn { get; set; } public string? DueByMerchantOn { get; set; } }
public class DisputeDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
    public string? Type { get; set; }
    public string? Amount { get; set; }
    public string? Currency { get; set; }
    public string? ReasonMessage { get; set; }
    public string? Status { get; set; }
    public string? EvidenceDueBy { get; set; }
    public string? FinalizedOn { get; set; }
    public string? InitiatedAt { get; set; }
    public DisputeEvidenceDto? Evidence { get; set; }
}
public class ListDisputesResponse { public List<DisputeDto>? Disputes { get; set; } }
public class DisputeResponse { public DisputeDto? Dispute { get; set; } }

// ---- Balance Transaction DTOs ----
public class BalanceTransactionDto
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? Amount { get; set; }
    public string? Fee { get; set; }
    public string? Net { get; set; }
    public string? Currency { get; set; }
    public string? SourceId { get; set; }
    public string? SourceType { get; set; }
    public string? PayoutId { get; set; }
    public string? PayoutStatus { get; set; }
    public string? ProcessedAt { get; set; }
}
public class ListBalanceTransactionsResponse { public List<BalanceTransactionDto>? Transactions { get; set; } }

// ---- Event DTOs ----
public class EventDto
{
    public string? Id { get; set; }
    public string? SubjectId { get; set; }
    public string? SubjectType { get; set; }
    public string? Verb { get; set; }
    public string? ObjectId { get; set; }
    public string? ObjectType { get; set; }
    public string? Message { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? CreatedAt { get; set; }
}
public class ListEventsResponse { public List<EventDto>? Events { get; set; } }
public class EventResponse { public EventDto? Event { get; set; } }

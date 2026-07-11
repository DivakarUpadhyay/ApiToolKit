using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Flipkart.Returns;

// ── Get Returns ───────────────────────────────────────────────────────────────

public class GetReturnsRequest
{
    public string? Source { get; set; }
    public string? ModifiedAfter { get; set; }
    public string? ModifiedBefore { get; set; }
    public string? CreatedAfter { get; set; }
    public string? CreatedBefore { get; set; }
    public string? LocationId { get; set; }
    public string? ReturnIds { get; set; }
    public string? TrackingIds { get; set; }
}

// ── Approve / Reject / Complete ───────────────────────────────────────────────

public class ApproveReturnsRequest { [Required] public List<ReturnActionItem> Returns { get; set; } = new(); }
public class RejectReturnsRequest { [Required] public List<ReturnActionItem> Returns { get; set; } = new(); }

public class ReturnActionItem
{
    [Required] public string ReturnId { get; set; } = string.Empty;
    public string? Comments { get; set; }
    public string? ServiceDate { get; set; }
    public string? ServiceFulfilmentId { get; set; }
    public string? LocationId { get; set; }
}

public class CompleteReturnsRequest
{
    public string? LocationId { get; set; }
    public string? ReturnIds { get; set; }
}

// ── Pickup ────────────────────────────────────────────────────────────────────

public class PickupReturnsRequest { [Required] public List<PickupReturnItem> Returns { get; set; } = new(); }

public class PickupReturnItem
{
    [Required] public string ReturnId { get; set; } = string.Empty;
    [Required] public string PickupDate { get; set; } = string.Empty;
    public string? LocationId { get; set; }
}

public class PickupAttemptRequest { [Required] public List<PickupAttemptItem> Returns { get; set; } = new(); }

public class PickupAttemptItem
{
    [Required] public string ReturnId { get; set; } = string.Empty;
    public string? PickupDate { get; set; }
    public string? Reason { get; set; }
    public string? NewPickupDate { get; set; }
    public string? LocationId { get; set; }
}

// ── OTC Returns ───────────────────────────────────────────────────────────────

public class InitialReturnLoadRequest { [Required] public string SellerLocationId { get; set; } = string.Empty; public string TaskType { get; set; } = "RETURN"; }
public class StartOtcReturnRequest { [Required] public string SellerLocationId { get; set; } = string.Empty; public string TaskType { get; set; } = "RETURN"; }
public class ReturnTaskShipmentsRequest { [Required] public string TaskId { get; set; } = string.Empty; public string? FeId { get; set; } public int PageSize { get; set; } = 25; }
public class EndOtcReturnRequest { [Required] public string TaskId { get; set; } = string.Empty; public string? FeId { get; set; } public string TaskType { get; set; } = "RETURN"; }

// ── Responses ─────────────────────────────────────────────────────────────────

public class ReturnsResponse { public object? Data { get; set; } public string? Status { get; set; } }

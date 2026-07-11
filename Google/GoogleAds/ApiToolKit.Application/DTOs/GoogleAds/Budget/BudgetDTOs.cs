using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Budget;

public class ListBudgetsRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
}

public class BudgetResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long AmountMicros { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public bool ExplicitlyShared { get; set; }
}

public class CreateBudgetRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public long AmountMicros { get; set; }
    public string Period { get; set; } = "DAILY";
    public bool ExplicitlyShared { get; set; } = false;
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.AdAccount;

public class AdAccountResponse
{
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? Balance { get; set; }
    public int Status { get; set; }
    public string? TimezoneName { get; set; }
    public string? BusinessId { get; set; }
    public int? DisableReason { get; set; }
    public string? FundingSource { get; set; }
}

public class GetAdAccountsRequest
{
    [Required] public string UserId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,account_id,name,currency,balance,account_status,timezone_name,business";
}

public class AdAccountUsersRequest
{
    [Required] public string AccountId { get; set; } = string.Empty;
}

public class AdAccountUserResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Role { get; set; }
    public List<string> Permissions { get; set; } = new();
}

public class AdAccountBillingResponse
{
    public string AccountId { get; set; } = string.Empty;
    public string Balance { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? MinimumBudget { get; set; }
}

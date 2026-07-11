using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Customer;

public class GetCustomerRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
}

public class CustomerResponse
{
    public string ResourceName { get; set; } = string.Empty;
    public long Id { get; set; }
    public string DescriptiveName { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string TimeZone { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool TestAccount { get; set; }
}

public class ListAccessibleCustomersResponse
{
    public List<string> ResourceNames { get; set; } = new();
}

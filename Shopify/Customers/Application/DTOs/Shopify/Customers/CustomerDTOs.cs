using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopify.Customers;

public class AddressDto
{
    public string? Id { get; set; }
    public string? CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Company { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? Zip { get; set; }
    public string? Phone { get; set; }
    public string? CountryCode { get; set; }
    public string? ProvinceCode { get; set; }
    public bool? Default { get; set; }
}

public class CustomerDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public string? State { get; set; }
    public bool? VerifiedEmail { get; set; }
    public bool? TaxExempt { get; set; }
    public bool? AcceptsMarketing { get; set; }
    public int? OrdersCount { get; set; }
    public string? TotalSpent { get; set; }
    public string? Currency { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public AddressDto? DefaultAddress { get; set; }
    public List<AddressDto>? Addresses { get; set; }
}

public class OrderSummaryDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? TotalPrice { get; set; }
    public string? FinancialStatus { get; set; }
    public string? FulfillmentStatus { get; set; }
    public string? CreatedAt { get; set; }
}

public class ListCustomersResponse { public List<CustomerDto>? Customers { get; set; } }
public class CustomerResponse { public CustomerDto? Customer { get; set; } }
public class ListOrdersResponse { public List<OrderSummaryDto>? Orders { get; set; } }
public class AccountActivationUrlResponse { public string? AccountActivationUrl { get; set; } }

public class CreateCustomerRequest
{
    [Required] public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public bool? VerifiedEmail { get; set; }
    public bool? SendEmailInvite { get; set; }
    public AddressDto? Address { get; set; }
}

public class UpdateCustomerRequest
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public bool? AcceptsMarketing { get; set; }
    public bool? TaxExempt { get; set; }
}

public class SendInviteRequest { public string? To { get; set; } public string? From { get; set; } public string? Subject { get; set; } public string? CustomMessage { get; set; } }

public class ListAddressesResponse { public List<AddressDto>? Addresses { get; set; } }
public class AddressResponse { public AddressDto? CustomerAddress { get; set; } }
public class CreateAddressRequest
{
    [Required] public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    [Required] public string City { get; set; } = string.Empty;
    [Required] public string Country { get; set; } = string.Empty;
    public string? Zip { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Province { get; set; }
    public string? CountryCode { get; set; }
}
public class UpdateAddressRequest { public string? Address1 { get; set; } public string? City { get; set; } public string? Province { get; set; } public string? Country { get; set; } public string? Zip { get; set; } public string? Phone { get; set; } }

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Shopify.Storefront;

// ---- Product DTOs ----
public class StorefrontImage { public string? Id { get; set; } public string? Url { get; set; } public string? AltText { get; set; } }
public class MoneyDto { public string? Amount { get; set; } public string? CurrencyCode { get; set; } }
public class PriceRangeDto { public MoneyDto? MinVariantPrice { get; set; } public MoneyDto? MaxVariantPrice { get; set; } }
public class StorefrontVariantDto { public string? Id { get; set; } public string? Title { get; set; } public MoneyDto? Price { get; set; } public bool? AvailableForSale { get; set; } public string? Sku { get; set; } }
public class StorefrontProductDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Handle { get; set; }
    public string? Description { get; set; }
    public string? DescriptionHtml { get; set; }
    public string? ProductType { get; set; }
    public string? Vendor { get; set; }
    public string? Tags { get; set; }
    public bool? AvailableForSale { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public PriceRangeDto? PriceRange { get; set; }
    public StorefrontImage? FeaturedImage { get; set; }
    public List<StorefrontVariantDto>? Variants { get; set; }
    public List<StorefrontImage>? Images { get; set; }
}
public class PageInfoDto { public bool? HasNextPage { get; set; } public bool? HasPreviousPage { get; set; } public string? EndCursor { get; set; } public string? StartCursor { get; set; } }
public class StorefrontProductsResponse { public List<StorefrontProductDto>? Products { get; set; } public PageInfoDto? PageInfo { get; set; } }
public class StorefrontProductResponse { public StorefrontProductDto? Product { get; set; } }

public class StorefrontCollectionDto { public string? Id { get; set; } public string? Title { get; set; } public string? Handle { get; set; } public string? Description { get; set; } public List<StorefrontProductDto>? Products { get; set; } }
public class StorefrontCollectionsResponse { public List<StorefrontCollectionDto>? Collections { get; set; } }
public class StorefrontCollectionResponse { public StorefrontCollectionDto? Collection { get; set; } }

// ---- Cart DTOs ----
public class CartLineInput { [Required] public string MerchandiseId { get; set; } = string.Empty; public int Quantity { get; set; } = 1; }
public class CartLineUpdateInput { [Required] public string Id { get; set; } = string.Empty; public int Quantity { get; set; } }
public class BuyerIdentityInput { public string? Email { get; set; } public string? Phone { get; set; } public string? CustomerAccessToken { get; set; } }
public class CartLineDto { public string? Id { get; set; } public int? Quantity { get; set; } public MoneyDto? Cost { get; set; } }
public class CartCostDto { public MoneyDto? TotalAmount { get; set; } public MoneyDto? SubtotalAmount { get; set; } public MoneyDto? TotalTaxAmount { get; set; } }
public class CartDto
{
    public string? Id { get; set; }
    public string? CheckoutUrl { get; set; }
    public int? TotalQuantity { get; set; }
    public CartCostDto? Cost { get; set; }
    public List<CartLineDto>? Lines { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}
public class CartResponse { public CartDto? Cart { get; set; } public List<string>? UserErrors { get; set; } }
public class CreateCartRequest { public List<CartLineInput>? Lines { get; set; } public BuyerIdentityInput? BuyerIdentity { get; set; } public string? Note { get; set; } }
public class AddCartLinesRequest { [Required] public List<CartLineInput> Lines { get; set; } = new(); }
public class UpdateCartLinesRequest { [Required] public List<CartLineUpdateInput> Lines { get; set; } = new(); }
public class RemoveCartLinesRequest { [Required] public List<string> LineIds { get; set; } = new(); }
public class CartBuyerIdentityRequest { [Required] public BuyerIdentityInput BuyerIdentity { get; set; } = new(); }

// ---- Customer DTOs ----
public class CustomerLoginRequest { [Required] public string Email { get; set; } = string.Empty; [Required] public string Password { get; set; } = string.Empty; }
public class CustomerAccessTokenResponse { public string? CustomerAccessToken { get; set; } public string? ExpiresAt { get; set; } public List<string>? UserErrors { get; set; } }
public class RegisterCustomerRequest { [Required] public string Email { get; set; } = string.Empty; [Required] public string Password { get; set; } = string.Empty; public string? FirstName { get; set; } public string? LastName { get; set; } public string? Phone { get; set; } public bool? AcceptsMarketing { get; set; } }
public class StorefrontCustomerDto { public string? Id { get; set; } public string? Email { get; set; } public string? FirstName { get; set; } public string? LastName { get; set; } public string? Phone { get; set; } public string? CreatedAt { get; set; } }
public class StorefrontCustomerResponse { public StorefrontCustomerDto? Customer { get; set; } public List<string>? UserErrors { get; set; } }
public class CreateCustomerResponse { public StorefrontCustomerDto? Customer { get; set; } public List<string>? UserErrors { get; set; } }

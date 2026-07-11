using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.Commerce;

public record FbShop([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name, [property:JsonProperty("description")] string? Description, [property:JsonProperty("status")] string? Status);
public record FbShopsResponse([property:JsonProperty("data")] List<FbShop>? Data);
public record BuyerDetails([property:JsonProperty("name")] string? Name, [property:JsonProperty("email")] string? Email, [property:JsonProperty("phone")] string? Phone);
public record OrderItem([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name, [property:JsonProperty("quantity")] int? Quantity, [property:JsonProperty("price_amount")] decimal? PriceAmount, [property:JsonProperty("currency")] string? Currency, [property:JsonProperty("retailer_id")] string? RetailerId);
public record ShippingAddress([property:JsonProperty("street1")] string? Street1, [property:JsonProperty("street2")] string? Street2, [property:JsonProperty("city")] string? City, [property:JsonProperty("state")] string? State, [property:JsonProperty("postal_code")] string? PostalCode, [property:JsonProperty("country")] string? Country);
public record EstimatedPaymentDetails([property:JsonProperty("subtotal")] decimal? Subtotal, [property:JsonProperty("tax")] decimal? Tax, [property:JsonProperty("total_price")] decimal? TotalPrice, [property:JsonProperty("currency")] string? Currency);
public record CommerceOrder([property:JsonProperty("id")] string? Id, [property:JsonProperty("order_status")] OrderStatus? OrderStatus, [property:JsonProperty("buyer_details")] BuyerDetails? BuyerDetails, [property:JsonProperty("items")] OrderItemsWrapper? Items, [property:JsonProperty("shipping_address")] ShippingAddress? ShippingAddress, [property:JsonProperty("estimated_payment_details")] EstimatedPaymentDetails? EstimatedPaymentDetails, [property:JsonProperty("created")] string? Created, [property:JsonProperty("last_updated")] string? LastUpdated);
public record OrderStatus([property:JsonProperty("state")] string? State);
public record OrderItemsWrapper([property:JsonProperty("data")] List<OrderItem>? Data);
public record CommerceOrdersResponse([property:JsonProperty("data")] List<CommerceOrder>? Data);
public record AcknowledgeOrderRequest(string MerchantOrderReference);
public record ShipmentItem(string RetailerId, int Quantity);
public record AddShipmentRequest(string TrackingNumber, string CarrierName, string? ShippingMethod = null, List<ShipmentItem>? Items = null);
public record CancelOrderRequest(string ReasonCode, string? ReasonDescription = null);
public record RefundOrderRequest(decimal RefundAmount, string Currency, string ReasonText);
public record CommerceAccount([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name);
public record CommerceSettings([property:JsonProperty("checkout_enabled")] bool? CheckoutEnabled, [property:JsonProperty("is_shop_enabled")] bool? IsShopEnabled);
public record TaxSettings([property:JsonProperty("countries_with_tax_remittance")] string? CountriesWithTaxRemittance, [property:JsonProperty("disable_payment_tax")] bool? DisablePaymentTax);
public record UpdateTaxSettingsRequest(bool DisablePaymentTax);
public record ReturnSettings([property:JsonProperty("return_window_days")] int? ReturnWindowDays, [property:JsonProperty("is_enabled")] bool? IsEnabled);
public record UpdateReturnSettingsRequest(int ReturnWindowDays, bool IsEnabled, string? ReturnShippingOption = null);
public record ShippingProfile([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name, [property:JsonProperty("origin_country")] string? OriginCountry);
public record ShippingProfilesResponse([property:JsonProperty("data")] List<ShippingProfile>? Data);
public record OrderFulfillment([property:JsonProperty("id")] string? Id, [property:JsonProperty("status")] string? Status, [property:JsonProperty("created_time")] string? CreatedTime);
public record FulfillmentsResponse([property:JsonProperty("data")] List<OrderFulfillment>? Data);
public record OrderReturn([property:JsonProperty("id")] string? Id, [property:JsonProperty("status")] string? Status, [property:JsonProperty("reason_code")] string? ReasonCode);
public record ReturnsResponse([property:JsonProperty("data")] List<OrderReturn>? Data);
public record ShopCollection([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name);
public record ShopCollectionsResponse([property:JsonProperty("data")] List<ShopCollection>? Data);

// ── Shop setup ──────────────────────────────────────────────────────────────
public record CreateShopRequest(
    [property:JsonProperty("page_id")] string PageId,
    [property:JsonProperty("catalog_id")] string CatalogId,
    string? Name = null,
    string? Description = null);

public record UpdateShopRequest(
    string ShopId,
    string? Name = null,
    string? Description = null,
    [property:JsonProperty("is_enabled")] bool? IsEnabled = null);

// ── Catalog connection ───────────────────────────────────────────────────────
public record ConnectCatalogRequest(string CommerceAccountId, string CatalogId);

public record ConnectedCatalog(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("vertical")] string? Vertical,
    [property:JsonProperty("product_count")] int? ProductCount);

public record ConnectedCatalogsResponse([property:JsonProperty("data")] List<ConnectedCatalog>? Data);

// ── Collections ──────────────────────────────────────────────────────────────
public record CreateCollectionRequest(
    string CommerceAccountId,
    string Name,
    string? Description = null,
    string? ProductSetId = null,
    [property:JsonProperty("cover_image_url")] string? CoverImageUrl = null);

public record UpdateCollectionRequest(
    string CollectionId,
    string? Name = null,
    string? Description = null,
    [property:JsonProperty("cover_image_url")] string? CoverImageUrl = null);

public record CollectionResponse(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("description")] string? Description,
    [property:JsonProperty("product_set_id")] string? ProductSetId,
    [property:JsonProperty("cover_image_url")] string? CoverImageUrl);

public record CollectionProductsResponse([property:JsonProperty("data")] List<CollectionProduct>? Data);

public record CollectionProduct(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("price")] string? Price,
    [property:JsonProperty("image_url")] string? ImageUrl,
    [property:JsonProperty("availability")] string? Availability,
    [property:JsonProperty("retailer_id")] string? RetailerId);

public record AddProductToCollectionRequest(string CollectionId, string ProductId);
public record RemoveProductFromCollectionRequest(string CollectionId, string ProductId);

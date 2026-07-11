using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.InstagramShopping;

public record IgProductCatalog(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("vertical")] string? Vertical);

public record IgProductTag(
    [property:JsonProperty("product_id")] string? ProductId,
    [property:JsonProperty("merchant_id")] string? MerchantId,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("image_url")] string? ImageUrl,
    [property:JsonProperty("price_string")] string? PriceString,
    [property:JsonProperty("currency_code")] string? CurrencyCode,
    [property:JsonProperty("x")] float? X,
    [property:JsonProperty("y")] float? Y);

public record IgProductTagsResponse([property:JsonProperty("data")] List<IgProductTag>? Data);

public record IgProductTagItem(string ProductId, float? X = null, float? Y = null);
public record TagProductsRequest(List<IgProductTagItem> Tags);

public record IgTaggedMedia(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("media_type")] string? MediaType,
    [property:JsonProperty("media_url")] string? MediaUrl,
    [property:JsonProperty("timestamp")] string? Timestamp,
    [property:JsonProperty("permalink")] string? Permalink);

public record IgTaggedMediaResponse([property:JsonProperty("data")] List<IgTaggedMedia>? Data);

public record ProductAppealRequest(string ProductId, string? AppealReason = null);

public record IgShoppingEligibility(
    [property:JsonProperty("shopping_product_tag_eligibility")] bool? ShoppingProductTagEligibility);

public record IgCatalogProduct(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("description")] string? Description,
    [property:JsonProperty("price_amount")] string? PriceAmount,
    [property:JsonProperty("currency")] string? Currency,
    [property:JsonProperty("availability")] string? Availability,
    [property:JsonProperty("image_url")] string? ImageUrl,
    [property:JsonProperty("retailer_id")] string? RetailerId,
    [property:JsonProperty("review_status")] string? ReviewStatus);

public record IgCatalogProductsResponse([property:JsonProperty("data")] List<IgCatalogProduct>? Data);

public record IgShop(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("status")] string? Status);

public record IgShopsResponse([property:JsonProperty("data")] List<IgShop>? Data);

public record AvailableCatalog(
    [property:JsonProperty("catalog_id")] string? CatalogId,
    [property:JsonProperty("catalog_name")] string? CatalogName,
    [property:JsonProperty("catalog_source")] string? CatalogSource);

public record AvailableCatalogsResponse([property:JsonProperty("data")] List<AvailableCatalog>? Data);

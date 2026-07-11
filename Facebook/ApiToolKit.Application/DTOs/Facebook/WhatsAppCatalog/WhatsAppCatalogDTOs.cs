using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.WhatsAppCatalog;

public record WabaCatalog(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("vertical")] string? Vertical);

public record WabaCatalogsResponse([property:JsonProperty("data")] List<WabaCatalog>? Data);
public record CreateWabaCatalogRequest(string Name, string Vertical);

public record WabaCatalogProduct(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("description")] string? Description,
    [property:JsonProperty("price")] string? Price,
    [property:JsonProperty("currency")] string? Currency,
    [property:JsonProperty("availability")] string? Availability,
    [property:JsonProperty("condition")] string? Condition,
    [property:JsonProperty("image_url")] string? ImageUrl,
    [property:JsonProperty("retailer_id")] string? RetailerId,
    [property:JsonProperty("url")] string? Url);

public record WabaCatalogProductsResponse([property:JsonProperty("data")] List<WabaCatalogProduct>? Data);

public record CreateWabaCatalogProductRequest(
    string Name,
    string Description,
    string Price,
    string Currency,
    string Availability,
    string Condition,
    string? ImageUrl = null,
    string? Brand = null,
    string? RetailerId = null,
    string? Url = null);

public record UpdateWabaCatalogProductRequest(
    string? Name = null,
    string? Description = null,
    string? Price = null,
    string? Availability = null,
    string? ImageUrl = null);

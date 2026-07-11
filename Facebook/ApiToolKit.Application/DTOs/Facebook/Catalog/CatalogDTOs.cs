using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Catalog;

public class CreateCatalogRequest
{
    [Required] public string BusinessId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string VerticalType { get; set; } = "commerce";
}

public class CreateCatalogResponse
{
    public string Id { get; set; } = string.Empty;
}

public class CatalogResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? BusinessId { get; set; }
    public string? VerticalType { get; set; }
    public int ProductCount { get; set; }
    public string? CreatedTime { get; set; }
}

public class GetCatalogsRequest
{
    [Required] public string BusinessId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class CatalogPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetCatalogsResponse
{
    public List<CatalogResponse> Data { get; set; } = new();
    public CatalogPaging? Paging { get; set; }
}

public class CreateProductRequest
{
    [Required] public string CatalogId { get; set; } = string.Empty;
    [Required] public string RetailerId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public string? Brand { get; set; }
    public string? Price { get; set; }
    public string? Currency { get; set; }
    public string Availability { get; set; } = "in stock";
    public string Condition { get; set; } = "new";
    public string? Gtin { get; set; }
    public string? Mpn { get; set; }
}

public class CreateProductResponse
{
    public string Id { get; set; } = string.Empty;
}

public class ProductResponse
{
    public string Id { get; set; } = string.Empty;
    public string? RetailerId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public string? Brand { get; set; }
    public string? Price { get; set; }
    public string? Currency { get; set; }
    public string? Availability { get; set; }
    public string? Condition { get; set; }
}

public class GetProductsRequest
{
    [Required] public string CatalogId { get; set; } = string.Empty;
    public string? Filter { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,retailer_id,name,description,url,image_url,brand,price,currency,availability,condition";
}

public class ProductPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetProductsResponse
{
    public List<ProductResponse> Data { get; set; } = new();
    public ProductPaging? Paging { get; set; }
}

public class UpdateProductRequest
{
    [Required] public string ProductId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Price { get; set; }
    public string? Availability { get; set; }
}

public class CreateProductSetRequest
{
    [Required] public string CatalogId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public object? Filter { get; set; }
}

public class ProductSetResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public object? Filter { get; set; }
    public int ProductCount { get; set; }
}

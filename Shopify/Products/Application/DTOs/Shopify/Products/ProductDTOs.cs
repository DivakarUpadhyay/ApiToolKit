using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Shopify.Products;

// ---- Product DTOs ----
public class ProductOption
{
    public string? Name { get; set; }
    public List<string>? Values { get; set; }
}

public class ProductImage
{
    public string? Id { get; set; }
    public string? Src { get; set; }
    public int? Position { get; set; }
    public List<string>? VariantIds { get; set; }
}

public class ProductVariantDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Price { get; set; }
    public string? Sku { get; set; }
    public int? Position { get; set; }
    public string? InventoryPolicy { get; set; }
    public string? FulfillmentService { get; set; }
    public string? InventoryManagement { get; set; }
    public string? Option1 { get; set; }
    public string? Option2 { get; set; }
    public string? Option3 { get; set; }
    public int? InventoryQuantity { get; set; }
    public bool? Taxable { get; set; }
    public string? Barcode { get; set; }
    public double? Grams { get; set; }
    public string? WeightUnit { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class ProductDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? BodyHtml { get; set; }
    public string? Vendor { get; set; }
    public string? ProductType { get; set; }
    public string? Status { get; set; }
    public string? Tags { get; set; }
    public string? Handle { get; set; }
    public string? PublishedAt { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public List<ProductOption>? Options { get; set; }
    public List<ProductVariantDto>? Variants { get; set; }
    public List<ProductImage>? Images { get; set; }
}

public class ListProductsResponse { public List<ProductDto>? Products { get; set; } }
public class ProductResponse { public ProductDto? Product { get; set; } }

public class CreateProductRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? BodyHtml { get; set; }
    public string? Vendor { get; set; }
    public string? ProductType { get; set; }
    public string? Status { get; set; } = "draft";
    public string? Tags { get; set; }
    public List<ProductOption>? Options { get; set; }
    public List<ProductVariantDto>? Variants { get; set; }
    public List<ProductImage>? Images { get; set; }
}

public class UpdateProductRequest
{
    public string? Title { get; set; }
    public string? BodyHtml { get; set; }
    public string? Vendor { get; set; }
    public string? ProductType { get; set; }
    public string? Status { get; set; }
    public string? Tags { get; set; }
}

// ---- Variant DTOs ----
public class VariantDto
{
    public string? Id { get; set; }
    public string? ProductId { get; set; }
    public string? Title { get; set; }
    public string? Price { get; set; }
    public string? Sku { get; set; }
    public string? Option1 { get; set; }
    public string? Option2 { get; set; }
    public string? Option3 { get; set; }
    public int? InventoryQuantity { get; set; }
    public bool? Taxable { get; set; }
    public string? Barcode { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class ListVariantsResponse { public List<VariantDto>? Variants { get; set; } }
public class VariantResponse { public VariantDto? Variant { get; set; } }

public class CreateVariantRequest
{
    [Required]
    public string? Price { get; set; }
    public string? Sku { get; set; }
    public string? Option1 { get; set; }
    public string? Option2 { get; set; }
    public bool? Taxable { get; set; }
    public string? Barcode { get; set; }
}

public class UpdateVariantRequest
{
    public string? Price { get; set; }
    public string? Sku { get; set; }
    public int? InventoryQuantity { get; set; }
    public bool? Taxable { get; set; }
    public string? Barcode { get; set; }
}

// ---- Collection DTOs ----
public class CollectionDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Handle { get; set; }
    public string? BodyHtml { get; set; }
    public string? PublishedAt { get; set; }
    public string? SortOrder { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class ListCollectionsResponse
{
    public List<CollectionDto>? CustomCollections { get; set; }
    public List<CollectionDto>? SmartCollections { get; set; }
}

public class CollectionResponse
{
    public CollectionDto? CustomCollection { get; set; }
    public CollectionDto? SmartCollection { get; set; }
}

public class CreateCollectionRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? BodyHtml { get; set; }
    public string? SortOrder { get; set; }
}

public class UpdateCollectionRequest
{
    public string? Title { get; set; }
    public string? BodyHtml { get; set; }
    public string? SortOrder { get; set; }
}

// ---- Image DTOs ----
public class ImageDto
{
    public string? Id { get; set; }
    public string? ProductId { get; set; }
    public string? Src { get; set; }
    public int? Position { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class ListImagesResponse { public List<ImageDto>? Images { get; set; } }
public class ImageResponse { public ImageDto? Image { get; set; } }

public class CreateImageRequest
{
    [Required]
    public string Src { get; set; } = string.Empty;
    public int? Position { get; set; }
    public string? Alt { get; set; }
}

public class UpdateImageRequest
{
    public int? Position { get; set; }
    public string? Alt { get; set; }
}

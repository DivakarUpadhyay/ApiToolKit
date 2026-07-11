using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyStorefrontProductService : IShopifyStorefrontProductService
{
    private readonly IShopifyStorefrontRepository _repo;
    private readonly IShopifyStorefrontApiClient _apiClient;

    public ShopifyStorefrontProductService(IShopifyStorefrontRepository repo, IShopifyStorefrontApiClient apiClient)
    {
        _repo = repo;
        _apiClient = apiClient;
    }

    private static List<StorefrontProductDto> MapProductEdges(JToken? edges)
    {
        var products = new List<StorefrontProductDto>();
        if (edges == null) return products;
        foreach (var edge in edges)
        {
            var node = edge["node"];
            if (node == null) continue;
            var product = new StorefrontProductDto
            {
                Id = node["id"]?.ToString(),
                Title = node["title"]?.ToString(),
                Handle = node["handle"]?.ToString(),
                Description = node["description"]?.ToString(),
                DescriptionHtml = node["descriptionHtml"]?.ToString(),
                ProductType = node["productType"]?.ToString(),
                Vendor = node["vendor"]?.ToString(),
                Tags = node["tags"]?.ToString(),
                AvailableForSale = node["availableForSale"]?.ToObject<bool?>(),
                CreatedAt = node["createdAt"]?.ToString(),
                UpdatedAt = node["updatedAt"]?.ToString(),
            };

            var pr = node["priceRange"];
            if (pr != null)
                product.PriceRange = new PriceRangeDto
                {
                    MinVariantPrice = pr["minVariantPrice"] != null ? new MoneyDto { Amount = pr["minVariantPrice"]?["amount"]?.ToString(), CurrencyCode = pr["minVariantPrice"]?["currencyCode"]?.ToString() } : null,
                    MaxVariantPrice = pr["maxVariantPrice"] != null ? new MoneyDto { Amount = pr["maxVariantPrice"]?["amount"]?.ToString(), CurrencyCode = pr["maxVariantPrice"]?["currencyCode"]?.ToString() } : null
                };

            var fi = node["featuredImage"];
            if (fi != null)
                product.FeaturedImage = new StorefrontImage { Id = fi["id"]?.ToString(), Url = fi["url"]?.ToString(), AltText = fi["altText"]?.ToString() };

            var variantEdges = node["variants"]?["edges"];
            if (variantEdges != null)
                product.Variants = variantEdges.Select(ve => new StorefrontVariantDto
                {
                    Id = ve["node"]?["id"]?.ToString(),
                    Title = ve["node"]?["title"]?.ToString(),
                    Sku = ve["node"]?["sku"]?.ToString(),
                    AvailableForSale = ve["node"]?["availableForSale"]?.ToObject<bool?>(),
                    Price = ve["node"]?["price"] != null ? new MoneyDto { Amount = ve["node"]?["price"]?["amount"]?.ToString(), CurrencyCode = ve["node"]?["price"]?["currencyCode"]?.ToString() } : null
                }).ToList();

            var imageEdges = node["images"]?["edges"];
            if (imageEdges != null)
                product.Images = imageEdges.Select(ie => new StorefrontImage { Id = ie["node"]?["id"]?.ToString(), Url = ie["node"]?["url"]?.ToString(), AltText = ie["node"]?["altText"]?.ToString() }).ToList();

            products.Add(product);
        }
        return products;
    }

    public async Task<Result<StorefrontProductsResponse>> GetProductsAsync(string shopDomain, int first = 10, string? after = null)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<StorefrontProductsResponse>.Fail("Shopify Storefront configuration not found.");

        const string query = @"
query GetProducts($first: Int!, $after: String) {
  products(first: $first, after: $after) {
    pageInfo { hasNextPage hasPreviousPage endCursor startCursor }
    edges {
      node {
        id title handle description descriptionHtml productType vendor
        availableForSale createdAt updatedAt
        priceRange { minVariantPrice { amount currencyCode } maxVariantPrice { amount currencyCode } }
        featuredImage { id url altText }
        variants(first: 10) { edges { node { id title sku availableForSale price { amount currencyCode } } } }
        images(first: 5) { edges { node { id url altText } } }
      }
    }
  }
}";

        var variables = new { first, after };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetProducts",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"] != null,
            StatusCode = response?["data"] != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<StorefrontProductsResponse>.Fail("No response from Shopify Storefront API.");

        var productsData = response["data"]?["products"];
        var pageInfo = productsData?["pageInfo"];
        var result = new StorefrontProductsResponse
        {
            Products = MapProductEdges(productsData?["edges"]),
            PageInfo = pageInfo != null ? new PageInfoDto
            {
                HasNextPage = pageInfo["hasNextPage"]?.ToObject<bool?>(),
                HasPreviousPage = pageInfo["hasPreviousPage"]?.ToObject<bool?>(),
                EndCursor = pageInfo["endCursor"]?.ToString(),
                StartCursor = pageInfo["startCursor"]?.ToString()
            } : null
        };

        return Result<StorefrontProductsResponse>.Ok(result);
    }

    public async Task<Result<StorefrontProductResponse>> GetProductByHandleAsync(string shopDomain, string handle)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<StorefrontProductResponse>.Fail("Shopify Storefront configuration not found.");

        const string query = @"
query GetProductByHandle($handle: String!) {
  product(handle: $handle) {
    id title handle description descriptionHtml productType vendor
    availableForSale createdAt updatedAt
    priceRange { minVariantPrice { amount currencyCode } maxVariantPrice { amount currencyCode } }
    featuredImage { id url altText }
    variants(first: 10) { edges { node { id title sku availableForSale price { amount currencyCode } } } }
    images(first: 5) { edges { node { id url altText } } }
  }
}";

        var variables = new { handle };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetProductByHandle",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["product"] != null,
            StatusCode = response?["data"]?["product"] != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<StorefrontProductResponse>.Fail("No response from Shopify Storefront API.");

        var productNode = response["data"]?["product"];
        if (productNode == null)
            return Result<StorefrontProductResponse>.Fail($"Product with handle '{handle}' not found.");

        var product = MapProductEdges(new JArray { new JObject { ["node"] = productNode } }).FirstOrDefault();
        return Result<StorefrontProductResponse>.Ok(new StorefrontProductResponse { Product = product });
    }

    public async Task<Result<StorefrontProductResponse>> GetProductByIdAsync(string shopDomain, string productId)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<StorefrontProductResponse>.Fail("Shopify Storefront configuration not found.");

        const string query = @"
query GetProductById($id: ID!) {
  product(id: $id) {
    id title handle description descriptionHtml productType vendor
    availableForSale createdAt updatedAt
    priceRange { minVariantPrice { amount currencyCode } maxVariantPrice { amount currencyCode } }
    featuredImage { id url altText }
    variants(first: 10) { edges { node { id title sku availableForSale price { amount currencyCode } } } }
    images(first: 5) { edges { node { id url altText } } }
  }
}";

        var variables = new { id = productId };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetProductById",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["product"] != null,
            StatusCode = response?["data"]?["product"] != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<StorefrontProductResponse>.Fail("No response from Shopify Storefront API.");

        var productNode = response["data"]?["product"];
        if (productNode == null)
            return Result<StorefrontProductResponse>.Fail($"Product with ID '{productId}' not found.");

        var product = MapProductEdges(new JArray { new JObject { ["node"] = productNode } }).FirstOrDefault();
        return Result<StorefrontProductResponse>.Ok(new StorefrontProductResponse { Product = product });
    }

    public async Task<Result<StorefrontCollectionsResponse>> GetCollectionsAsync(string shopDomain, int first = 10)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<StorefrontCollectionsResponse>.Fail("Shopify Storefront configuration not found.");

        const string query = @"
query GetCollections($first: Int!) {
  collections(first: $first) {
    edges {
      node {
        id title handle description
        products(first: 10) {
          edges {
            node {
              id title handle availableForSale
              priceRange { minVariantPrice { amount currencyCode } }
              featuredImage { id url altText }
            }
          }
        }
      }
    }
  }
}";

        var variables = new { first };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetCollections",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"] != null,
            StatusCode = response?["data"] != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<StorefrontCollectionsResponse>.Fail("No response from Shopify Storefront API.");

        var collectionEdges = response["data"]?["collections"]?["edges"];
        var collections = new List<StorefrontCollectionDto>();
        if (collectionEdges != null)
        {
            foreach (var edge in collectionEdges)
            {
                var node = edge["node"];
                if (node == null) continue;
                collections.Add(new StorefrontCollectionDto
                {
                    Id = node["id"]?.ToString(),
                    Title = node["title"]?.ToString(),
                    Handle = node["handle"]?.ToString(),
                    Description = node["description"]?.ToString(),
                    Products = MapProductEdges(node["products"]?["edges"])
                });
            }
        }

        return Result<StorefrontCollectionsResponse>.Ok(new StorefrontCollectionsResponse { Collections = collections });
    }

    public async Task<Result<StorefrontCollectionResponse>> GetCollectionByHandleAsync(string shopDomain, string handle)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<StorefrontCollectionResponse>.Fail("Shopify Storefront configuration not found.");

        const string query = @"
query GetCollectionByHandle($handle: String!) {
  collection(handle: $handle) {
    id title handle description
    products(first: 20) {
      edges {
        node {
          id title handle availableForSale
          priceRange { minVariantPrice { amount currencyCode } }
          featuredImage { id url altText }
          variants(first: 10) { edges { node { id title sku availableForSale price { amount currencyCode } } } }
        }
      }
    }
  }
}";

        var variables = new { handle };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetCollectionByHandle",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["collection"] != null,
            StatusCode = response?["data"]?["collection"] != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<StorefrontCollectionResponse>.Fail("No response from Shopify Storefront API.");

        var collectionNode = response["data"]?["collection"];
        if (collectionNode == null)
            return Result<StorefrontCollectionResponse>.Fail($"Collection with handle '{handle}' not found.");

        var collection = new StorefrontCollectionDto
        {
            Id = collectionNode["id"]?.ToString(),
            Title = collectionNode["title"]?.ToString(),
            Handle = collectionNode["handle"]?.ToString(),
            Description = collectionNode["description"]?.ToString(),
            Products = MapProductEdges(collectionNode["products"]?["edges"])
        };

        return Result<StorefrontCollectionResponse>.Ok(new StorefrontCollectionResponse { Collection = collection });
    }
}

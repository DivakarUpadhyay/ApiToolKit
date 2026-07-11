using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyCartService : IShopifyCartService
{
    private readonly IShopifyStorefrontRepository _repo;
    private readonly IShopifyStorefrontApiClient _apiClient;

    public ShopifyCartService(IShopifyStorefrontRepository repo, IShopifyStorefrontApiClient apiClient)
    {
        _repo = repo;
        _apiClient = apiClient;
    }

    private const string CartFragment = @"
cart {
  id
  checkoutUrl
  totalQuantity
  createdAt
  updatedAt
  cost {
    totalAmount { amount currencyCode }
    subtotalAmount { amount currencyCode }
    totalTaxAmount { amount currencyCode }
  }
  lines(first: 20) {
    edges {
      node {
        id
        quantity
        cost { totalAmount { amount currencyCode } }
      }
    }
  }
}";

    private static CartResponse MapCartResponse(JToken? data, string mutationKey)
    {
        var mutationData = data?[mutationKey];
        var cartNode = mutationData?["cart"];
        var userErrors = mutationData?["userErrors"]?.ToObject<List<JObject>>()
            ?.Select(e => e["message"]?.ToString() ?? "").ToList();

        return new CartResponse
        {
            Cart = cartNode != null ? MapCart(cartNode) : null,
            UserErrors = userErrors
        };
    }

    private static CartDto? MapCart(JToken? cartNode)
    {
        if (cartNode == null) return null;
        var lineEdges = cartNode["lines"]?["edges"];
        var lines = lineEdges?.Select(e => new CartLineDto
        {
            Id = e["node"]?["id"]?.ToString(),
            Quantity = e["node"]?["quantity"]?.ToObject<int?>(),
            Cost = e["node"]?["cost"]?["totalAmount"] != null ? new MoneyDto
            {
                Amount = e["node"]?["cost"]?["totalAmount"]?["amount"]?.ToString(),
                CurrencyCode = e["node"]?["cost"]?["totalAmount"]?["currencyCode"]?.ToString()
            } : null
        }).ToList();

        var cost = cartNode["cost"];
        return new CartDto
        {
            Id = cartNode["id"]?.ToString(),
            CheckoutUrl = cartNode["checkoutUrl"]?.ToString(),
            TotalQuantity = cartNode["totalQuantity"]?.ToObject<int?>(),
            CreatedAt = cartNode["createdAt"]?.ToString(),
            UpdatedAt = cartNode["updatedAt"]?.ToString(),
            Cost = cost != null ? new CartCostDto
            {
                TotalAmount = cost["totalAmount"] != null ? new MoneyDto { Amount = cost["totalAmount"]?["amount"]?.ToString(), CurrencyCode = cost["totalAmount"]?["currencyCode"]?.ToString() } : null,
                SubtotalAmount = cost["subtotalAmount"] != null ? new MoneyDto { Amount = cost["subtotalAmount"]?["amount"]?.ToString(), CurrencyCode = cost["subtotalAmount"]?["currencyCode"]?.ToString() } : null,
                TotalTaxAmount = cost["totalTaxAmount"] != null ? new MoneyDto { Amount = cost["totalTaxAmount"]?["amount"]?.ToString(), CurrencyCode = cost["totalTaxAmount"]?["currencyCode"]?.ToString() } : null
            } : null,
            Lines = lines
        };
    }

    public async Task<Result<CartResponse>> CreateCartAsync(string shopDomain, CreateCartRequest request)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CartResponse>.Fail("Shopify Storefront configuration not found.");

        var mutation = $@"
mutation cartCreate($input: CartInput!) {{
  cartCreate(input: $input) {{
    {CartFragment}
    userErrors {{ field message }}
  }}
}}";

        var lineInputs = request.Lines?.Select(l => new { merchandiseId = l.MerchandiseId, quantity = l.Quantity }).ToList();
        var variables = new
        {
            input = new
            {
                lines = lineInputs,
                buyerIdentity = request.BuyerIdentity != null ? new
                {
                    email = request.BuyerIdentity.Email,
                    phone = request.BuyerIdentity.Phone,
                    customerAccessToken = request.BuyerIdentity.CustomerAccessToken
                } : null,
                note = request.Note
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateCart",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["cartCreate"]?["cart"] != null,
            StatusCode = response?["data"]?["cartCreate"]?["cart"] != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CartResponse>.Fail("No response from Shopify Storefront API.");

        return Result<CartResponse>.Ok(MapCartResponse(response["data"], "cartCreate"));
    }

    public async Task<Result<CartResponse>> GetCartAsync(string shopDomain, string cartId)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CartResponse>.Fail("Shopify Storefront configuration not found.");

        var query = $@"
query GetCart($cartId: ID!) {{
  cart(id: $cartId) {{
    id
    checkoutUrl
    totalQuantity
    createdAt
    updatedAt
    cost {{
      totalAmount {{ amount currencyCode }}
      subtotalAmount {{ amount currencyCode }}
      totalTaxAmount {{ amount currencyCode }}
    }}
    lines(first: 20) {{
      edges {{
        node {{
          id
          quantity
          cost {{ totalAmount {{ amount currencyCode }} }}
        }}
      }}
    }}
  }}
}}";

        var variables = new { cartId };
        var response = await _apiClient.QueryAsync<JObject>(shopDomain, query, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetCart",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["cart"] != null,
            StatusCode = response?["data"]?["cart"] != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CartResponse>.Fail("No response from Shopify Storefront API.");

        var cartNode = response["data"]?["cart"];
        if (cartNode == null)
            return Result<CartResponse>.Fail($"Cart '{cartId}' not found.");

        return Result<CartResponse>.Ok(new CartResponse { Cart = MapCart(cartNode) });
    }

    public async Task<Result<CartResponse>> AddCartLinesAsync(string shopDomain, string cartId, AddCartLinesRequest request)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CartResponse>.Fail("Shopify Storefront configuration not found.");

        var mutation = $@"
mutation cartLinesAdd($cartId: ID!, $lines: [CartLineInput!]!) {{
  cartLinesAdd(cartId: $cartId, lines: $lines) {{
    {CartFragment}
    userErrors {{ field message }}
  }}
}}";

        var variables = new
        {
            cartId,
            lines = request.Lines.Select(l => new { merchandiseId = l.MerchandiseId, quantity = l.Quantity }).ToList()
        };

        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "AddCartLines",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["cartLinesAdd"]?["cart"] != null,
            StatusCode = response?["data"]?["cartLinesAdd"]?["cart"] != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CartResponse>.Fail("No response from Shopify Storefront API.");

        return Result<CartResponse>.Ok(MapCartResponse(response["data"], "cartLinesAdd"));
    }

    public async Task<Result<CartResponse>> UpdateCartLinesAsync(string shopDomain, string cartId, UpdateCartLinesRequest request)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CartResponse>.Fail("Shopify Storefront configuration not found.");

        var mutation = $@"
mutation cartLinesUpdate($cartId: ID!, $lines: [CartLineUpdateInput!]!) {{
  cartLinesUpdate(cartId: $cartId, lines: $lines) {{
    {CartFragment}
    userErrors {{ field message }}
  }}
}}";

        var variables = new
        {
            cartId,
            lines = request.Lines.Select(l => new { id = l.Id, quantity = l.Quantity }).ToList()
        };

        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateCartLines",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["cartLinesUpdate"]?["cart"] != null,
            StatusCode = response?["data"]?["cartLinesUpdate"]?["cart"] != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CartResponse>.Fail("No response from Shopify Storefront API.");

        return Result<CartResponse>.Ok(MapCartResponse(response["data"], "cartLinesUpdate"));
    }

    public async Task<Result<CartResponse>> RemoveCartLinesAsync(string shopDomain, string cartId, RemoveCartLinesRequest request)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CartResponse>.Fail("Shopify Storefront configuration not found.");

        var mutation = $@"
mutation cartLinesRemove($cartId: ID!, $lineIds: [ID!]!) {{
  cartLinesRemove(cartId: $cartId, lineIds: $lineIds) {{
    {CartFragment}
    userErrors {{ field message }}
  }}
}}";

        var variables = new { cartId, lineIds = request.LineIds };

        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "RemoveCartLines",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["cartLinesRemove"]?["cart"] != null,
            StatusCode = response?["data"]?["cartLinesRemove"]?["cart"] != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CartResponse>.Fail("No response from Shopify Storefront API.");

        return Result<CartResponse>.Ok(MapCartResponse(response["data"], "cartLinesRemove"));
    }

    public async Task<Result<CartResponse>> UpdateCartBuyerIdentityAsync(string shopDomain, string cartId, CartBuyerIdentityRequest request)
    {
        var config = await _repo.GetConfigAsync(shopDomain);
        if (config == null)
            return Result<CartResponse>.Fail("Shopify Storefront configuration not found.");

        var mutation = $@"
mutation cartBuyerIdentityUpdate($cartId: ID!, $buyerIdentity: CartBuyerIdentityInput!) {{
  cartBuyerIdentityUpdate(cartId: $cartId, buyerIdentity: $buyerIdentity) {{
    {CartFragment}
    userErrors {{ field message }}
  }}
}}";

        var variables = new
        {
            cartId,
            buyerIdentity = new
            {
                email = request.BuyerIdentity.Email,
                phone = request.BuyerIdentity.Phone,
                customerAccessToken = request.BuyerIdentity.CustomerAccessToken
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(shopDomain, mutation, variables, config.StorefrontAccessToken);

        await _repo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateCartBuyerIdentity",
            RequestBody = JsonConvert.SerializeObject(variables),
            ResponseBody = response?.ToString(),
            IsSuccess = response?["data"]?["cartBuyerIdentityUpdate"]?["cart"] != null,
            StatusCode = response?["data"]?["cartBuyerIdentityUpdate"]?["cart"] != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null || response["data"] == null)
            return Result<CartResponse>.Fail("No response from Shopify Storefront API.");

        return Result<CartResponse>.Ok(MapCartResponse(response["data"], "cartBuyerIdentityUpdate"));
    }
}

using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopify.Marketing;

// ---- Price Rule DTOs ----
public class PriceRuleDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? TargetType { get; set; }
    public string? TargetSelection { get; set; }
    public string? AllocationMethod { get; set; }
    public string? ValueType { get; set; }
    public string? Value { get; set; }
    public string? CustomerSelection { get; set; }
    public string? OncePerCustomer { get; set; }
    public string? UsageLimit { get; set; }
    public string? StartsAt { get; set; }
    public string? EndsAt { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public List<string>? EntitledProductIds { get; set; }
    public List<string>? EntitledVariantIds { get; set; }
    public List<string>? EntitledCollectionIds { get; set; }
    public string? PrerequisiteSubtotalRange { get; set; }
    public string? PrerequisiteQuantityRange { get; set; }
}

public class ListPriceRulesResponse { public List<PriceRuleDto>? PriceRules { get; set; } }
public class PriceRuleResponse { public PriceRuleDto? PriceRule { get; set; } }

public class CreatePriceRuleRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string TargetType { get; set; } = "line_item";
    [Required] public string TargetSelection { get; set; } = "all";
    [Required] public string AllocationMethod { get; set; } = "across";
    [Required] public string ValueType { get; set; } = "percentage";
    [Required] public string Value { get; set; } = string.Empty;
    [Required] public string CustomerSelection { get; set; } = "all";
    [Required] public string StartsAt { get; set; } = string.Empty;
    public string? EndsAt { get; set; }
    public int? UsageLimit { get; set; }
    public bool? OncePerCustomer { get; set; }
    public List<string>? EntitledProductIds { get; set; }
    public List<string>? EntitledCollectionIds { get; set; }
}

public class UpdatePriceRuleRequest { public string? Title { get; set; } public string? Value { get; set; } public string? EndsAt { get; set; } public int? UsageLimit { get; set; } public bool? OncePerCustomer { get; set; } }

// ---- Discount Code DTOs ----
public class DiscountCodeDto { public string? Id { get; set; } public string? PriceRuleId { get; set; } public string? Code { get; set; } public int? UsageCount { get; set; } public string? CreatedAt { get; set; } public string? UpdatedAt { get; set; } }
public class ListDiscountCodesResponse { public List<DiscountCodeDto>? DiscountCodes { get; set; } }
public class DiscountCodeResponse { public DiscountCodeDto? DiscountCode { get; set; } }
public class CreateDiscountCodeRequest { [Required] public string Code { get; set; } = string.Empty; }
public class UpdateDiscountCodeRequest { [Required] public string Code { get; set; } = string.Empty; }
public class BatchDiscountCodeItem { [Required] public string Code { get; set; } = string.Empty; }
public class BatchDiscountCodeRequest { [Required] public List<BatchDiscountCodeItem> Codes { get; set; } = new(); }
public class BatchDiscountCodeDto { public string? Id { get; set; } public string? PriceRuleId { get; set; } public string? Status { get; set; } public int? CompletedCount { get; set; } public int? FailedCount { get; set; } public string? CreatedAt { get; set; } public string? StartedAt { get; set; } public string? CompletedAt { get; set; } }
public class BatchDiscountCodeResponse { public BatchDiscountCodeDto? DiscountCodeCreationJob { get; set; } }

// ---- Webhook DTOs ----
public class WebhookDto
{
    public string? Id { get; set; }
    public string? Topic { get; set; }
    public string? Address { get; set; }
    public string? Format { get; set; }
    public List<string>? Fields { get; set; }
    public List<string>? MetafieldNamespaces { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class ListWebhooksResponse { public List<WebhookDto>? Webhooks { get; set; } }
public class WebhookResponse { public WebhookDto? Webhook { get; set; } }

public class CreateWebhookRequest
{
    [Required] public string Topic { get; set; } = string.Empty;
    [Required] public string Address { get; set; } = string.Empty;
    public string? Format { get; set; } = "json";
    public List<string>? Fields { get; set; }
}

public class UpdateWebhookRequest { public string? Address { get; set; } public List<string>? Fields { get; set; } }

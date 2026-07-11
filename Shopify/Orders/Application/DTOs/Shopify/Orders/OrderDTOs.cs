using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Shopify.Orders;

// ---- Order DTOs ----
public class OrderAddress
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? Zip { get; set; }
    public string? Phone { get; set; }
    public string? CountryCode { get; set; }
    public string? ProvinceCode { get; set; }
}

public class LineItemDto
{
    public string? Id { get; set; }
    public string? VariantId { get; set; }
    public string? Title { get; set; }
    public int? Quantity { get; set; }
    public string? Price { get; set; }
    public string? Sku { get; set; }
    public string? Name { get; set; }
    public bool? RequiresShipping { get; set; }
    public bool? Taxable { get; set; }
    public string? FulfillmentStatus { get; set; }
}

public class OrderCustomer
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
}

public class OrderDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? FinancialStatus { get; set; }
    public string? FulfillmentStatus { get; set; }
    public string? TotalPrice { get; set; }
    public string? SubtotalPrice { get; set; }
    public string? TotalTax { get; set; }
    public string? Currency { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public string? CancelReason { get; set; }
    public string? CancelledAt { get; set; }
    public string? ClosedAt { get; set; }
    public string? ProcessedAt { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public OrderAddress? BillingAddress { get; set; }
    public OrderAddress? ShippingAddress { get; set; }
    public OrderCustomer? Customer { get; set; }
    public List<LineItemDto>? LineItems { get; set; }
}

public class ListOrdersResponse { public List<OrderDto>? Orders { get; set; } }
public class OrderResponse { public OrderDto? Order { get; set; } }

public class CreateOrderLineItem { [Required] public string? VariantId { get; set; } [Required] public int Quantity { get; set; } }
public class CreateOrderRequest
{
    [Required] public List<CreateOrderLineItem> LineItems { get; set; } = new();
    public string? Email { get; set; }
    public OrderAddress? BillingAddress { get; set; }
    public OrderAddress? ShippingAddress { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public bool? SendReceipt { get; set; }
    public bool? SendFulfillmentReceipt { get; set; }
}

public class UpdateOrderRequest
{
    public string? Email { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public OrderAddress? ShippingAddress { get; set; }
}

// ---- Transaction DTOs ----
public class TransactionDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
    public string? Kind { get; set; }
    public string? Gateway { get; set; }
    public string? Status { get; set; }
    public string? Amount { get; set; }
    public string? Currency { get; set; }
    public string? Authorization { get; set; }
    public string? CreatedAt { get; set; }
    public string? ProcessedAt { get; set; }
}
public class ListTransactionsResponse { public List<TransactionDto>? Transactions { get; set; } }
public class TransactionResponse { public TransactionDto? Transaction { get; set; } }
public class CreateTransactionRequest { [Required] public string Kind { get; set; } = string.Empty; public string? Amount { get; set; } public string? Currency { get; set; } public string? ParentId { get; set; } }

// ---- Refund DTOs ----
public class RefundLineItem { public string? LineItemId { get; set; } public int? Quantity { get; set; } public string? RestockType { get; set; } }
public class RefundDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
    public string? Note { get; set; }
    public string? CreatedAt { get; set; }
    public List<RefundLineItem>? RefundLineItems { get; set; }
    public List<TransactionDto>? Transactions { get; set; }
}
public class ListRefundsResponse { public List<RefundDto>? Refunds { get; set; } }
public class RefundResponse { public RefundDto? Refund { get; set; } }
public class CreateRefundRequest { public string? Note { get; set; } public bool? Notify { get; set; } public List<RefundLineItem>? RefundLineItems { get; set; } public List<CreateTransactionRequest>? Transactions { get; set; } }
public class CalculateRefundRequest { public bool? Shipping { get; set; } public List<RefundLineItem>? RefundLineItems { get; set; } }

// ---- Draft Order DTOs ----
public class DraftLineItem { [Required] public string? VariantId { get; set; } [Required] public int Quantity { get; set; } public string? Price { get; set; } }
public class DraftOrderDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? Email { get; set; }
    public string? TotalPrice { get; set; }
    public string? Currency { get; set; }
    public string? Note { get; set; }
    public string? Tags { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public List<DraftLineItem>? LineItems { get; set; }
    public OrderCustomer? Customer { get; set; }
    public OrderAddress? ShippingAddress { get; set; }
}
public class ListDraftOrdersResponse { public List<DraftOrderDto>? DraftOrders { get; set; } }
public class DraftOrderResponse { public DraftOrderDto? DraftOrder { get; set; } }
public class CreateDraftOrderRequest { [Required] public List<DraftLineItem> LineItems { get; set; } = new(); public string? Email { get; set; } public string? Note { get; set; } public string? Tags { get; set; } public OrderAddress? ShippingAddress { get; set; } }
public class UpdateDraftOrderRequest { public string? Email { get; set; } public string? Note { get; set; } public string? Tags { get; set; } public List<DraftLineItem>? LineItems { get; set; } }

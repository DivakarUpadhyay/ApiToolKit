using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Order;

public class GetOrderListRequest
{
    [Required] public long ShopId { get; set; }
    public long TimeRangeField { get; set; }
    public long TimeFrom { get; set; }
    public long TimeTo { get; set; }
    public string OrderStatus { get; set; } = "ALL";
    public int PageSize { get; set; } = 20;
    public string? Cursor { get; set; }
}

public class GetOrderDetailRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public List<string> OrderSnList { get; set; } = new();
    public string ResponseOptionalFields { get; set; } = "buyer_user_id,buyer_username,estimated_shipping_fee,recipient_address,actual_shipping_fee,goods_to_declare,note,note_update_time,item_list,pay_time,dropshipper,dropshipper_phone,split_up,buyer_cancel_reason,cancel_by,cancel_reason,actual_shipping_fee_confirmed,buyer_cpf_id,fulfillment_flag,pickup_done_time,package_list,shipping_carrier,payment_method,total_amount,invoice_data,no_plastic_packing,order_chargeable_weight_gram,edt";
}

public class CancelOrderRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    [Required] public string CancelReason { get; set; } = string.Empty;
    public long? ItemId { get; set; }
}

public class SetNoteRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    [Required] public string Note { get; set; } = string.Empty;
}

public class HandleBuyerCancellationRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OrderSn { get; set; } = string.Empty;
    [Required] public string Operation { get; set; } = string.Empty; // ACCEPT or REJECT
}

public class OrderResponse
{
    public string OrderSn { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
    public string BuyerUserId { get; set; } = string.Empty;
    public string BuyerUsername { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public long CreateTime { get; set; }
    public long UpdateTime { get; set; }
    public string Note { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public List<OrderItemResponse> ItemList { get; set; } = new();
    public RecipientAddressResponse? RecipientAddress { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class OrderItemResponse
{
    public string OrderItemId { get; set; } = string.Empty;
    public long ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public long ModelId { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public string ItemSku { get; set; } = string.Empty;
    public int ModelQuantityPurchased { get; set; }
    public decimal ModelDiscountedPrice { get; set; }
    public decimal ModelOriginalPrice { get; set; }
    public string ImageInfo { get; set; } = string.Empty;
}

public class RecipientAddressResponse
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Town { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
}

public class OrderListResponse
{
    public List<string> OrderSnList { get; set; } = new();
    public bool More { get; set; }
    public string? NextCursor { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

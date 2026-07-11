# Shopee Open API - Complete Documentation
**Base URL:** `https://localhost:{port}`  
**Swagger UI:** `https://localhost:{port}/swagger`

---

## 1. AUTH APIs

### 1.1 Get Authorization URL
**GET** `/api/shopee/auth/auth-url`

**Request:** No body required

**Response:**
```json
{
  "success": true,
  "data": {
    "authUrl": "https://partner.test-stable.shopeemobile.com/api/v2/shop/auth_partner?partner_id=123456&timestamp=1720000000&sign=abc123&redirect=https%3A%2F%2Fyourdomain.com%2Fcallback"
  }
}
```
> Open this URL in browser → user logs in → Shopee redirects back with `code` and `shop_id`.

---

### 1.2 Get Access Token
**POST** `/api/shopee/auth/token`

**Request Body:**
```json
{
  "code": "1a2b3c4d5e6f7g8h",
  "shopId": 123456789
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "accessToken": "IAA...",
    "expireIn": 14400,
    "refreshToken": "IRE...",
    "refreshTokenExpireIn": 2592000,
    "shopId": 123456789,
    "partnerId": 123456,
    "error": "",
    "message": "",
    "requestId": "abc123"
  }
}
```

---

### 1.3 Refresh Access Token
**POST** `/api/shopee/auth/token/refresh`

**Request Body:**
```json
{
  "refreshToken": "IRE...",
  "shopId": 123456789
}
```

**Response:** Same as 1.2

---

## 2. SHOP APIs

### 2.1 Get Shop Info
**GET** `/api/shopee/shop/{shopId}/info`

**Path Param:** `shopId` = 123456789

**Response:**
```json
{
  "success": true,
  "data": {
    "shopId": 123456789,
    "shopName": "My Shopee Store",
    "region": "MY",
    "shopStatus": "NORMAL",
    "shopDescription": "Best products at best prices",
    "avatar": "https://...",
    "openTime": 1620000000,
    "isOfficialShop": false,
    "isCbOption": false
  }
}
```

---

### 2.2 Get Shops By Partner
**GET** `/api/shopee/shop/list?pageNo=1&pageSize=20`

**Query Params:**
| Param    | Type | Default | Description          |
|----------|------|---------|----------------------|
| pageNo   | int  | 1       | Page number          |
| pageSize | int  | 20      | Items per page (max 100) |

**Response:**
```json
{
  "success": true,
  "data": {
    "shopList": [
      { "shopId": 123, "shopName": "Shop A", "region": "MY", "shopStatus": "NORMAL" }
    ],
    "totalCount": 1,
    "hasNextPage": false
  }
}
```

---

### 2.3 Get Shop Profile
**GET** `/api/shopee/shop/{shopId}/profile`

**Response:** Same as 2.1

---

### 2.4 Update Shop Profile
**PUT** `/api/shopee/shop/profile`

**Request Body:**
```json
{
  "shopId": 123456789,
  "shopName": "My Updated Store",
  "shopDescription": "Updated description",
  "avatar": "https://image-url.com/avatar.jpg"
}
```

**Response:**
```json
{ "success": true, "data": true }
```

---

## 3. PRODUCT APIs

### 3.1 Get Item List
**GET** `/api/shopee/product/list`

**Query Params:**
| Param          | Type   | Default  | Description                        |
|----------------|--------|----------|------------------------------------|
| shopId         | long   | required | Shop ID                            |
| offset         | int    | 0        | Pagination offset                  |
| pageSize       | int    | 50       | Items per page (max 100)           |
| itemStatus     | string | NORMAL   | NORMAL, BANNED, DELETED, UNLIST    |
| updateTimeFrom | long   | 0        | Unix timestamp filter start        |
| updateTimeTo   | long   | 0        | Unix timestamp filter end          |

**Response:**
```json
{
  "success": true,
  "data": {
    "itemIdList": [111, 222, 333],
    "hasNextPage": true,
    "nextOffset": 50,
    "totalCount": 150
  }
}
```

---

### 3.2 Get Item Base Info
**GET** `/api/shopee/product/info`

**Query Params:**
| Param           | Type        | Required | Description           |
|-----------------|-------------|----------|-----------------------|
| shopId          | long        | Yes      | Shop ID               |
| itemIdList      | long[]      | Yes      | Comma-separated IDs   |
| needTaxInfo     | bool        | No       | Include tax info      |
| needComplaintPolicy | bool    | No       | Include complaint policy |

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "itemId": 111,
      "itemName": "Blue T-Shirt",
      "description": "Comfortable cotton t-shirt",
      "categoryId": 100630,
      "condition": "NEW",
      "status": "NORMAL",
      "originalPrice": 29.90,
      "currentPrice": 24.90,
      "stock": 50,
      "images": ["https://cdn.shopee.com/img/111.jpg"]
    }
  ]
}
```

---

### 3.3 Add Item
**POST** `/api/shopee/product`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemName": "Blue T-Shirt Size M",
  "description": "100% Cotton. Machine washable.",
  "categoryId": 100630,
  "condition": "NEW",
  "originalPrice": "29.90",
  "stock": 100,
  "weight": "0.5",
  "imageUrlList": ["https://your-image.com/front.jpg", "https://your-image.com/back.jpg"],
  "attributeList": [
    { "attributeId": 9946, "attributeValueList": [{ "valueId": 0, "originalValueName": "Blue" }] }
  ],
  "logistics": { "logisticId": 80033, "enabled": true, "shippingFee": 0, "isFree": true, "size": "" },
  "dimension": { "packageLength": 20, "packageWidth": 15, "packageHeight": 5 }
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "itemId": 987654321,
    "itemName": "Blue T-Shirt Size M",
    "status": "NORMAL"
  }
}
```

---

### 3.4 Update Item
**PUT** `/api/shopee/product`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemId": 987654321,
  "itemName": "Blue T-Shirt Size M - Updated",
  "description": "Updated description",
  "categoryId": 100630,
  "condition": "NEW",
  "imageUrlList": ["https://your-image.com/new.jpg"]
}
```

---

### 3.5 Update Item Price
**PUT** `/api/shopee/product/price`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemId": 987654321,
  "price": 24.90,
  "modelId": 0
}
```

**Response:**
```json
{ "success": true, "data": true }
```

---

### 3.6 Update Item Stock
**PUT** `/api/shopee/product/stock`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemId": 987654321,
  "stock": 75,
  "modelId": 0,
  "locationId": 0
}
```

---

### 3.7 Delete Item
**DELETE** `/api/shopee/product`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemIdList": [987654321, 987654322]
}
```

---

### 3.8 Get Categories
**GET** `/api/shopee/product/categories?shopId=123456789&language=en`

---

### 3.9 Get Attributes
**GET** `/api/shopee/product/attributes?shopId=123456789&categoryId=100630&language=en`

---

### 3.10 Get Brands
**GET** `/api/shopee/product/brands?shopId=123456789&categoryId=100630&language=en`

---

### 3.11 Get Model List (Variants)
**GET** `/api/shopee/product/models?shopId=123456789&itemId=987654321`

---

### 3.12 Init Tier Variation
**POST** `/api/shopee/product/tier-variation?shopId=123456789&itemId=987654321`

**Request Body:**
```json
[
  { "name": "Color", "optionList": [{ "option": "Red", "image": { "imageId": "img001" } }, { "option": "Blue" }] },
  { "name": "Size", "optionList": [{ "option": "S" }, { "option": "M" }, { "option": "L" }] }
]
```

---

## 4. ORDER APIs

### 4.1 Get Order List
**GET** `/api/shopee/order/list`

**Query Params:**
| Param       | Type   | Default     | Description                                             |
|-------------|--------|-------------|---------------------------------------------------------|
| shopId      | long   | required    | Shop ID                                                 |
| timeFrom    | long   | required    | Unix timestamp (start)                                  |
| timeTo      | long   | required    | Unix timestamp (end) — max 15 days from timeFrom        |
| orderStatus | string | ALL         | UNPAID, READY_TO_SHIP, PROCESSED, SHIPPED, COMPLETED, CANCELLED, IN_CANCEL |
| pageSize    | int    | 20          | Max 100                                                 |
| cursor      | string |             | Cursor for next page (from previous response)           |

**Sample Request:**
```
GET /api/shopee/order/list?shopId=123456789&timeFrom=1700000000&timeTo=1701296000&orderStatus=READY_TO_SHIP&pageSize=20
```

**Response:**
```json
{
  "success": true,
  "data": {
    "orderSnList": ["240101ABCDEF", "240101GHIJKL"],
    "more": true,
    "nextCursor": "cursor_abc123"
  }
}
```

---

### 4.2 Get Order Detail
**POST** `/api/shopee/order/detail`

**Request Body:**
```json
{
  "shopId": 123456789,
  "orderSnList": ["240101ABCDEF", "240101GHIJKL"],
  "responseOptionalFields": "buyer_user_id,buyer_username,recipient_address,item_list,total_amount,payment_method"
}
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "orderSn": "240101ABCDEF",
      "orderStatus": "READY_TO_SHIP",
      "buyerUserId": "buyer123",
      "buyerUsername": "john_doe",
      "totalAmount": 59.90,
      "currency": "MYR",
      "createTime": 1700050000,
      "updateTime": 1700060000,
      "note": "Please pack carefully",
      "paymentMethod": "Credit Card",
      "itemList": [
        {
          "itemId": 987654321,
          "itemName": "Blue T-Shirt Size M",
          "modelId": 0,
          "modelName": "",
          "itemSku": "SKU-001",
          "modelQuantityPurchased": 2,
          "modelDiscountedPrice": 24.90,
          "modelOriginalPrice": 29.90
        }
      ],
      "recipientAddress": {
        "name": "John Doe",
        "phone": "+60123456789",
        "city": "Kuala Lumpur",
        "state": "Kuala Lumpur",
        "region": "MY",
        "zipcode": "50000",
        "fullAddress": "123 Jalan ABC, Kuala Lumpur"
      }
    }
  ]
}
```

---

### 4.3 Cancel Order
**POST** `/api/shopee/order/cancel`

**Request Body:**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "cancelReason": "OUT_OF_STOCK",
  "itemId": 987654321
}
```
> **cancelReason values:** `OUT_OF_STOCK`, `CUSTOMER_REQUEST`, `UNDELIVERABLE_AREA`, `COD_NOT_SUPPORTED`

---

### 4.4 Set Order Note
**POST** `/api/shopee/order/note`

**Request Body:**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "note": "VIP customer - handle with care"
}
```

---

### 4.5 Handle Buyer Cancellation
**POST** `/api/shopee/order/buyer-cancellation`

**Request Body:**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "operation": "ACCEPT"
}
```
> **operation values:** `ACCEPT` or `REJECT`

---

### 4.6 Get Escrow Detail
**GET** `/api/shopee/order/{shopId}/escrow/{orderSn}`

**Example:** `GET /api/shopee/order/123456789/escrow/240101ABCDEF`

---

## 5. LOGISTICS APIs

### 5.1 Get Logistic Info
**GET** `/api/shopee/logistics/info?shopId=123456789&orderSn=240101ABCDEF`

**Response:**
```json
{
  "success": true,
  "data": {
    "logisticId": 80033,
    "logisticName": "J&T Express",
    "cod": false,
    "enabled": true,
    "shippingFee": 7.50,
    "status": "normal"
  }
}
```

---

### 5.2 Init Logistics (Arrange Shipment)
**POST** `/api/shopee/logistics/init`

**Request Body (Pickup):**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "packageNumber": "",
  "pickup": {
    "addressId": 111222333,
    "pickupTimeId": "time_slot_id"
  }
}
```

**Request Body (Drop-off):**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "packageNumber": "",
  "dropoff": {
    "branchId": 444555666,
    "senderRealName": "John Doe"
  }
}
```

**Request Body (Non-integrated):**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "packageNumber": "",
  "nonIntegrated": {
    "trackingNumber": "JT1234567890MY"
  }
}
```

---

### 5.3 Ship Order
**POST** `/api/shopee/logistics/ship`

**Request Body:** Same format as Init Logistics (5.2)

---

### 5.4 Get Tracking Number
**GET** `/api/shopee/logistics/tracking-number?shopId=123456789&orderSn=240101ABCDEF`

**Response:**
```json
{
  "success": true,
  "data": {
    "trackingNumber": "JT1234567890MY",
    "plpNumber": ""
  }
}
```

---

### 5.5 Get Order Tracking Info
**GET** `/api/shopee/logistics/{shopId}/tracking/{orderSn}`

**Example:** `GET /api/shopee/logistics/123456789/tracking/240101ABCDEF`

---

### 5.6 Update Logistics Order
**PUT** `/api/shopee/logistics/update`

**Request Body:**
```json
{
  "shopId": 123456789,
  "orderSn": "240101ABCDEF",
  "trackingNumber": "JT9876543210MY",
  "handoverMethod": "DROP_OFF",
  "branchId": 444555666,
  "senderAddressId": 0,
  "pickupTimeId": ""
}
```

---

### 5.7 Get Address List
**GET** `/api/shopee/logistics/addresses?shopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "addressList": [
      {
        "addressId": 111222333,
        "region": "MY",
        "state": "Selangor",
        "city": "Petaling Jaya",
        "district": "Subang Jaya",
        "address": "No. 1, Jalan ABC",
        "zipcode": "47500",
        "addressType": ["DEFAULT_ADDRESS", "PICKUP_ADDRESS"]
      }
    ]
  }
}
```

---

## 6. VOUCHER APIs

### 6.1 Create Voucher
**POST** `/api/shopee/voucher`

**Request Body:**
```json
{
  "shopId": 123456789,
  "voucherCode": "SAVE10",
  "voucherType": 1,
  "discountType": 1,
  "rewardValue": 10.00,
  "minimumSpend": 50.00,
  "startTime": 1720000000,
  "endTime": 1722592000,
  "usageQuantity": 100
}
```
> **voucherType:** 1=Shop Voucher, 2=Product Voucher  
> **discountType:** 1=Fixed Amount, 2=Percentage

**Response:**
```json
{
  "success": true,
  "data": {
    "voucherId": 777888999,
    "voucherCode": "SAVE10",
    "voucherStatus": "upcoming"
  }
}
```

---

### 6.2 Delete Voucher
**DELETE** `/api/shopee/voucher`

**Request Body:**
```json
{ "shopId": 123456789, "voucherId": 777888999 }
```

---

### 6.3 Update Voucher
**PUT** `/api/shopee/voucher`

**Request Body:**
```json
{
  "shopId": 123456789,
  "voucherId": 777888999,
  "voucherCode": "SAVE15",
  "rewardValue": 15.00,
  "minimumSpend": 60.00,
  "startTime": 1720000000,
  "endTime": 1725184000,
  "usageQuantity": 200
}
```

---

### 6.4 End Voucher
**POST** `/api/shopee/voucher/end`

**Request Body:**
```json
{ "shopId": 123456789, "voucherId": 777888999 }
```

---

### 6.5 Get Voucher
**GET** `/api/shopee/voucher/{shopId}/{voucherId}`

**Example:** `GET /api/shopee/voucher/123456789/777888999`

---

### 6.6 Get Voucher List
**GET** `/api/shopee/voucher/list?shopId=123456789&status=1&pageNo=1&pageSize=20`

> **status:** 1=Upcoming, 2=Ongoing, 3=Expired

---

## 7. DISCOUNT APIs

### 7.1 Add Discount
**POST** `/api/shopee/discount`

**Request Body:**
```json
{
  "shopId": 123456789,
  "discountName": "Merdeka Sale 2024",
  "startTime": 1724688000,
  "endTime": 1724774400,
  "itemList": [
    {
      "itemId": 987654321,
      "itemPromotionPrice": 19.90,
      "modelId": 0,
      "purchaseLimit": 2
    },
    {
      "itemId": 987654322,
      "itemPromotionPrice": 39.90,
      "modelId": 0
    }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "discountId": 555666777,
    "discountName": "Merdeka Sale 2024",
    "status": "upcoming"
  }
}
```

---

### 7.2 Delete Discount
**DELETE** `/api/shopee/discount`

**Request Body:**
```json
{ "shopId": 123456789, "discountId": 555666777 }
```

---

### 7.3 Update Discount
**PUT** `/api/shopee/discount`

**Request Body:**
```json
{
  "shopId": 123456789,
  "discountId": 555666777,
  "discountName": "Merdeka Sale 2024 - Extended",
  "startTime": 1724688000,
  "endTime": 1725120000
}
```

---

### 7.4 End Discount
**POST** `/api/shopee/discount/end`

**Request Body:**
```json
{ "shopId": 123456789, "discountId": 555666777 }
```

---

### 7.5 Get Discount
**GET** `/api/shopee/discount/{shopId}/{discountId}`

**Example:** `GET /api/shopee/discount/123456789/555666777`

---

### 7.6 Get Discount List
**GET** `/api/shopee/discount/list?shopId=123456789&discountStatus=upcoming&pageNo=1&pageSize=20`

> **discountStatus:** `upcoming`, `ongoing`, `expired`

---

### 7.7 Add Discount Items
**POST** `/api/shopee/discount/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "discountId": 555666777,
  "itemList": [
    { "itemId": 987654323, "itemPromotionPrice": 25.00, "modelId": 0, "purchaseLimit": 3 }
  ]
}
```

---

### 7.8 Delete Discount Items
**DELETE** `/api/shopee/discount/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "discountId": 555666777,
  "itemIdList": [987654323]
}
```

---

### 7.9 Update Discount Items
**PUT** `/api/shopee/discount/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "discountId": 555666777,
  "itemList": [
    { "itemId": 987654321, "itemPromotionPrice": 17.90, "modelId": 0 }
  ]
}
```

---

## 8. MEDIA APIs

### 8.1 Upload Image (File)
**POST** `/api/shopee/media/image/upload?shopId=123456789`  
**Content-Type:** `multipart/form-data`

**Form Field:** `image` = (binary image file, max 2MB, PNG/JPG/JPEG)

**Response:**
```json
{
  "success": true,
  "data": {
    "imageId": "img_abc123",
    "imageUrl": "https://cf.shopee.com.my/file/img_abc123"
  }
}
```

---

### 8.2 Upload Image By URL
**POST** `/api/shopee/media/image/upload-url`

**Request Body:**
```json
{
  "shopId": 123456789,
  "imageUrl": "https://your-server.com/product-image.jpg"
}
```

**Response:** Same as 8.1

---

### 8.3 Upload Video Part
**POST** `/api/shopee/media/video/upload`

**Request Body:**
```json
{
  "shopId": 123456789,
  "videoUploadId": "vid_upload_abc123",
  "videoContent": "base64_encoded_video_chunk_here",
  "partSeq": 1
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "videoUploadId": "vid_upload_abc123"
  }
}
```

---

## 9. MERCHANT APIs

### 9.1 Get Merchant Info
**GET** `/api/shopee/merchant/{merchantId}/info`

**Example:** `GET /api/shopee/merchant/999888777/info`

**Response:**
```json
{
  "success": true,
  "data": {
    "merchantId": 999888777,
    "merchantName": "Ample Group Global",
    "merchantStatus": "NORMAL",
    "merchantDescription": "Trusted seller",
    "region": "MY"
  }
}
```

---

### 9.2 Get Shops By Merchant
**GET** `/api/shopee/merchant/{merchantId}/shops?pageNo=1&pageSize=20`

**Example:** `GET /api/shopee/merchant/999888777/shops?pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "shopList": [
      { "shopId": 123456789, "shopName": "Store A", "region": "MY", "status": "NORMAL" },
      { "shopId": 123456790, "shopName": "Store B", "region": "SG", "status": "NORMAL" }
    ],
    "more": false,
    "totalCount": 2
  }
}
```

---

## Quick Start Guide

### Step 1: Configure Shopee Credentials
Edit `appsettings.json` in `ApiToolKit.Web`:
```json
{
  "Shopee": {
    "PartnerId": "YOUR_PARTNER_ID",
    "PartnerKey": "YOUR_PARTNER_KEY",
    "RedirectUrl": "https://yourdomain.com/api/shopee/auth/callback",
    "BaseUrl": "https://partner.test-stable.shopeemobile.com"
  }
}
```

### Step 2: Run Database Scripts
1. Run `Database/Shopee/01_CreateTables.sql` → creates all tables
2. Run `Database/Shopee/02_StoredProcedures.sql` → creates all stored procedures
3. Run `Database/Shopee/03_SeedConfig.sql` → update with your credentials

### Step 3: Authorize a Shop
1. Call `GET /api/shopee/auth/auth-url` → get the OAuth URL
2. Open URL in browser → user authorizes
3. Shopee redirects to your callback with `?code=xxx&shop_id=yyy`
4. Call `POST /api/shopee/auth/token` with the code and shop_id
5. Token is stored in DB — all subsequent calls use it automatically

### Step 4: Test APIs
Open Swagger at `https://localhost:{port}/swagger` and test all endpoints interactively.

---

## Error Response Format
All error responses follow this format:
```json
{
  "success": false,
  "message": "Error description here",
  "errorCode": null
}
```

## Common Shopee Error Codes
| Code | Meaning |
|------|---------|
| error.auth.token_expired | Access token expired — call refresh |
| error.not_found | Resource not found |
| error.param.invalid | Invalid parameter value |
| error.item.stock_insufficient | Not enough stock |
| error.order.already_cancelled | Order already cancelled |

---

## 10. RETURN/REFUND APIs

### 10.1 Get Return List
**GET** `/api/shopee/returns/list`

**Query Params:**
| Param          | Type   | Required | Description                                          |
|----------------|--------|----------|------------------------------------------------------|
| shopId         | long   | Yes      | Shop ID                                              |
| pageNo         | int    | No       | Page number (default: 1)                             |
| pageSize       | int    | No       | Items per page (default: 20)                         |
| createTimeFrom | long   | No       | Unix timestamp filter start                          |
| createTimeTo   | long   | No       | Unix timestamp filter end                            |
| status         | string | No       | Return status filter (e.g. `REQUESTED`, `ACCEPTED`)  |

**Response:**
```json
{
  "success": true,
  "data": {
    "returnList": [
      {
        "returnSn": "RET240101ABCDEF",
        "orderSn": "240101ABCDEF",
        "status": "REQUESTED",
        "reason": "DOES_NOT_MATCH_DESCRIPTION",
        "textReason": "Product color is different from listing",
        "refundAmount": 24.90,
        "createTime": 1720100000,
        "updateTime": 1720110000,
        "buyerUserId": "buyer123",
        "error": "",
        "message": ""
      }
    ],
    "more": false,
    "totalCount": 1
  }
}
```

---

### 10.2 Get Return Detail
**GET** `/api/shopee/returns/detail`

**Query Params:**
| Param    | Type   | Required | Description       |
|----------|--------|----------|-------------------|
| shopId   | long   | Yes      | Shop ID           |
| returnSn | string | Yes      | Return serial no. |

**Response:**
```json
{
  "success": true,
  "data": {
    "returnSn": "RET240101ABCDEF",
    "orderSn": "240101ABCDEF",
    "status": "REQUESTED",
    "reason": "DOES_NOT_MATCH_DESCRIPTION",
    "textReason": "Product color is different from listing",
    "refundAmount": 24.90,
    "createTime": 1720100000,
    "updateTime": 1720110000,
    "buyerUserId": "buyer123",
    "error": "",
    "message": ""
  }
}
```

---

### 10.3 Confirm Return
**POST** `/api/shopee/returns/confirm`

**Request Body:**
```json
{
  "shopId": 123456789,
  "returnSn": "RET240101ABCDEF"
}
```

**Response:**
```json
{ "success": true, "data": true }
```

---

### 10.4 Dispute Return
**POST** `/api/shopee/returns/dispute`

**Request Body:**
```json
{
  "shopId": 123456789,
  "returnSn": "RET240101ABCDEF",
  "disputeReason": "ITEM_NOT_RETURNED",
  "disputeTextReason": "Buyer has not shipped the item back within the required window",
  "imageList": ["https://cdn.shopee.com/proof/img001.jpg"]
}
```

**Response:**
```json
{ "success": true, "data": true }
```

---

### 10.5 Offer Return
**POST** `/api/shopee/returns/offer`

**Request Body:**
```json
{
  "shopId": 123456789,
  "returnSn": "RET240101ABCDEF",
  "offerType": "PARTIAL_REFUND",
  "amount": 12.00
}
```

---

### 10.6 Accept Offer
**POST** `/api/shopee/returns/accept-offer`

**Request Body:**
```json
{
  "shopId": 123456789,
  "returnSn": "RET240101ABCDEF"
}
```

---

### 10.7 Cancel Dispute
**POST** `/api/shopee/returns/cancel-dispute`

**Request Body:**
```json
{
  "shopId": 123456789,
  "returnSn": "RET240101ABCDEF"
}
```

---

### 10.8 Upload Proof
**POST** `/api/shopee/returns/upload-proof`

**Request Body:**
```json
{
  "shopId": 123456789,
  "returnSn": "RET240101ABCDEF",
  "text": "Item was shipped in perfect condition as shown",
  "imageList": ["https://cdn.shopee.com/proof/img001.jpg", "https://cdn.shopee.com/proof/img002.jpg"],
  "videoList": []
}
```

---

### 10.9 Get Dispute Reasons
**GET** `/api/shopee/returns/{shopId}/dispute-reasons`

**Example:** `GET /api/shopee/returns/123456789/dispute-reasons`

**Response:**
```json
{
  "success": true,
  "data": {
    "reasonList": [
      { "reasonKey": "ITEM_NOT_RETURNED", "description": "Buyer has not returned the item" },
      { "reasonKey": "ITEM_DAMAGED", "description": "Item was returned in damaged condition" }
    ]
  }
}
```

---

### 10.10 Get Available Solutions
**GET** `/api/shopee/returns/{shopId}/solutions/{returnSn}`

**Example:** `GET /api/shopee/returns/123456789/solutions/RET240101ABCDEF`

---

### 10.11 Get Shipping Carrier
**GET** `/api/shopee/returns/{shopId}/carriers/{returnSn}`

**Example:** `GET /api/shopee/returns/123456789/carriers/RET240101ABCDEF`

---

### 10.12 Query Proof
**GET** `/api/shopee/returns/{shopId}/proof/{returnSn}`

**Example:** `GET /api/shopee/returns/123456789/proof/RET240101ABCDEF`

---

### 10.13 Get Reverse Tracking Info
**GET** `/api/shopee/returns/{shopId}/reverse-tracking/{returnSn}`

**Example:** `GET /api/shopee/returns/123456789/reverse-tracking/RET240101ABCDEF`

---

### 10.14 Upload Shipping Proof
**POST** `/api/shopee/returns/{shopId}/shipping-proof?returnSn=RET240101ABCDEF&trackingNumber=JT1234567890MY&carrierId=80033`

**Example:** `POST /api/shopee/returns/123456789/shipping-proof?returnSn=RET240101ABCDEF&trackingNumber=JT1234567890MY&carrierId=80033`

---

### 10.15 Convert Image
**POST** `/api/shopee/returns/{shopId}/convert-image`

**Request Body:**
```json
["https://your-server.com/evidence1.jpg", "https://your-server.com/evidence2.jpg"]
```

**Response:**
```json
{
  "success": true,
  "data": {
    "imageUrlList": [
      "https://cdn.shopee.com/file/converted_img001",
      "https://cdn.shopee.com/file/converted_img002"
    ]
  }
}
```

---

## 11. PAYMENT APIs

### 11.1 Get Escrow List
**GET** `/api/shopee/payment/escrow/list`

**Query Params:**
| Param           | Type | Required | Description                   |
|-----------------|------|----------|-------------------------------|
| shopId          | long | Yes      | Shop ID                       |
| releaseTimeFrom | long | No       | Unix timestamp filter start   |
| releaseTimeTo   | long | No       | Unix timestamp filter end     |
| pageNo          | int  | No       | Page number (default: 1)      |
| pageSize        | int  | No       | Items per page (default: 20)  |

**Response:**
```json
{
  "success": true,
  "data": {
    "escrowList": [
      {
        "orderSn": "240101ABCDEF",
        "buyerTotalAmount": 59.90,
        "sellerTotalAmount": 54.12,
        "escrowAmount": 54.12,
        "releaseTime": 1721000000
      }
    ],
    "more": false,
    "totalCount": 1
  }
}
```

---

### 11.2 Get Escrow Detail
**GET** `/api/shopee/payment/escrow/detail?shopId=123456789&orderSn=240101ABCDEF`

**Response:**
```json
{
  "success": true,
  "data": {
    "orderSn": "240101ABCDEF",
    "buyerTotalAmount": 59.90,
    "sellerTotalAmount": 54.12,
    "escrowAmount": 54.12,
    "releaseTime": 1721000000,
    "items": [
      { "itemId": 987654321, "itemName": "Blue T-Shirt Size M", "itemPrice": 24.90, "quantity": 2 }
    ]
  }
}
```

---

### 11.3 Get Escrow Detail (Batch)
**POST** `/api/shopee/payment/escrow/detail-batch?shopId=123456789`

**Request Body:**
```json
["240101ABCDEF", "240101GHIJKL"]
```

**Response:**
```json
{
  "success": true,
  "data": [
    { "orderSn": "240101ABCDEF", "escrowAmount": 54.12, "releaseTime": 1721000000 },
    { "orderSn": "240101GHIJKL", "escrowAmount": 89.50, "releaseTime": 1721050000 }
  ]
}
```

---

### 11.4 Get Wallet Transaction List
**GET** `/api/shopee/payment/wallet/transactions`

**Query Params:**
| Param          | Type   | Required | Description                  |
|----------------|--------|----------|------------------------------|
| shopId         | long   | Yes      | Shop ID                      |
| createTimeFrom | long   | No       | Unix timestamp filter start  |
| createTimeTo   | long   | No       | Unix timestamp filter end    |
| pageNo         | int    | No       | Page number (default: 1)     |
| pageSize       | int    | No       | Items per page (default: 20) |
| walletType     | string | No       | Wallet type filter           |

**Response:**
```json
{
  "success": true,
  "data": {
    "transactionList": [
      {
        "transactionId": "TXN240101001",
        "transactionType": "PAYMENT",
        "amount": 54.12,
        "currency": "MYR",
        "createTime": 1720200000,
        "description": "Order 240101ABCDEF payment released"
      }
    ],
    "more": false,
    "totalCount": 1
  }
}
```

---

### 11.5 Get Payment Method List
**GET** `/api/shopee/payment/methods?shopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "paymentMethodList": [
      { "paymentMethodId": 1, "paymentMethod": "Credit Card", "isAvailable": true },
      { "paymentMethodId": 2, "paymentMethod": "Online Banking", "isAvailable": true },
      { "paymentMethodId": 3, "paymentMethod": "COD", "isAvailable": false }
    ]
  }
}
```

---

### 11.6 Get Shop Installment Status
**GET** `/api/shopee/payment/installment/shop?shopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": { "installmentStatus": true }
}
```

---

### 11.7 Set Shop Installment Status
**POST** `/api/shopee/payment/installment/shop`

**Request Body:**
```json
{
  "shopId": 123456789,
  "installmentStatus": true
}
```

**Response:**
```json
{ "success": true, "data": true }
```

---

### 11.8 Get Item Installment Status
**POST** `/api/shopee/payment/installment/items/status`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemIdList": [987654321, 987654322]
}
```

**Response:**
```json
{
  "success": true,
  "data": [
    { "itemId": 987654321, "installmentStatus": true },
    { "itemId": 987654322, "installmentStatus": false }
  ]
}
```

---

### 11.9 Set Item Installment Status
**POST** `/api/shopee/payment/installment/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "itemId": 987654321,
  "installmentStatus": true
}
```

---

### 11.10 Generate Income Report
**POST** `/api/shopee/payment/income/generate`

**Request Body:**
```json
{
  "shopId": 123456789,
  "startDate": "2024-06-01",
  "endDate": "2024-06-30"
}
```

**Response:**
```json
{ "success": true, "data": { "reportGenerating": true } }
```

---

### 11.11 Get Income Report
**GET** `/api/shopee/payment/income/report?shopId=123456789&reportType=MONTHLY&startDate=2024-06-01&endDate=2024-06-30`

**Response:**
```json
{
  "success": true,
  "data": {
    "reportUrl": "https://cdn.shopee.com/reports/income_report_june2024.csv",
    "status": "READY"
  }
}
```

---

### 11.12 Get Billing Transactions
**GET** `/api/shopee/payment/billing/transactions`

**Query Params:**
| Param          | Type | Required | Description                  |
|----------------|------|----------|------------------------------|
| shopId         | long | Yes      | Shop ID                      |
| createTimeFrom | long | No       | Unix timestamp filter start  |
| createTimeTo   | long | No       | Unix timestamp filter end    |
| pageNo         | int  | No       | Page number (default: 1)     |
| pageSize       | int  | No       | Items per page (default: 20) |

**Response:**
```json
{
  "success": true,
  "data": {
    "transactionList": [
      { "billingId": "BIL240101001", "type": "COMMISSION_FEE", "amount": 2.49, "createTime": 1720200000 }
    ],
    "more": false,
    "totalCount": 1
  }
}
```

---

### 11.13 Get Payout Detail
**GET** `/api/shopee/payment/payout/detail?shopId=123456789&pageNo=1&pageSize=20`

---

### 11.14 Get Payout Info
**GET** `/api/shopee/payment/payout/info?shopId=123456789&pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "payoutList": [
      {
        "payoutId": "PAY240101001",
        "amount": 540.00,
        "currency": "MYR",
        "status": "COMPLETED",
        "payoutTime": 1721500000
      }
    ],
    "more": false,
    "totalCount": 1
  }
}
```

---

## 12. BUNDLE DEAL APIs

### 12.1 Create Bundle Deal
**POST** `/api/shopee/bundle-deal`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealName": "Buy 3 Save More",
  "startTime": 1722000000,
  "endTime": 1722604800,
  "itemList": [
    { "itemId": 987654321, "bundlePrice": 19.90, "modelId": 0 },
    { "itemId": 987654322, "bundlePrice": 35.00, "modelId": 0 }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "bundleDealId": 111222333,
    "bundleDealName": "Buy 3 Save More",
    "startTime": 1722000000,
    "endTime": 1722604800,
    "status": "upcoming",
    "error": "",
    "message": ""
  }
}
```

---

### 12.2 Update Bundle Deal
**PUT** `/api/shopee/bundle-deal`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealId": 111222333,
  "bundleDealName": "Buy 3 Save Even More",
  "startTime": 1722000000,
  "endTime": 1723209600
}
```

---

### 12.3 Delete Bundle Deal
**DELETE** `/api/shopee/bundle-deal`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealId": 111222333
}
```

---

### 12.4 End Bundle Deal
**POST** `/api/shopee/bundle-deal/end`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealId": 111222333
}
```

---

### 12.5 Get Bundle Deal
**GET** `/api/shopee/bundle-deal/{shopId}/{bundleDealId}`

**Example:** `GET /api/shopee/bundle-deal/123456789/111222333`

**Response:**
```json
{
  "success": true,
  "data": {
    "bundleDealId": 111222333,
    "bundleDealName": "Buy 3 Save More",
    "startTime": 1722000000,
    "endTime": 1722604800,
    "status": "ongoing",
    "error": "",
    "message": ""
  }
}
```

---

### 12.6 Get Bundle Deal List
**GET** `/api/shopee/bundle-deal/list?shopId=123456789&status=ongoing&pageNo=1&pageSize=20`

> **status values:** `upcoming`, `ongoing`, `expired`

**Response:**
```json
{
  "success": true,
  "data": {
    "bundleDealList": [
      { "bundleDealId": 111222333, "bundleDealName": "Buy 3 Save More", "startTime": 1722000000, "endTime": 1722604800, "status": "ongoing" }
    ],
    "more": false,
    "totalCount": 1
  }
}
```

---

### 12.7 Get Bundle Deal Items
**GET** `/api/shopee/bundle-deal/{shopId}/{bundleDealId}/items`

**Example:** `GET /api/shopee/bundle-deal/123456789/111222333/items`

---

### 12.8 Add Bundle Deal Items
**POST** `/api/shopee/bundle-deal/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealId": 111222333,
  "itemList": [
    { "itemId": 987654323, "bundlePrice": 45.00, "modelId": 0 }
  ]
}
```

---

### 12.9 Delete Bundle Deal Items
**DELETE** `/api/shopee/bundle-deal/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealId": 111222333,
  "itemIdList": [987654323]
}
```

---

### 12.10 Update Bundle Deal Items
**PUT** `/api/shopee/bundle-deal/items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "bundleDealId": 111222333,
  "itemList": [
    { "itemId": 987654321, "bundlePrice": 17.90, "modelId": 0 }
  ]
}
```

---

## 13. ADD-ON DEAL APIs

### 13.1 Create Add-On Deal
**POST** `/api/shopee/add-on-deal`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealName": "Buy This, Get That Cheap",
  "startTime": 1722000000,
  "endTime": 1722604800
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "addOnDealId": 444555666,
    "addOnDealName": "Buy This, Get That Cheap",
    "startTime": 1722000000,
    "endTime": 1722604800,
    "status": "upcoming",
    "error": "",
    "message": ""
  }
}
```

---

### 13.2 Update Add-On Deal
**PUT** `/api/shopee/add-on-deal`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "addOnDealName": "Buy This, Get That Even Cheaper",
  "endTime": 1723209600
}
```

---

### 13.3 Delete Add-On Deal
**DELETE** `/api/shopee/add-on-deal`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666
}
```

---

### 13.4 End Add-On Deal
**POST** `/api/shopee/add-on-deal/end`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666
}
```

---

### 13.5 Get Add-On Deal
**GET** `/api/shopee/add-on-deal/{shopId}/{addOnDealId}`

**Example:** `GET /api/shopee/add-on-deal/123456789/444555666`

**Response:**
```json
{
  "success": true,
  "data": {
    "addOnDealId": 444555666,
    "addOnDealName": "Buy This, Get That Cheap",
    "startTime": 1722000000,
    "endTime": 1722604800,
    "status": "ongoing",
    "error": "",
    "message": ""
  }
}
```

---

### 13.6 Get Add-On Deal List
**GET** `/api/shopee/add-on-deal/list?shopId=123456789&status=ongoing&pageNo=1&pageSize=20`

---

### 13.7 Add Main Items
**POST** `/api/shopee/add-on-deal/main-items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "itemList": [
    { "itemId": 987654321, "modelId": 0, "minPurchaseQty": 1 }
  ]
}
```

---

### 13.8 Delete Main Items
**DELETE** `/api/shopee/add-on-deal/main-items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "itemIdList": [987654321]
}
```

---

### 13.9 Update Main Items
**PUT** `/api/shopee/add-on-deal/main-items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "itemList": [
    { "itemId": 987654321, "modelId": 0, "minPurchaseQty": 2 }
  ]
}
```

---

### 13.10 Get Main Items
**GET** `/api/shopee/add-on-deal/{shopId}/{addOnDealId}/main-items`

**Example:** `GET /api/shopee/add-on-deal/123456789/444555666/main-items`

---

### 13.11 Add Sub Items
**POST** `/api/shopee/add-on-deal/sub-items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "itemList": [
    { "itemId": 987654399, "modelId": 0, "addOnPrice": 5.00, "stock": 50, "purchaseLimit": 2 }
  ]
}
```

---

### 13.12 Delete Sub Items
**DELETE** `/api/shopee/add-on-deal/sub-items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "itemIdList": [987654399]
}
```

---

### 13.13 Update Sub Items
**PUT** `/api/shopee/add-on-deal/sub-items`

**Request Body:**
```json
{
  "shopId": 123456789,
  "addOnDealId": 444555666,
  "itemList": [
    { "itemId": 987654399, "modelId": 0, "addOnPrice": 3.90, "stock": 100, "purchaseLimit": 3 }
  ]
}
```

---

### 13.14 Get Sub Items
**GET** `/api/shopee/add-on-deal/{shopId}/{addOnDealId}/sub-items`

**Example:** `GET /api/shopee/add-on-deal/123456789/444555666/sub-items`

---

## 14. SHOP FLASH SALE APIs

### 14.1 Get Time Slot ID
**POST** `/api/shopee/shop-flash-sale/get-time-slot-id`

**Request Body:**
```json
{
  "startTime": 1722052800,
  "endTime": 1722060000
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "timeslotId": 8801234567,
    "error": "",
    "message": ""
  }
}
```

---

### 14.2 Create Flash Sale
**POST** `/api/shopee/shop-flash-sale/create`

**Request Body:**
```json
{
  "timeslotId": 8801234567
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "flashSaleId": 66677788,
    "error": "",
    "message": ""
  }
}
```

---

### 14.3 Add Flash Sale Items
**POST** `/api/shopee/shop-flash-sale/items`

**Request Body:**
```json
{
  "flashSaleId": 66677788,
  "items": [
    { "itemId": 987654321, "purchaseLimit": 2, "originalPrice": 2990, "price": 1990, "stock": 30 },
    { "itemId": 987654322, "purchaseLimit": 1, "originalPrice": 5990, "price": 3990, "stock": 15 }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "flashSaleId": 66677788,
    "itemList": [
      { "itemId": 987654321, "purchaseLimit": 2, "originalPrice": 2990, "price": 1990, "stock": 30 }
    ],
    "error": "",
    "message": ""
  }
}
```

---

### 14.4 Update Flash Sale
**POST** `/api/shopee/shop-flash-sale/update`

**Request Body:**
```json
{
  "flashSaleId": 66677788,
  "status": "ongoing"
}
```

---

### 14.5 Delete Flash Sale
**POST** `/api/shopee/shop-flash-sale/delete`

**Request Body:**
```json
{
  "flashSaleId": 66677788
}
```

---

### 14.6 Get Flash Sale List
**GET** `/api/shopee/shop-flash-sale/list?pageNo=1&pageSize=20&filter=upcoming`

**Response:**
```json
{
  "success": true,
  "data": {
    "flashSaleList": [
      {
        "flashSaleId": 66677788,
        "timeslotId": 8801234567,
        "status": "upcoming",
        "startTime": 1722052800,
        "endTime": 1722060000
      }
    ],
    "totalCount": 1,
    "hasNextPage": false
  }
}
```

---

### 14.7 Get Flash Sale Detail
**GET** `/api/shopee/shop-flash-sale/{flashSaleId}`

**Example:** `GET /api/shopee/shop-flash-sale/66677788`

**Response:**
```json
{
  "success": true,
  "data": {
    "flashSaleId": 66677788,
    "timeslotId": 8801234567,
    "status": "upcoming",
    "startTime": 1722052800,
    "endTime": 1722060000,
    "itemCount": 2,
    "error": "",
    "message": ""
  }
}
```

---

### 14.8 Update Flash Sale Items
**POST** `/api/shopee/shop-flash-sale/items/update`

**Request Body:**
```json
{
  "flashSaleId": 66677788,
  "items": [
    { "itemId": 987654321, "purchaseLimit": 3, "originalPrice": 2990, "price": 1790, "stock": 50 }
  ]
}
```

---

### 14.9 Delete Flash Sale Items
**POST** `/api/shopee/shop-flash-sale/items/delete`

**Request Body:**
```json
{
  "flashSaleId": 66677788,
  "itemIdList": [987654322]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "flashSaleId": 66677788,
    "failedList": [],
    "error": "",
    "message": ""
  }
}
```

---

### 14.10 Get Flash Sale Items
**GET** `/api/shopee/shop-flash-sale/items?flashSaleId=66677788&pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "flashSaleId": 66677788,
    "itemList": [
      { "itemId": 987654321, "purchaseLimit": 2, "originalPrice": 2990, "price": 1990, "stock": 30 }
    ],
    "totalCount": 1,
    "hasNextPage": false
  }
}
```

---

### 14.11 Get Item Criteria
**GET** `/api/shopee/shop-flash-sale/item-criteria?itemIdList=987654321&itemIdList=987654322`

**Response:**
```json
{
  "success": true,
  "data": {
    "itemCriteriaList": [
      { "itemId": 987654321, "eligible": true },
      { "itemId": 987654322, "eligible": false, "reason": "Item stock insufficient" }
    ]
  }
}
```

---

## 15. TOP PICKS APIs

### 15.1 Get Top Picks List
**GET** `/api/shopee/top-picks/list?pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "topPicksList": [
      {
        "topPicksId": 334455667,
        "name": "Best Sellers",
        "isActivated": true,
        "itemList": [987654321, 987654322, 987654323]
      }
    ],
    "totalCount": 1
  }
}
```

---

### 15.2 Add Top Picks
**POST** `/api/shopee/top-picks`

**Request Body:**
```json
{
  "name": "Summer Collection",
  "itemIdList": [987654321, 987654322, 987654323, 987654324]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "topPicksId": 334455668,
    "error": "",
    "message": ""
  }
}
```

---

### 15.3 Update Top Picks
**POST** `/api/shopee/top-picks/update`

**Request Body:**
```json
{
  "topPicksId": 334455668,
  "name": "Summer Collection 2024",
  "itemIdList": [987654321, 987654322, 987654325],
  "isActivated": true
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "topPicksId": 334455668,
    "error": "",
    "message": ""
  }
}
```

---

### 15.4 Delete Top Picks
**POST** `/api/shopee/top-picks/delete`

**Request Body:**
```json
{
  "topPicksId": 334455668
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "topPicksId": 334455668,
    "error": "",
    "message": ""
  }
}
```

---

## 16. SHOP CATEGORY APIs

### 16.1 Add Shop Category
**POST** `/api/shopee/shop-category`

**Request Body:**
```json
{
  "name": "Electronics",
  "sortWeight": 1
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "shopCategoryId": 778899001,
    "error": "",
    "message": ""
  }
}
```

---

### 16.2 Get Shop Category List
**GET** `/api/shopee/shop-category/list?pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "shopCategoryList": [
      { "shopCategoryId": 778899001, "name": "Electronics", "sortWeight": 1, "status": "NORMAL" },
      { "shopCategoryId": 778899002, "name": "Fashion", "sortWeight": 2, "status": "NORMAL" }
    ],
    "totalCount": 2,
    "hasNextPage": false
  }
}
```

---

### 16.3 Delete Shop Category
**POST** `/api/shopee/shop-category/delete`

**Request Body:**
```json
{
  "shopCategoryId": 778899001
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "shopCategoryId": 778899001,
    "error": "",
    "message": ""
  }
}
```

---

### 16.4 Update Shop Category
**POST** `/api/shopee/shop-category/update`

**Request Body:**
```json
{
  "shopCategoryId": 778899001,
  "name": "Consumer Electronics",
  "sortWeight": 1,
  "status": "NORMAL"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "shopCategoryId": 778899001,
    "error": "",
    "message": ""
  }
}
```

---

### 16.5 Add Items to Shop Category
**POST** `/api/shopee/shop-category/items`

**Request Body:**
```json
{
  "shopCategoryId": 778899001,
  "itemIdList": [987654321, 987654322, 987654323]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "shopCategoryId": 778899001,
    "failedList": [],
    "error": "",
    "message": ""
  }
}
```

---

### 16.6 Get Items of Shop Category
**GET** `/api/shopee/shop-category/items?shopCategoryId=778899001&pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "itemList": [987654321, 987654322, 987654323],
    "totalCount": 3,
    "hasNextPage": false
  }
}
```

---

### 16.7 Delete Items from Shop Category
**POST** `/api/shopee/shop-category/items/delete`

**Request Body:**
```json
{
  "shopCategoryId": 778899001,
  "itemIdList": [987654323]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "shopCategoryId": 778899001,
    "failedList": [],
    "error": "",
    "message": ""
  }
}
```

---

## 17. PUBLIC APIs

### 17.1 Get Shops By Partner
**GET** `/api/shopee/public/shops?pageNo=1&pageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "shopList": [
      { "shopId": 123456789, "shopName": "My Store MY", "region": "MY", "status": "NORMAL" },
      { "shopId": 123456790, "shopName": "My Store SG", "region": "SG", "status": "NORMAL" }
    ],
    "totalCount": 2,
    "hasNextPage": false
  }
}
```

---

### 17.2 Get Categories By Country
**GET** `/api/shopee/public/categories?country=MY&language=en`

**Response:**
```json
{
  "success": true,
  "data": {
    "categoryList": [
      {
        "categoryId": 100001,
        "parentCategoryId": 0,
        "originalCategoryName": "Electronics",
        "displayCategoryName": "Electronics",
        "hasChildren": true
      },
      {
        "categoryId": 100630,
        "parentCategoryId": 100001,
        "originalCategoryName": "Mobile & Gadgets",
        "displayCategoryName": "Mobile & Gadgets",
        "hasChildren": false
      }
    ]
  }
}
```

---

### 17.3 Get Payment Method List
**GET** `/api/shopee/public/payment-methods?region=MY`

**Response:**
```json
{
  "success": true,
  "data": {
    "paymentMethodList": [
      { "paymentMethodId": 1, "paymentMethod": "Credit/Debit Card", "description": "Visa, Mastercard, Amex", "isAvailable": true },
      { "paymentMethodId": 2, "paymentMethod": "Online Banking (FPX)", "description": "Malaysian banks via FPX", "isAvailable": true },
      { "paymentMethodId": 3, "paymentMethod": "ShopeePay", "description": "Shopee e-wallet", "isAvailable": true },
      { "paymentMethodId": 4, "paymentMethod": "Cash on Delivery", "description": "Pay on delivery", "isAvailable": false }
    ]
  }
}
```

---

## 18. FIRST MILE APIs

### 18.1 Generate First Mile Tracking Number
**POST** `/api/shopee/first-mile/generate`

**Request Body:**
```json
{
  "shopId": 123456789,
  "quantity": 5,
  "logistics_channel_id": 80033
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "first_mile_tracking_number_list": [
      "FM240101001MY",
      "FM240101002MY",
      "FM240101003MY",
      "FM240101004MY",
      "FM240101005MY"
    ]
  }
}
```

---

### 18.2 Get First Mile Tracking Number
**GET** `/api/shopee/first-mile/tracking-number?shopId=123456789&first_mile_tracking_number=FM240101001MY`

**Response:**
```json
{
  "success": true,
  "data": {
    "first_mile_tracking_number": "FM240101001MY",
    "status": "BOUND",
    "orders": ["240101ABCDEF", "240101GHIJKL"]
  }
}
```

---

### 18.3 Upload First Mile Tracking Number
**POST** `/api/shopee/first-mile/upload`

**Request Body:**
```json
{
  "shopId": 123456789,
  "first_mile_tracking_number": "FM240101001MY",
  "order_sn_list": ["240101ABCDEF", "240101GHIJKL"]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "first_mile_tracking_number": "FM240101001MY",
    "failed_list": []
  }
}
```

---

### 18.4 Unbind First Mile Tracking Number
**POST** `/api/shopee/first-mile/unbind`

**Request Body:**
```json
{
  "shopId": 123456789,
  "first_mile_tracking_number": "FM240101001MY"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "first_mile_tracking_number": "FM240101001MY"
  }
}
```

---

### 18.5 Get Unbind Order List
**GET** `/api/shopee/first-mile/unbind-orders`

**Query Params:**
| Param            | Type | Required | Description                   |
|------------------|------|----------|-------------------------------|
| shopId           | long | Yes      | Shop ID                       |
| page_no          | int  | Yes      | Page number                   |
| page_size        | int  | Yes      | Items per page                |
| pickup_time_from | long | No       | Unix timestamp filter start   |
| pickup_time_to   | long | No       | Unix timestamp filter end     |

**Response:**
```json
{
  "success": true,
  "data": {
    "order_list": [
      {
        "order_sn": "240101MNOPQR",
        "logistics_channel_id": 80033,
        "pickup_time": 1722080000
      }
    ],
    "total_count": 1,
    "has_next_page": false
  }
}
```

---

### 18.6 Create Channel
**POST** `/api/shopee/first-mile/create-channel`

**Request Body:**
```json
{
  "shopId": 123456789,
  "order_sn_list": ["240101MNOPQR", "240101STUVWX"],
  "logistics_channel_id": 80033
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "batch_ship_result": [
      { "order_sn": "240101MNOPQR", "fail_error": "", "fail_message": "" },
      { "order_sn": "240101STUVWX", "fail_error": "", "fail_message": "" }
    ]
  }
}
```

---

### 18.7 Get First Mile Channel List
**GET** `/api/shopee/first-mile/channels?shopId=123456789&region=MY`

**Response:**
```json
{
  "success": true,
  "data": {
    "logistics_channel_list": [
      { "logistics_channel_id": 80033, "logistics_channel_name": "J&T Express", "cod_enabled": false },
      { "logistics_channel_id": 80034, "logistics_channel_name": "Pos Laju", "cod_enabled": true }
    ]
  }
}
```

---

### 18.8 Get First Mile Tracking List
**GET** `/api/shopee/first-mile/tracking-list?shopId=123456789&page_no=1&page_size=20&status=BOUND`

> **status values:** `UNBOUND`, `BOUND`, `SHIPPED`

**Response:**
```json
{
  "success": true,
  "data": {
    "tracking_list": [
      {
        "first_mile_tracking_number": "FM240101001MY",
        "status": "BOUND",
        "create_time": 1722000000,
        "order_count": 2
      },
      {
        "first_mile_tracking_number": "FM240101002MY",
        "status": "SHIPPED",
        "create_time": 1722010000,
        "order_count": 3
      }
    ],
    "total_count": 2,
    "has_next_page": false
  }
}
```

---

## 19. PUSH NOTIFICATION APIs

### 19.1 Set App Push Config
**POST** `/api/shopee/push/config`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "callback_url": "https://yourdomain.com/webhooks/shopee",
  "push_type": 1,
  "blocked_shop_id_list": []
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "msg": "success"
  }
}
```

---

### 19.2 Get App Push Config
**GET** `/api/shopee/push/config?ShopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "callback_url": "https://yourdomain.com/webhooks/shopee",
    "push_type": 1,
    "blocked_shop_id_list": []
  }
}
```

---

## 20. ACCOUNT HEALTH APIs

### 20.1 Get Shop Penalty
**GET** `/api/shopee/account-health/penalty?ShopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "penalty_point": 2,
    "violation_list": [
      {
        "violation_type": "LATE_SHIPMENT",
        "violation_time": 1719800000,
        "penalty_point": 1,
        "order_sn": "230601ABCDEF01",
        "offense_count": 1
      },
      {
        "violation_type": "NON_FULFILMENT",
        "violation_time": 1719900000,
        "penalty_point": 1,
        "order_sn": "230602ABCDEF02",
        "offense_count": 1
      }
    ]
  }
}
```

---

### 20.2 Get Metric List
**GET** `/api/shopee/account-health/metrics?ShopId=123456789&metric_list=late_shipment_rate&metric_list=non_fulfilment_rate`

**Response:**
```json
{
  "success": true,
  "data": {
    "metric_data": [
      {
        "metric_key": "late_shipment_rate",
        "metric_value": 1.5,
        "threshold": 5.0,
        "score": 98.5,
        "unit": "%"
      },
      {
        "metric_key": "non_fulfilment_rate",
        "metric_value": 0.2,
        "threshold": 2.0,
        "score": 99.8,
        "unit": "%"
      }
    ]
  }
}
```

---

### 20.3 Get Shop Brief Penalty
**GET** `/api/shopee/account-health/brief-penalty?ShopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "tier_variation_list": [
      {
        "tier_variation_type": "LATE_SHIPMENT",
        "tier_variation_point": 1
      },
      {
        "tier_variation_type": "NON_FULFILMENT",
        "tier_variation_point": 1
      }
    ]
  }
}
```

---

## 21. SBS (SHOPEE BROADCAST SYSTEM) APIs

### 21.1 Get SBS Item List
**GET** `/api/shopee/sbs/items?ShopId=123456789&page_no=1&page_size=20&item_status=NORMAL`

**Response:**
```json
{
  "success": true,
  "data": {
    "item_list": [
      {
        "item_id": 980000001,
        "item_name": "Wireless Earbuds Pro",
        "item_sku": "WE-PRO-BLK",
        "price": 5990,
        "stock": 150,
        "status": "NORMAL"
      }
    ],
    "total_count": 1,
    "has_next_page": false
  }
}
```

---

### 21.2 Create SBS Item
**POST** `/api/shopee/sbs/items`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "item_id": 980000001,
  "price": 5990,
  "stock": 150,
  "logistics_channel_id": 70001
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "item_id": 980000001,
    "price": 5990,
    "stock": 150
  }
}
```

---

### 21.3 Update SBS Item
**PUT** `/api/shopee/sbs/items`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "item_id": 980000001,
  "price": 5490,
  "stock": 200
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "item_id": 980000001,
    "price": 5490,
    "stock": 200
  }
}
```

---

### 21.4 Delete SBS Items
**POST** `/api/shopee/sbs/items/delete`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "item_id_list": [980000001, 980000002]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "failed_list": []
  }
}
```

---

### 21.5 Get SBS Item
**GET** `/api/shopee/sbs/item?ShopId=123456789&item_id=980000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "item_id": 980000001,
    "item_name": "Wireless Earbuds Pro",
    "price": 5490,
    "stock": 200,
    "status": "NORMAL",
    "logistics_channel_id": 70001
  }
}
```

---

## 22. FBS (FULFILMENT BY SHOPEE) APIs

### 22.1 Get FBS Item List
**GET** `/api/shopee/fbs/items?ShopId=123456789&page_no=1&page_size=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "item_list": [
      {
        "item_id": 980000010,
        "item_name": "Ergonomic Office Chair",
        "sku_list": ["CHAIR-BLK-L", "CHAIR-WHT-L"],
        "stock": 30,
        "status": "NORMAL"
      }
    ],
    "total_count": 1,
    "has_next_page": false
  }
}
```

---

### 22.2 Create FBS Item
**POST** `/api/shopee/fbs/items`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "item_id": 980000010,
  "sku_list": ["CHAIR-BLK-L", "CHAIR-WHT-L"]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "item_id": 980000010
  }
}
```

---

### 22.3 Update FBS Item
**PUT** `/api/shopee/fbs/items`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "item_id": 980000010,
  "sku_list": ["CHAIR-BLK-L", "CHAIR-WHT-L", "CHAIR-GRY-L"]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "item_id": 980000010
  }
}
```

---

### 22.4 Delete FBS Items
**POST** `/api/shopee/fbs/items/delete`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "item_id_list": [980000010, 980000011]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "failed_list": []
  }
}
```

---

## 23. GLOBAL PRODUCT APIs

### 23.1 Get Global Category List
**GET** `/api/shopee/global-product/categories?Language=en`

**Response:**
```json
{
  "success": true,
  "data": {
    "CategoryList": [
      {
        "CategoryId": 100001,
        "ParentCategoryId": 0,
        "OriginalCategoryName": "Electronics",
        "DisplayCategoryName": "Electronics",
        "HasChildren": true
      },
      {
        "CategoryId": 100002,
        "ParentCategoryId": 100001,
        "OriginalCategoryName": "Mobile Phones",
        "DisplayCategoryName": "Mobile Phones",
        "HasChildren": false
      }
    ]
  }
}
```

---

### 23.2 Get Category Attribute
**GET** `/api/shopee/global-product/attributes?CategoryId=100002&Language=en`

**Response:**
```json
{
  "success": true,
  "data": {
    "Attributes": [
      {
        "AttributeId": 2001,
        "OriginalAttributeName": "Brand",
        "IsMandatory": true,
        "AttributeType": "ENUM_TYPE",
        "AttributeValueList": [
          { "ValueId": 5001, "OriginalValueName": "Samsung", "DisplayValueName": "Samsung" },
          { "ValueId": 5002, "OriginalValueName": "Apple", "DisplayValueName": "Apple" }
        ]
      }
    ]
  }
}
```

---

### 23.3 Get Brand List
**GET** `/api/shopee/global-product/brands?CategoryId=100002&Offset=0&PageSize=20&Status=1`

**Response:**
```json
{
  "success": true,
  "data": {
    "BrandList": [
      { "BrandId": 6001, "OriginalBrandName": "Samsung", "DisplayBrandName": "Samsung" },
      { "BrandId": 6002, "OriginalBrandName": "Apple", "DisplayBrandName": "Apple" }
    ],
    "TotalCount": 2,
    "HasNextPage": false,
    "NextOffset": 0
  }
}
```

---

### 23.4 Add Global Item
**POST** `/api/shopee/global-product/item`

**Request Body:**
```json
{
  "OriginalName": "Wireless Bluetooth Headphones X1",
  "Description": "Premium sound quality with active noise cancellation.",
  "OriginalSku": "WBH-X1-BLK",
  "Price": 12900,
  "Stock": 500,
  "Image": [
    { "Url": "https://cdn.shopee.com/img/headphones-main.jpg" }
  ],
  "CategoryId": 100002,
  "Condition": "NEW",
  "ItemDangerous": 0,
  "LogisticInfo": [
    { "LogisticsChannelId": 70001, "Enabled": true, "IsFree": false }
  ],
  "AttributeList": [
    {
      "AttributeId": 2001,
      "AttributeValueList": [
        { "ValueId": 5001, "OriginalValueName": "Samsung", "DisplayValueName": "Samsung" }
      ]
    }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001,
    "GlobalSkuList": [
      { "GlobalModelId": 88000001, "ModelSku": "WBH-X1-BLK-DEFAULT" }
    ]
  }
}
```

---

### 23.5 Copy Item to Shops
**POST** `/api/shopee/global-product/item/copy`

**Request Body:**
```json
{
  "ItemId": 77000001,
  "ShopIdList": [123456789, 123456790]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "SuccessShopIdList": [123456789, 123456790],
    "FailedList": []
  }
}
```

---

### 23.6 Delete Global Items
**POST** `/api/shopee/global-product/item/delete`

**Request Body:**
```json
{
  "GlobalItemIdList": [77000001, 77000002]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 23.7 Get Global Item List
**GET** `/api/shopee/global-product/items?Offset=0&PageSize=20&ItemStatus=NORMAL`

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemList": [
      {
        "GlobalItemId": 77000001,
        "OriginalName": "Wireless Bluetooth Headphones X1",
        "GlobalSku": "WBH-X1-BLK",
        "Price": 12900,
        "Stock": 500,
        "Status": "NORMAL"
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false,
    "NextOffset": 0
  }
}
```

---

### 23.8 Get Global Item Detail
**POST** `/api/shopee/global-product/item/detail`

**Request Body:**
```json
{
  "GlobalItemIdList": [77000001, 77000002]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemList": [
      {
        "GlobalItemId": 77000001,
        "OriginalName": "Wireless Bluetooth Headphones X1",
        "Description": "Premium sound quality with active noise cancellation.",
        "OriginalSku": "WBH-X1-BLK",
        "Price": 12900,
        "Stock": 500,
        "Status": "NORMAL",
        "CategoryId": 100002,
        "Image": [
          { "Url": "https://cdn.shopee.com/img/headphones-main.jpg" }
        ]
      }
    ]
  }
}
```

---

### 23.9 Update Global Item
**POST** `/api/shopee/global-product/item/update`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "OriginalName": "Wireless Bluetooth Headphones X1 Pro",
  "Description": "Updated: Premium ANC headphones with 40h battery.",
  "Price": 13900,
  "Stock": 450,
  "Image": [
    { "Url": "https://cdn.shopee.com/img/headphones-main-v2.jpg" }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001
  }
}
```

---

### 23.10 Search Global Items
**GET** `/api/shopee/global-product/items/search?Keyword=headphones&CategoryId=100002&Offset=0&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemList": [
      {
        "GlobalItemId": 77000001,
        "OriginalName": "Wireless Bluetooth Headphones X1 Pro",
        "GlobalSku": "WBH-X1-BLK",
        "Price": 13900,
        "Stock": 450,
        "Status": "NORMAL"
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false,
    "NextOffset": 0
  }
}
```

---

### 23.11 Init Tier Variation
**POST** `/api/shopee/global-product/item/tier-variation/init`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "TierVariation": [
    {
      "Name": "Color",
      "OptionList": [
        { "Option": "Black", "Image": { "Url": "https://cdn.shopee.com/img/blk.jpg" } },
        { "Option": "White", "Image": { "Url": "https://cdn.shopee.com/img/wht.jpg" } }
      ]
    }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001,
    "GlobalModelList": [
      { "GlobalModelId": 88000001, "TierIndex": [0], "Price": 12900, "Stock": 250, "ModelSku": "WBH-X1-BLK" },
      { "GlobalModelId": 88000002, "TierIndex": [1], "Price": 12900, "Stock": 250, "ModelSku": "WBH-X1-WHT" }
    ]
  }
}
```

---

### 23.12 Update Tier Variation
**POST** `/api/shopee/global-product/item/tier-variation/update`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "TierVariation": [
    {
      "Name": "Color",
      "OptionList": [
        { "Option": "Black", "Image": { "Url": "https://cdn.shopee.com/img/blk.jpg" } },
        { "Option": "White", "Image": { "Url": "https://cdn.shopee.com/img/wht.jpg" } },
        { "Option": "Grey", "Image": { "Url": "https://cdn.shopee.com/img/gry.jpg" } }
      ]
    }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001
  }
}
```

---

### 23.13 Get Model List
**GET** `/api/shopee/global-product/item/models?GlobalItemId=77000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "TierVariation": [
      {
        "Name": "Color",
        "OptionList": [
          { "Option": "Black", "Image": { "Url": "https://cdn.shopee.com/img/blk.jpg" } },
          { "Option": "White", "Image": { "Url": "https://cdn.shopee.com/img/wht.jpg" } }
        ]
      }
    ],
    "GlobalModelList": [
      { "GlobalModelId": 88000001, "TierIndex": [0], "Price": 12900, "Stock": 250, "ModelSku": "WBH-X1-BLK" },
      { "GlobalModelId": 88000002, "TierIndex": [1], "Price": 12900, "Stock": 250, "ModelSku": "WBH-X1-WHT" }
    ]
  }
}
```

---

### 23.14 Update Models
**POST** `/api/shopee/global-product/item/models/update`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "GlobalModelList": [
    { "GlobalModelId": 88000001, "Price": 11900, "Stock": 300, "ModelSku": "WBH-X1-BLK" },
    { "GlobalModelId": 88000002, "Price": 11900, "Stock": 200, "ModelSku": "WBH-X1-WHT" }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001,
    "GlobalModelList": [
      { "GlobalModelId": 88000001, "TierIndex": [0], "Price": 11900, "Stock": 300, "ModelSku": "WBH-X1-BLK" },
      { "GlobalModelId": 88000002, "TierIndex": [1], "Price": 11900, "Stock": 200, "ModelSku": "WBH-X1-WHT" }
    ]
  }
}
```

---

### 23.15 Add Global Models
**POST** `/api/shopee/global-product/item/models/add`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "GlobalModelList": [
    { "GlobalModelId": 0, "Price": 11900, "Stock": 150, "ModelSku": "WBH-X1-GRY" }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001,
    "GlobalModelList": [
      { "GlobalModelId": 88000003, "TierIndex": [2], "Price": 11900, "Stock": 150, "ModelSku": "WBH-X1-GRY" }
    ]
  }
}
```

---

### 23.16 Delete Global Models
**POST** `/api/shopee/global-product/item/models/delete`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "GlobalModelIdList": [88000003]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001
  }
}
```

---

### 23.17 Get Global Item Info
**GET** `/api/shopee/global-product/item/info?GlobalItemId=77000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001,
    "OriginalName": "Wireless Bluetooth Headphones X1 Pro",
    "Description": "Updated: Premium ANC headphones with 40h battery.",
    "Price": 13900,
    "Stock": 500,
    "Status": "NORMAL"
  }
}
```

---

### 23.18 Update Global Item Status
**POST** `/api/shopee/global-product/item/status`

**Request Body:**
```json
{
  "GlobalItemIdList": [77000001, 77000002],
  "Status": "UNLIST"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 23.19 Get Shop Item Info
**GET** `/api/shopee/global-product/item/shop-info?GlobalItemId=77000001&ShopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "ItemId": 980000099,
    "ShopId": 123456789,
    "Status": "NORMAL"
  }
}
```

---

### 23.20 Get Channel List
**GET** `/api/shopee/global-product/item/channels?GlobalItemId=77000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "LogisticInfo": [
      { "LogisticsChannelId": 70001, "Enabled": true, "IsFree": false },
      { "LogisticsChannelId": 70002, "Enabled": false, "IsFree": false }
    ]
  }
}
```

---

### 23.21 Update Global Item Logistic
**POST** `/api/shopee/global-product/item/logistic`

**Request Body:**
```json
{
  "GlobalItemId": 77000001,
  "LogisticInfo": [
    { "LogisticsChannelId": 70001, "Enabled": true, "IsFree": true },
    { "LogisticsChannelId": 70002, "Enabled": true, "IsFree": false }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "GlobalItemId": 77000001
  }
}
```

---

### 23.22 Batch Update Price
**POST** `/api/shopee/global-product/items/batch-price`

**Request Body:**
```json
{
  "GlobalItemIdList": [77000001, 77000002],
  "Price": 10900
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 23.23 Batch Update Stock
**POST** `/api/shopee/global-product/items/batch-stock`

**Request Body:**
```json
{
  "GlobalItemIdList": [77000001, 77000002],
  "Stock": 500
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

## 24. ADS APIs

### 24.1 Get Recommend Keywords
**GET** `/api/shopee/ads/recommend-keywords?ItemId=980000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "KeywordList": [
      {
        "Keyword": "wireless earbuds",
        "MatchType": "BROAD",
        "Bid": 500,
        "CpcBid": 450,
        "IsRecommend": true
      },
      {
        "Keyword": "bluetooth earphones",
        "MatchType": "EXACT",
        "Bid": 600,
        "CpcBid": 550,
        "IsRecommend": true
      }
    ]
  }
}
```

---

### 24.2 Create Campaign
**POST** `/api/shopee/ads/campaign`

**Request Body:**
```json
{
  "Name": "Q3 Mega Sale Campaign",
  "CampaignType": 1,
  "BudgetType": 1,
  "Budget": 50000,
  "StartTime": 1720000000,
  "EndTime": 1722678400
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 55000001
  }
}
```

---

### 24.3 Get Campaign List
**GET** `/api/shopee/ads/campaigns?PageNo=1&PageSize=20&Status=ONGOING`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignList": [
      {
        "CampaignId": 55000001,
        "Name": "Q3 Mega Sale Campaign",
        "Status": "ONGOING",
        "BudgetType": 1,
        "Budget": 50000,
        "StartTime": 1720000000,
        "EndTime": 1722678400
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 24.4 Get Campaign
**GET** `/api/shopee/ads/campaign?CampaignId=55000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 55000001,
    "Name": "Q3 Mega Sale Campaign",
    "Status": "ONGOING",
    "BudgetType": 1,
    "Budget": 50000,
    "StartTime": 1720000000,
    "EndTime": 1722678400,
    "TotalImpression": 125000,
    "TotalClick": 3400,
    "TotalCost": 12500
  }
}
```

---

### 24.5 Update Campaign
**POST** `/api/shopee/ads/campaign/update`

**Request Body:**
```json
{
  "CampaignId": 55000001,
  "Name": "Q3 Mega Sale Campaign - Extended",
  "Budget": 75000,
  "StartTime": 1720000000,
  "EndTime": 1725356800,
  "Status": "ONGOING"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 55000001
  }
}
```

---

### 24.6 Delete Campaigns
**POST** `/api/shopee/ads/campaign/delete`

**Request Body:**
```json
{
  "CampaignIdList": [55000001, 55000002]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 24.7 Create Ad Group
**POST** `/api/shopee/ads/adgroup`

**Request Body:**
```json
{
  "CampaignId": 55000001,
  "Name": "Earbuds Ad Group",
  "BidType": 1,
  "Bid": 500,
  "Status": "ONGOING"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdgroupId": 56000001
  }
}
```

---

### 24.8 Get Ad Group List
**GET** `/api/shopee/ads/adgroups?CampaignId=55000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "AdgroupList": [
      {
        "AdgroupId": 56000001,
        "CampaignId": 55000001,
        "Name": "Earbuds Ad Group",
        "BidType": 1,
        "Bid": 500,
        "Status": "ONGOING"
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 24.9 Update Ad Group
**POST** `/api/shopee/ads/adgroup/update`

**Request Body:**
```json
{
  "AdgroupId": 56000001,
  "Name": "Earbuds Ad Group - Boosted",
  "Bid": 650,
  "Status": "ONGOING"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdgroupId": 56000001
  }
}
```

---

### 24.10 Create Ad
**POST** `/api/shopee/ads/ad`

**Request Body:**
```json
{
  "AdgroupId": 56000001,
  "ItemId": 980000001,
  "ModelId": 88000001,
  "Bid": 480
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdId": 57000001
  }
}
```

---

### 24.11 Get Ad List
**GET** `/api/shopee/ads/ads?AdgroupId=56000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "AdList": [
      {
        "AdId": 57000001,
        "AdgroupId": 56000001,
        "ItemId": 980000001,
        "ModelId": 88000001,
        "Bid": 480,
        "Status": "ONGOING"
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 24.12 Update Ad
**POST** `/api/shopee/ads/ad/update`

**Request Body:**
```json
{
  "AdId": 57000001,
  "Bid": 550,
  "Status": "ONGOING"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdId": 57000001
  }
}
```

---

### 24.13 Get Ad Daily Performance
**GET** `/api/shopee/ads/ad/performance?AdId=57000001&StartDate=2024-07-01&EndDate=2024-07-07`

**Response:**
```json
{
  "success": true,
  "data": {
    "AdId": 57000001,
    "PerformanceList": [
      {
        "Date": "2024-07-01",
        "Impression": 18500,
        "Click": 520,
        "Cost": 2496,
        "Order": 32,
        "Gmv": 191680
      },
      {
        "Date": "2024-07-02",
        "Impression": 21000,
        "Click": 610,
        "Cost": 3355,
        "Order": 41,
        "Gmv": 245990
      }
    ]
  }
}
```

---

### 24.14 Get Campaign Daily Performance
**GET** `/api/shopee/ads/campaign/performance?CampaignId=55000001&StartDate=2024-07-01&EndDate=2024-07-07`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 55000001,
    "PerformanceList": [
      {
        "Date": "2024-07-01",
        "Impression": 45000,
        "Click": 1250,
        "Cost": 6000,
        "Order": 78,
        "Gmv": 468000
      }
    ]
  }
}
```

---

### 24.15 Add Keywords
**POST** `/api/shopee/ads/keywords`

**Request Body:**
```json
{
  "AdgroupId": 56000001,
  "KeywordList": [
    { "Keyword": "wireless earbuds", "MatchType": "BROAD", "Bid": 500, "CpcBid": 450, "IsRecommend": false },
    { "Keyword": "bluetooth earphones", "MatchType": "EXACT", "Bid": 600, "CpcBid": 550, "IsRecommend": false }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdgroupId": 56000001,
    "KeywordList": [
      { "Keyword": "wireless earbuds", "MatchType": "BROAD", "Bid": 500, "CpcBid": 450, "IsRecommend": false },
      { "Keyword": "bluetooth earphones", "MatchType": "EXACT", "Bid": 600, "CpcBid": 550, "IsRecommend": false }
    ]
  }
}
```

---

### 24.16 Get Keyword List
**GET** `/api/shopee/ads/keywords?AdgroupId=56000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "KeywordList": [
      { "Keyword": "wireless earbuds", "MatchType": "BROAD", "Bid": 500, "CpcBid": 450, "IsRecommend": false },
      { "Keyword": "bluetooth earphones", "MatchType": "EXACT", "Bid": 600, "CpcBid": 550, "IsRecommend": false }
    ],
    "TotalCount": 2,
    "HasNextPage": false
  }
}
```

---

### 24.17 Update Keywords
**POST** `/api/shopee/ads/keywords/update`

**Request Body:**
```json
{
  "AdgroupId": 56000001,
  "KeywordList": [
    { "Keyword": "wireless earbuds", "MatchType": "BROAD", "Bid": 650, "CpcBid": 600, "IsRecommend": false }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdgroupId": 56000001,
    "KeywordList": [
      { "Keyword": "wireless earbuds", "MatchType": "BROAD", "Bid": 650, "CpcBid": 600, "IsRecommend": false }
    ]
  }
}
```

---

### 24.18 Delete Keywords
**POST** `/api/shopee/ads/keywords/delete`

**Request Body:**
```json
{
  "AdgroupId": 56000001,
  "KeywordList": ["wireless earbuds", "bluetooth earphones"]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "AdgroupId": 56000001,
    "FailedList": []
  }
}
```

---

## 25. FOLLOW PRIZE APIs

### 25.1 Get Follow Prize Info
**GET** `/api/shopee/follow-prize/info?ShopId=123456789`

**Response:**
```json
{
  "success": true,
  "data": {
    "FollowPrizeId": 66000001,
    "Name": "New Follower Lucky Draw",
    "Status": "ONGOING",
    "StartTime": 1720000000,
    "EndTime": 1722678400,
    "PrizeType": 1,
    "PrizeQuantity": 50,
    "PrizeItemId": 980000001
  }
}
```

---

### 25.2 Create Follow Prize
**POST** `/api/shopee/follow-prize`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "Name": "New Follower Lucky Draw",
  "StartTime": 1720000000,
  "EndTime": 1722678400,
  "PrizeType": 1,
  "PrizeQuantity": 50,
  "PrizeItemId": 980000001
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FollowPrizeId": 66000001
  }
}
```

---

### 25.3 Update Follow Prize
**PUT** `/api/shopee/follow-prize`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "FollowPrizeId": 66000001,
  "Name": "New Follower Lucky Draw - Extended",
  "StartTime": 1720000000,
  "EndTime": 1725356800
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FollowPrizeId": 66000001
  }
}
```

---

### 25.4 End Follow Prize
**POST** `/api/shopee/follow-prize/end`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "FollowPrizeId": 66000001
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FollowPrizeId": 66000001
  }
}
```

---

### 25.5 Get Follow Prize Participant List
**GET** `/api/shopee/follow-prize/participants?ShopId=123456789&FollowPrizeId=66000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "ParticipantList": [
      {
        "UserId": 445566778,
        "FollowTime": 1720100000,
        "IsWinner": true,
        "PrizeStatus": "SENT"
      },
      {
        "UserId": 445566779,
        "FollowTime": 1720110000,
        "IsWinner": false,
        "PrizeStatus": "NONE"
      }
    ],
    "TotalCount": 2,
    "HasNextPage": false
  }
}
```

---

### 25.6 Get Follow Prize Winner List
**GET** `/api/shopee/follow-prize/winners?ShopId=123456789&FollowPrizeId=66000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "WinnerList": [
      {
        "UserId": 445566778,
        "WinTime": 1720200000,
        "PrizeStatus": "SENT",
        "OrderSn": "240701PRIZE001"
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

## 26. VIDEO APIs

### 26.1 Upload Video
**POST** `/api/shopee/video/upload`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "VideoData": "<base64-encoded-video-bytes>",
  "FileName": "product-demo-earbuds.mp4"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "VideoId": "v_7a8b9c0d1e2f3a4b",
    "ThumbnailUrl": "https://cdn.shopee.com/thumb/product-demo-earbuds.jpg",
    "VideoUrl": "https://cdn.shopee.com/video/product-demo-earbuds.mp4",
    "Duration": 45,
    "Size": 20480000,
    "Status": "PROCESSING"
  }
}
```

---

### 26.2 Get Video List
**GET** `/api/shopee/video/list?ShopId=123456789&PageNo=1&PageSize=20&Status=NORMAL`

**Response:**
```json
{
  "success": true,
  "data": {
    "VideoList": [
      {
        "VideoId": "v_7a8b9c0d1e2f3a4b",
        "ThumbnailUrl": "https://cdn.shopee.com/thumb/product-demo-earbuds.jpg",
        "VideoUrl": "https://cdn.shopee.com/video/product-demo-earbuds.mp4",
        "Duration": 45,
        "Size": 20480000,
        "Status": "NORMAL",
        "CreateTime": 1720100000
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 26.3 Get Video
**GET** `/api/shopee/video?ShopId=123456789&VideoId=v_7a8b9c0d1e2f3a4b`

**Response:**
```json
{
  "success": true,
  "data": {
    "VideoId": "v_7a8b9c0d1e2f3a4b",
    "ThumbnailUrl": "https://cdn.shopee.com/thumb/product-demo-earbuds.jpg",
    "VideoUrl": "https://cdn.shopee.com/video/product-demo-earbuds.mp4",
    "Duration": 45,
    "Size": 20480000,
    "Status": "NORMAL",
    "CreateTime": 1720100000,
    "Title": "Wireless Earbuds Demo - Noise Cancellation Test"
  }
}
```

---

### 26.4 Delete Videos
**DELETE** `/api/shopee/video`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "VideoIdList": ["v_7a8b9c0d1e2f3a4b", "v_1b2c3d4e5f6g7h8i"]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 26.5 Update Video
**PUT** `/api/shopee/video`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "VideoId": "v_7a8b9c0d1e2f3a4b",
  "Title": "Wireless Earbuds X1 Pro - Full Feature Demo"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "VideoId": "v_7a8b9c0d1e2f3a4b"
  }
}
```

---

### 26.6 Get Video Upload Status
**GET** `/api/shopee/video/upload-status?ShopId=123456789&VideoId=v_7a8b9c0d1e2f3a4b`

**Response:**
```json
{
  "success": true,
  "data": {
    "VideoId": "v_7a8b9c0d1e2f3a4b",
    "Status": "SUCCESS",
    "Progress": 100,
    "Message": "Video processed successfully"
  }
}
```

---

### 26.7 Add Video to Item
**POST** `/api/shopee/video/item`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "ItemId": 980000001,
  "VideoId": "v_7a8b9c0d1e2f3a4b"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "ItemId": 980000001,
    "VideoId": "v_7a8b9c0d1e2f3a4b"
  }
}
```

---

### 26.8 Delete Video from Item
**DELETE** `/api/shopee/video/item`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "ItemId": 980000001,
  "VideoId": "v_7a8b9c0d1e2f3a4b"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "ItemId": 980000001
  }
}
```

---

### 26.9 Get Item Video List
**GET** `/api/shopee/video/item-list?ShopId=123456789&ItemId=980000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "VideoList": [
      {
        "VideoId": "v_7a8b9c0d1e2f3a4b",
        "ThumbnailUrl": "https://cdn.shopee.com/thumb/product-demo-earbuds.jpg",
        "VideoUrl": "https://cdn.shopee.com/video/product-demo-earbuds.mp4",
        "Duration": 45,
        "Size": 20480000,
        "Status": "NORMAL",
        "CreateTime": 1720100000
      }
    ]
  }
}
```

---

## 27. LIVESTREAM APIs

### 27.1 Create Livestream
**POST** `/api/shopee/livestream`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "Title": "Weekend Flash Sale Livestream",
  "Description": "Live demo of our newest arrivals with exclusive discounts.",
  "ScheduledStartTime": 1720800000,
  "CoverImage": "https://cdn.shopee.com/covers/livestream-cover-001.jpg"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "RtmpUrl": "rtmp://live.shopee.com/live/stream",
    "StreamKey": "sk_abc123def456ghi789"
  }
}
```

---

### 27.2 Get Livestream Detail
**GET** `/api/shopee/livestream/detail?ShopId=123456789&LivestreamId=99000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "Title": "Weekend Flash Sale Livestream",
    "Description": "Live demo of our newest arrivals with exclusive discounts.",
    "Status": "SCHEDULED",
    "StartTime": 1720800000,
    "EndTime": 0,
    "ViewerCount": 0,
    "LikeCount": 0,
    "ShareCount": 0,
    "CoverUrl": "https://cdn.shopee.com/covers/livestream-cover-001.jpg"
  }
}
```

---

### 27.3 Update Livestream
**PUT** `/api/shopee/livestream`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "LivestreamId": 99000001,
  "Title": "Weekend Flash Sale Livestream - LIVE NOW",
  "Description": "Join us live! Huge discounts on all electronics.",
  "CoverImage": "https://cdn.shopee.com/covers/livestream-cover-002.jpg"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001
  }
}
```

---

### 27.4 End Livestream
**POST** `/api/shopee/livestream/end`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "LivestreamId": 99000001
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "Status": "ENDED"
  }
}
```

---

### 27.5 Get Livestream List
**GET** `/api/shopee/livestream/list?ShopId=123456789&PageNo=1&PageSize=20&Status=ENDED`

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamList": [
      {
        "LivestreamId": 99000001,
        "Title": "Weekend Flash Sale Livestream",
        "Status": "ENDED",
        "StartTime": 1720800000,
        "ViewerCount": 8500
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 27.6 Add Livestream Products
**POST** `/api/shopee/livestream/products`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "LivestreamId": 99000001,
  "ItemIdList": [980000001, 980000002, 980000010]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "FailedList": []
  }
}
```

---

### 27.7 Delete Livestream Products
**DELETE** `/api/shopee/livestream/products`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "LivestreamId": 99000001,
  "ItemIdList": [980000010]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "FailedList": []
  }
}
```

---

### 27.8 Get Livestream Product List
**GET** `/api/shopee/livestream/products?ShopId=123456789&LivestreamId=99000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "ItemList": [
      {
        "ItemId": 980000001,
        "ItemName": "Wireless Earbuds Pro",
        "Price": 5490,
        "Stock": 200,
        "Status": "NORMAL"
      },
      {
        "ItemId": 980000002,
        "ItemName": "Bluetooth Speaker Mini",
        "Price": 3990,
        "Stock": 120,
        "Status": "NORMAL"
      }
    ]
  }
}
```

---

### 27.9 Pin Livestream Product
**POST** `/api/shopee/livestream/products/pin`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "LivestreamId": 99000001,
  "ItemId": 980000001
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "ItemId": 980000001
  }
}
```

---

### 27.10 Get Livestream Metrics
**GET** `/api/shopee/livestream/metrics?ShopId=123456789&LivestreamId=99000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "TotalViewer": 12500,
    "UniqueViewer": 9800,
    "TotalLike": 45000,
    "TotalShare": 3200,
    "TotalOrder": 620,
    "TotalGmv": 3409800,
    "PeakViewer": 4200,
    "AvgWatchTime": 840
  }
}
```

---

### 27.11 Get Livestream Comments
**GET** `/api/shopee/livestream/comments?ShopId=123456789&LivestreamId=99000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "CommentList": [
      {
        "CommentId": 33000001,
        "UserId": 445566778,
        "Username": "shopee_user_778",
        "Comment": "Is this compatible with iPhone?",
        "CommentTime": 1720801200
      },
      {
        "CommentId": 33000002,
        "UserId": 445566779,
        "Username": "buyer_sg_779",
        "Comment": "Love the sound quality!",
        "CommentTime": 1720801800
      }
    ],
    "TotalCount": 2,
    "HasNextPage": false
  }
}
```

---

### 27.12 Get Livestream Replay List
**GET** `/api/shopee/livestream/replays?ShopId=123456789&LivestreamId=99000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "ReplayList": [
      {
        "ReplayId": "rp_abc123456def789",
        "VideoUrl": "https://cdn.shopee.com/replay/livestream-99000001-part1.mp4",
        "ThumbnailUrl": "https://cdn.shopee.com/thumb/replay-99000001-part1.jpg",
        "Duration": 3600,
        "CreateTime": 1720804000
      }
    ]
  }
}
```

---

### 27.13 Get Livestream Cover
**GET** `/api/shopee/livestream/cover?ShopId=123456789&LivestreamId=99000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "LivestreamId": 99000001,
    "CoverUrl": "https://cdn.shopee.com/covers/livestream-cover-002.jpg",
    "ThumbnailUrl": "https://cdn.shopee.com/thumb/livestream-thumb-99000001.jpg"
  }
}
```

---

### 27.14 Send Livestream Notification
**POST** `/api/shopee/livestream/notification`

**Request Body:**
```json
{
  "ShopId": 123456789,
  "LivestreamId": 99000001,
  "NotificationType": "UPCOMING",
  "Message": "Our flash sale livestream starts in 30 minutes! Don't miss exclusive deals."
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "Success": true,
    "FailedCount": 0
  }
}
```

---

### 27.15 Get Livestream Report
**GET** `/api/shopee/livestream/report?ShopId=123456789&StartDate=2024-07-01&EndDate=2024-07-07&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "ReportList": [
      {
        "LivestreamId": 99000001,
        "Title": "Weekend Flash Sale Livestream",
        "StartTime": 1720800000,
        "EndTime": 1720814400,
        "ViewerCount": 12500,
        "OrderCount": 620,
        "Gmv": 3409800
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

## 28. AMS (AFFILIATE MARKETING SYSTEM) APIs

### 28.1 Get Affiliate Campaign List
**GET** `/api/shopee/ams/campaigns?PageNo=1&PageSize=20&Status=ONGOING`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignList": [
      {
        "CampaignId": 60000001,
        "Name": "Influencer Partner Program Q3",
        "CampaignType": 1,
        "Status": "ONGOING",
        "StartTime": 1720000000,
        "EndTime": 1722678400,
        "CommissionRate": 8.5,
        "Budget": 100000,
        "TotalClick": 52000,
        "TotalOrder": 1800,
        "TotalGmv": 10800000
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 28.2 Create Affiliate Campaign
**POST** `/api/shopee/ams/campaigns`

**Request Body:**
```json
{
  "Name": "Influencer Partner Program Q3",
  "CampaignType": 1,
  "CommissionRate": 8.5,
  "StartTime": 1720000000,
  "EndTime": 1722678400,
  "Budget": 100000,
  "Description": "Partner with top KOLs to drive sales across our electronics range."
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001
  }
}
```

---

### 28.3 Get Affiliate Campaign Detail
**GET** `/api/shopee/ams/campaigns/detail?CampaignId=60000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001,
    "Name": "Influencer Partner Program Q3",
    "CampaignType": 1,
    "Status": "ONGOING",
    "StartTime": 1720000000,
    "EndTime": 1722678400,
    "CommissionRate": 8.5,
    "Budget": 100000,
    "Description": "Partner with top KOLs to drive sales across our electronics range.",
    "TotalClick": 52000,
    "TotalOrder": 1800,
    "TotalGmv": 10800000
  }
}
```

---

### 28.4 Update Affiliate Campaign
**POST** `/api/shopee/ams/campaigns/update`

**Request Body:**
```json
{
  "CampaignId": 60000001,
  "Name": "Influencer Partner Program Q3 - Extended",
  "CommissionRate": 9.0,
  "Budget": 150000,
  "EndTime": 1725356800,
  "Status": "ONGOING",
  "Description": "Extended campaign with increased commission rate."
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001
  }
}
```

---

### 28.5 Delete Affiliate Campaigns
**POST** `/api/shopee/ams/campaigns/delete`

**Request Body:**
```json
{
  "CampaignIdList": [60000001, 60000002]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 28.6 Add Items to Campaign
**POST** `/api/shopee/ams/campaigns/items/add`

**Request Body:**
```json
{
  "CampaignId": 60000001,
  "ItemIdList": [980000001, 980000002, 980000010]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001,
    "FailedList": []
  }
}
```

---

### 28.7 Remove Items from Campaign
**POST** `/api/shopee/ams/campaigns/items/remove`

**Request Body:**
```json
{
  "CampaignId": 60000001,
  "ItemIdList": [980000010]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001,
    "FailedList": []
  }
}
```

---

### 28.8 Get Campaign Item List
**GET** `/api/shopee/ams/campaigns/items?CampaignId=60000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001,
    "ItemList": [
      {
        "ItemId": 980000001,
        "ItemName": "Wireless Earbuds Pro",
        "ItemSku": "WE-PRO-BLK",
        "Price": 5490,
        "CommissionRate": 8.5,
        "Click": 18500,
        "Order": 620,
        "Gmv": 3403800
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 28.9 Get Affiliate Order List
**GET** `/api/shopee/ams/orders?CampaignId=60000001&PageNo=1&PageSize=20&StartTime=1720000000&EndTime=1722678400`

**Response:**
```json
{
  "success": true,
  "data": {
    "OrderList": [
      {
        "OrderId": "240701AMS0000001",
        "CampaignId": 60000001,
        "AffiliateId": 70000001,
        "ItemId": 980000001,
        "ItemName": "Wireless Earbuds Pro",
        "Quantity": 2,
        "OrderAmount": 10980,
        "Commission": 933,
        "Status": "COMPLETED",
        "CreateTime": 1720100000
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 28.10 Get Affiliate List
**GET** `/api/shopee/ams/affiliates?CampaignId=60000001&PageNo=1&PageSize=20&Status=APPROVED`

**Response:**
```json
{
  "success": true,
  "data": {
    "AffiliateList": [
      {
        "AffiliateId": 70000001,
        "Name": "TechReviewer_MY",
        "Status": "APPROVED",
        "TotalClick": 18500,
        "TotalOrder": 620,
        "TotalGmv": 3403800,
        "TotalCommission": 289323
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 28.11 Get Affiliate Detail
**GET** `/api/shopee/ams/affiliates/detail?AffiliateId=70000001`

**Response:**
```json
{
  "success": true,
  "data": {
    "AffiliateId": 70000001,
    "Name": "TechReviewer_MY",
    "Status": "APPROVED",
    "ApprovedTime": 1719900000,
    "TotalClick": 18500,
    "TotalOrder": 620,
    "TotalGmv": 3403800,
    "TotalCommission": 289323
  }
}
```

---

### 28.12 Approve Affiliates
**POST** `/api/shopee/ams/affiliates/approve`

**Request Body:**
```json
{
  "AffiliateIdList": [70000001, 70000002]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 28.13 Reject Affiliates
**POST** `/api/shopee/ams/affiliates/reject`

**Request Body:**
```json
{
  "AffiliateIdList": [70000003],
  "Reason": "Profile does not meet minimum follower requirements."
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "FailedList": []
  }
}
```

---

### 28.14 Get Affiliate Link List
**GET** `/api/shopee/ams/links?CampaignId=60000001&PageNo=1&PageSize=20`

**Response:**
```json
{
  "success": true,
  "data": {
    "LinkList": [
      {
        "LinkId": 80000001,
        "CampaignId": 60000001,
        "AffiliateId": 70000001,
        "ItemId": 980000001,
        "LinkUrl": "https://shopee.com.my/product/123456789/980000001?affiliate_token=abc123",
        "ShortLink": "https://shope.ee/aff_abc123",
        "Click": 18500,
        "Order": 620,
        "Gmv": 3403800,
        "CreateTime": 1719900000
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 28.15 Get Campaign Performance
**GET** `/api/shopee/ams/campaigns/performance?CampaignId=60000001&StartDate=2024-07-01&EndDate=2024-07-07`

**Response:**
```json
{
  "success": true,
  "data": {
    "CampaignId": 60000001,
    "PerformanceList": [
      {
        "Date": "2024-07-01",
        "Click": 7800,
        "Order": 260,
        "Gmv": 1427400,
        "Commission": 121329,
        "UniqueClick": 6200
      },
      {
        "Date": "2024-07-02",
        "Click": 9100,
        "Order": 310,
        "Gmv": 1701900,
        "Commission": 144661,
        "UniqueClick": 7400
      }
    ]
  }
}
```

---

### 28.16 Get Commission Settlement List
**GET** `/api/shopee/ams/settlements?PageNo=1&PageSize=20&StartTime=1720000000&EndTime=1722678400&Status=SETTLED`

**Response:**
```json
{
  "success": true,
  "data": {
    "SettlementList": [
      {
        "SettlementId": 90000001,
        "AffiliateId": 70000001,
        "Commission": 289323,
        "Status": "SETTLED",
        "SettleTime": 1722600000,
        "OrderCount": 620
      }
    ],
    "TotalCount": 1,
    "HasNextPage": false
  }
}
```

---

### 28.17 Get AMS Config
**GET** `/api/shopee/ams/config`

**Response:**
```json
{
  "success": true,
  "data": {
    "IsEnabled": true,
    "DefaultCommissionRate": 8.5,
    "MaxCommissionRate": 20.0,
    "CampaignCount": 3
  }
}
```

---

### 28.18 Update AMS Config
**POST** `/api/shopee/ams/config`

**Request Body:**
```json
{
  "DefaultCommissionRate": 9.0
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "IsEnabled": true,
    "DefaultCommissionRate": 9.0
  }
}
```

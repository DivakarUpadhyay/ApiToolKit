import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

// ── 8. Media ────────────────────────────────────────────────────────────────
test.describe('Media APIs', () => {
  test('POST /api/shopee/media/image/upload - accepts multipart form', async ({ request }) => {
    // Endpoint requires multipart/form-data; JSON body returns 415 (expected)
    const form = new FormData();
    form.append('shopId', String(shopId));
    const res = await request.post('http://localhost:5051/api/shopee/media/image/upload', {
      multipart: { shopId: String(shopId), fileName: 'test.png' },
    });
    expect([200, 400, 415]).toContain(res.status());
  });

  test('POST /api/shopee/media/image/upload-url', async ({ request }) => {
    const res = await post(request, '/api/shopee/media/image/upload-url', {
      shopId, url: 'https://example.com/image.jpg',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/media/video/upload', async ({ request }) => {
    const res = await post(request, '/api/shopee/media/video/upload', {
      shopId, videoData: '', fileName: 'test.mp4',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 9. Merchant ─────────────────────────────────────────────────────────────
test.describe('Merchant APIs', () => {
  test('GET /api/shopee/merchant/{merchantId}/info', async ({ request }) => {
    const res = await get(request, '/api/shopee/merchant/1001/info');
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/merchant/{merchantId}/shops', async ({ request }) => {
    const res = await get(request, '/api/shopee/merchant/1001/shops', {
      pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 10. Return / Refund ─────────────────────────────────────────────────────
test.describe('Return APIs', () => {
  test('GET /api/shopee/returns/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/returns/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/returns/detail', async ({ request }) => {
    const res = await get(request, '/api/shopee/returns/detail', {
      shopId: String(shopId), returnSn: 'RET123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/returns/confirm', async ({ request }) => {
    const res = await post(request, '/api/shopee/returns/confirm', {
      shopId, returnSn: 'RET123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/returns/dispute', async ({ request }) => {
    const res = await post(request, '/api/shopee/returns/dispute', {
      shopId, returnSn: 'RET123456', email: 'seller@test.com',
      reason: 'NOT_RECEIVED', textReason: 'Item was not returned',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/returns/{shopId}/dispute-reasons', async ({ request }) => {
    const res = await get(request, `/api/shopee/returns/${shopId}/dispute-reasons`);
    expect([200, 400]).toContain(res.status());
  });
});

// ── 11. Payment ─────────────────────────────────────────────────────────────
test.describe('Payment APIs', () => {
  test('GET /api/shopee/payment/escrow/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/payment/escrow/list', {
      shopId: String(shopId), releaseTimeFrom: '1700000000', releaseTimeTo: '1720000000', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/payment/escrow/detail', async ({ request }) => {
    const res = await get(request, '/api/shopee/payment/escrow/detail', {
      shopId: String(shopId), orderSn: 'TEST123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/payment/wallet/transactions', async ({ request }) => {
    const res = await get(request, '/api/shopee/payment/wallet/transactions', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/payment/methods', async ({ request }) => {
    const res = await get(request, '/api/shopee/payment/methods', {
      shopId: String(shopId),
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/payment/payout/detail', async ({ request }) => {
    const res = await get(request, '/api/shopee/payment/payout/detail', {
      shopId: String(shopId), withdrawId: '111222',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/payment/payout/info', async ({ request }) => {
    const res = await get(request, '/api/shopee/payment/payout/info', {
      shopId: String(shopId),
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 12. Bundle Deal ─────────────────────────────────────────────────────────
test.describe('Bundle Deal APIs', () => {
  test('POST /api/shopee/bundle-deal - add bundle deal', async ({ request }) => {
    const res = await post(request, '/api/shopee/bundle-deal', {
      shopId, bundleDealName: 'Buy 2 Get 10% Off', startTime: 1720000000, endTime: 1720086400,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/bundle-deal/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/bundle-deal/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/bundle-deal/{shopId}/{bundleDealId}', async ({ request }) => {
    const res = await get(request, `/api/shopee/bundle-deal/${shopId}/777`);
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/bundle-deal - update bundle deal', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/bundle-deal', {
      data: { shopId, bundleDealId: 777, bundleDealName: 'Buy 3 Get 15% Off' },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/bundle-deal/items - add items', async ({ request }) => {
    const res = await post(request, '/api/shopee/bundle-deal/items', {
      shopId, bundleDealId: 777, itemList: [{ itemId: 123456, modelIdList: [] }],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/bundle-deal/{shopId}/{bundleDealId}/items', async ({ request }) => {
    const res = await get(request, `/api/shopee/bundle-deal/${shopId}/777/items`);
    expect([200, 400]).toContain(res.status());
  });
});

// ── 13. Add-on Deal ─────────────────────────────────────────────────────────
test.describe('Add-on Deal APIs', () => {
  test('POST /api/shopee/add-on-deal - add add-on deal', async ({ request }) => {
    const res = await post(request, '/api/shopee/add-on-deal', {
      shopId, addOnDealName: 'Add-on Promo', startTime: 1720000000, endTime: 1720086400,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/add-on-deal/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/add-on-deal/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/add-on-deal/{shopId}/{addOnDealId}', async ({ request }) => {
    const res = await get(request, `/api/shopee/add-on-deal/${shopId}/666`);
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/add-on-deal/main-items', async ({ request }) => {
    const res = await post(request, '/api/shopee/add-on-deal/main-items', {
      shopId, addOnDealId: 666, itemList: [{ itemId: 123456 }],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/add-on-deal/sub-items', async ({ request }) => {
    const res = await post(request, '/api/shopee/add-on-deal/sub-items', {
      shopId, addOnDealId: 666, itemList: [{ itemId: 123457, discount: 10 }],
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 14. Shop Flash Sale ──────────────────────────────────────────────────────
test.describe('Shop Flash Sale APIs', () => {
  test('POST /api/shopee/shop-flash-sale/get-time-slot-id', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-flash-sale/get-time-slot-id', {
      shopId, startTime: 1720000000, endTime: 1720003600,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/shop-flash-sale/create', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-flash-sale/create', {
      shopId, timeslotId: 12345,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop-flash-sale/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop-flash-sale/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop-flash-sale/{flashSaleId}', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop-flash-sale/555', {
      shopId: String(shopId),
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/shop-flash-sale/items - add items', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-flash-sale/items', {
      shopId, flashSaleId: 555, items: [{ itemId: 123456, purchaseLimit: 2, price: 8000, stock: 10 }],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop-flash-sale/items', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop-flash-sale/items', {
      shopId: String(shopId), flashSaleId: '555', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop-flash-sale/item-criteria', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop-flash-sale/item-criteria', {
      shopId: String(shopId),
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 15. Top Picks ────────────────────────────────────────────────────────────
test.describe('Top Picks APIs', () => {
  test('GET /api/shopee/top-picks/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/top-picks/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/top-picks - add top picks', async ({ request }) => {
    const res = await post(request, '/api/shopee/top-picks', {
      shopId, name: 'Best Sellers', itemIdList: [123456, 123457],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/top-picks/update', async ({ request }) => {
    const res = await post(request, '/api/shopee/top-picks/update', {
      shopId, topPicksId: 444, name: 'Top Sellers', itemIdList: [123456], isActivated: true,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/top-picks/delete', async ({ request }) => {
    const res = await post(request, '/api/shopee/top-picks/delete', {
      shopId, topPicksId: 444,
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 16. Shop Category ───────────────────────────────────────────────────────
test.describe('Shop Category APIs', () => {
  test('POST /api/shopee/shop-category - add category', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-category', {
      shopId, name: 'Electronics', sortWeight: 1,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop-category/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop-category/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/shop-category/update', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-category/update', {
      shopId, shopCategoryId: 333, name: 'Tech', sortWeight: 2, status: 'NORMAL',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/shop-category/delete', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-category/delete', {
      shopId, shopCategoryId: 333,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/shop-category/items - add items', async ({ request }) => {
    const res = await post(request, '/api/shopee/shop-category/items', {
      shopId, shopCategoryId: 333, itemIdList: [123456],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop-category/items', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop-category/items', {
      shopId: String(shopId), shopCategoryId: '333', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 17. Public ───────────────────────────────────────────────────────────────
test.describe('Public APIs', () => {
  test('GET /api/shopee/public/shops', async ({ request }) => {
    const res = await get(request, '/api/shopee/public/shops', { pageNo: '1', pageSize: '10' });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/public/categories', async ({ request }) => {
    const res = await get(request, '/api/shopee/public/categories', { country: 'MY', language: 'en' });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/public/payment-methods', async ({ request }) => {
    const res = await get(request, '/api/shopee/public/payment-methods', { region: 'MY' });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 18. First Mile ───────────────────────────────────────────────────────────
test.describe('First Mile APIs', () => {
  test('POST /api/shopee/first-mile/generate', async ({ request }) => {
    const res = await post(request, '/api/shopee/first-mile/generate', {
      shopId, quantity: 1, logisticsChannelId: 80003,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/first-mile/tracking-number', async ({ request }) => {
    const res = await get(request, '/api/shopee/first-mile/tracking-number', {
      shopId: String(shopId), firstMileTrackingNumber: 'FM123456789',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/first-mile/upload', async ({ request }) => {
    const res = await post(request, '/api/shopee/first-mile/upload', {
      shopId, firstMileTrackingNumber: 'FM123456789', orderSnList: ['TEST123456'],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/first-mile/unbind', async ({ request }) => {
    const res = await post(request, '/api/shopee/first-mile/unbind', {
      shopId, firstMileTrackingNumber: 'FM123456789',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/first-mile/channels', async ({ request }) => {
    const res = await get(request, '/api/shopee/first-mile/channels', {
      shopId: String(shopId), region: 'MY',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/first-mile/tracking-list', async ({ request }) => {
    const res = await get(request, '/api/shopee/first-mile/tracking-list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 19. Push Notification ────────────────────────────────────────────────────
test.describe('Push Notification APIs', () => {
  test('POST /api/shopee/push/config - set config', async ({ request }) => {
    const res = await post(request, '/api/shopee/push/config', {
      callbackUrl: 'https://yourdomain.com/shopee/webhook', pushType: 0, blockedShopIdList: [],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/push/config - get config', async ({ request }) => {
    const res = await get(request, '/api/shopee/push/config');
    expect([200, 400]).toContain(res.status());
  });
});

// ── 20. Account Health ───────────────────────────────────────────────────────
test.describe('Account Health APIs', () => {
  test('GET /api/shopee/account-health/penalty', async ({ request }) => {
    const res = await get(request, '/api/shopee/account-health/penalty', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/account-health/metrics', async ({ request }) => {
    const res = await get(request, '/api/shopee/account-health/metrics', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/account-health/brief-penalty', async ({ request }) => {
    const res = await get(request, '/api/shopee/account-health/brief-penalty', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 21. SBS ──────────────────────────────────────────────────────────────────
test.describe('SBS APIs', () => {
  test('GET /api/shopee/sbs/items', async ({ request }) => {
    const res = await get(request, '/api/shopee/sbs/items', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/sbs/items - create item', async ({ request }) => {
    const res = await post(request, '/api/shopee/sbs/items', {
      shopId, itemId: 123456, price: 10000, stock: 50, logisticsChannelId: 80003,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/sbs/item', async ({ request }) => {
    const res = await get(request, '/api/shopee/sbs/item', {
      shopId: String(shopId), itemId: '123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/sbs/items - update item', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/sbs/items', {
      data: { shopId, itemId: 123456, price: 9500 },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/sbs/items/delete', async ({ request }) => {
    const res = await post(request, '/api/shopee/sbs/items/delete', {
      shopId, itemIdList: [123456],
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 22. FBS ──────────────────────────────────────────────────────────────────
test.describe('FBS APIs', () => {
  test('GET /api/shopee/fbs/items', async ({ request }) => {
    const res = await get(request, '/api/shopee/fbs/items', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/fbs/items - create item', async ({ request }) => {
    const res = await post(request, '/api/shopee/fbs/items', {
      shopId, itemId: 123456, skuList: ['SKU-001', 'SKU-002'],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/fbs/items - update item', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/fbs/items', {
      data: { shopId, itemId: 123456, skuList: ['SKU-001'] },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/fbs/items/delete', async ({ request }) => {
    const res = await post(request, '/api/shopee/fbs/items/delete', {
      shopId, itemIdList: [123456],
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 23. Global Product ──────────────────────────────────────────────────────
test.describe('Global Product APIs', () => {
  test('GET /api/shopee/global-product/categories', async ({ request }) => {
    const res = await get(request, '/api/shopee/global-product/categories', { language: 'en' });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/global-product/attributes', async ({ request }) => {
    const res = await get(request, '/api/shopee/global-product/attributes', {
      categoryId: '100001', language: 'en',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/global-product/brands', async ({ request }) => {
    const res = await get(request, '/api/shopee/global-product/brands', {
      categoryId: '100001', offset: '0', pageSize: '10', status: '1',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/global-product/item - add item', async ({ request }) => {
    const res = await post(request, '/api/shopee/global-product/item', {
      originalName: 'Global Test Product', description: 'Test', originalSku: 'GSKU-001',
      price: 15000, stock: 100, categoryId: 100001, condition: 'NEW', itemDangerous: 0,
      image: [{ url: 'https://example.com/img.jpg' }], logisticInfo: [], attributeList: [],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/global-product/items', async ({ request }) => {
    const res = await get(request, '/api/shopee/global-product/items', {
      offset: '0', pageSize: '10', itemStatus: 'NORMAL',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/global-product/items/search', async ({ request }) => {
    const res = await get(request, '/api/shopee/global-product/items/search', {
      keyword: 'test', offset: '0', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/global-product/item/models', async ({ request }) => {
    const res = await get(request, '/api/shopee/global-product/item/models', {
      globalItemId: '999',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/global-product/items/batch-price', async ({ request }) => {
    const res = await post(request, '/api/shopee/global-product/items/batch-price', {
      globalItemIdList: [999], price: 12000,
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 24. Ads ─────────────────────────────────────────────────────────────────
test.describe('Ads APIs', () => {
  test('GET /api/shopee/ads/recommend-keywords', async ({ request }) => {
    const res = await get(request, '/api/shopee/ads/recommend-keywords', {
      shopId: String(shopId), itemId: '123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/ads/campaign - create campaign', async ({ request }) => {
    const res = await post(request, '/api/shopee/ads/campaign', {
      shopId, name: 'Test Campaign', campaignType: 1, budgetType: 1,
      budget: 10000, startTime: 1720000000, endTime: 1720086400,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ads/campaigns', async ({ request }) => {
    const res = await get(request, '/api/shopee/ads/campaigns', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ads/campaign', async ({ request }) => {
    const res = await get(request, '/api/shopee/ads/campaign', {
      shopId: String(shopId), campaignId: '111',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/ads/adgroup - create adgroup', async ({ request }) => {
    const res = await post(request, '/api/shopee/ads/adgroup', {
      shopId, campaignId: 111, name: 'Test AdGroup', bidType: 1, bid: 500, status: 'active',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/ads/keywords - add keywords', async ({ request }) => {
    const res = await post(request, '/api/shopee/ads/keywords', {
      shopId, adgroupId: 222,
      keywordList: [{ keyword: 'test product', matchType: 'BROAD', bid: 200, cpcBid: 0, isRecommend: false }],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ads/keywords', async ({ request }) => {
    const res = await get(request, '/api/shopee/ads/keywords', {
      shopId: String(shopId), adgroupId: '222', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ads/ad/performance', async ({ request }) => {
    const res = await get(request, '/api/shopee/ads/ad/performance', {
      shopId: String(shopId), adId: '333', startDate: '2024-01-01', endDate: '2024-01-31',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 25. Follow Prize ────────────────────────────────────────────────────────
test.describe('Follow Prize APIs', () => {
  test('GET /api/shopee/follow-prize/info', async ({ request }) => {
    const res = await get(request, '/api/shopee/follow-prize/info', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/follow-prize - create', async ({ request }) => {
    const res = await post(request, '/api/shopee/follow-prize', {
      shopId, name: 'Follow & Win', startTime: 1720000000, endTime: 1720086400,
      prizeType: 1, prizeQuantity: 100, prizeItemId: 123456,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/follow-prize/participants', async ({ request }) => {
    const res = await get(request, '/api/shopee/follow-prize/participants', {
      shopId: String(shopId), followPrizeId: '300', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/follow-prize/winners', async ({ request }) => {
    const res = await get(request, '/api/shopee/follow-prize/winners', {
      shopId: String(shopId), followPrizeId: '300', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/follow-prize/end', async ({ request }) => {
    const res = await post(request, '/api/shopee/follow-prize/end', {
      shopId, followPrizeId: 300,
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 26. Video ────────────────────────────────────────────────────────────────
test.describe('Video APIs', () => {
  test('GET /api/shopee/video/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/video/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/video - detail', async ({ request }) => {
    const res = await get(request, '/api/shopee/video', {
      shopId: String(shopId), videoId: 'vid_abc123',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('DELETE /api/shopee/video - delete', async ({ request }) => {
    const res = await request.delete('http://localhost:5051/api/shopee/video', {
      data: { shopId, videoIdList: ['vid_abc123'] },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/video/upload-status', async ({ request }) => {
    const res = await get(request, '/api/shopee/video/upload-status', {
      shopId: String(shopId), videoId: 'vid_abc123',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/video/item-list', async ({ request }) => {
    const res = await get(request, '/api/shopee/video/item-list', {
      shopId: String(shopId), itemId: '123456',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 27. Livestream ──────────────────────────────────────────────────────────
test.describe('Livestream APIs', () => {
  test('POST /api/shopee/livestream - create', async ({ request }) => {
    const res = await post(request, '/api/shopee/livestream', {
      shopId, title: 'Test Livestream', description: 'Live test',
      scheduledStartTime: 1720003600, coverImage: 'https://example.com/cover.jpg',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/livestream/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/livestream/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10', status: 'upcoming',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/livestream/detail', async ({ request }) => {
    const res = await get(request, '/api/shopee/livestream/detail', {
      shopId: String(shopId), livestreamId: '500',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/livestream/metrics', async ({ request }) => {
    const res = await get(request, '/api/shopee/livestream/metrics', {
      shopId: String(shopId), livestreamId: '500',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/livestream/comments', async ({ request }) => {
    const res = await get(request, '/api/shopee/livestream/comments', {
      shopId: String(shopId), livestreamId: '500', pageNo: '1', pageSize: '20',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/livestream/report', async ({ request }) => {
    const res = await get(request, '/api/shopee/livestream/report', {
      shopId: String(shopId), startDate: '2024-01-01', endDate: '2024-01-31',
      pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 28. AMS (Affiliate Marketing) ────────────────────────────────────────────
test.describe('AMS APIs', () => {
  test('GET /api/shopee/ams/campaigns', async ({ request }) => {
    const res = await get(request, '/api/shopee/ams/campaigns', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/ams/campaigns - create campaign', async ({ request }) => {
    const res = await post(request, '/api/shopee/ams/campaigns', {
      shopId, name: 'Affiliate Campaign', campaignType: 1, commissionRate: 5.0,
      startTime: 1720000000, endTime: 1720086400, budget: 50000, description: 'Test',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ams/affiliates', async ({ request }) => {
    const res = await get(request, '/api/shopee/ams/affiliates', {
      shopId: String(shopId), campaignId: '200', pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ams/config', async ({ request }) => {
    const res = await get(request, '/api/shopee/ams/config', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ams/orders', async ({ request }) => {
    const res = await get(request, '/api/shopee/ams/orders', {
      shopId: String(shopId), campaignId: '200', pageNo: '1', pageSize: '10',
      startTime: '1720000000', endTime: '1720086400',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ams/campaigns/performance', async ({ request }) => {
    const res = await get(request, '/api/shopee/ams/campaigns/performance', {
      shopId: String(shopId), campaignId: '200',
      startDate: '2024-01-01', endDate: '2024-01-31',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/ams/settlements', async ({ request }) => {
    const res = await get(request, '/api/shopee/ams/settlements', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
      startTime: '1720000000', endTime: '1720086400',
    });
    expect([200, 400]).toContain(res.status());
  });
});

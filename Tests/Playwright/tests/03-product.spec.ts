import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

test.describe('Product APIs', () => {
  test('GET /api/shopee/product/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/product/list', {
      shopId: String(shopId), offset: '0', pageSize: '10', itemStatus: 'NORMAL',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/product/info', async ({ request }) => {
    const res = await get(request, '/api/shopee/product/info', {
      shopId: String(shopId), itemId: '123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/product - add item', async ({ request }) => {
    const res = await post(request, '/api/shopee/product', {
      shopId, originalSku: 'SKU-TEST-001', name: 'Test Product',
      description: 'A test product', price: 10000, stock: 100,
      categoryId: 100001, condition: 'NEW', itemDangerous: 0,
      images: [{ url: 'https://example.com/image.jpg' }],
      logisticInfo: [{ logisticsChannelId: 80003, enabled: true, isFree: false }],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/product - update item', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/product', {
      data: { shopId, itemId: 123456, name: 'Updated Product' },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/product/price - update price', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/product/price', {
      data: { shopId, priceList: [{ itemId: 123456, price: 9000 }] },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/product/stock - update stock', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/product/stock', {
      data: { shopId, stockList: [{ itemId: 123456, stock: 90 }] },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('DELETE /api/shopee/product - delete item', async ({ request }) => {
    const res = await request.delete('http://localhost:5051/api/shopee/product', {
      data: { shopId, itemIdList: [123456] },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/product/categories', async ({ request }) => {
    const res = await get(request, '/api/shopee/product/categories', { language: 'en' });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/product/attributes', async ({ request }) => {
    const res = await get(request, '/api/shopee/product/attributes', {
      categoryId: '100001', language: 'en',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/product/brands', async ({ request }) => {
    const res = await get(request, '/api/shopee/product/brands', {
      categoryId: '100001', offset: '0', pageSize: '10', status: '1',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/product/models', async ({ request }) => {
    const res = await get(request, '/api/shopee/product/models', {
      shopId: String(shopId), itemId: '123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/product/tier-variation - init variation', async ({ request }) => {
    const res = await post(request, '/api/shopee/product/tier-variation', {
      shopId, itemId: 123456, tierVariation: [], modelList: [],
    });
    expect([200, 400]).toContain(res.status());
  });
});

import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

test.describe('Discount APIs', () => {
  test('POST /api/shopee/discount - add discount', async ({ request }) => {
    const res = await post(request, '/api/shopee/discount', {
      shopId, discountName: 'Test Sale', startTime: 1720000000, endTime: 1720086400,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('DELETE /api/shopee/discount - delete discount', async ({ request }) => {
    const res = await request.delete('http://localhost:5051/api/shopee/discount', {
      data: { shopId, discountId: 888 },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/discount - update discount', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/discount', {
      data: { shopId, discountId: 888, discountName: 'Updated Sale' },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/discount/end - end discount', async ({ request }) => {
    const res = await post(request, '/api/shopee/discount/end', { shopId, discountId: 888 });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/discount/{shopId}/{discountId}', async ({ request }) => {
    const res = await get(request, `/api/shopee/discount/${shopId}/888`);
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/discount/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/discount/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/discount/items - add discount items', async ({ request }) => {
    const res = await post(request, '/api/shopee/discount/items', {
      shopId, discountId: 888, items: [{ itemId: 123456, itemPromotionPrice: 9000, modelList: [] }],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('DELETE /api/shopee/discount/items - remove discount items', async ({ request }) => {
    const res = await request.delete('http://localhost:5051/api/shopee/discount/items', {
      data: { shopId, discountId: 888, itemIdList: [123456] },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/discount/items - update discount items', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/discount/items', {
      data: { shopId, discountId: 888, items: [{ itemId: 123456, itemPromotionPrice: 8000, modelList: [] }] },
    });
    expect([200, 400]).toContain(res.status());
  });
});

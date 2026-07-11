import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

test.describe('Logistics APIs', () => {
  test('GET /api/shopee/logistics/info', async ({ request }) => {
    const res = await get(request, '/api/shopee/logistics/info', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/logistics/init', async ({ request }) => {
    const res = await post(request, '/api/shopee/logistics/init', {
      shopId, orderSn: 'TEST123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/logistics/ship', async ({ request }) => {
    const res = await post(request, '/api/shopee/logistics/ship', {
      shopId, orderSn: 'TEST123456', pickup: {}, dropoff: {}, nonIntegrated: {},
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/logistics/tracking-number', async ({ request }) => {
    const res = await get(request, '/api/shopee/logistics/tracking-number', {
      shopId: String(shopId), orderSn: 'TEST123456',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/logistics/{shopId}/tracking/{orderSn}', async ({ request }) => {
    const res = await get(request, `/api/shopee/logistics/${shopId}/tracking/TEST123456`);
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/logistics/update', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/logistics/update', {
      data: { shopId, orderSn: 'TEST123456', pickup: {} },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/logistics/addresses', async ({ request }) => {
    const res = await get(request, '/api/shopee/logistics/addresses', { shopId: String(shopId) });
    expect([200, 400]).toContain(res.status());
  });
});

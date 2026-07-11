import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

test.describe('Shop APIs', () => {
  test('GET /api/shopee/shop/{shopId}/info', async ({ request }) => {
    const res = await get(request, `/api/shopee/shop/${shopId}/info`);
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/shop/list');
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/shop/{shopId}/profile', async ({ request }) => {
    const res = await get(request, `/api/shopee/shop/${shopId}/profile`);
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/shop/profile - update profile', async ({ request }) => {
    const res = await request.put(`http://localhost:5051/api/shopee/shop/profile`, {
      data: { shopId, shopName: 'Updated Test Shop', shopDescription: 'Updated description' },
    });
    expect([200, 400]).toContain(res.status());
  });
});

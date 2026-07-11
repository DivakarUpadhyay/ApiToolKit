import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

test.describe('Order APIs', () => {
  test('GET /api/shopee/order/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/order/list', {
      shopId: String(shopId), timeRangeField: 'create_time',
      timeFrom: '1700000000', timeTo: '1720000000', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/order/detail', async ({ request }) => {
    const res = await post(request, '/api/shopee/order/detail', {
      shopId, orderSnList: ['TEST123456'],
      responseOptionalFields: 'buyer_user_id,buyer_username',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/order/cancel', async ({ request }) => {
    const res = await post(request, '/api/shopee/order/cancel', {
      shopId, orderSn: 'TEST123456', cancelReason: 'OUT_OF_STOCK', itemList: [],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/order/note', async ({ request }) => {
    const res = await post(request, '/api/shopee/order/note', {
      shopId, orderSn: 'TEST123456', note: 'Handle with care',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/order/buyer-cancellation', async ({ request }) => {
    const res = await post(request, '/api/shopee/order/buyer-cancellation', {
      shopId, orderSn: 'TEST123456', operation: 'ACCEPT',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/order/{shopId}/escrow/{orderSn}', async ({ request }) => {
    const res = await get(request, `/api/shopee/order/${shopId}/escrow/TEST123456`);
    expect([200, 400]).toContain(res.status());
  });
});

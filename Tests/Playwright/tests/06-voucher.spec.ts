import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

const shopId = 123456789;

test.describe('Voucher APIs', () => {
  test('POST /api/shopee/voucher - add voucher', async ({ request }) => {
    const res = await post(request, '/api/shopee/voucher', {
      shopId, voucherCode: 'TESTCODE10', startTime: 1720000000, endTime: 1720086400,
      voucherType: 1, discountAmount: 1000, minimumSpend: 5000, usageQuantity: 100,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/voucher/list', async ({ request }) => {
    const res = await get(request, '/api/shopee/voucher/list', {
      shopId: String(shopId), pageNo: '1', pageSize: '10',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/shopee/voucher/{shopId}/{voucherId}', async ({ request }) => {
    const res = await get(request, `/api/shopee/voucher/${shopId}/999`);
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/shopee/voucher - update voucher', async ({ request }) => {
    const res = await request.put('http://localhost:5051/api/shopee/voucher', {
      data: { shopId, voucherId: 999, usageQuantity: 200 },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('DELETE /api/shopee/voucher - delete voucher', async ({ request }) => {
    const res = await request.delete('http://localhost:5051/api/shopee/voucher', {
      data: { shopId, voucherId: 999 },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/voucher/end - end voucher', async ({ request }) => {
    const res = await post(request, '/api/shopee/voucher/end', { shopId, voucherId: 999 });
    expect([200, 400]).toContain(res.status());
  });
});

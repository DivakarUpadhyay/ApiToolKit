/**
 * Direct Shopee Sandbox API Tests
 * Base URL: https://partner.test-stable.shopeemobile.com
 *
 * SETUP:
 * 1. Log in to https://open.shopee.com
 * 2. Go to My Apps → your app → Credentials
 * 3. Copy Partner ID and Partner Key
 * 4. Copy .env.example to .env and fill in the values
 * 5. Run: npx playwright test sandbox.spec.ts
 */

import { test, expect } from '@playwright/test';
import { sandboxGet, sandboxPost, buildAuthUrl, hasCredentials, SANDBOX } from './sandbox-helpers';

test.beforeAll(() => {
  if (!hasCredentials()) {
    console.warn('\n⚠️  No Shopee credentials found. Copy .env.example to .env and fill in SHOPEE_PARTNER_ID and SHOPEE_PARTNER_KEY.\n');
  }
});

// ── Partner-level APIs (no shop token needed) ────────────────────────────────

test.describe('Sandbox - Partner Level APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set SHOPEE_PARTNER_ID and SHOPEE_PARTNER_KEY in .env');

  test('Build auth URL', () => {
    const url = buildAuthUrl('https://yourdomain.com/api/shopee/auth/callback');
    expect(url).toContain('partner.test-stable.shopeemobile.com');
    expect(url).toContain(`partner_id=${SANDBOX.partnerId}`);
    expect(url).toContain('sign=');
    console.log('Auth URL:', url);
  });

  test('GET /api/v2/shop/get_shop_list - get authorized shops', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/shop/get_shop_list', { page_size: '10', page_no: '1' });
    const body = await res.json();
    console.log('Shop list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/public/get_payment_method_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/public/get_payment_method_list', { region: 'MY' });
    const body = await res.json();
    console.log('Payment methods:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });
});

// ── Shop-level APIs (requires access_token + shop_id) ────────────────────────

test.describe('Sandbox - Shop Level APIs', () => {
  test.skip(() => !hasCredentials() || SANDBOX.shopId === 0 || SANDBOX.accessToken === '',
    'Skipped: set SHOPEE_TEST_SHOP_ID and SHOPEE_TEST_ACCESS_TOKEN in .env');

  test('GET /api/v2/shop/get_shop_info', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/shop/get_shop_info', {}, { useShopAuth: true });
    const body = await res.json();
    console.log('Shop info:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
    expect(body.error).toBe('');
  });

  test('GET /api/v2/product/get_item_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/product/get_item_list', {
      offset: '0', page_size: '10', item_status: 'NORMAL',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Product list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/order/get_order_list', async ({ request }) => {
    const ts = Math.floor(Date.now() / 1000);
    const res = await sandboxGet(request, '/api/v2/order/get_order_list', {
      time_range_field: 'create_time',
      time_from: String(ts - 30 * 24 * 3600),
      time_to:   String(ts),
      page_size:  '10',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Order list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/logistics/get_channel_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/logistics/get_channel_list', {}, { useShopAuth: true });
    const body = await res.json();
    console.log('Logistics channels:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/voucher/get_voucher_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/voucher/get_voucher_list', {
      page_no: '1', page_size: '10', status: 'ongoing',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Voucher list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/discount/get_discount_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/discount/get_discount_list', {
      page_no: '1', page_size: '10', discount_status: 'ongoing',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Discount list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/returns/get_return_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/returns/get_return_list', {
      page_no: '1', page_size: '10',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Return list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/payment/get_escrow_list', async ({ request }) => {
    const ts = Math.floor(Date.now() / 1000);
    const res = await sandboxGet(request, '/api/v2/payment/get_escrow_list', {
      release_time_from: String(ts - 30 * 24 * 3600),
      release_time_to:   String(ts),
      page_no: '1', page_size: '10',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Escrow list:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/shop_category/get_shop_category_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/shop_category/get_shop_category_list', {
      page_no: '1', page_size: '10',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Shop categories:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });

  test('GET /api/v2/ads/get_campaign_list', async ({ request }) => {
    const res = await sandboxGet(request, '/api/v2/ads/get_campaign_list', {
      page_no: '1', page_size: '10',
    }, { useShopAuth: true });
    const body = await res.json();
    console.log('Ad campaigns:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
  });
});

// ── Token Exchange Test ───────────────────────────────────────────────────────

test.describe('Sandbox - Token Exchange', () => {
  test.skip(() => !hasCredentials(), 'Skipped: credentials required');

  test('POST /api/v2/auth/token/get - exchange code for token', async ({ request }) => {
    // This requires a real code from the OAuth redirect.
    // Run the auth URL test first, complete the OAuth flow, then paste the code here.
    const code = process.env.SHOPEE_AUTH_CODE ?? '';
    test.skip(code === '', 'Set SHOPEE_AUTH_CODE in .env after completing OAuth flow');

    const res = await sandboxPost(request, '/api/v2/auth/token/get', {
      code,
      shop_id: Number(process.env.SHOPEE_OAUTH_SHOP_ID ?? '0'),
      partner_id: Number(SANDBOX.partnerId),
    });
    const body = await res.json();
    console.log('Token response:', JSON.stringify(body, null, 2));
    expect(res.status()).toBe(200);
    expect(body.error).toBeFalsy();
    console.log('\n✅ Access Token:', body.access_token);
    console.log('✅ Shop ID:', body.shop_id);
    console.log('Add these to your .env file as SHOPEE_TEST_ACCESS_TOKEN and SHOPEE_TEST_SHOP_ID\n');
  });
});

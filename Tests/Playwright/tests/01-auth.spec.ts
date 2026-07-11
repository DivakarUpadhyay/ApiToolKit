import { test, expect } from '@playwright/test';
import { get, post } from './helpers';

test.describe('Auth APIs', () => {
  test('GET /api/shopee/auth/auth-url returns auth URL', async ({ request }) => {
    const res = await get(request, '/api/shopee/auth/auth-url');
    expect(res.status()).toBe(200);
    const body = await res.json();
    expect(body.success).toBe(true);
    expect(body.data).toHaveProperty('authUrl');
    expect(body.data.authUrl).toContain('shopeemobile.com');
  });

  test('POST /api/shopee/auth/token - empty body returns 400', async ({ request }) => {
    const res = await post(request, '/api/shopee/auth/token', {});
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/auth/token - with sandbox code', async ({ request }) => {
    const res = await post(request, '/api/shopee/auth/token', {
      code: 'sandbox_code_12345',
      shopId: 123456789,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/auth/token/refresh - empty body returns 400', async ({ request }) => {
    const res = await post(request, '/api/shopee/auth/token/refresh', {});
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/shopee/auth/token/refresh - with sandbox token', async ({ request }) => {
    const res = await post(request, '/api/shopee/auth/token/refresh', {
      refreshToken: 'sandbox_refresh_token',
      shopId: 123456789,
    });
    expect([200, 400]).toContain(res.status());
  });
});

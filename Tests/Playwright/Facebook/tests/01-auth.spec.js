const { test, expect } = require('@playwright/test');
const { post, get, assertOkOrBadRequest } = require('./helpers');

test.describe('Facebook Auth API', () => {

  test('POST /api/facebook/auth/auth-url - returns auth URL or config error', async ({ request }) => {
    const { status, body } = await post(request, '/api/facebook/auth/auth-url', {
      scope: ['public_profile', 'email', 'pages_manage_posts'],
      state: 'random_state_123'
    });
    assertOkOrBadRequest(status, body, 'GetAuthUrl');
    expect(body).toHaveProperty('success');
    if (status === 200) {
      expect(body.data).toHaveProperty('authUrl');
      expect(body.data.authUrl).toContain('facebook.com');
    }
  });

  test('POST /api/facebook/auth/token - exchange code for token', async ({ request }) => {
    const { status, body } = await post(request, '/api/facebook/auth/token', {
      code: 'test_auth_code_12345',
      state: 'random_state_123'
    });
    assertOkOrBadRequest(status, body, 'ExchangeCode');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/auth/app-token - get app access token', async ({ request }) => {
    const { status, body } = await get(request, '/api/facebook/auth/app-token');
    assertOkOrBadRequest(status, body, 'GetAppToken');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/auth/token/long-lived - exchange short-lived token', async ({ request }) => {
    const { status, body } = await post(request, '/api/facebook/auth/token/long-lived', {
      shortLivedToken: 'EAABsbCS1iHgBAKtest123'
    });
    assertOkOrBadRequest(status, body, 'LongLivedToken');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/auth/token/debug - debug a token', async ({ request }) => {
    const { status, body } = await post(request, '/api/facebook/auth/token/debug', {
      inputToken: 'EAABsbCS1iHgBAKtest123'
    });
    assertOkOrBadRequest(status, body, 'DebugToken');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/auth/token/refresh - refresh long-lived token', async ({ request }) => {
    const { status, body } = await post(request, '/api/facebook/auth/token/refresh', {
      accessToken: 'EAABsbCS1iHgBAKtest123'
    });
    assertOkOrBadRequest(status, body, 'RefreshToken');
    expect(body).toHaveProperty('success');
  });

});

/**
 * Smoke tests — verify server is up and Swagger is accessible before running full suite.
 */
const { test, expect } = require('@playwright/test');

test.describe('Smoke Tests — Facebook API Server', () => {

  test('Server is reachable', async ({ request }) => {
    const res = await request.get('http://localhost:5150/swagger/v1/swagger.json');
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(json.info.title).toContain('Facebook');
  });

  test('Swagger UI is available', async ({ request }) => {
    const res = await request.get('http://localhost:5150/swagger');
    expect([200, 301, 302]).toContain(res.status());
  });

  test('All 19 API route groups are registered', async ({ request }) => {
    const res = await request.get('http://localhost:5150/swagger/v1/swagger.json');
    const json = await res.json();
    const paths = Object.keys(json.paths);

    const modules = [
      '/api/facebook/auth',
      '/api/facebook/user',
      '/api/facebook/page',
      '/api/facebook/post',
      '/api/facebook/photo',
      '/api/facebook/video',
      '/api/facebook/comment',
      '/api/facebook/reaction',
      '/api/facebook/event',
      '/api/facebook/group',
      '/api/facebook/ad-account',
      '/api/facebook/campaign',
      '/api/facebook/adset',
      '/api/facebook/ad',
      '/api/facebook/insight',
      '/api/facebook/messenger',
      '/api/facebook/instagram',
      '/api/facebook/catalog',
      '/api/facebook/webhook',
    ];

    for (const prefix of modules) {
      const found = paths.some(p => p.startsWith(prefix));
      expect(found, `No routes found for module: ${prefix}`).toBe(true);
    }
  });

  test('Endpoint count — at least 100 routes registered', async ({ request }) => {
    const res = await request.get('http://localhost:5150/swagger/v1/swagger.json');
    const json = await res.json();
    // Count total operations (each path × method)
    let count = 0;
    for (const methods of Object.values(json.paths)) {
      count += Object.keys(methods).length;
    }
    expect(count).toBeGreaterThanOrEqual(100);
    console.log(`Total registered endpoints: ${count}`);
  });

});

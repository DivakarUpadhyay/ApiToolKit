const { test, expect } = require('@playwright/test');
const { get, post, put, del, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const BUSINESS_ID = 'business_111222333';
const CATALOG_ID = 'catalog_444555666';
const PRODUCT_ID = 'product_777888999';
const APP_ID = 'app_123456789';

test.describe('Facebook Catalog API (Facebook Commerce / Marketplace)', () => {

  test('POST /api/facebook/catalog - create catalog', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/catalog?accessToken=${ACCESS_TOKEN}`, {
      businessId: BUSINESS_ID,
      name: 'Playwright Test Catalog',
      verticalType: 'commerce'
    });
    assertOkOrBadRequest(status, body, 'CreateCatalog');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/catalog/{catalogId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/catalog/${CATALOG_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetCatalog');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/catalog/list - get catalogs', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/catalog/list?accessToken=${ACCESS_TOKEN}`, {
      businessId: BUSINESS_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetCatalogs');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/catalog/{catalogId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/catalog/${CATALOG_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteCatalog');
    expect(body).toHaveProperty('success');
  });

  // ── Product Upload (Facebook Marketplace / Commerce) ───────────────────────
  test('POST /api/facebook/catalog/{catalogId}/product - upload product', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/catalog/${CATALOG_ID}/product?accessToken=${ACCESS_TOKEN}`, {
      catalogId: CATALOG_ID,
      retailerId: 'SKU-PLAYWRIGHT-001',
      name: 'Playwright Test Product',
      description: 'A product created by automated Playwright tests for Facebook Commerce',
      url: 'https://example.com/product/playwright-001',
      imageUrl: 'https://example.com/images/playwright-001.jpg',
      brand: 'TestBrand',
      price: '29.99 USD',
      currency: 'USD',
      availability: 'in stock',
      condition: 'new',
      gtin: '1234567890123'
    });
    assertOkOrBadRequest(status, body, 'CreateProduct (Marketplace Upload)');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/catalog/product/{productId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/catalog/product/${PRODUCT_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetProduct');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/catalog/{catalogId}/products - list products', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/catalog/${CATALOG_ID}/products?accessToken=${ACCESS_TOKEN}`, {
      catalogId: CATALOG_ID,
      limit: 25,
      fields: 'id,retailer_id,name,description,url,image_url,brand,price,currency,availability,condition'
    });
    assertOkOrBadRequest(status, body, 'GetProducts');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/catalog/product/{productId} - update product', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/catalog/product/${PRODUCT_ID}?accessToken=${ACCESS_TOKEN}`, {
      productId: PRODUCT_ID,
      name: 'Updated Playwright Product',
      price: '24.99 USD',
      availability: 'in stock'
    });
    assertOkOrBadRequest(status, body, 'UpdateProduct');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/catalog/product/{productId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/catalog/product/${PRODUCT_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteProduct');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/catalog/{catalogId}/product-set - create product set', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/catalog/${CATALOG_ID}/product-set?accessToken=${ACCESS_TOKEN}`, {
      catalogId: CATALOG_ID,
      name: 'Test Product Set',
      filter: { availability: { i_contains: 'in stock' } }
    });
    assertOkOrBadRequest(status, body, 'CreateProductSet');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/catalog/{catalogId}/product-sets', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/catalog/${CATALOG_ID}/product-sets`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetProductSets');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Webhook API', () => {

  test('POST /api/facebook/webhook/subscribe', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/webhook/subscribe?accessToken=${ACCESS_TOKEN}`, {
      objectType: 'page',
      callbackUrl: 'https://yourdomain.com/webhook/facebook',
      verifyToken: 'my_verify_token_12345',
      fields: ['messages', 'feed', 'mention', 'name']
    });
    assertOkOrBadRequest(status, body, 'SubscribeWebhook');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/webhook/subscriptions - get subscriptions', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/webhook/subscriptions?accessToken=${ACCESS_TOKEN}`, {
      appId: APP_ID
    });
    assertOkOrBadRequest(status, body, 'GetWebhookSubscriptions');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/webhook - delete subscription', async ({ request }) => {
    const res = await request.delete(`http://localhost:5150/api/facebook/webhook?accessToken=${ACCESS_TOKEN}`, {
      data: { objectType: 'page', fields: ['messages'] }
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/facebook/webhook/verify - verify webhook challenge', async ({ request }) => {
    const { status } = await get(request, '/api/facebook/webhook/verify', {
      'hub.mode': 'subscribe',
      'hub.verify_token': 'my_verify_token_12345',
      'hub.challenge': 'challenge_code_abc123'
    });
    // Facebook sends GET to verify - 200 returns challenge text, 400/401 if token mismatch
    expect([200, 400, 401]).toContain(status);
  });

  test('POST /api/facebook/webhook/payload - process webhook payload', async ({ request }) => {
    const { status, body } = await post(request, '/api/facebook/webhook/payload', {
      object: 'page',
      entry: [{
        id: '123456789',
        time: Math.floor(Date.now() / 1000),
        changes: [{ field: 'feed', value: { item: 'status', verb: 'add' } }]
      }]
    });
    assertOkOrBadRequest(status, body, 'ProcessWebhookPayload');
    expect(body).toHaveProperty('success');
  });

});

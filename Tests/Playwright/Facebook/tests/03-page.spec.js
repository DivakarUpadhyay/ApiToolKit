const { test, expect } = require('@playwright/test');
const { get, post, put, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const PAGE_ID = '123456789';
const USER_ID = 'me';

test.describe('Facebook Page API', () => {

  test('GET /api/facebook/page/{pageId}/info', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/page/${PAGE_ID}/info`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPageInfo');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/page/user/{userId}/pages', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/page/user/${USER_ID}/pages`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPagesByUser');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/page/{pageId}/access-token', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/page/${PAGE_ID}/access-token`, { userAccessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPageAccessToken');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/page/{pageId}/settings', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/page/${PAGE_ID}/settings?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      about: 'Test page about text',
      description: 'Test description',
      website: 'https://example.com'
    });
    assertOkOrBadRequest(status, body, 'UpdatePageSettings');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/page/{pageId}/post - create page post', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/page/${PAGE_ID}/post?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      message: 'Hello from ApiToolKit Facebook integration test!',
      published: false
    });
    assertOkOrBadRequest(status, body, 'CreatePagePost');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/page/{pageId}/feed', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/page/${PAGE_ID}/feed?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetPageFeed');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/page/{pageId}/photos', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/page/${PAGE_ID}/photos?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetPagePhotos');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/page/{pageId}/videos', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/page/${PAGE_ID}/videos?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetPageVideos');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/page/{pageId}/events', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/page/${PAGE_ID}/events`, { accessToken: ACCESS_TOKEN, limit: '10' });
    assertOkOrBadRequest(status, body, 'GetPageEvents');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/page/{pageId}/roles', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/page/${PAGE_ID}/roles`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPageRoles');
    expect(body).toHaveProperty('success');
  });

});

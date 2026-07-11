const { test, expect } = require('@playwright/test');
const { get, post, put, del, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const PAGE_ID = '123456789';
const POST_ID = '123456789_987654321';

test.describe('Facebook Post API', () => {

  test('POST /api/facebook/post - create post', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/post?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      message: 'Test post from ApiToolKit Playwright tests',
      published: false
    });
    assertOkOrBadRequest(status, body, 'CreatePost');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/post/{postId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/post/${POST_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPost');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/post/{postId} - update post', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/post/${POST_ID}?accessToken=${ACCESS_TOKEN}`, {
      postId: POST_ID,
      message: 'Updated post message'
    });
    assertOkOrBadRequest(status, body, 'UpdatePost');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/post/{postId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/post/${POST_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeletePost');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/post/feed - get feed', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/post/feed?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10,
      fields: 'id,message,created_time'
    });
    assertOkOrBadRequest(status, body, 'GetFeed');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/post/{postId}/comments - get comments', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/post/${POST_ID}/comments?accessToken=${ACCESS_TOKEN}`, {
      objectId: POST_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetPostComments');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/post/{postId}/reactions - get reactions', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/post/${POST_ID}/reactions?accessToken=${ACCESS_TOKEN}`, {
      objectId: POST_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetPostReactions');
    expect(body).toHaveProperty('success');
  });

});

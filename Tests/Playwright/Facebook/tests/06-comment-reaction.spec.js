const { test, expect } = require('@playwright/test');
const { get, post, put, del, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const POST_ID = '123456789_987654321';
const COMMENT_ID = 'comment_111222333';

test.describe('Facebook Comment API', () => {

  test('POST /api/facebook/comment - create comment', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/comment?accessToken=${ACCESS_TOKEN}`, {
      objectId: POST_ID,
      message: 'This is a test comment from Playwright!'
    });
    assertOkOrBadRequest(status, body, 'CreateComment');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/comment/{commentId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/comment/${COMMENT_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetComment');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/comment/list - get comments', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/comment/list?accessToken=${ACCESS_TOKEN}`, {
      objectId: POST_ID,
      limit: 10,
      summary: true
    });
    assertOkOrBadRequest(status, body, 'GetComments');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/comment/{commentId} - update comment', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/comment/${COMMENT_ID}?accessToken=${ACCESS_TOKEN}`, {
      commentId: COMMENT_ID,
      message: 'Updated comment text'
    });
    assertOkOrBadRequest(status, body, 'UpdateComment');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/comment/{commentId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/comment/${COMMENT_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteComment');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/comment/{commentId}/hide', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/comment/${COMMENT_ID}/hide?accessToken=${ACCESS_TOKEN}`, {
      commentId: COMMENT_ID,
      isHidden: true
    });
    assertOkOrBadRequest(status, body, 'HideComment');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/comment/{commentId}/replies', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/comment/${COMMENT_ID}/replies`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetCommentReplies');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/comment/{commentId}/like', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/comment/${COMMENT_ID}/like?accessToken=${ACCESS_TOKEN}`, {});
    assertOkOrBadRequest(status, body, 'LikeComment');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Reaction API', () => {

  test('POST /api/facebook/reaction - create reaction', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/reaction?accessToken=${ACCESS_TOKEN}`, {
      objectId: POST_ID,
      type: 'LIKE'
    });
    assertOkOrBadRequest(status, body, 'CreateReaction');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/reaction/list - get reactions', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/reaction/list?accessToken=${ACCESS_TOKEN}`, {
      objectId: POST_ID,
      limit: 25
    });
    assertOkOrBadRequest(status, body, 'GetReactions');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/reaction/{objectId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/reaction/${POST_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteReaction');
    expect(body).toHaveProperty('success');
  });

});

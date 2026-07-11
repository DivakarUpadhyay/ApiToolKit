const { test, expect } = require('@playwright/test');
const { get, post, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const PAGE_ID = '123456789';
const PSID = 'psid_test_user_111';
const CONVERSATION_ID = 'conv_111222333';
const IG_USER_ID = 'ig_111222333';
const MEDIA_ID = 'igmedia_555666777';
const HASHTAG_ID = 'hashtag_888999000';
const COMMENT_ID = 'igcomment_111222';

test.describe('Facebook Messenger API', () => {

  test('POST /api/facebook/messenger/send', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/send?pageAccessToken=${ACCESS_TOKEN}`, {
      recipientId: PSID,
      messageText: 'Hello from Playwright API test!'
    });
    assertOkOrBadRequest(status, body, 'SendMessage');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/messenger/conversations', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/conversations?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetConversations');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/messenger/messages', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/messages?accessToken=${ACCESS_TOKEN}`, {
      conversationId: CONVERSATION_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetMessages');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/messenger/user/profile', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/user/profile?pageAccessToken=${ACCESS_TOKEN}`, { psid: PSID });
    assertOkOrBadRequest(status, body, 'GetMessengerUserProfile');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/messenger/mark-seen', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/mark-seen?pageAccessToken=${ACCESS_TOKEN}`, {
      recipientId: PSID
    });
    assertOkOrBadRequest(status, body, 'MarkSeen');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/messenger/typing', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/typing?pageAccessToken=${ACCESS_TOKEN}`, {
      recipientId: PSID
    });
    assertOkOrBadRequest(status, body, 'TypingIndicator');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/messenger/subscribe', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/messenger/subscribe?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      subscribedFields: ['messages', 'messaging_postbacks', 'messaging_referrals']
    });
    assertOkOrBadRequest(status, body, 'SubscribeAppToPage');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/messenger/page/{pageId}/subscriptions', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/messenger/page/${PAGE_ID}/subscriptions`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPageSubscriptions');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Instagram API', () => {

  test('GET /api/facebook/instagram/{igUserId}/account', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/instagram/${IG_USER_ID}/account`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetInstagramAccount');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/instagram/page/{pageId}/instagram', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/instagram/page/${PAGE_ID}/instagram`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetLinkedInstagramAccount');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/media - create media', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/media?accessToken=${ACCESS_TOKEN}`, {
      igUserId: IG_USER_ID,
      imageUrl: 'https://example.com/test-image.jpg',
      caption: 'Test Instagram post from Playwright #testing',
      mediaType: 'IMAGE'
    });
    assertOkOrBadRequest(status, body, 'CreateInstagramMedia');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/media/publish', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/media/publish?accessToken=${ACCESS_TOKEN}`, {
      igUserId: IG_USER_ID,
      creationId: 'creation_test_id_123'
    });
    assertOkOrBadRequest(status, body, 'PublishInstagramMedia');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/media/list - get media list', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/media/list?accessToken=${ACCESS_TOKEN}`, {
      igUserId: IG_USER_ID,
      limit: 12,
      fields: 'id,caption,media_type,media_url,timestamp'
    });
    assertOkOrBadRequest(status, body, 'GetInstagramMedia');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/media/insights', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/media/insights?accessToken=${ACCESS_TOKEN}`, {
      mediaId: MEDIA_ID
    });
    assertOkOrBadRequest(status, body, 'GetMediaInsights');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/hashtag/search - search hashtag', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/hashtag/search?accessToken=${ACCESS_TOKEN}`, {
      hashtagName: 'technology',
      igUserId: IG_USER_ID
    });
    assertOkOrBadRequest(status, body, 'GetInstagramHashtag');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/hashtag/media', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/hashtag/media?accessToken=${ACCESS_TOKEN}`, {
      hashtagId: HASHTAG_ID,
      igUserId: IG_USER_ID,
      mediaType: 'TOP_MEDIA'
    });
    assertOkOrBadRequest(status, body, 'GetHashtagMedia');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/instagram/media/{mediaId}/comments', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/instagram/media/${MEDIA_ID}/comments`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetIgComments');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/instagram/business-discovery', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/instagram/business-discovery?accessToken=${ACCESS_TOKEN}`, {
      username: 'testbusiness',
      igUserId: IG_USER_ID
    });
    assertOkOrBadRequest(status, body, 'GetBusinessDiscovery');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/instagram/{igUserId}/insights', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/instagram/${IG_USER_ID}/insights`, {
      accessToken: ACCESS_TOKEN,
      metrics: 'impressions,reach,profile_views',
      period: 'day'
    });
    assertOkOrBadRequest(status, body, 'GetIgAccountInsights');
    expect(body).toHaveProperty('success');
  });

});

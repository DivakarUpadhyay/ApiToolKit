const { test, expect } = require('@playwright/test');
const { get, post, put, del, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const USER_ID = 'me';
const ACCOUNT_ID = 'act_123456789';
const CAMPAIGN_ID = '111222333444';
const ADSET_ID = '555666777888';
const AD_ID = '999000111222';

test.describe('Facebook Ad Account API', () => {

  test('GET /api/facebook/ad-account/{accountId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/ad-account/${ACCOUNT_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetAdAccount');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/ad-account/user/ad-accounts', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/ad-account/user/ad-accounts?accessToken=${ACCESS_TOKEN}`, {
      userId: USER_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetAdAccountsByUser');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/ad-account/{accountId}/billing', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/ad-account/${ACCOUNT_ID}/billing`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetAdAccountBilling');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/ad-account/{accountId}/users', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/ad-account/${ACCOUNT_ID}/users?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID
    });
    assertOkOrBadRequest(status, body, 'GetAdAccountUsers');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/ad-account/{accountId}/campaigns', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/ad-account/${ACCOUNT_ID}/campaigns?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetAdAccountCampaigns');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Campaign API', () => {

  test('POST /api/facebook/campaign - create campaign', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/campaign?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID,
      name: 'Playwright Test Campaign',
      objective: 'OUTCOME_TRAFFIC',
      status: 'PAUSED',
      specialAdCategories: [],
      dailyBudget: '1000'
    });
    assertOkOrBadRequest(status, body, 'CreateCampaign');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/campaign/{campaignId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/campaign/${CAMPAIGN_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetCampaign');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/campaign/list - get campaigns', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/campaign/list?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetCampaigns');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/campaign/{campaignId} - update campaign', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/campaign/${CAMPAIGN_ID}?accessToken=${ACCESS_TOKEN}`, {
      campaignId: CAMPAIGN_ID,
      name: 'Updated Campaign Name',
      status: 'PAUSED'
    });
    assertOkOrBadRequest(status, body, 'UpdateCampaign');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/campaign/{campaignId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/campaign/${CAMPAIGN_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteCampaign');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/campaign/{campaignId}/adsets', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/campaign/${CAMPAIGN_ID}/adsets`, { accessToken: ACCESS_TOKEN, limit: '10' });
    assertOkOrBadRequest(status, body, 'GetCampaignAdSets');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/campaign/{campaignId}/ads', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/campaign/${CAMPAIGN_ID}/ads`, { accessToken: ACCESS_TOKEN, limit: '10' });
    assertOkOrBadRequest(status, body, 'GetCampaignAds');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook AdSet API', () => {

  test('POST /api/facebook/adset - create ad set', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/adset?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID,
      campaignId: CAMPAIGN_ID,
      name: 'Playwright Test AdSet',
      status: 'PAUSED',
      optimizationGoal: 'REACH',
      billingEvent: 'IMPRESSIONS',
      dailyBudget: '500',
      targeting: { geoLocations: { countries: ['US'] }, ageMin: 18, ageMax: 65 }
    });
    assertOkOrBadRequest(status, body, 'CreateAdSet');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/adset/{adSetId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/adset/${ADSET_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetAdSet');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/adset/list - get ad sets', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/adset/list?accessToken=${ACCESS_TOKEN}`, {
      campaignId: CAMPAIGN_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetAdSets');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/adset/{adSetId} - update ad set', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/adset/${ADSET_ID}?accessToken=${ACCESS_TOKEN}`, {
      adSetId: ADSET_ID,
      name: 'Updated AdSet Name',
      status: 'PAUSED'
    });
    assertOkOrBadRequest(status, body, 'UpdateAdSet');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/adset/{adSetId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/adset/${ADSET_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteAdSet');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Ad API', () => {

  test('POST /api/facebook/ad - create ad', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/ad?accessToken=${ACCESS_TOKEN}`, {
      adSetId: ADSET_ID,
      accountId: ACCOUNT_ID,
      name: 'Playwright Test Ad',
      status: 'PAUSED',
      creative: { creativeId: 'creative_123' }
    });
    assertOkOrBadRequest(status, body, 'CreateAd');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/ad/{adId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/ad/${AD_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetAd');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/ad/list - get ads', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/ad/list?accessToken=${ACCESS_TOKEN}`, {
      adSetId: ADSET_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetAds');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/ad/{adId} - update ad', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/ad/${AD_ID}?accessToken=${ACCESS_TOKEN}`, {
      adId: AD_ID,
      name: 'Updated Ad Name',
      status: 'PAUSED'
    });
    assertOkOrBadRequest(status, body, 'UpdateAd');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/ad/{adId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/ad/${AD_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteAd');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/ad/creative - create ad creative', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/ad/creative?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID,
      name: 'Playwright Test Creative',
      objectStorySpec: {
        pageId: '123456789',
        linkData: {
          link: 'https://example.com',
          message: 'Test ad creative',
          name: 'Test Creative'
        }
      }
    });
    assertOkOrBadRequest(status, body, 'CreateAdCreative');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/ad/{adId}/preview', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/ad/${AD_ID}/preview`, { accessToken: ACCESS_TOKEN, adFormat: 'DESKTOP_FEED_STANDARD' });
    assertOkOrBadRequest(status, body, 'GetAdPreview');
    expect(body).toHaveProperty('success');
  });

});

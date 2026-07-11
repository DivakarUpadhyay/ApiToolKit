const { test, expect } = require('@playwright/test');
const { get, post, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const PAGE_ID = '123456789';
const VIDEO_ID = '444555666';
const AD_ID = '999000111222';
const CAMPAIGN_ID = '111222333444';
const ADSET_ID = '555666777888';
const ACCOUNT_ID = 'act_123456789';

test.describe('Facebook Insight API', () => {

  test('POST /api/facebook/insight/page - get page insights', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/insight/page?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      metric: ['page_fans', 'page_views_total', 'page_impressions'],
      period: 'day',
      since: new Date(Date.now() - 7 * 86400000).toISOString().split('T')[0],
      until: new Date().toISOString().split('T')[0]
    });
    assertOkOrBadRequest(status, body, 'GetPageInsights');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/insight/video - get video insights', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/insight/video?accessToken=${ACCESS_TOKEN}`, {
      videoId: VIDEO_ID,
      metric: 'total_video_views',
      period: 'lifetime'
    });
    assertOkOrBadRequest(status, body, 'GetVideoInsights');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/insight/ad - get ad insights', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/insight/ad?accessToken=${ACCESS_TOKEN}`, {
      accountId: ACCOUNT_ID,
      adId: AD_ID,
      datePreset: 'last_7d',
      level: 'ad',
      fields: ['impressions', 'reach', 'clicks', 'spend', 'cpc', 'cpm', 'ctr']
    });
    assertOkOrBadRequest(status, body, 'GetAdInsights');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/insight/campaign/{campaignId} - get campaign insights', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/insight/campaign/${CAMPAIGN_ID}`, {
      accessToken: ACCESS_TOKEN,
      datePreset: 'last_30d'
    });
    assertOkOrBadRequest(status, body, 'GetCampaignInsights');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/insight/adset/{adSetId} - get adset insights', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/insight/adset/${ADSET_ID}`, {
      accessToken: ACCESS_TOKEN,
      datePreset: 'last_7d'
    });
    assertOkOrBadRequest(status, body, 'GetAdSetInsights');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/insight/account/{accountId} - get account insights', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/insight/account/${ACCOUNT_ID}`, {
      accessToken: ACCESS_TOKEN,
      datePreset: 'last_30d'
    });
    assertOkOrBadRequest(status, body, 'GetAccountInsights');
    expect(body).toHaveProperty('success');
  });

});

const { test, expect } = require('@playwright/test');
const { get, post, put, del, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const PAGE_ID = '123456789';
const EVENT_ID = 'event_111222333';
const GROUP_ID = 'group_444555666';
const USER_ID = 'me';

test.describe('Facebook Event API', () => {

  test('POST /api/facebook/event - create event', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/event?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      name: 'Playwright Test Event',
      description: 'An event created by automated Playwright tests',
      startTime: new Date(Date.now() + 86400000).toISOString(),
      endTime: new Date(Date.now() + 90000000).toISOString(),
      location: 'Online',
      privacy: 'PUBLIC',
      isOnline: true
    });
    assertOkOrBadRequest(status, body, 'CreateEvent');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/event/{eventId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/event/${EVENT_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetEvent');
    expect(body).toHaveProperty('success');
  });

  test('PUT /api/facebook/event/{eventId} - update event', async ({ request }) => {
    const { status, body } = await put(request, `/api/facebook/event/${EVENT_ID}?accessToken=${ACCESS_TOKEN}`, {
      eventId: EVENT_ID,
      name: 'Updated Event Name',
      description: 'Updated description'
    });
    assertOkOrBadRequest(status, body, 'UpdateEvent');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/event/{eventId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/event/${EVENT_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteEvent');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/event/{pageId}/events - get page events', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/event/${PAGE_ID}/events`, { accessToken: ACCESS_TOKEN, limit: '10' });
    assertOkOrBadRequest(status, body, 'GetEventsByPage');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/event/{eventId}/attendees - get attendees', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/event/${EVENT_ID}/attendees?accessToken=${ACCESS_TOKEN}`, {
      eventId: EVENT_ID,
      limit: 25
    });
    assertOkOrBadRequest(status, body, 'GetEventAttendees');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/event/{eventId}/rsvp', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/event/${EVENT_ID}/rsvp?accessToken=${ACCESS_TOKEN}`, {
      eventId: EVENT_ID,
      rsvp: 'attending'
    });
    assertOkOrBadRequest(status, body, 'RsvpEvent');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Group API', () => {

  test('GET /api/facebook/group/{groupId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/group/${GROUP_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetGroup');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/group/user/{userId}/groups', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/group/user/${USER_ID}/groups`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetGroupsByUser');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/group/{groupId}/feed - get group feed', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/group/${GROUP_ID}/feed?accessToken=${ACCESS_TOKEN}`, {
      groupId: GROUP_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetGroupFeed');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/group/{groupId}/members - get members', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/group/${GROUP_ID}/members?accessToken=${ACCESS_TOKEN}`, {
      groupId: GROUP_ID,
      limit: 25
    });
    assertOkOrBadRequest(status, body, 'GetGroupMembers');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/group/{groupId}/post - create group post', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/group/${GROUP_ID}/post?accessToken=${ACCESS_TOKEN}`, {
      groupId: GROUP_ID,
      message: 'Test group post from Playwright'
    });
    assertOkOrBadRequest(status, body, 'CreateGroupPost');
    expect(body).toHaveProperty('success');
  });

});

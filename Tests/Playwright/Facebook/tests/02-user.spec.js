const { test, expect } = require('@playwright/test');
const { get, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const USER_ID = 'me';

test.describe('Facebook User API', () => {

  test('GET /api/facebook/user/{userId}/profile', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/profile`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserProfile');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/user/{userId}/friends', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/friends`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserFriends');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/user/{userId}/permissions', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/permissions`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserPermissions');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/user/{userId}/picture', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/picture`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserPicture');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/user/{userId}/feed', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/feed`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserFeed');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/user/{userId}/pages', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/pages`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserPages');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/user/{userId}/ad-accounts', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/user/${USER_ID}/ad-accounts`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetUserAdAccounts');
    expect(body).toHaveProperty('success');
  });

});

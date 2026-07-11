const { test, expect } = require('@playwright/test');
const { get, post, put, del, assertOkOrBadRequest } = require('./helpers');

const ACCESS_TOKEN = 'test_access_token';
const PAGE_ID = '123456789';
const PHOTO_ID = '111222333';
const VIDEO_ID = '444555666';

test.describe('Facebook Photo API', () => {

  test('POST /api/facebook/photo/upload', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/photo/upload?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      url: 'https://example.com/test-image.jpg',
      caption: 'Test photo caption'
    });
    assertOkOrBadRequest(status, body, 'UploadPhoto');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/photo/{photoId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/photo/${PHOTO_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetPhoto');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/photo/list - get photos', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/photo/list?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetPhotos');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/photo/{photoId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/photo/${PHOTO_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeletePhoto');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/photo/{objectId}/albums', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/photo/${PAGE_ID}/albums`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetAlbums');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/photo/album - create album', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/photo/album?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      name: 'Test Album',
      message: 'Created by Playwright test'
    });
    assertOkOrBadRequest(status, body, 'CreateAlbum');
    expect(body).toHaveProperty('success');
  });

});

test.describe('Facebook Video API', () => {

  test('POST /api/facebook/video/upload', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/video/upload?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      fileUrl: 'https://example.com/test-video.mp4',
      title: 'Test Video',
      description: 'Playwright test video upload'
    });
    assertOkOrBadRequest(status, body, 'UploadVideo');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/video/{videoId}', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/video/${VIDEO_ID}`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetVideo');
    expect(body).toHaveProperty('success');
  });

  test('POST /api/facebook/video/list - get videos', async ({ request }) => {
    const { status, body } = await post(request, `/api/facebook/video/list?accessToken=${ACCESS_TOKEN}`, {
      pageId: PAGE_ID,
      limit: 10
    });
    assertOkOrBadRequest(status, body, 'GetVideos');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/video/{videoId}/status', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/video/${VIDEO_ID}/status`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetVideoStatus');
    expect(body).toHaveProperty('success');
  });

  test('GET /api/facebook/video/{videoId}/thumbnails', async ({ request }) => {
    const { status, body } = await get(request, `/api/facebook/video/${VIDEO_ID}/thumbnails`, { accessToken: ACCESS_TOKEN });
    assertOkOrBadRequest(status, body, 'GetVideoThumbnails');
    expect(body).toHaveProperty('success');
  });

  test('DELETE /api/facebook/video/{videoId}', async ({ request }) => {
    const { status, body } = await del(request, `/api/facebook/video/${VIDEO_ID}?accessToken=${ACCESS_TOKEN}`);
    assertOkOrBadRequest(status, body, 'DeleteVideo');
    expect(body).toHaveProperty('success');
  });

});

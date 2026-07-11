/**
 * Shared helpers for Facebook API tests.
 * All tests use APIRequestContext (no browser needed).
 * Tests verify HTTP status + response shape only — no real FB credentials required.
 */

const BASE = 'http://localhost:5150';

/** POST helper — returns { status, body } */
async function post(request, path, payload = {}) {
  const res = await request.post(`${BASE}${path}`, { data: payload });
  let body;
  try { body = await res.json(); } catch { body = {}; }
  return { status: res.status(), body };
}

/** GET helper */
async function get(request, path, params = {}) {
  const qs = Object.keys(params).length
    ? '?' + new URLSearchParams(params).toString()
    : '';
  const res = await request.get(`${BASE}${path}${qs}`);
  let body;
  try { body = await res.json(); } catch { body = {}; }
  return { status: res.status(), body };
}

/** PUT helper */
async function put(request, path, payload = {}) {
  const res = await request.put(`${BASE}${path}`, { data: payload });
  let body;
  try { body = await res.json(); } catch { body = {}; }
  return { status: res.status(), body };
}

/** DELETE helper */
async function del(request, path) {
  const res = await request.delete(`${BASE}${path}`);
  let body;
  try { body = await res.json(); } catch { body = {}; }
  return { status: res.status(), body };
}

/**
 * Asserts that the response has an acceptable status code.
 * We accept 200 (success) and 400 (validation/missing-token errors from FB)
 * because we don't have real credentials. We reject 404/500.
 */
function assertOkOrBadRequest(status, body, label) {
  if (status !== 200 && status !== 400) {
    throw new Error(`[${label}] Unexpected HTTP ${status}. Body: ${JSON.stringify(body)}`);
  }
}

module.exports = { post, get, put, del, assertOkOrBadRequest };

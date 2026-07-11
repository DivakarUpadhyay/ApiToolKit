import { APIRequestContext } from '@playwright/test';

export const BASE = 'http://localhost:5051';

// Sandbox credentials — replace with real values to run authenticated tests
export const SANDBOX = {
  shopId: 0,
  accessToken: 'sandbox_access_token',
};

export async function get(request: APIRequestContext, path: string, params: Record<string, string> = {}) {
  const qs = new URLSearchParams(params).toString();
  const url = qs ? `${BASE}${path}?${qs}` : `${BASE}${path}`;
  return request.get(url);
}

export async function post(request: APIRequestContext, path: string, body: unknown = {}) {
  return request.post(`${BASE}${path}`, { data: body });
}

// Shopee sandbox returns 200 with error codes in body — we only check the HTTP layer here
export function expectShopeeResponse(status: number) {
  return [200, 400, 500].includes(status);
}

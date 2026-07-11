import { APIRequestContext } from '@playwright/test';

export const FLIPKART = {
  baseUrl:     process.env.FLIPKART_BASE_URL     ?? 'https://api.flipkart.net/sellers',
  accessToken: process.env.FLIPKART_ACCESS_TOKEN ?? '',
};

export function hasCredentials(): boolean {
  return FLIPKART.accessToken !== '';
}

function authHeaders() {
  return {
    'Authorization': `Bearer ${FLIPKART.accessToken}`,
    'Content-Type': 'application/json',
  };
}

export async function fkGet(
  request: APIRequestContext,
  path: string,
  params: Record<string, string> = {}
) {
  const qs = new URLSearchParams(params).toString();
  const url = qs
    ? `${FLIPKART.baseUrl}${path}?${qs}`
    : `${FLIPKART.baseUrl}${path}`;
  return request.get(url, { headers: authHeaders() });
}

export async function fkPost(
  request: APIRequestContext,
  path: string,
  body: unknown = {}
) {
  return request.post(`${FLIPKART.baseUrl}${path}`, {
    headers: authHeaders(),
    data: body,
  });
}

export async function fkPut(
  request: APIRequestContext,
  path: string,
  body: unknown = {}
) {
  return request.put(`${FLIPKART.baseUrl}${path}`, {
    headers: authHeaders(),
    data: body,
  });
}

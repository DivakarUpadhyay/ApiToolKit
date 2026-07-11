import crypto from 'crypto';
import { APIRequestContext } from '@playwright/test';

// ── Sandbox Configuration ────────────────────────────────────────────────────
// Fill these in from: open.shopee.com → My Apps → Credentials
export const SANDBOX = {
  partnerId:   process.env.SHOPEE_PARTNER_ID   ?? '',
  partnerKey:  process.env.SHOPEE_PARTNER_KEY  ?? '',
  baseUrl:     process.env.SHOPEE_SANDBOX_BASE_URL ?? 'https://partner.test-stable.shopeemobile.com',
  shopId:      Number(process.env.SHOPEE_TEST_SHOP_ID    ?? '0'),
  accessToken: process.env.SHOPEE_TEST_ACCESS_TOKEN ?? '',
};

export function hasCredentials(): boolean {
  return SANDBOX.partnerId !== '' && SANDBOX.partnerKey !== '';
}

// ── HMAC-SHA256 Signing ──────────────────────────────────────────────────────
function sign(path: string, timestamp: number, accessToken = '', shopId = 0): string {
  const base = shopId > 0
    ? `${SANDBOX.partnerId}${path}${timestamp}${accessToken}${shopId}`
    : `${SANDBOX.partnerId}${path}${timestamp}`;
  return crypto.createHmac('sha256', SANDBOX.partnerKey).update(base).digest('hex');
}

function buildQueryString(path: string, timestamp: number, extra: Record<string, string> = {}, accessToken = '', shopId = 0): string {
  const sig = sign(path, timestamp, accessToken, shopId);
  const params = new URLSearchParams({
    partner_id: SANDBOX.partnerId,
    timestamp: String(timestamp),
    sign: sig,
    ...extra,
  });
  if (accessToken) params.set('access_token', accessToken);
  if (shopId > 0)  params.set('shop_id', String(shopId));
  return params.toString();
}

// ── Sandbox HTTP helpers ─────────────────────────────────────────────────────
export async function sandboxGet(
  request: APIRequestContext,
  path: string,
  queryParams: Record<string, string> = {},
  opts: { useShopAuth?: boolean } = {}
) {
  const ts = Math.floor(Date.now() / 1000);
  const at = opts.useShopAuth ? SANDBOX.accessToken : '';
  const sid = opts.useShopAuth ? SANDBOX.shopId : 0;
  const qs = buildQueryString(path, ts, queryParams, at, sid);
  return request.get(`${SANDBOX.baseUrl}${path}?${qs}`);
}

export async function sandboxPost(
  request: APIRequestContext,
  path: string,
  body: unknown = {},
  opts: { useShopAuth?: boolean } = {}
) {
  const ts = Math.floor(Date.now() / 1000);
  const at = opts.useShopAuth ? SANDBOX.accessToken : '';
  const sid = opts.useShopAuth ? SANDBOX.shopId : 0;
  const qs = buildQueryString(path, ts, {}, at, sid);
  return request.post(`${SANDBOX.baseUrl}${path}?${qs}`, { data: body });
}

// ── Auth URL builder (partner-level, no shop) ────────────────────────────────
export function buildAuthUrl(redirectUrl: string): string {
  const path = '/api/v2/shop/auth_partner';
  const ts = Math.floor(Date.now() / 1000);
  const sig = sign(path, ts);
  return `${SANDBOX.baseUrl}${path}?partner_id=${SANDBOX.partnerId}&timestamp=${ts}&sign=${sig}&redirect=${encodeURIComponent(redirectUrl)}`;
}

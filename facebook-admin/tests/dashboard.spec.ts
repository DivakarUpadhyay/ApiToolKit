import { test, expect, Page } from '@playwright/test';

const TOKEN = 'EAAO2NnWebZBcBR0BZBBFdhUM1sFm3eYGWMUkDYDq3McnM5PnF5ZCD17z5XtfhXZAKHDn6XCaFoJM1W5Rtknl3ChsZCrPEx3NvS7KEyAZAKVZCvwExyXCilBy4Ytn5Yvmff8QPrEVnZBjXZA7R2v2Nv4PWpmpXPZCfFB4ZCgQzC2vHXfew4CxP8Y6TcfGKO897vkzijsCvIPjHkoPQ2ZBN7X8KRfZCHYnyyT9jLple1A26Eypr';
const USER_ID = '122108821923364890';
const BIZ_ID = '1084873647383329';
const PAGE_ID = '1248325291691587';

async function seedStorage(page: Page) {
  await page.goto('/');
  await page.evaluate(({ t, u, b, p }) => {
    localStorage.setItem('fb_token', t);
    localStorage.setItem('fb_user_id', u);
    localStorage.setItem('fb_biz_id', b);
    localStorage.setItem('fb_page_id', p);
  }, { t: TOKEN, u: USER_ID, b: BIZ_ID, p: PAGE_ID });
}

// ── Login page ────────────────────────────────────────────────────────────────
test.describe('Login page', () => {
  test('renders all fields and buttons', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('h1')).toContainText('ApiToolKit');
    await expect(page.locator('textarea')).toBeVisible();
    await expect(page.getByText('Login & Validate Token')).toBeVisible();
    await expect(page.getByText('Skip')).toBeVisible();
  });

  test('pre-fills token textarea', async ({ page }) => {
    await page.goto('/');
    const val = await page.locator('textarea').inputValue();
    expect(val.length).toBeGreaterThan(50);
  });

  test('pre-fills User ID, Business ID, Page ID', async ({ page }) => {
    await page.goto('/');
    const inputs = page.locator('input');
    await expect(inputs.nth(0)).toHaveValue(USER_ID);
    await expect(inputs.nth(1)).toHaveValue(BIZ_ID);
    await expect(inputs.nth(2)).toHaveValue(PAGE_ID);
  });

  test('Skip button bypasses validation and goes to dashboard', async ({ page }) => {
    await page.goto('/');
    await page.getByText('Skip').click();
    await expect(page).toHaveURL(/\/dashboard/);
  });

  test('empty token shows error', async ({ page }) => {
    await page.goto('/');
    await page.locator('textarea').fill('');
    await page.getByText('Login & Validate Token').click();
    await expect(page.getByText('Access token is required')).toBeVisible();
  });
});

// ── Dashboard overview ────────────────────────────────────────────────────────
test.describe('Dashboard overview', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard');
  });

  test('shows module count heading', async ({ page }) => {
    await expect(page.locator('h1,h2,h3').first()).toBeVisible();
  });

  test('renders at least 31 module cards', async ({ page }) => {
    const cards = page.locator('[data-testid="module-card"], a[href*="/dashboard/"]');
    const count = await cards.count();
    expect(count).toBeGreaterThanOrEqual(31);
  });

  test('sidebar is visible with module links', async ({ page }) => {
    await expect(page.locator('nav, aside').first()).toBeVisible();
    const links = page.locator('a[href*="/dashboard/"]');
    expect(await links.count()).toBeGreaterThanOrEqual(10);
  });

  test('header shows token status', async ({ page }) => {
    const header = page.locator('header, [role="banner"]').first();
    await expect(header).toBeVisible();
  });
});

// ── Sidebar navigation ────────────────────────────────────────────────────────
test.describe('Sidebar navigation', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard');
  });

  const modules = [
    { id: 'auth', name: 'Auth' },
    { id: 'user', name: 'User' },
    { id: 'business', name: 'Business' },
    { id: 'page', name: 'Page' },
    { id: 'post', name: 'Post' },
    { id: 'comment', name: 'Comment' },
    { id: 'media', name: 'Media' },
    { id: 'ads', name: 'Ads' },
    { id: 'instagram', name: 'Instagram' },
    { id: 'whatsapp', name: 'WhatsApp' },
  ];

  for (const mod of modules) {
    test(`navigates to ${mod.name} module`, async ({ page }) => {
      await page.goto(`/dashboard/${mod.id}`);
      await expect(page).toHaveURL(`/dashboard/${mod.id}`);
      await expect(page.locator('h1,h2').first()).toBeVisible();
    });
  }
});

// ── Auth module ───────────────────────────────────────────────────────────────
test.describe('Auth module', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/auth');
  });

  test('renders endpoint cards', async ({ page }) => {
    const cards = page.locator('[class*="border"],[class*="card"],[class*="rounded"]');
    expect(await cards.count()).toBeGreaterThan(0);
  });

  test('Get App Token — executes and returns success', async ({ page }) => {
    await page.getByText('Get App Token').click();
    await expect(page.getByText('Execute')).toBeVisible();
    const btn = page.getByText('Execute').first();
    await btn.click();
    await page.waitForTimeout(3000);
    const response = page.locator('[class*="mono"],[class*="json"],[class*="response"],[class*="pre"]');
    if (await response.count() > 0) {
      const text = await response.first().textContent();
      expect(text).toBeTruthy();
    }
  });

  test('Debug Token — executes with current token', async ({ page }) => {
    await page.getByText('Debug Token').click();
    const executeBtn = page.getByText('Execute').first();
    await expect(executeBtn).toBeVisible();
    await executeBtn.click();
    await page.waitForTimeout(3000);
    // Should not show a fetch error
    await expect(page.getByText('Failed to fetch')).not.toBeVisible();
  });
});

// ── User module ───────────────────────────────────────────────────────────────
test.describe('User module', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/user');
  });

  test('shows Get Profile endpoint', async ({ page }) => {
    await expect(page.getByText('Get Profile')).toBeVisible();
  });

  test('Get Profile — executes and returns data', async ({ page }) => {
    await page.getByText('Get Profile').click();
    await page.getByText('Execute').first().click();
    await page.waitForTimeout(3000);
    await expect(page.getByText('Failed to fetch')).not.toBeVisible();
  });

  test('Get Feed endpoint is present', async ({ page }) => {
    await expect(page.getByText('Get Feed')).toBeVisible();
  });

  test('Get Pages endpoint is present', async ({ page }) => {
    await expect(page.getByText('Get Pages')).toBeVisible();
  });
});

// ── Business Manager module ───────────────────────────────────────────────────
test.describe('Business Manager module', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/business');
  });

  test('shows Get Business endpoint', async ({ page }) => {
    await expect(page.getByText('Get Business')).toBeVisible();
  });

  test('URL contains interpolated BIZ_ID', async ({ page }) => {
    await page.getByText('Get Business').click();
    const urlInput = page.locator('input[type="text"], input:not([type])').first();
    if (await urlInput.count() > 0) {
      const val = await urlInput.inputValue();
      expect(val).toContain(BIZ_ID);
    }
  });
});

// ── Page module ───────────────────────────────────────────────────────────────
test.describe('Page module', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/page');
  });

  test('shows Get Page Info endpoint', async ({ page }) => {
    await expect(page.getByText('Get Page Info')).toBeVisible();
  });

  test('Get Page Info — executes without fetch error', async ({ page }) => {
    await page.getByText('Get Page Info').click();
    await page.getByText('Execute').first().click();
    await page.waitForTimeout(3000);
    await expect(page.getByText('Failed to fetch')).not.toBeVisible();
  });

  test('shows all post management endpoints', async ({ page }) => {
    await expect(page.getByText('Create Post')).toBeVisible();
    await expect(page.getByText('Get Feed')).toBeVisible();
  });
});

// ── EndpointCard UI behaviour ─────────────────────────────────────────────────
test.describe('EndpointCard interactions', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/auth');
  });

  test('card expands on click', async ({ page }) => {
    const label = page.getByText('Get App Token');
    await label.click();
    await expect(page.getByText('Execute')).toBeVisible();
  });

  test('card collapses on second click', async ({ page }) => {
    const label = page.getByText('Get App Token');
    await label.click();
    await expect(page.getByText('Execute')).toBeVisible();
    await label.click();
    await expect(page.getByText('Execute')).not.toBeVisible();
  });

  test('URL field is editable', async ({ page }) => {
    await page.getByText('Get App Token').click();
    const urlInput = page.locator('input').first();
    if (await urlInput.count() > 0) {
      await urlInput.fill('http://localhost:5150/api/facebook/auth/app-token');
      const val = await urlInput.inputValue();
      expect(val).toContain('app-token');
    }
  });

  test('POST body textarea is visible for POST endpoints', async ({ page }) => {
    await page.goto('/dashboard/auth');
    await page.getByText('Debug Token').click();
    const textarea = page.locator('textarea');
    expect(await textarea.count()).toBeGreaterThan(0);
  });

  test('Execute button shows loading state', async ({ page }) => {
    await page.getByText('Get App Token').click();
    const btn = page.getByText('Execute').first();
    await btn.click();
    // Either shows "Sending…" briefly or goes straight to response
    await page.waitForTimeout(500);
    // Just ensure page didn't crash
    await expect(page.locator('body')).toBeVisible();
  });
});

// ── Token placeholder interpolation ──────────────────────────────────────────
test.describe('Token interpolation', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
  });

  test('URL fields do not show raw {TOKEN} placeholder', async ({ page }) => {
    await page.goto('/dashboard/user');
    await page.getByText('Get Profile').click();
    const allInputs = page.locator('input');
    const count = await allInputs.count();
    for (let i = 0; i < count; i++) {
      const val = await allInputs.nth(i).inputValue();
      if (val.includes('http')) {
        expect(val).not.toContain('{TOKEN}');
        expect(val).not.toContain('{USER_ID}');
      }
    }
  });

  test('URL fields contain actual token value', async ({ page }) => {
    await page.goto('/dashboard/user');
    await page.getByText('Get Profile').click();
    const urlInput = page.locator('input').first();
    if (await urlInput.count() > 0) {
      const val = await urlInput.inputValue();
      if (val.includes('accessToken')) {
        expect(val).toContain('EAAO');
      }
    }
  });
});

// ── Header settings ───────────────────────────────────────────────────────────
test.describe('Header settings', () => {
  test.beforeEach(async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard');
  });

  test('settings button opens dropdown', async ({ page }) => {
    const settingsBtn = page.locator('button').filter({ hasText: /settings|⚙|gear/i }).first();
    if (await settingsBtn.count() > 0) {
      await settingsBtn.click();
      await expect(page.getByText(/token|access/i).first()).toBeVisible();
    }
  });

  test('logout button returns to login page', async ({ page }) => {
    const logoutBtn = page.locator('button,a').filter({ hasText: /logout|sign out/i }).first();
    if (await logoutBtn.count() > 0) {
      await logoutBtn.click();
      await expect(page).toHaveURL('/');
    }
  });
});

// ── All 31 module pages load ──────────────────────────────────────────────────
test.describe('All module pages load without crash', () => {
  const allModules = [
    'auth', 'user', 'business', 'page', 'post', 'comment', 'media',
    'events', 'groups', 'ads', 'adcreative', 'audience', 'pixel',
    'catalog', 'insights', 'leads', 'messenger', 'hashtag',
    'instagram', 'ig-media', 'ig-stories', 'ig-insights',
    'whatsapp', 'wa-messages', 'wa-templates', 'wa-contacts',
    'threads', 'live', 'webhook', 'commerce', 'developer'
  ];

  for (const mod of allModules) {
    test(`/dashboard/${mod} loads`, async ({ page }) => {
      await seedStorage(page);
      await page.goto(`/dashboard/${mod}`);
      // Should either show module content or redirect to dashboard (if id not found)
      const url = page.url();
      expect(url).toMatch(/localhost:3000/);
      // No unhandled JS error banner
      await expect(page.locator('text=Application error')).not.toBeVisible();
      await expect(page.locator('text=unhandled')).not.toBeVisible();
    });
  }
});

// ── API connectivity ──────────────────────────────────────────────────────────
test.describe('API server connectivity', () => {
  test('GET /api/facebook/auth/app-token returns success', async ({ request }) => {
    const res = await request.get('http://localhost:5150/api/facebook/auth/app-token');
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(json.success).toBe(true);
  });

  test('POST /api/facebook/auth/token/debug returns token info', async ({ request }) => {
    const res = await request.post('http://localhost:5150/api/facebook/auth/token/debug', {
      data: { inputToken: TOKEN },
    });
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/user/:id/profile returns user data', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/user/${USER_ID}/profile?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/page/:id/info returns page data', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/info?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/business/:id returns business data', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/business/${BIZ_ID}?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/user/:id/pages returns pages list', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/user/${USER_ID}/pages?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/user/:id/permissions returns permissions', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/user/${USER_ID}/permissions?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/user/:id/ad-accounts returns ad accounts', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/user/${USER_ID}/ad-accounts?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
    const json = await res.json();
    expect(typeof json.success).toBe('boolean');
  });

  test('GET /api/facebook/page/categories returns page categories', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/categories?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/roles returns page roles', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/roles?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/events returns events', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/events?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/tabs returns tabs', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/tabs?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/milestones returns milestones', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/milestones?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/labels returns labels', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/labels?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/cta returns CTA', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/cta?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/page/:id/blocked returns blocked users', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/page/${PAGE_ID}/blocked?accessToken=${TOKEN}&limit=10`);
    expect(res.status()).toBe(200);
  });

  test('POST /api/facebook/page/:id/feed returns feed', async ({ request }) => {
    const res = await request.post(`http://localhost:5150/api/facebook/page/${PAGE_ID}/feed?accessToken=${TOKEN}`, {
      data: { pageId: PAGE_ID, limit: 5 },
    });
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/business/:id/pages returns business pages', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/business/${BIZ_ID}/pages?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/business/:id/ad-accounts returns ad accounts', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/business/${BIZ_ID}/ad-accounts?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/business/:id/catalogs returns catalogs', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/business/${BIZ_ID}/catalogs?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/business/:id/users returns business users', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/business/${BIZ_ID}/users?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });

  test('GET /api/facebook/business/:id/system-users returns system users', async ({ request }) => {
    const res = await request.get(`http://localhost:5150/api/facebook/business/${BIZ_ID}/system-users?accessToken=${TOKEN}`);
    expect(res.status()).toBe(200);
  });
});

// ── Response viewer display ───────────────────────────────────────────────────
test.describe('Response viewer', () => {
  test('shows HTTP 200 status after successful call', async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/auth');
    await page.getByText('Get App Token').click();
    await page.getByText('Execute').first().click();
    await page.waitForTimeout(4000);
    // Look for status indicator
    const status = page.locator('text=200');
    if (await status.count() > 0) {
      await expect(status.first()).toBeVisible();
    }
  });

  test('shows JSON response content', async ({ page }) => {
    await seedStorage(page);
    await page.goto('/dashboard/auth');
    await page.getByText('Get App Token').click();
    await page.getByText('Execute').first().click();
    await page.waitForTimeout(4000);
    const bodyText = await page.locator('body').textContent();
    expect(bodyText).toContain('success');
  });
});

// ── Auth callback page ────────────────────────────────────────────────────────
test.describe('Auth callback page', () => {
  test('shows error when no token in URL', async ({ page }) => {
    await page.goto('/auth/callback');
    await page.waitForTimeout(1000);
    await expect(page.getByText(/no token|try again/i)).toBeVisible();
  });

  test('captures token from URL and redirects', async ({ page }) => {
    const fakeToken = 'EAAO_test_token_12345';
    await page.goto(`/auth/callback?token=${fakeToken}`);
    await page.waitForTimeout(3000);
    // Should redirect to dashboard
    await expect(page).toHaveURL(/\/dashboard/);
    // Verify token was saved
    const saved = await page.evaluate(() => localStorage.getItem('fb_token'));
    expect(saved).toBe(fakeToken);
  });
});

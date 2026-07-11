import { defineConfig } from '@playwright/test';
import { config } from 'dotenv';

// Load .env file if it exists
config();

const isSandbox = process.env.PLAYWRIGHT_MODE === 'sandbox';

export default defineConfig({
  testDir: './tests',
  timeout: 30000,
  retries: 0,
  reporter: [['html', { outputFolder: 'playwright-report', open: 'never' }], ['list']],
  use: {
    baseURL: isSandbox
      ? (process.env.SHOPEE_SANDBOX_BASE_URL ?? 'https://partner.test-stable.shopeemobile.com')
      : 'http://localhost:5051',
    extraHTTPHeaders: { 'Content-Type': 'application/json' },
    ignoreHTTPSErrors: true,
  },
  projects: [
    {
      name: 'local-api',
      testMatch: /(?!sandbox|flipkart-direct).*\.spec\.ts/,
    },
    {
      name: 'sandbox-direct',
      testMatch: /sandbox\.spec\.ts/,
    },
    {
      name: 'flipkart-direct',
      testMatch: /flipkart-direct\.spec\.ts/,
    },
  ],
});

'use client';
import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import type { AppState } from '@/lib/types';

const Ctx = createContext<AppState | null>(null);

export function AppProvider({ children }: { children: ReactNode }) {
  const [token, setTokenState] = useState('');
  const [pageToken, setPageTokenState] = useState('');
  const [userId, setUserIdState] = useState('me');
  const [businessId, setBusinessIdState] = useState('');
  const [pageId, setPageIdState] = useState('');

  useEffect(() => {
    setTokenState(localStorage.getItem('fb_token') ?? 'EAAO2NnWebZBcBR0BZBBFdhUM1sFm3eYGWMUkDYDq3McnM5PnF5ZCD17z5XtfhXZAKHDn6XCaFoJM1W5Rtknl3ChsZCrPEx3NvS7KEyAZAKVZCvwExyXCilBy4Ytn5Yvmff8QPrEVnZBjXZA7R2v2Nv4PWpmpXPZCfFB4ZCgQzC2vHXfew4CxP8Y6TcfGKO897vkzijsCvIPjHkoPQ2ZBN7X8KRfZCHYnyyT9jLple1A26Eypr');
    setPageTokenState(localStorage.getItem('fb_page_token') ?? '');
    setUserIdState(localStorage.getItem('fb_user_id') ?? '122108821923364890');
    setBusinessIdState(localStorage.getItem('fb_biz_id') ?? '1084873647383329');
    setPageIdState(localStorage.getItem('fb_page_id') ?? '1248325291691587');
  }, []);

  const setToken = (v: string) => { setTokenState(v); localStorage.setItem('fb_token', v); };
  const setPageToken = (v: string) => { setPageTokenState(v); localStorage.setItem('fb_page_token', v); };
  const setUserId = (v: string) => { setUserIdState(v); localStorage.setItem('fb_user_id', v); };
  const setBusinessId = (v: string) => { setBusinessIdState(v); localStorage.setItem('fb_biz_id', v); };
  const setPageId = (v: string) => { setPageIdState(v); localStorage.setItem('fb_page_id', v); };

  return (
    <Ctx.Provider value={{ apiBase: 'http://localhost:5150', token, pageToken, userId, businessId, pageId, setToken, setPageToken, setUserId, setBusinessId, setPageId }}>
      {children}
    </Ctx.Provider>
  );
}

export function useApp() {
  const ctx = useContext(Ctx);
  if (!ctx) throw new Error('useApp must be used within AppProvider');
  return ctx;
}

export function interpolate(template: string, ctx: AppState): string {
  return template
    .replace(/\{TOKEN\}/g, ctx.token || 'ACCESS_TOKEN')
    .replace(/\{PAGE_TOKEN\}/g, ctx.pageToken || ctx.token || 'PAGE_TOKEN')
    .replace(/\{USER_ID\}/g, ctx.userId || 'USER_ID')
    .replace(/\{BIZ_ID\}/g, ctx.businessId || 'BIZ_ID')
    .replace(/\{PAGE_ID\}/g, ctx.pageId || 'PAGE_ID');
}

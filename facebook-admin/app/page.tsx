'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';

export default function LoginPage() {
  const router = useRouter();
  const [token, setToken] = useState('EAAO2NnWebZBcBR0BZBBFdhUM1sFm3eYGWMUkDYDq3McnM5PnF5ZCD17z5XtfhXZAKHDn6XCaFoJM1W5Rtknl3ChsZCrPEx3NvS7KEyAZAKVZCvwExyXCilBy4Ytn5Yvmff8QPrEVnZBjXZA7R2v2Nv4PWpmpXPZCfFB4ZCgQzC2vHXfew4CxP8Y6TcfGKO897vkzijsCvIPjHkoPQ2ZBN7X8KRfZCHYnyyT9jLple1A26Eypr');
  const [userId, setUserId] = useState('122108821923364890');
  const [bizId, setBizId] = useState('1084873647383329');
  const [pageId, setPageId] = useState('1248325291691587');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  async function login() {
    if (!token.trim()) { setError('Access token is required'); return; }
    setLoading(true); setError('');
    try {
      const res = await fetch('http://localhost:5150/api/facebook/auth/token/debug', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ inputToken: token }),
      });
      const data = await res.json();
      if (!data.success) { setError(data.message ?? 'Token validation failed'); setLoading(false); return; }
    } catch { /* server offline — allow login anyway */ }

    localStorage.setItem('fb_token', token);
    if (userId) localStorage.setItem('fb_user_id', userId);
    if (bizId) localStorage.setItem('fb_biz_id', bizId);
    if (pageId) localStorage.setItem('fb_page_id', pageId);
    router.push('/dashboard');
  }

  function skipLogin() {
    if (token) localStorage.setItem('fb_token', token);
    if (userId) localStorage.setItem('fb_user_id', userId);
    if (bizId) localStorage.setItem('fb_biz_id', bizId);
    if (pageId) localStorage.setItem('fb_page_id', pageId);
    router.push('/dashboard');
  }

  return (
    <div className="min-h-full bg-gray-950 flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        <div className="text-center mb-8">
          <div className="inline-flex items-center justify-center w-16 h-16 bg-blue-600 rounded-2xl mb-4">
            <span className="text-3xl font-bold text-white">f</span>
          </div>
          <h1 className="text-2xl font-bold text-white">ApiToolKit</h1>
          <p className="text-gray-400 mt-1 text-sm">Facebook API Admin Dashboard</p>
          <p className="text-gray-500 mt-0.5 text-xs">31 modules · 347 endpoints</p>
        </div>

        <div className="bg-gray-800 border border-gray-700 rounded-2xl p-6 space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-300 mb-1.5">
              Access Token <span className="text-red-400">*</span>
            </label>
            <textarea
              value={token}
              onChange={e => setToken(e.target.value)}
              rows={3}
              placeholder="EAAxxxxxxx... (User, Page, or System User token)"
              className="w-full bg-gray-900 border border-gray-600 rounded-lg px-3 py-2 text-sm text-gray-200 focus:outline-none focus:border-blue-500 resize-none font-mono"
            />
          </div>

          <div className="grid grid-cols-2 gap-3">
            <div>
              <label className="block text-xs font-medium text-gray-400 mb-1">User ID</label>
              <input value={userId} onChange={e => setUserId(e.target.value)} placeholder="me or numeric ID"
                className="w-full bg-gray-900 border border-gray-600 rounded-lg px-3 py-1.5 text-sm text-gray-200 focus:outline-none focus:border-blue-500" />
            </div>
            <div>
              <label className="block text-xs font-medium text-gray-400 mb-1">Business ID</label>
              <input value={bizId} onChange={e => setBizId(e.target.value)} placeholder="Business Manager ID"
                className="w-full bg-gray-900 border border-gray-600 rounded-lg px-3 py-1.5 text-sm text-gray-200 focus:outline-none focus:border-blue-500" />
            </div>
          </div>

          <div>
            <label className="block text-xs font-medium text-gray-400 mb-1">Page ID</label>
            <input value={pageId} onChange={e => setPageId(e.target.value)} placeholder="Facebook Page ID"
              className="w-full bg-gray-900 border border-gray-600 rounded-lg px-3 py-1.5 text-sm text-gray-200 focus:outline-none focus:border-blue-500" />
          </div>

          {error && <p className="text-red-400 text-sm bg-red-950/40 border border-red-800 rounded-lg px-3 py-2">{error}</p>}

          <div className="flex gap-3 pt-1">
            <button onClick={login} disabled={loading}
              className="flex-1 bg-blue-600 hover:bg-blue-500 disabled:bg-gray-700 text-white font-semibold py-2.5 rounded-lg text-sm transition-colors">
              {loading ? 'Validating…' : 'Login & Validate Token'}
            </button>
            <button onClick={skipLogin}
              className="px-4 bg-gray-700 hover:bg-gray-600 text-gray-200 font-medium py-2.5 rounded-lg text-sm transition-colors">
              Skip
            </button>
          </div>
        </div>

        <p className="text-center text-gray-600 text-xs mt-6">
          Connected to <span className="font-mono text-gray-500">http://localhost:5150</span>
        </p>
      </div>
    </div>
  );
}

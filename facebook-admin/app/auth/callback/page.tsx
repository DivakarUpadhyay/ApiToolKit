'use client';
import { useEffect, useState } from 'react';
import { useRouter, useSearchParams } from 'next/navigation';
import { Suspense } from 'react';

function CallbackInner() {
  const router = useRouter();
  const params = useSearchParams();
  const [status, setStatus] = useState('Processing login…');
  const [token, setToken] = useState('');

  useEffect(() => {
    const t = params.get('token');
    if (!t) { setStatus('No token received. Please try again.'); return; }

    setToken(t);
    localStorage.setItem('fb_token', t);
    setStatus('Token saved! Redirecting to dashboard…');

    // Also try to get user info
    fetch(`http://localhost:5150/api/facebook/auth/token/debug`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ inputToken: t }),
    }).catch(() => {});

    setTimeout(() => router.push('/dashboard'), 1500);
  }, [params, router]);

  return (
    <div className="min-h-full bg-gray-950 flex items-center justify-center p-4">
      <div className="text-center space-y-4 max-w-lg w-full">
        <div className="w-16 h-16 bg-green-600 rounded-2xl flex items-center justify-center mx-auto">
          <span className="text-3xl">✓</span>
        </div>
        <h1 className="text-xl font-bold text-white">Facebook Login</h1>
        <p className="text-gray-400 text-sm">{status}</p>
        {token && (
          <div className="bg-gray-800 border border-gray-700 rounded-xl p-4 text-left">
            <p className="text-xs text-gray-400 mb-2">Access Token (saved automatically):</p>
            <p className="text-xs font-mono text-green-400 break-all">{token.slice(0, 60)}…</p>
          </div>
        )}
        {!token && status.includes('No token') && (
          <button onClick={() => router.push('/')} className="px-4 py-2 bg-blue-600 text-white rounded-lg text-sm">
            Back to Login
          </button>
        )}
      </div>
    </div>
  );
}

export default function AuthCallbackPage() {
  return (
    <Suspense fallback={<div className="min-h-full bg-gray-950 flex items-center justify-center"><p className="text-gray-400">Loading…</p></div>}>
      <CallbackInner />
    </Suspense>
  );
}

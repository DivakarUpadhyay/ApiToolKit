'use client';
import { useState } from 'react';
import { useApp } from '@/context/AppContext';
import { useRouter } from 'next/navigation';

export default function Header() {
  const ctx = useApp();
  const router = useRouter();
  const [showSettings, setShowSettings] = useState(false);

  function logout() {
    localStorage.clear();
    router.push('/');
  }

  return (
    <header className="h-14 border-b border-gray-700 bg-gray-900 flex items-center px-4 gap-3 shrink-0">
      <div className="flex-1 flex items-center gap-3">
        {/* Token indicator */}
        <div className="flex items-center gap-2 text-xs">
          <div className={`w-2 h-2 rounded-full ${ctx.token ? 'bg-green-400' : 'bg-red-400'}`} />
          <span className="text-gray-400">{ctx.token ? 'Token set' : 'No token'}</span>
          {ctx.pageId && <span className="text-gray-500">· Page: {ctx.pageId}</span>}
          {ctx.businessId && <span className="text-gray-500">· Biz: {ctx.businessId}</span>}
        </div>
      </div>

      {/* Settings toggle */}
      <button
        onClick={() => setShowSettings(s => !s)}
        className="px-3 py-1.5 text-xs text-gray-300 hover:text-white border border-gray-600 hover:border-gray-400 rounded transition-colors"
      >
        ⚙ Settings
      </button>

      <button
        onClick={logout}
        className="px-3 py-1.5 text-xs text-red-400 hover:text-red-300 border border-red-800 hover:border-red-600 rounded transition-colors"
      >
        Logout
      </button>

      {/* Settings drawer */}
      {showSettings && (
        <div className="absolute right-4 top-14 w-96 bg-gray-800 border border-gray-600 rounded-xl shadow-2xl p-4 z-50 space-y-3">
          <div className="flex items-center justify-between">
            <h3 className="text-sm font-semibold text-white">Global Settings</h3>
            <button onClick={() => setShowSettings(false)} className="text-gray-400 hover:text-white text-lg leading-none">×</button>
          </div>
          <p className="text-xs text-gray-400">These values are auto-substituted into all endpoint URLs and request bodies.</p>

          {[
            { label: 'Access Token', value: ctx.token, setter: ctx.setToken, placeholder: 'EAA...', key: 'token' },
            { label: 'Page Access Token', value: ctx.pageToken, setter: ctx.setPageToken, placeholder: 'EAA... (page-specific)', key: 'pageToken' },
            { label: 'User ID', value: ctx.userId, setter: ctx.setUserId, placeholder: 'me or numeric ID', key: 'userId' },
            { label: 'Business ID', value: ctx.businessId, setter: ctx.setBusinessId, placeholder: 'Business Manager ID', key: 'businessId' },
            { label: 'Page ID', value: ctx.pageId, setter: ctx.setPageId, placeholder: 'Facebook Page ID', key: 'pageId' },
          ].map(f => (
            <div key={f.key}>
              <label className="block text-xs text-gray-400 mb-1">{f.label} <span className="font-mono text-gray-600">{`{${f.key === 'token' ? 'TOKEN' : f.key === 'pageToken' ? 'PAGE_TOKEN' : f.key === 'userId' ? 'USER_ID' : f.key === 'businessId' ? 'BIZ_ID' : 'PAGE_ID'}}`}</span></label>
              <input
                type={f.key === 'token' || f.key === 'pageToken' ? 'password' : 'text'}
                value={f.value}
                onChange={e => f.setter(e.target.value)}
                placeholder={f.placeholder}
                className="w-full bg-gray-900 border border-gray-600 rounded px-3 py-1.5 text-xs text-gray-200 focus:outline-none focus:border-blue-500"
              />
            </div>
          ))}
        </div>
      )}
    </header>
  );
}

'use client';
import Link from 'next/link';
import { MODULES, MODULE_GROUPS } from '@/lib/modules';
import { useApp } from '@/context/AppContext';

const GROUP_COLORS: Record<string, string> = {
  'Core': 'border-blue-600/40 bg-blue-950/20',
  'Content': 'border-purple-600/40 bg-purple-950/20',
  'Analytics': 'border-green-600/40 bg-green-950/20',
  'Advertising': 'border-orange-600/40 bg-orange-950/20',
  'Catalog & Commerce': 'border-yellow-600/40 bg-yellow-950/20',
  'Instagram': 'border-pink-600/40 bg-pink-950/20',
  'Messaging & Social': 'border-cyan-600/40 bg-cyan-950/20',
  'WhatsApp': 'border-emerald-600/40 bg-emerald-950/20',
  'Developer': 'border-gray-600/40 bg-gray-800/20',
};

export default function DashboardPage() {
  const ctx = useApp();
  const totalEndpoints = MODULES.reduce((s, m) => s + m.endpoints.length, 0);

  return (
    <div className="max-w-6xl mx-auto space-y-8">
      {/* Status */}
      <div>
        <h1 className="text-2xl font-bold text-white mb-1">Facebook API Dashboard</h1>
        <p className="text-gray-400 text-sm">Meta platform integration: Facebook · Instagram · WhatsApp · Threads</p>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
        {[
          { label: 'Modules', value: MODULES.length },
          { label: 'Endpoints', value: totalEndpoints },
          { label: 'Token', value: ctx.token ? '✓ Set' : '✗ Missing', color: ctx.token ? 'text-green-400' : 'text-red-400' },
          { label: 'API Server', value: 'localhost:5150' },
        ].map(s => (
          <div key={s.label} className="bg-gray-800 border border-gray-700 rounded-xl p-4">
            <div className={`text-2xl font-bold ${s.color ?? 'text-white'}`}>{s.value}</div>
            <div className="text-gray-400 text-xs mt-0.5">{s.label}</div>
          </div>
        ))}
      </div>

      {!ctx.token && (
        <div className="bg-yellow-950/40 border border-yellow-700/40 rounded-xl p-4 text-sm text-yellow-300">
          ⚠ No access token set. Click <strong>Settings</strong> in the top bar to add your token before testing endpoints.
        </div>
      )}

      {/* Module grid by group */}
      {MODULE_GROUPS.map(group => {
        const mods = MODULES.filter(m => m.group === group);
        return (
          <div key={group}>
            <h2 className="text-sm font-semibold text-gray-400 uppercase tracking-wider mb-3">{group}</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-3">
              {mods.map(m => (
                <Link
                  key={m.id}
                  href={`/dashboard/${m.id}`}
                  className={`border rounded-xl p-4 hover:brightness-125 transition-all cursor-pointer ${GROUP_COLORS[group] ?? 'border-gray-700 bg-gray-800/20'}`}
                >
                  <div className="flex items-center gap-3">
                    <span className="text-xs font-mono text-gray-500 w-6">{m.num}</span>
                    <div className="flex-1 min-w-0">
                      <div className="text-white font-semibold text-sm">{m.name}</div>
                      <div className="text-gray-400 text-xs mt-0.5">{m.endpoints.length} endpoints</div>
                    </div>
                    <span className="text-gray-600 text-sm">→</span>
                  </div>
                </Link>
              ))}
            </div>
          </div>
        );
      })}
    </div>
  );
}

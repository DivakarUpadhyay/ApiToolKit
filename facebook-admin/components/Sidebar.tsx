'use client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { MODULES, MODULE_GROUPS } from '@/lib/modules';

const GROUP_ICONS: Record<string, string> = {
  'Core': '🔑',
  'Content': '📝',
  'Analytics': '📊',
  'Advertising': '📣',
  'Catalog & Commerce': '🛍️',
  'Instagram': '📸',
  'Messaging & Social': '💬',
  'WhatsApp': '📱',
  'Developer': '⚙️',
};

export default function Sidebar() {
  const pathname = usePathname();

  return (
    <aside className="w-64 shrink-0 bg-gray-900 border-r border-gray-700 flex flex-col overflow-y-auto">
      <div className="px-4 py-4 border-b border-gray-700">
        <div className="flex items-center gap-2">
          <div className="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center text-white font-bold text-sm">f</div>
          <div>
            <div className="text-white font-bold text-sm">ApiToolKit</div>
            <div className="text-gray-400 text-xs">Facebook API Admin</div>
          </div>
        </div>
      </div>

      <nav className="flex-1 py-3 px-2 space-y-4 overflow-y-auto">
        <Link
          href="/dashboard"
          className={`flex items-center gap-2 px-3 py-2 rounded-lg text-sm transition-colors ${pathname === '/dashboard' ? 'bg-blue-600 text-white' : 'text-gray-300 hover:bg-gray-800 hover:text-white'}`}
        >
          <span>🏠</span> Overview
        </Link>

        {MODULE_GROUPS.map(group => {
          const mods = MODULES.filter(m => m.group === group);
          return (
            <div key={group}>
              <div className="flex items-center gap-1.5 px-3 py-1 text-xs font-semibold text-gray-500 uppercase tracking-wider">
                <span>{GROUP_ICONS[group] ?? '▸'}</span>
                <span>{group}</span>
              </div>
              <div className="mt-1 space-y-0.5">
                {mods.map(m => {
                  const active = pathname === `/dashboard/${m.id}`;
                  return (
                    <Link
                      key={m.id}
                      href={`/dashboard/${m.id}`}
                      className={`flex items-center gap-2 px-3 py-1.5 rounded-lg text-sm transition-colors ${active ? 'bg-blue-600 text-white' : 'text-gray-300 hover:bg-gray-800 hover:text-white'}`}
                    >
                      <span className="text-xs text-gray-500 font-mono w-6 shrink-0">{m.num}</span>
                      <span className="truncate">{m.name}</span>
                    </Link>
                  );
                })}
              </div>
            </div>
          );
        })}
      </nav>

      <div className="p-3 border-t border-gray-700">
        <div className="text-xs text-gray-500 text-center">31 modules · 347 endpoints</div>
      </div>
    </aside>
  );
}

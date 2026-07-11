'use client';
import { use } from 'react';
import { notFound } from 'next/navigation';
import { getModule } from '@/lib/modules';
import EndpointCard from '@/components/EndpointCard';

const METHOD_COLORS: Record<string, string> = {
  GET: 'bg-blue-600',
  POST: 'bg-green-600',
  PUT: 'bg-yellow-500',
  DELETE: 'bg-red-600',
};

export default function ModulePage({ params }: { params: Promise<{ module: string }> }) {
  const { module: moduleId } = use(params);
  const mod = getModule(moduleId);
  if (!mod) notFound();

  const byMethod = {
    GET: mod.endpoints.filter(e => e.method === 'GET').length,
    POST: mod.endpoints.filter(e => e.method === 'POST').length,
    PUT: mod.endpoints.filter(e => e.method === 'PUT').length,
    DELETE: mod.endpoints.filter(e => e.method === 'DELETE').length,
  };

  return (
    <div className="max-w-4xl mx-auto space-y-6">
      {/* Header */}
      <div>
        <div className="flex items-center gap-3 mb-1">
          <span className="text-xs font-mono text-gray-500 bg-gray-800 px-2 py-0.5 rounded">{mod.num}</span>
          <h1 className="text-2xl font-bold text-white">{mod.name}</h1>
          <span className="text-xs text-gray-500 bg-gray-800 px-2 py-0.5 rounded">{mod.group}</span>
        </div>
        <p className="text-gray-400 text-sm">{mod.endpoints.length} endpoints</p>

        {/* Method pills */}
        <div className="flex gap-2 mt-3">
          {(Object.entries(byMethod) as [string, number][]).filter(([, c]) => c > 0).map(([m, c]) => (
            <span key={m} className={`text-xs font-bold px-2 py-0.5 rounded text-white ${METHOD_COLORS[m]}`}>
              {m} ×{c}
            </span>
          ))}
        </div>
      </div>

      {/* Endpoints */}
      <div className="space-y-2">
        {mod.endpoints.map(ep => (
          <EndpointCard key={ep.id} endpoint={ep} />
        ))}
      </div>
    </div>
  );
}

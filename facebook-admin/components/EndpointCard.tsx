'use client';
import { useState } from 'react';
import { useApp, interpolate } from '@/context/AppContext';
import type { Endpoint } from '@/lib/types';
import ResponseViewer from './ResponseViewer';

const METHOD_COLORS: Record<string, string> = {
  GET: 'bg-blue-600 text-white',
  POST: 'bg-green-600 text-white',
  PUT: 'bg-yellow-500 text-black',
  DELETE: 'bg-red-600 text-white',
};

interface Props { endpoint: Endpoint; }

export default function EndpointCard({ endpoint }: Props) {
  const ctx = useApp();
  const [open, setOpen] = useState(false);
  const [url, setUrl] = useState('');
  const [body, setBody] = useState('');
  const [loading, setLoading] = useState(false);
  const [status, setStatus] = useState<number | null>(null);
  const [respBody, setRespBody] = useState<string | null>(null);
  const [ms, setMs] = useState<number | null>(null);
  const [err, setErr] = useState<string | undefined>();

  function handleOpen() {
    if (!open) {
      setUrl(interpolate(endpoint.url, ctx));
      setBody(endpoint.body ? interpolate(endpoint.body, ctx) : '');
    }
    setOpen(o => !o);
    setStatus(null); setRespBody(null); setMs(null); setErr(undefined);
  }

  async function execute() {
    setLoading(true); setStatus(null); setRespBody(null); setMs(null); setErr(undefined);
    const t0 = Date.now();
    try {
      const opts: RequestInit = { method: endpoint.method };
      if (body && (endpoint.method === 'POST' || endpoint.method === 'PUT' || endpoint.method === 'DELETE')) {
        opts.headers = { 'Content-Type': 'application/json' };
        opts.body = body;
      }
      const resp = await fetch(url, opts);
      const text = await resp.text();
      setStatus(resp.status);
      setRespBody(text);
      setMs(Date.now() - t0);
    } catch (e) {
      setErr(e instanceof Error ? e.message : 'Network error');
      setMs(Date.now() - t0);
    } finally {
      setLoading(false);
    }
  }

  const hasBody = endpoint.method === 'POST' || endpoint.method === 'PUT' || endpoint.method === 'DELETE';

  return (
    <div className="border border-gray-700 rounded-lg overflow-hidden bg-gray-800/50">
      <button
        onClick={handleOpen}
        className="w-full flex items-center gap-3 px-4 py-3 hover:bg-gray-700/50 transition-colors text-left"
      >
        <span className={`text-xs font-bold px-2 py-0.5 rounded font-mono min-w-[52px] text-center ${METHOD_COLORS[endpoint.method]}`}>
          {endpoint.method}
        </span>
        <span className="text-gray-100 text-sm font-medium flex-1">{endpoint.label}</span>
        <span className="text-gray-500 text-xs">{open ? '▲' : '▼'}</span>
      </button>

      {open && (
        <div className="border-t border-gray-700 p-4 space-y-3">
          {/* URL */}
          <div>
            <label className="block text-xs text-gray-400 mb-1 font-medium">URL</label>
            <input
              value={url}
              onChange={e => setUrl(e.target.value)}
              className="w-full bg-gray-900 border border-gray-600 rounded px-3 py-2 text-xs font-mono text-gray-200 focus:outline-none focus:border-blue-500"
            />
          </div>

          {/* Body */}
          {hasBody && (
            <div>
              <label className="block text-xs text-gray-400 mb-1 font-medium">Request Body (JSON)</label>
              <textarea
                value={body}
                onChange={e => setBody(e.target.value)}
                rows={Math.min(12, (body.match(/\n/g)?.length ?? 0) + 2)}
                className="w-full bg-gray-900 border border-gray-600 rounded px-3 py-2 text-xs font-mono text-gray-200 focus:outline-none focus:border-blue-500 resize-y"
                placeholder="{}"
              />
            </div>
          )}

          {/* Execute */}
          <button
            onClick={execute}
            disabled={loading}
            className="px-4 py-2 bg-blue-600 hover:bg-blue-500 disabled:bg-gray-600 text-white text-sm rounded font-medium transition-colors"
          >
            {loading ? 'Executing…' : 'Execute'}
          </button>

          {/* Response */}
          <ResponseViewer status={status} body={respBody} ms={ms} error={err} />
        </div>
      )}
    </div>
  );
}

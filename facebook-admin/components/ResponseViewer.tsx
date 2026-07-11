'use client';

interface Props {
  status: number | null;
  body: string | null;
  ms: number | null;
  error?: string;
}

export default function ResponseViewer({ status, body, ms, error }: Props) {
  if (status === null && !error) return null;

  let parsed: unknown = null;
  let pretty = body ?? '';
  try { parsed = JSON.parse(body ?? ''); pretty = JSON.stringify(parsed, null, 2); } catch { /* raw */ }

  const isSuccess = status !== null && status >= 200 && status < 300;
  const apiSuccess = parsed && typeof parsed === 'object' && 'success' in (parsed as object) ? (parsed as { success: boolean }).success : isSuccess;

  return (
    <div className="mt-3 rounded-lg border border-gray-700 overflow-hidden text-sm">
      <div className={`flex items-center gap-3 px-3 py-2 ${apiSuccess ? 'bg-green-900/40 border-b border-green-700/40' : 'bg-red-900/40 border-b border-red-700/40'}`}>
        {status !== null && (
          <span className={`font-mono font-bold ${apiSuccess ? 'text-green-400' : 'text-red-400'}`}>
            HTTP {status}
          </span>
        )}
        {ms !== null && <span className="text-gray-400">{ms}ms</span>}
        {error && <span className="text-red-400">{error}</span>}
        <span className={`ml-auto text-xs font-medium ${apiSuccess ? 'text-green-400' : 'text-red-400'}`}>
          {apiSuccess ? '✓ Success' : '✗ Failed'}
        </span>
      </div>
      <pre className="p-3 overflow-x-auto bg-gray-900 text-gray-200 text-xs leading-relaxed max-h-80 overflow-y-auto whitespace-pre-wrap break-all">
        {pretty}
      </pre>
    </div>
  );
}

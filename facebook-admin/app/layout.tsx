import type { Metadata } from 'next';
import './globals.css';

export const metadata: Metadata = {
  title: 'ApiToolKit — Facebook API Admin',
  description: 'Test all 347 Facebook, Instagram, WhatsApp & Threads API endpoints in one place.',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en" className="h-full">
      <body className="h-full bg-gray-950 text-gray-100 antialiased">{children}</body>
    </html>
  );
}

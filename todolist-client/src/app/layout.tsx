import React from 'react';
import '../globals.scss';

export const metadata = {
    title: 'Hello, Next.js!',
    description: 'Minimal Next.js layout for testing',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        <title>{metadata.title}</title>
        <meta name="description" content={metadata.description} />
      </head>
      <body style={{ margin: 0, fontFamily: 'sans-serif', background: '#f9fafb' }}>
        {children}
      </body>
    </html>
  );
}

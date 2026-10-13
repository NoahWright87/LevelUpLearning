import type { Metadata } from "next";
import { theme, darkTheme } from "@/lib/theme";
import { buildThemeCss } from "@/lib/themeCss";
import { SITE } from "@/lib/site";
import "@noahwright/design/styles.css";
import "./globals.css";

export const metadata: Metadata = {
  title: {
    default: SITE.name,
    template: `%s | ${SITE.name}`,
  },
  description: SITE.description,
  icons: { icon: { url: "/icon.svg", type: "image/svg+xml" } },
};

export default function RootLayout({
  children,
}: Readonly<{ children: React.ReactNode }>) {
  const themeCss = buildThemeCss(theme, darkTheme);

  return (
    <html lang="en">
      <head>
        {themeCss ? <style id="nw-theme">{themeCss}</style> : null}
        {/* Blocking script: sets data-theme before first paint to prevent dark mode flash */}
        <script dangerouslySetInnerHTML={{ __html: `(function(){try{var m=localStorage.getItem('nw-theme-mode');if(m==='dark'||m==='light'){document.documentElement.dataset.theme=m;}else if(window.matchMedia('(prefers-color-scheme:dark)').matches){document.documentElement.dataset.theme='dark';}}catch(e){}})();` }} />
      </head>
      <body>{children}</body>
    </html>
  );
}

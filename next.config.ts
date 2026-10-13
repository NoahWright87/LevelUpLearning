import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  // Readable stack traces everywhere except the live site: Netlify sets CONTEXT to
  // "production" for main, and "deploy-preview" or "branch-deploy" for everything else.
  // With these maps, browser dev tools show real file and function names instead of
  // minified ones like "u is not a function".
  productionBrowserSourceMaps: process.env.CONTEXT !== "production",
};

export default nextConfig;

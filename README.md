# Level Up Learning

A side project by [Noah Wright](https://noahwright.dev) to help kids practice school skills.
Activities track your "experience" in each subject so you can level up, RPG-style.

This is the **web app** rewrite. The original Windows desktop app (2020–2021) lives in
[`archive/`](archive/) until its useful parts are salvaged.

## Status

🚧 Shell only: themed layout, a home page, and placeholder pages for Spelling and Math.

## Stack

- [Next.js](https://nextjs.org/) 15 (App Router), React 19, TypeScript
- [`@noahwright/design`](https://github.com/NoahWright87/design) for components and theming,
  set up the same way as [noahwright.dev](https://github.com/NoahWright87/NoahWrightDev2026)
- Netlify for hosting (`netlify.toml`)

## Develop

```bash
npm install
npm run dev        # http://localhost:3000
npm run lint
npm run typecheck
npm run build
```

## Layout

| Path | What |
| --- | --- |
| `src/app/` | Routes: `/` and `/[activity]` (one per entry in `ACTIVITIES`) |
| `src/components/SiteShell.tsx` | Header, nav, ⚙️ settings menu, horizon footer |
| `src/lib/site.ts` | Site constants and the list of activities |
| `src/lib/theme.ts` | Palette overrides (same as noahwright.dev) |
| `archive/` | The retired desktop app; see its README |

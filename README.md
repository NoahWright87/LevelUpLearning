# Level Up Learning

A side project by [Noah Wright](https://noahwright.dev) to help kids practice school skills.
Activities track your "experience" in each subject so you can level up, RPG-style.

This is the **web app** rewrite. The original Windows desktop app (2020–2021) lives in
[`archive/`](archive/) until its useful parts are salvaged.

## Status

- **Spelling** (`/spelling`): pick word lists and a difficulty, hear each word and its example
  sentence read aloud (the browser's text to speech), and type it. Lists can be uploaded,
  quick-created and downloaded as JSON at `/spelling/lists`; 22 built-in lists ship with the app.
- **Math**: placeholder.

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
npm test           # vitest, logic in src/lib
npm run build
```

## Layout

| Path | What |
| --- | --- |
| `src/app/` | Routes: `/`, `/spelling`, `/spelling/lists`, and `/[activity]` placeholders for unbuilt activities |
| `src/components/SiteShell.tsx` | Header, nav, ⚙️ settings menu, horizon footer |
| `src/lib/site.ts` | Site constants and the list of activities |
| `src/lib/theme.ts` | Palette overrides (same as noahwright.dev) |
| `src/lib/spelling/` | Quiz logic, difficulty presets, list file format, speech, storage (UI-free, tested) |
| `src/components/spelling/` | Spelling screens: setup, quiz, results, list manager |
| `src/data/spellingLists.json` | Built-in word lists |
| `archive/` | The retired desktop app; see its README |

# Project Memory

This file is the cross-chat handoff record for this repository. Keep it short and durable.

## Direction (Oct 2026, restart with Noah)

- **Web app, not desktop.** The 2020–2021 WinForms app was judged not worth carrying forward
  and moved to `archive/`. It gets deleted once the salvage list in `archive/README.md` is done.
- **Look like Noah's other sites.** Same setup as noahwright.dev (`NoahWright87/NoahWrightDev2026`):
  Next.js 15 + React 19, `@noahwright/design` (pinned), theme injected in `layout.tsx` via
  `buildThemeCss`, same plum + burnt orange palette, ⚙️ settings menu, horizon `SceneFooter`.
  `SettingsMenu.tsx`, `themeCss.ts`, `theme.ts` and the base/header/settings CSS in
  `globals.css` were copied from that repo; keep them in step with it.
- **Vision** (from the original README): activities across subjects that track experience so
  kids "level up" school skills; multiple kids on one install. First activities: Spelling, Math.

## Current Decisions

- Activities are listed in `ACTIVITIES` (`src/lib/site.ts`); `/[activity]` renders a
  placeholder for each until it has a real page. `ready: false` shows a "Coming soon" pill.
- `.npmrc` sets `legacy-peer-deps` because the design package declares React 18 peers
  (noahwright.dev does the same via `NPM_FLAGS` on Netlify).

## Open Questions

- Where progress is stored (browser-only vs. accounts/sync) — not decided.
- Speech for spelling: likely the browser's Web Speech API; not decided.
- Logo: a ⭐ emoji placeholder for now.

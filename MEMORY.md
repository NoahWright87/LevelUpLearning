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
- Hosting is Netlify via `netlify.toml` (`@netlify/plugin-nextjs`, Node 20), same as noahwright.dev.
- `.npmrc` sets `legacy-peer-deps` because the design package declares React 18 peers
  (noahwright.dev does the same via `NPM_FLAGS` on Netlify).

## Spelling Quiz (Oct 2026)

- First activity, and the one the system gets built around. A web recreation of the old app's
  "classic" quiz; its auto-leveling "adventure" mode is not built yet.
- Difficulty = the old app's settings: show blanks, hint letters to start, fewer hint letters per
  correct answer in the word's streak, times each word must be spelled right to finish, and streak
  penalty on a miss. Presets Intro/Easy/Medium/Hard/Custom keep the old values.
- Scoring changed from the old app on purpose: **stars come from accuracy alone** (kid-friendly;
  94% on Intro got 1.5 stars the old way), and the difficulty multiplier feeds a separate
  **points** number instead. The old multiplier was negative on Intro (int.MaxValue hints); fixed.
- List file format: `{ format: "level-up-learning.spelling-lists", version: 1, lists: [{ id,
  name, grade?, words: [{ word, sentence }] }] }`, `%%` marks the word in a sentence. Import also
  takes a bare list, an array, and words as plain strings. Built-ins live in
  `src/data/spellingLists.json` and are read-only; uploaded and quick-created lists go to
  localStorage.
- Speech: Web Speech API. Says the word, then the sentence; on a miss it says the word and spells
  it letter by letter (not the misspelling, which TTS reads badly). The sentence is also shown
  with a blank, so it works without sound.
- The answer box is a plain `<input>` with design classes until NoahWright87/design#32 (Input
  ref + native attributes) lands, so spellcheck and autocorrect can be turned off.
- Results aren't saved yet: progress storage is still undecided (below).

## Open Questions

- Where progress is stored (browser-only vs. accounts/sync) — not decided.
- Logo: a ⭐ emoji placeholder for now.

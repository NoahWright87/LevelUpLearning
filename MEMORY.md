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
- Word list files and the save file are separate formats: a list file is for sharing lists;
  the save file (below) holds everything, including the lists people added.
- List file format: `{ format: "level-up-learning.spelling-lists", version: 1, lists: [{ id,
  name, grade?, words: [{ word, sentence }] }] }`, `%%` marks the word in a sentence. Import also
  takes a bare list, an array, and words as plain strings. Built-ins live in
  `src/data/spellingLists.json` and are read-only; uploaded and quick-created lists go in the
  save file.
- Speech: Web Speech API. Says the word, then the sentence; on a miss it says the word and spells
  it letter by letter (not the misspelling, which TTS reads badly). The sentence is also shown
  with a blank, so it works without sound.
- The answer box is a plain `<input>` with design classes until NoahWright87/design#32 (Input
  ref + native attributes) lands, so spellcheck and autocorrect can be turned off.

## Save File (Oct 2026)

- **Decided with Noah:** everything a person has (players, results, their word lists) is one
  versioned JSON document, `src/lib/save/schema.ts` (`format: "level-up-learning.save"`,
  `version: 1`). It lives in localStorage (`lul.save.v1`) and can be downloaded and uploaded on
  `/progress`. **This is the contract for the future backend/accounts**: the server should store and
  return this same shape, and the download/upload path stays for people without an account (e.g. if
  accounts become paid). Not throwaway work: change it only through `SAVE_VERSION` + `migrate`.
- Results are a **history of sessions** per player (`player.spelling.sessions`, capped at
  `MAX_SESSIONS` = 500); bests, totals and tricky words are worked out from it
  (`src/lib/save/spellingProgress.ts`), so new stats need no migration. Each session also scores
  every list on that list's own words, like the old app's per-list performance.
- Players: several kids per browser, like the old app's users. A "Player 1" is created the first
  time a quiz finishes with nobody picked. Results go to the active player.
- Upload **replaces** what's in the browser (after a confirm showing both sides); there's no merge
  yet. Damaged files keep their valid parts; files from a newer version are refused.
- Device conveniences (last quiz setup, voice, theme) stay out of the save file.
- The first spelling release kept uploaded lists under `lul.spelling.lists.v1`; the store migrates
  them into the save file on first load and removes the old key.

## Gotchas

- **Effects need a block body.** `useEffect(() => window.scrollTo(...))` returns the call's
  value as the cleanup; newer Chrome returns a Promise from `scrollTo`, and the spelling quiz
  crashed with "u is not a function" on the first deploy preview. An ESLint rule in
  `eslint.config.mjs` now rejects expression-bodied effects.
- **Source maps** ship on every Netlify build except production (`next.config.ts` keys off
  Netlify's `CONTEXT`), so preview errors show real names in dev tools.
- Headless Chromium here has no voices and an older `scrollTo`; browser checks fake both
  (several voices, Promise-returning `scrollTo`) to match real Chrome.

## Open Questions

- Backend and accounts: when, and whether they're paid. The save file above is the data shape.
- Merging an uploaded save into an existing one (today it replaces).
- Logo: a ⭐ emoji placeholder for now.

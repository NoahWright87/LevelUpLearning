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

- Activities are listed in `ACTIVITIES` (`src/lib/site.ts`); Spelling, Math and Matching have real
  pages. `/[activity]` renders placeholders for future activities marked `ready: false`.
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
  it letter by letter (not the misspelling, which TTS reads badly). Noah requested that example
  sentences not be shown on the quiz screen, since they can give away other answers.
- The answer box is a plain `<input>` with design classes until NoahWright87/design#32 (Input
  ref + native attributes) lands, so spellcheck and autocorrect can be turned off.

## Save File (Oct 2026)

- **Decided with Noah:** everything a person has (players, results, their word lists) is one
  versioned JSON document, `src/lib/save/schema.ts` (`format: "level-up-learning.save"`,
  `version: 3`). It lives in localStorage (`lul.save.v1`, retained for migration) and can be downloaded and uploaded on
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

## Spelling celebrations (Oct 2026)

- Design 1.6.0 provides the fireworks renderer and trigger API. The spelling app owns
  one stable `useSceneEffects` handle, passes it to `SiteShell`'s footer, and triggers
  one small firework per combo point for correct answers and a large group on quiz completion.
- The hot streak lives in UI-free `QuizState.combo`, spans words/rounds, increments per
  correct answer, and resets on a miss or new quiz. It is separate from each word's hint
  streak. The play screen shows Combo ×N between answers, with escalating cheers.
- Combo ×N launches N small fireworks at fresh random positions; no local tier cap.
  The design renderer bounds particle usage. Unavailable scenes stop the trigger loop.
- Triggers stay in event handlers, outside the UI-free quiz logic. Wrong/blank answers
  and quitting do not celebrate. The design package drops unavailable/offscreen events
  and respects Still/reduced motion; celebrations do not scroll or queue for later.
- Logo, spelling fireworks, and compact quiz layout shipped together in merged PR #30.
- Active quizzes use compact spacing and a smaller scene via public sky/sea height props,
  keeping the fireworks visible. Combo sits in the progress row; Speak shares the Check/Next
  row. A reserved feedback slot keeps the footer from shifting when an answer is checked.

## Open Questions

- Backend and accounts: when, and whether they're paid. The save file above is the data shape.
- Merging an uploaded save into an existing one (today it replaces).

## Math sprint (Oct 2026)

- `/math` is playable: addition/multiplication, 1–3 digits per operand (0–9, 10–99,
  100–999), and goals of 10/30/100 correct; default 30 is a short replayable round.
- Three hearts; a miss costs one, resets combo and shows the correction until Next.
  Correct answers advance immediately and retain input focus. Blank/invalid input is ignored.
- Each correct answer refills a five-second combo timer; expiry resets only combo, never hearts.
  Score = 10 × current combo per correct answer. Fireworks = one small shell per combo point,
  plus a large burst on reaching the goal, through the shared design renderer.
- Rules and clock calculations live in `src/lib/math/game.ts`, with tests beside them.
  Browser timing uses `performance.now()`; submission checks expiry independently of UI ticks.
- Compact scene plus responsive play layout keeps the game and footer within one viewport,
  checked at desktop, phone (including 320×568) and 844×390 landscape sizes.
- Save version 2 migrates version 1 by adding `player.math.sessions`; existing spelling data
  and the storage key stay intact. Completed wins/losses save to the player selected at round
  start, capped at 500 math rounds; quitting doesn't record a result. `/progress` shows math
  totals and recent rounds. Native numeric input uses the existing design#32 workaround.

## Logo (Oct 2026)

- Noah selected the clean Graduate silhouette: graduation cap, original head/glasses,
  no collar or held objects. Source silhouette/glasses came from
  `NoahWright87/NoahWrightDev2026` `src/lib/logo.ts`.
- Shared paths in `src/lib/logo.ts` render the header via `components/Logo.tsx` and
  the SVG favicon via `app/icon.svg/route.ts`. Header colors follow the site theme;
  favicon colors follow system light/dark mode. Logo is visible at every screen width.

## Matching (Oct 2026)

- `/matching` is a reusable two-column activity. The engine in `src/lib/matching/game.ts`
  accepts plain `{ id, left, right }` pairs; math sources and future subjects use the same rules.
- Default: five rows, a 60-second challenge and continuous refill after a 400 ms fade.
  Customize also offers clear-the-board batches and 2–6 rows. Both styles support timed
  or match-count goals (including a short final board). Small custom decks use fewer cards.
- Quick math sources: 0–9 addition/multiplication, squares 1–12 and simplifying proper fractions
  with reduced denominators up to 10. Boards never contain duplicate prompts or answers.
- Wrong pairs remain, briefly show feedback and add a miss. Either column can be picked first;
  tapping the selected card deselects it. Pure transitions reject stale/rapid taps and check
  time on input, so delayed browser intervals cannot extend a round.
- Custom decks support create/edit/delete, 2–200 pairs, with unique labels per side to avoid
  ambiguity. Save version 3 adds `matching.decks`; v1/v2 progress migrates intact, keeping
  `lul.save.v1`. Decks are shared across players and included in Progress backup/restore.
- Results currently show round matches, time, misses and accuracy; Matching history is not
  recorded per player. All controls use the design system; card styling is activity-specific.

## Shared activity UI (Oct 2026)

- Start new games with `src/components/activity/ActivityUI.tsx`: `ActivityPage` owns the
  standard page width/site frame, `ActivityToolbar` the quit bar, `ActivityStats` the labeled
  metrics, `ActivityFeedback` the reserved live region, and `ActivityResults` the heading
  focus and replay/setup actions. Math and Matching share these; Spelling shares results.
- Shared `.activity-stack`, `.activity-actions`, `.activity-rules` and `.activity-result-score`
  replace per-game composition CSS. Use design `CardGrid` for responsive setup fields and
  design `Link` with `as={NextLink}` for links. Keep game boards and feedback states local.
- `useActivityClock(running, onTick?)` owns the browser interval and monotonic time. Call
  `sample()` on inputs/start; optional ticks let Matching advance fades and deadlines while
  Math derives its combo display from `now`. Rules remain UI-free in `src/lib/<activity>/`.
- Generic statistics belong upstream in NoahWright87/design#38; the shared stats component
  and styles are a small commented workaround. Tinted instructions track design#26.
- PRs should target `main`: the Netlify preview only builds for that base. Matching PR #32
  includes the pending Math Sprint work from #31 so it can preview against main.

# Level Up Learning

A side project by [Noah Wright](https://noahwright.dev) to help kids practice school skills.
Activities track your "experience" in each subject so you can level up, RPG-style.

This is the **web app** rewrite. The original Windows desktop app (2020–2021) lives in
[`archive/`](archive/) until its useful parts are salvaged.

## Status

- **Spelling** (`/spelling`): pick word lists and a difficulty, hear each word and its example
  sentence read aloud (the browser's text to speech), and type it. Lists can be uploaded,
  quick-created and downloaded as JSON at `/spelling/lists`; 22 built-in lists ship with the app.
- **Progress** (`/progress`): players, quiz history, best per list, tricky words, and the save
  file: everything is kept in this browser and can be downloaded and uploaded as one JSON file.
- **Math sprint** (`/math`): addition or multiplication with 1–3 digit operands, 10/30/100-answer
  rounds, three hearts, a five-second combo timer, and fireworks. Scores and round history
  save to the selected player and appear on `/progress`.

- **Matching** (`/matching`): reusable two-column speed drills with refill or clear-the-board
  play, timed or match-count goals, and quick math facts (addition, multiplication, squares,
  fractions). Create and edit custom decks for any subject; decks live in the browser save
  and travel with downloaded backups. Round results are shown at the end of each game.

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

## Deploy

Netlify builds the site from `netlify.toml`: production from `main`, and a deploy preview for
every pull request.

## Layout

| Path | What |
| --- | --- |
| `src/app/` | Routes: `/`, `/matching`, `/math`, `/spelling`, `/spelling/lists`, `/progress`, and `/[activity]` placeholders for unbuilt activities |
| `src/components/SiteShell.tsx` | Header, nav, ⚙️ settings menu, horizon footer |
| `src/components/activity/` | Shared game page, toolbar, stats, feedback, results, clock and composition styles; start here for new games |
| `src/lib/site.ts` | Site constants and the list of activities |
| `src/lib/theme.ts` | Palette overrides (same as noahwright.dev) |
| `src/lib/save/` | The save file: format, validation, players, localStorage store, spelling stats |
| `src/lib/spelling/` | Quiz logic, difficulty presets, list file format, speech, storage (UI-free, tested) |
| `src/components/spelling/` | Spelling screens: setup, quiz, results, list manager |
| `src/lib/matching/` | Subject-independent matching engine, math sources and deck validation (UI-free, tested) |
| `src/components/matching/` | Matching setup, deck editor, board, results and browser timing |
| `src/lib/math/` | Math sprint rules, timing, scoring and session history (UI-free, tested) |
| `src/components/math/` | Math setup, play, results and browser orchestration |
| `src/data/spellingLists.json` | Built-in word lists |
| `archive/` | The retired desktop app; see its README |

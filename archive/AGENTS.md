# Agent notes for `archive/`

This folder is **read-only reference material**: the retired Windows desktop app.

- **Don't edit, build, or "fix" anything in here.** No upgrades, no lint fixes, no refactors.
  It can't be built in this environment anyway (.NET Framework WinForms).
- **Do read from it** when salvaging. The salvage list lives in [`README.md`](README.md);
  tick items off there as they land in the web app.
- **Rewrite, don't port.** Reimplement salvaged logic in TypeScript at the repo root with
  tests, following the root `AGENTS.md`. Don't copy C# structure or naming across.
- Data files (`Spelling Lists/*.xml`) may be converted with a script; keep the word and
  example sentence content as written.
- Deleting this folder is Noah's call; don't do it unprompted.

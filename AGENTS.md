# Agent Handoff Convention

This repository uses `MEMORY.md` at the root as the source of truth for cross-chat handoff context.

## Required Behavior For New Chat Sessions

1. Read `MEMORY.md` before making planning decisions.
2. Treat `MEMORY.md` as the canonical summary of durable project context.
3. Update `MEMORY.md` when significant decisions are made that future sessions should inherit.

## Working Rules

- Run `npm run lint`, `npm run typecheck`, `npm test` and `npm run build` before pushing.
- Keep activity logic UI-free in `src/lib/<activity>/` with vitest tests beside it; components only render.
- **Lean on `@noahwright/design`.** When something belongs in the design system, file an issue
  in `NoahWright87/design` and keep only a small, commented local workaround here that names it.
- `archive/` is reference only; see `archive/AGENTS.md`.

## Related Docs

- `README.md` for high-level project status
- `archive/README.md` for the salvage list from the old desktop app

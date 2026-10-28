export type Pair = { id: string; left: string; right: string };
export type Deck = { id: string; name: string; pairs: Pair[] };
export type MathKind = "addition" | "multiplication" | "squares" | "fractions";
export type Settings = { style: "refill" | "clear"; goal: "time" | "matches"; amount: number; rows: number };
export const DEFAULT_SETTINGS: Settings = { style: "refill", goal: "time", amount: 60, rows: 5 };
export const MATH_LABELS: Record<MathKind, string> = { addition: "Single digit addition", multiplication: "Single digit multiplication", squares: "Perfect squares · 1–12", fractions: "Simplifying fractions" };
export function normalizeSettings(s: Settings): Settings {
  return { style: s.style === "clear" ? "clear" : "refill", goal: s.goal === "matches" ? "matches" : "time",
    amount: Math.max(1, Math.min(s.goal === "matches" ? 500 : 1800, Math.round(Number.isFinite(s.amount) ? s.amount : 60))),
    rows: Math.max(2, Math.min(6, Math.round(Number.isFinite(s.rows) ? s.rows : 5))) };
}
const gcd = (a: number, b: number): number => b ? gcd(b, a % b) : a;
export function mathPairs(kind: MathKind): Pair[] {
  const pairs: Pair[] = [];
  const add = (left: string, right: string) => pairs.push({ id: `${kind}-${pairs.length}`, left, right });
  if (kind === "squares") for (let n = 1; n <= 12; n++) add(`${n}²`, String(n * n));
  else if (kind === "fractions") {
    for (let d = 2; d <= 10; d++) for (let n = 1; n < d; n++) if (gcd(n, d) === 1) {
      for (let scale = 2; scale <= 4; scale++) add(`${n * scale}/${d * scale}`, `${n}/${d}`);
    }
  } else for (let a = 0; a <= 9; a++) for (let b = 0; b <= 9; b++) {
    add(`${a} ${kind === "addition" ? "+" : "×"} ${b}`, String(kind === "addition" ? a + b : a * b));
  }
  return pairs;
}
export function validateDeck(name: string, rows: { left: string; right: string }[]): string | null {
  if (!name.trim()) return "Give your deck a name.";
  if (name.trim().length > 80) return "Keep the deck name under 80 characters.";
  if (rows.length < 2 || rows.length > 200) return "Add between 2 and 200 pairs.";
  if (rows.some(p => !p.left.trim() || !p.right.trim())) return "Fill in both sides of every pair.";
  if (rows.some(p => p.left.trim().length > 100 || p.right.trim().length > 100)) return "Keep each card under 100 characters.";
  for (const side of ["left", "right"] as const) {
    const labels = rows.map(p => p[side].trim().toLocaleLowerCase());
    if (new Set(labels).size !== labels.length) return `Each ${side} card must be unique so matches are clear.`;
  }
  return null;
}
export function readDecks(value: unknown): Deck[] {
  if (!Array.isArray(value)) return [];
  const decks: Deck[] = [];
  for (const raw of value) {
    if (!raw || typeof raw !== "object") continue;
    const d = raw as Deck;
    if (typeof d.id !== "string" || typeof d.name !== "string" || !Array.isArray(d.pairs)) continue;
    if (d.pairs.some(p => !p || typeof p.left !== "string" || typeof p.right !== "string") || validateDeck(d.name, d.pairs)) continue;
    if (decks.some(deck => deck.id === d.id)) continue;
    decks.push({ id: d.id, name: d.name.trim(), pairs: d.pairs.map((p, i) => ({ id: `${d.id}-${i}`, left: p.left.trim(), right: p.right.trim() })) });
  }
  return decks;
}
export type Card = { pair: Pair; token: number; appearedAt: number; clearedAt: number | null };
export type Game = { settings: Settings; pool: Pair[]; left: (Card | null)[]; right: (Card | null)[];
  selected: { side: "left" | "right"; token: number } | null; wrong: number[]; wrongUntil: number;
  correct: number; mistakes: number; startedAt: number; finishedAt: number | null; serial: number;
  refills: number[] };
export const FADE_MS = 400;
export const APPEAR_MS = 2500;
export const REFILL_MS = 30_000;
/** Appearing cards cannot be matched before their fixed fade-in finishes. */
export function isCardReady(card: Card, now: number): boolean {
  return card.clearedAt === null && now >= card.appearedAt + APPEAR_MS;
}
function shuffle<T>(items: T[], rng: () => number): T[] {
  const copy = [...items];
  for (let i = copy.length - 1; i > 0; i--) { const j = Math.floor(rng() * (i + 1)); [copy[i], copy[j]] = [copy[j], copy[i]]; }
  return copy;
}
function fill(game: Game, now: number, rng: () => number, limit = Infinity): Game {
  const left = [...game.left], right = [...game.right];
  const active = left.filter((c): c is Card => !!c);
  const available = shuffle(game.pool.filter(p => !active.some(c => c.pair.left === p.left || c.pair.right === p.right)), rng);
  const emptyLeft = shuffle(left.flatMap((c, i) => c ? [] : [i]), rng);
  const emptyRight = shuffle(right.flatMap((c, i) => c ? [] : [i]), rng);
  let serial = game.serial;
  let remaining = game.settings.goal === "matches" ? game.settings.amount - game.correct - active.filter(c => c.clearedAt === null).length : Infinity;
  for (let i = 0; i < Math.min(emptyLeft.length, limit) && remaining > 0; i++) {
    const pair = available.shift();
    if (!pair) break;
    // A board never has indistinguishable prompts or answers, even for random math facts.
    for (let j = available.length - 1; j >= 0; j--) if (available[j].left === pair.left || available[j].right === pair.right) available.splice(j, 1);
    left[emptyLeft[i]] = { pair, token: ++serial, appearedAt: now, clearedAt: null };
    right[emptyRight[i]] = { pair, token: ++serial, appearedAt: now, clearedAt: null };
    remaining--;
  }
  return { ...game, left, right, serial };
}
export function startGame(pool: Pair[], settings: Settings, now: number, rng = Math.random): Game {
  if (pool.length < 2) throw new Error("At least two pairs are needed.");
  const s = normalizeSettings(settings);
  return fill({ settings: s, pool: [...pool], left: Array(s.rows).fill(null), right: Array(s.rows).fill(null), selected: null,
    wrong: [], wrongUntil: 0, correct: 0, mistakes: 0, startedAt: now, finishedAt: null, serial: 0,
    refills: [] }, now, rng);
}
export function tick(game: Game, now: number, rng = Math.random): Game {
  if (game.finishedAt !== null) return game;
  const deadline = game.startedAt + game.settings.amount * 1000;
  if (game.settings.goal === "time" && now >= deadline) return { ...game, finishedAt: deadline, selected: null };
  let next = game;
  if (game.wrong.length && now >= game.wrongUntil) next = { ...next, wrong: [] };
  if (game.left.some(c => c && c.clearedAt !== null && now >= c.clearedAt + FADE_MS)) {
    const remove = (c: Card | null) => c && c.clearedAt !== null && now >= c.clearedAt + FADE_MS ? null : c;
    next = { ...next, left: next.left.map(remove), right: next.right.map(remove) };
  }
  if (next.settings.style === "clear") {
    if (next.left.every(c => !c)) next = fill(next, now, rng);
  } else {
    const due = next.refills.filter(time => time <= now).length;
    if (due) {
      const filled = fill(next, now, rng, due);
      const dealt = (filled.serial - next.serial) / 2;
      // Keep undelivered timers if their slots are still fading or a source has no free pair.
      next = { ...filled, refills: filled.refills.slice(dealt) };
    }
  }
  return next;
}
export function selectCard(game: Game, side: "left" | "right", token: number, now: number, rng = Math.random): Game {
  const g = tick(game, now, rng);
  if (g.finishedAt !== null || g.wrong.length) return g;
  const card = g[side].find(c => c?.token === token);
  if (!card || !isCardReady(card, now)) return g;
  const selected = g.selected;
  if (!selected || selected.side === side) return { ...g, selected: selected?.token === token ? null : { side, token } };
  const other = g[selected.side].find(c => c?.token === selected.token);
  if (!other || !isCardReady(other, now)) return { ...g, selected: { side, token } };
  if (other.pair.id !== card.pair.id) return { ...g, selected: null, mistakes: g.mistakes + 1, wrong: [other.token, token], wrongUntil: now + FADE_MS };
  const mark = (c: Card | null) => c?.token === token || c?.token === other.token ? { ...c, clearedAt: now } : c;
  const correct = g.correct + 1;
  // A match halves the time left on existing timers, then starts its own full 30 seconds.
  const refills = g.settings.style === "refill"
    ? [...g.refills.map(time => now + Math.max(0, time - now) / 2), now + REFILL_MS]
    : [];
  return { ...g, left: g.left.map(mark), right: g.right.map(mark), selected: null, correct,
    refills,
    finishedAt: g.settings.goal === "matches" && correct >= g.settings.amount ? now : null };
}
export function elapsed(game: Game, now: number): number { return Math.max(0, ((game.finishedAt ?? now) - game.startedAt) / 1000); }

import { describe, expect, it } from "vitest";
import { DEFAULT_SETTINGS, FADE_MS, mathPairs, normalizeSettings, readDecks, selectCard, startGame, tick, validateDeck, type Card, type Game, type Pair } from "./game";
import { emptySave, parseSaveFile, serializeSave } from "../save/schema";
const rng = () => 0.37;
const pool: Pair[] = Array.from({ length: 12 }, (_, i) => ({ id: String(i), left: `Question ${i}`, right: `Answer ${i}` }));
const cards = (g: Game, side: "left" | "right" = "left") => g[side].filter((c): c is Card => !!c && c.clearedAt === null);
function match(g: Game, now: number): Game {
  const left = cards(g)[0];
  const right = cards(g, "right").find(c => c.pair.id === left.pair.id)!;
  return selectCard(selectCard(g, "left", left.token, now, rng), "right", right.token, now, rng);
}
describe("Matching engine", () => {
  it("deals independently shuffled, solvable columns without changing the source", () => {
    const g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    expect(cards(g)).toHaveLength(5);
    expect(cards(g).map(c => c.pair.id).sort()).toEqual(cards(g, "right").map(c => c.pair.id).sort());
    expect(cards(g).map(c => c.pair.id)).not.toEqual(cards(g, "right").map(c => c.pair.id));
    expect(pool[0].id).toBe("0");
  });
  it("supports either selection order, toggling, and switching within a column", () => {
    let g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    const [a, b] = cards(g);
    g = selectCard(g, "left", a.token, 1, rng);
    expect(selectCard(g, "left", a.token, 2, rng).selected).toBeNull();
    g = selectCard(g, "left", b.token, 2, rng);
    expect(g.selected?.token).toBe(b.token);
    g = selectCard(g, "left", b.token, 3, rng);
    const right = cards(g, "right").find(c => c.pair.id === a.pair.id)!;
    g = selectCard(g, "right", right.token, 4, rng);
    g = selectCard(g, "left", a.token, 5, rng);
    expect(g.correct).toBe(1);
  });
  it("holds mismatches briefly, rejects rapid repeats, then lets the player retry", () => {
    let g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    const a = cards(g)[0], b = cards(g, "right").find(c => c.pair.id !== a.pair.id)!;
    g = selectCard(selectCard(g, "left", a.token, 1, rng), "right", b.token, 2, rng);
    expect(g.mistakes).toBe(1);
    expect(g.correct).toBe(0);
    expect(cards(g)).toHaveLength(5);
    expect(selectCard(g, "left", a.token, 3, rng)).toBe(g);
    g = tick(g, 2 + FADE_MS, rng);
    expect(g.wrong).toEqual([]);
    expect(match(g, 500).correct).toBe(1);
  });
  it("fades before refilling and stale button tokens cannot score again", () => {
    const initial = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    let g = match(initial, 10);
    expect(g.correct).toBe(1);
    expect(initial.correct).toBe(0);
    expect(g.left.filter(Boolean)).toHaveLength(5);
    const old = g.left.find(c => c?.clearedAt !== null && c)!;
    expect(selectCard(g, "left", old.token, 20, rng).correct).toBe(1);
    expect(tick(g, 10 + FADE_MS - 1, rng).serial).toBe(g.serial);
    g = tick(g, 10 + FADE_MS, rng);
    expect(cards(g)).toHaveLength(5);
    expect(g.left.some(c => c?.token === old.token)).toBe(false);
    expect(selectCard(g, "left", old.token, 600, rng).selected).toBeNull();
  });
  it("clears a whole batch before dealing again", () => {
    let g = startGame(pool, { ...DEFAULT_SETTINGS, style: "clear", rows: 2 }, 0, rng);
    g = tick(match(g, 10), 410, rng);
    expect(cards(g)).toHaveLength(1);
    g = tick(match(g, 500), 900, rng);
    expect(cards(g)).toHaveLength(2);
    expect(g.correct).toBe(2);
  });
  it("refills independently when several matches fade at different times", () => {
    let g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    g = match(g, 10); g = match(g, 100);
    g = tick(g, 410, rng);
    expect(g.left.filter(c => c?.clearedAt !== null && c)).toHaveLength(1);
    expect(cards(g)).toHaveLength(4);
    g = tick(g, 500, rng);
    expect(cards(g)).toHaveLength(5);
  });
  it.each(["refill", "clear"] as const)("finishes exactly at the target, including short final boards: %s", style => {
    let g = startGame(pool, { ...DEFAULT_SETTINGS, style, goal: "matches", amount: 7 }, 0, rng);
    let now = 1;
    while (g.finishedAt === null && now < 10000) {
      if (cards(g).length) g = match(g, now);
      now += FADE_MS;
      g = tick(g, now, rng);
      expect(cards(g).length).toBeLessThanOrEqual(7 - g.correct);
    }
    expect(g.correct).toBe(7);
    expect(g.finishedAt).not.toBeNull();
    expect(match(startGame(pool, { ...DEFAULT_SETTINGS, goal: "matches", amount: 1 }, 0, rng), 5).finishedAt).toBe(5);
  });
  it("checks the deadline on a tap, even before the interval runs", () => {
    const g = startGame(pool, { ...DEFAULT_SETTINGS, amount: 1 }, 100, rng);
    const ended = match(g, 1100);
    expect(ended.finishedAt).toBe(1100);
    expect(ended.correct).toBe(0);
    expect(tick(ended, 5000)).toBe(ended);
    expect(match(g, 1099).correct).toBe(1);
  });
  it("recycles small decks and preserves a partner for every visible card", () => {
    let g = startGame(pool.slice(0, 2), DEFAULT_SETTINGS, 0, rng);
    for (let i = 1; i < 20; i++) {
      g = tick(match(g, i * 500), i * 500 + FADE_MS, rng);
      expect(cards(g)).toHaveLength(2);
      expect(cards(g).map(c => c.pair.id).sort()).toEqual(cards(g, "right").map(c => c.pair.id).sort());
    }
  });
  it("normalizes invalid configuration and rejects empty sources", () => {
    expect(normalizeSettings({ ...DEFAULT_SETTINGS, rows: 100, amount: NaN })).toEqual({ ...DEFAULT_SETTINGS, rows: 6 });
    expect(() => startGame([], DEFAULT_SETTINGS, 0)).toThrow();
  });
});
describe("math sources", () => {
  it.each(["addition", "multiplication", "squares", "fractions"] as const)("keeps %s boards unambiguous across refills", kind => {
    let g = startGame(mathPairs(kind), DEFAULT_SETTINGS, 0, rng);
    for (let i = 1; i < 40; i++) {
      const active = cards(g);
      expect(active.length).toBe(5);
      expect(new Set(active.map(c => c.pair.left)).size).toBe(active.length);
      expect(new Set(active.map(c => c.pair.right)).size).toBe(active.length);
      g = tick(match(g, i * 500), i * 500 + FADE_MS, rng);
    }
  });
  it("generates correct, quick mental math", () => {
    for (const kind of ["addition", "multiplication"] as const) for (const p of mathPairs(kind)) {
      const [a, , b] = p.left.split(" ");
      expect(Number(p.right)).toBe(kind === "addition" ? Number(a) + Number(b) : Number(a) * Number(b));
      expect(Number(a)).toBeLessThan(10); expect(Number(b)).toBeLessThan(10);
    }
    for (const p of mathPairs("squares")) expect(Number(p.right)).toBe(Number(p.left.slice(0, -1)) ** 2);
    for (const p of mathPairs("fractions")) {
      const [n, d] = p.left.split("/").map(Number), [a, b] = p.right.split("/").map(Number);
      expect(n * b).toBe(a * d);
      expect(a).toBeLessThan(b);
      expect(b).toBeLessThanOrEqual(10);
      expect(Array.from({ length: a - 1 }, (_, i) => i + 2).some(k => a % k === 0 && b % k === 0)).toBe(false);
    }
  });
});
describe("custom decks and saves", () => {
  const deck = { id: "my-deck", name: "Capitals", pairs: [{ id: "my-deck-0", left: "France", right: "Paris" }, { id: "my-deck-1", left: "Japan", right: "Tokyo" }] };
  it("validates both sides and rejects ambiguous or incomplete cards", () => {
    expect(validateDeck(deck.name, deck.pairs)).toBeNull();
    expect(validateDeck("", deck.pairs)).toMatch(/name/);
    expect(validateDeck("Test", [{ left: "", right: "a" }, { left: "b", right: "c" }])).toMatch(/both sides/);
    expect(validateDeck("Test", [{ left: "a", right: "same" }, { left: "b", right: " SAME " }])).toMatch(/unique/);
    expect(readDecks([null, { pairs: [] }, deck])).toEqual([deck]);
    expect(readDecks(Array.from({ length: 101 }, (_, i) => ({ ...deck, id: `deck-${i}` })))).toHaveLength(101);
  });
  it("includes decks in exported save files and drops damaged decks", () => {
    const save = { ...emptySave(), matching: { decks: [deck] } };
    expect(parseSaveFile(serializeSave(save))).toEqual(save);
    expect(readDecks([{ ...deck, pairs: [{ left: 12, right: "a" }] }, deck, deck])).toEqual([deck]);
  });
  it.each([1, 2])("migrates version %s without losing existing progress", version => {
    const old = { ...emptySave(), version, matching: undefined, players: [{ id: "p", name: "Ava", createdAt: "2026-01-01", spelling: { sessions: [] }, math: { sessions: [] } }] };
    const next = parseSaveFile(JSON.stringify(old));
    expect(next.version).toBe(3); expect(next.matching.decks).toEqual([]);
    expect(next.players[0].name).toBe("Ava");
    expect(next.players[0].math.sessions).toEqual([]);
  });
});

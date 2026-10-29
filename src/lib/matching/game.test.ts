import { describe, expect, it } from "vitest";
import { APPEAR_MS, DEFAULT_SETTINGS, FADE_MS, INTERACTIVE_MS, REFILL_MS, isCardReady, mathPairs, normalizeSettings, readDecks, selectCard, startGame, tick, validateDeck, type Card, type Game, type Pair } from "./game";
import { emptySave, parseSaveFile, serializeSave } from "../save/schema";
const rng = () => 0.37;
const pool: Pair[] = Array.from({ length: 12 }, (_, i) => ({ id: String(i), left: `Question ${i}`, right: `Answer ${i}` }));
const cards = (g: Game, side: "left" | "right" = "left") => g[side].filter((c): c is Card => !!c && c.clearedAt === null);
function match(g: Game, now: number): Game {
  const left = cards(g).find(card => isCardReady(card, now))!;
  const right = cards(g, "right").find(c => c.pair.id === left.pair.id)!;
  return selectCard(selectCard(g, "left", left.token, now, rng), "right", right.token, now, rng);
}

describe("Matching engine", () => {
  it("deals solvable columns with distinct tokens and a fixed appearance time", () => {
    const g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    expect(cards(g)).toHaveLength(5);
    expect(cards(g).map(c => c.pair.id).sort()).toEqual(cards(g, "right").map(c => c.pair.id).sort());
    expect(new Set([...cards(g), ...cards(g, "right")].map(c => c.token)).size).toBe(10);
    expect(cards(g).every(c => c.appearedAt === 0)).toBe(true);
    expect(cards(g).every(c => c.initiallyVisible && isCardReady(c, 0))).toBe(true);
    expect(pool[0].id).toBe("0");
  });
  it("opens immediately and unlocks replacement cards 500ms into their 1.5-second fade", () => {
    const initial = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    let g = match(initial, 0);
    expect(g.correct).toBe(1);
    g = tick(g, REFILL_MS, rng);
    const card = cards(g).find(c => !c.initiallyVisible)!;
    expect(isCardReady(card, REFILL_MS)).toBe(false);
    expect(selectCard(g, "left", card.token, REFILL_MS + INTERACTIVE_MS - 1, rng).selected).toBeNull();
    expect(selectCard(g, "left", card.token, REFILL_MS + INTERACTIVE_MS, rng).selected?.token).toBe(card.token);
    expect(INTERACTIVE_MS).toBe(500);
    expect(APPEAR_MS).toBe(1500);
  });
  it("supports either selection order, toggling, and changing within a column", () => {
    let g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    const [a, b] = cards(g);
    g = selectCard(g, "left", a.token, 3000, rng);
    expect(selectCard(g, "left", a.token, 3001, rng).selected).toBeNull();
    g = selectCard(g, "left", b.token, 3002, rng);
    expect(g.selected?.token).toBe(b.token);
    g = selectCard(g, "left", b.token, 3003, rng);
    const right = cards(g, "right").find(c => c.pair.id === a.pair.id)!;
    g = selectCard(g, "right", right.token, 3004, rng);
    g = selectCard(g, "left", a.token, 3005, rng);
    expect(g.correct).toBe(1);
  });
  it("starts an independent 10-second replacement timer even for one match", () => {
    const initial = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    let g = match(initial, 3000);
    const old = g.left.find(c => c?.clearedAt !== null && c)!;
    expect(initial.correct).toBe(0);
    expect(g.refills).toEqual([13000]);
    expect(selectCard(g, "left", old.token, 3010, rng).correct).toBe(1);
    expect(tick(g, 3000 + FADE_MS - 1, rng).left.filter(Boolean)).toHaveLength(5);
    g = tick(g, 3000 + FADE_MS, rng);
    expect(cards(g)).toHaveLength(4);
    expect(g.left.some(c => c?.token === old.token)).toBe(false);
    expect(selectCard(g, "left", old.token, 3500, rng).selected).toBeNull();
    expect(cards(tick(g, 12999, rng))).toHaveLength(4);
    g = tick(g, 13000, rng);
    expect(cards(g)).toHaveLength(5);
    expect(g.refills).toEqual([]);
    expect(cards(g).find(c => c.token > initial.serial)?.appearedAt).toBe(13000);
  });
  it("subtracts three seconds from existing waits before starting a fresh timer", () => {
    let g = match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000);
    expect(g.refills).toEqual([13000]);
    g = match(g, 5000);
    expect(g.refills).toEqual([10000, 15000]);
    g = match(g, 7000);
    expect(g.refills).toEqual([7000, 12000, 17000]);
    expect(g.refills.at(-1)! - 7000).toBe(REFILL_MS);
  });
  it("makes a shortened wait due now instead of moving its deadline into the past", () => {
    let g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    for (const now of [0, 1000, 2000, 3000]) g = match(g, now);
    expect(g.refills).toEqual([3000, 5000, 9000, 13000]);
    g = tick(g, 3000, rng);
    expect(g.refills).toEqual([5000, 9000, 13000]);
    expect(cards(g).filter(c => !c.initiallyVisible)).toHaveLength(1);
  });
  it("keeps an immediately due timer queued until a fading pair leaves room", () => {
    let g = startGame(pool, DEFAULT_SETTINGS, 0, rng);
    for (const now of [0, 10, 20, 30, 40]) g = match(g, now);
    expect(g.refills[0]).toBe(40);
    const serial = g.serial;
    g = tick(g, FADE_MS - 1, rng);
    expect(g.serial).toBe(serial);
    expect(g.refills).toHaveLength(5);
    g = tick(g, FADE_MS, rng);
    expect(g.serial).toBe(serial + 2);
    expect(g.refills).toHaveLength(4);
  });
  it("spawns only the pair whose timer expires, leaving other timers running", () => {
    let g = match(match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000), 5000);
    g = tick(g, 10000, rng);
    expect(cards(g)).toHaveLength(4);
    expect(g.refills).toEqual([15000]);
    expect(cards(tick(g, 14999, rng))).toHaveLength(4);
    g = tick(g, 15000, rng);
    expect(cards(g)).toHaveLength(5);
    expect(g.refills).toEqual([]);
  });
  it("processes due timers on input even before a browser interval fires", () => {
    let g = match(match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000), 5000);
    const card = cards(g)[0];
    g = selectCard(g, "left", card.token, 10000, rng);
    expect(g.selected?.token).toBe(card.token);
    expect(cards(g)).toHaveLength(4);
    expect(g.refills).toEqual([15000]);
  });
  it("gives cards a full fade-in after a late browser tick rather than backdating their appearance", () => {
    const g = match(match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000), 5000);
    const late = tick(g, 40000, rng);
    const fresh = cards(late).filter(c => c.token > g.serial);
    expect(fresh).toHaveLength(2);
    expect(fresh.every(c => c.appearedAt === 40000)).toBe(true);
    expect(late.refills).toEqual([]);
    expect(selectCard(late, "left", fresh[0].token, 40000 + INTERACTIVE_MS - 1, rng).selected).toBeNull();
    expect(selectCard(late, "left", fresh[0].token, 40000 + INTERACTIVE_MS, rng).selected?.token).toBe(fresh[0].token);
  });
  it("does not speed up a fade-in when other matches accelerate pending timers", () => {
    let g = match(match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000), 5000);
    g = tick(g, 10000, rng);
    const fresh = cards(g).find(c => c.appearedAt === 10000)!;
    g = match(g, 10300);
    expect(g.refills).toEqual([12000, 20300]);
    expect(cards(g).find(c => c.token === fresh.token)?.appearedAt).toBe(10000);
    expect(isCardReady(fresh, 10000 + INTERACTIVE_MS - 1)).toBe(false);
    expect(isCardReady(fresh, 10000 + INTERACTIVE_MS)).toBe(true);
  });
  it("can spawn a due pair in an old empty slot while another match is fading out", () => {
    let g = match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000);
    g = match(g, 12999);
    g = tick(g, 13000, rng);
    expect(g.left.filter(c => c !== null && c.clearedAt !== null)).toHaveLength(1);
    expect(cards(g).some(c => c.appearedAt === 13000)).toBe(true);
    expect(g.refills).toEqual([22999]);
  });
  it("holds mismatches briefly without changing any replacement timers", () => {
    let g = match(startGame(pool, DEFAULT_SETTINGS, 0, rng), 3000);
    const a = cards(g)[0], b = cards(g, "right").find(c => c.pair.id !== a.pair.id)!;
    g = selectCard(selectCard(g, "left", a.token, 4000, rng), "right", b.token, 4001, rng);
    expect(g.mistakes).toBe(1);
    expect(g.refills).toEqual([13000]);
    expect(selectCard(g, "left", a.token, 4002, rng)).toBe(g);
    expect(tick(g, 4001 + FADE_MS, rng).wrong).toEqual([]);
  });
  it("keeps clear-the-board mode separate from replacement timers", () => {
    let g = startGame(pool, { ...DEFAULT_SETTINGS, style: "clear", rows: 2 }, 0, rng);
    g = tick(match(g, 3000), 3400, rng);
    expect(cards(g)).toHaveLength(1);
    expect(g.refills).toEqual([]);
    g = tick(match(g, 4000), 4400, rng);
    expect(cards(g)).toHaveLength(2);
    expect(cards(g).every(c => c.appearedAt === 4400)).toBe(true);
    expect(cards(g).every(c => !isCardReady(c, 4400 + INTERACTIVE_MS - 1))).toBe(true);
    expect(cards(g).every(c => isCardReady(c, 4400 + INTERACTIVE_MS))).toBe(true);
    expect(g.refills).toEqual([]);
  });
  it("shuffles each replacement into random empty positions while preserving partners", () => {
    let seed = 17;
    const random = () => (seed = seed * 16807 % 2147483647) / 2147483647;
    let g = startGame(pool, { ...DEFAULT_SETTINGS, amount: 1800 }, 0, random);
    const positions = new Set<number>();
    let now = APPEAR_MS;
    for (let wave = 0; wave < 10; wave++) {
      g = match(match(g, now + 10), now + 20);
      now = Math.max(...g.refills);
      g = tick(g, now, random);
      now += APPEAR_MS;
      positions.add(g.right.findIndex(c => c?.pair.id === cards(g)[0].pair.id));
      expect(cards(g)).toHaveLength(5);
      expect(cards(g).map(c => c.pair.id).sort()).toEqual(cards(g, "right").map(c => c.pair.id).sort());
    }
    expect(positions.size).toBeGreaterThan(1);
  });
  it.each(["refill", "clear"] as const)("finishes exactly at the target, including short final boards: %s", style => {
    let g = startGame(pool, { ...DEFAULT_SETTINGS, style, goal: "matches", amount: 7 }, 0, rng);
    let now = 0;
    for (let i = 0; i < 20 && g.finishedAt === null; i++) {
      now += APPEAR_MS;
      g = tick(g, now, rng);
      g = match(g, now);
      now += REFILL_MS;
      g = tick(g, now, rng);
      expect(cards(g).length).toBeLessThanOrEqual(7 - g.correct);
    }
    expect(g.correct).toBe(7);
    expect(g.finishedAt).not.toBeNull();
    expect(match(startGame(pool, { ...DEFAULT_SETTINGS, goal: "matches", amount: 1 }, 0, rng), APPEAR_MS).finishedAt).toBe(APPEAR_MS);
  });
  it("checks the round deadline on input and never spawns after the round ends", () => {
    const initial = startGame(pool, { ...DEFAULT_SETTINGS, amount: 10 }, 0, rng);
    expect(match(initial, 9999).correct).toBe(1);
    expect(match(initial, 10000).correct).toBe(0);
    const g = match(initial, 3000);
    const ended = tick(g, 40000, rng);
    expect(ended.finishedAt).toBe(10000);
    expect(ended.serial).toBe(g.serial);
    expect(tick(ended, 50000, rng)).toBe(ended);
  });
  it("recycles small decks without losing replacement timers or partners", () => {
    let g = startGame(pool.slice(0, 2), { ...DEFAULT_SETTINGS, amount: 1800 }, 0, rng);
    let now = APPEAR_MS;
    for (let i = 0; i < 19; i++) {
      g = match(match(g, now + 10), now + 20);
      now = Math.max(...g.refills);
      g = tick(g, now, rng);
      now += APPEAR_MS;
      expect(g.refills).toEqual([]);
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
    let g = startGame(mathPairs(kind), { ...DEFAULT_SETTINGS, amount: 1800 }, 0, rng);
    let now = APPEAR_MS;
    for (let i = 1; i < 40; i++) {
      const active = cards(g);
      expect(active.length).toBe(5);
      expect(new Set(active.map(c => c.pair.left)).size).toBe(active.length);
      expect(new Set(active.map(c => c.pair.right)).size).toBe(active.length);
      g = match(match(g, now + 10), now + 20);
      now = Math.max(...g.refills);
      g = tick(g, now, rng);
      now += APPEAR_MS;
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

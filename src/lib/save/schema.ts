/**
 * The save file: everything the app remembers, in one versioned JSON document. It lives in
 * localStorage today and is what people download and upload; a future backend should send and
 * receive this same shape, so bump `SAVE_VERSION` and add a step to `migrate` for any change.
 *
 * Results are kept as a history of finished sessions, and stats (bests, tricky words) are worked
 * out from it, so new stats never need a data migration.
 */

import { clampSettings, PRESETS, type DifficultyId, type DifficultySettings } from "../spelling/difficulty";
import { BUILT_IN_IDS } from "../spelling/builtInLists";
import { parseListFile, type SpellingList } from "../spelling/lists";
import type { MathSettings } from "../math/game";

export const SAVE_FORMAT = "level-up-learning.save";
export const SAVE_VERSION = 2;

export type MathSession = {
  id: string;
  finishedAt: string;
  settings: MathSettings;
  correct: number;
  attempts: number;
  points: number;
  bestCombo: number;
  seconds: number;
};

export type WordResult = {
  word: string;
  attempts: number;
  correct: number;
  /** Each wrong spelling and how many times it was given. */
  misspellings: Record<string, number>;
};

/** How one list in a session went, counting only that list's words. */
export type ListResult = {
  id: string;
  /** The list's name when played, so history still reads well if the list is deleted. */
  name: string;
  attempts: number;
  correct: number;
  stars: number;
  points: number;
};

export type SpellingSession = {
  id: string;
  /** ISO timestamp. */
  finishedAt: string;
  difficulty: DifficultyId;
  settings: DifficultySettings;
  attempts: number;
  correct: number;
  stars: number;
  points: number;
  lists: ListResult[];
  words: WordResult[];
};

export type Player = {
  id: string;
  name: string;
  createdAt: string;
  spelling: { sessions: SpellingSession[] };
  math: { sessions: MathSession[] };
};

export type SaveData = {
  format: typeof SAVE_FORMAT;
  version: number;
  /** ISO timestamp of the last change. */
  savedAt: string;
  activePlayerId: string | null;
  players: Player[];
  spelling: {
    /** Lists people uploaded or created (the built-in ones aren't saved). */
    lists: SpellingList[];
  };
};

export function newId(): string {
  if (typeof crypto !== "undefined" && "randomUUID" in crypto) return crypto.randomUUID();
  return `${Date.now().toString(36)}-${Math.random().toString(36).slice(2, 10)}`;
}

export function emptySave(now = new Date().toISOString()): SaveData {
  return {
    format: SAVE_FORMAT,
    version: SAVE_VERSION,
    savedAt: now,
    activePlayerId: null,
    players: [],
    spelling: { lists: [] },
  };
}

export class SaveFileError extends Error {}

// --- Reading untrusted data (uploads, storage): keep what's valid, drop what isn't. ---

type Obj = Record<string, unknown>;
const isObj = (v: unknown): v is Obj => !!v && typeof v === "object" && !Array.isArray(v);
const str = (v: unknown, fallback = "") => (typeof v === "string" ? v : fallback);
const count = (v: unknown) => (typeof v === "number" && Number.isFinite(v) && v >= 0 ? Math.floor(v) : 0);
const num = (v: unknown) => (typeof v === "number" && Number.isFinite(v) ? v : 0);
const date = (v: unknown, fallback: string) => (typeof v === "string" && !Number.isNaN(Date.parse(v)) ? v : fallback);

function readMisspellings(v: unknown): Record<string, number> {
  if (!isObj(v)) return {};
  return Object.fromEntries(Object.entries(v).filter(([, n]) => count(n) > 0).map(([k, n]) => [k, count(n)]));
}

function readSession(v: unknown, now: string): SpellingSession | null {
  if (!isObj(v)) return null;
  const difficulty = str(v.difficulty);
  return {
    id: str(v.id) || newId(),
    finishedAt: date(v.finishedAt, now),
    difficulty: difficulty === "custom" || difficulty in PRESETS ? (difficulty as DifficultyId) : "custom",
    settings: clampSettings({ ...PRESETS.easy.settings, ...(isObj(v.settings) ? v.settings : {}) } as DifficultySettings),
    attempts: count(v.attempts),
    correct: count(v.correct),
    stars: num(v.stars),
    points: num(v.points),
    lists: (Array.isArray(v.lists) ? v.lists : []).filter(isObj).map((l) => ({
      id: str(l.id),
      name: str(l.name, str(l.id)),
      attempts: count(l.attempts),
      correct: count(l.correct),
      stars: num(l.stars),
      points: num(l.points),
    })),
    words: (Array.isArray(v.words) ? v.words : [])
      .filter(isObj)
      .filter((w) => typeof w.word === "string" && w.word)
      .map((w) => ({
        word: w.word as string,
        attempts: count(w.attempts),
        correct: count(w.correct),
        misspellings: readMisspellings(w.misspellings),
      })),
  };
}

function readPlayer(v: unknown, now: string): Player | null {
  if (!isObj(v) || typeof v.name !== "string" || !v.name.trim()) return null;
  const spelling = isObj(v.spelling) ? v.spelling : {};
  const math = isObj(v.math) ? v.math : {};
  return {
    id: str(v.id) || newId(),
    name: v.name.trim(),
    createdAt: date(v.createdAt, now),
    spelling: {
      sessions: (Array.isArray(spelling.sessions) ? spelling.sessions : [])
        .map((s) => readSession(s, now))
        .filter((s): s is SpellingSession => !!s),
    },
    math: { sessions: (Array.isArray(math.sessions) ? math.sessions : [])
      .map((s) => readMathSession(s, now)).filter((s): s is MathSession => s !== null) },
  };
}

function readMathSession(v: unknown, now: string): MathSession | null {
  if (!isObj(v) || !isObj(v.settings)) return null;
  const { operation, digits, target } = v.settings;
  if ((operation !== "addition" && operation !== "multiplication") ||
    (digits !== 1 && digits !== 2 && digits !== 3) || (target !== 10 && target !== 30 && target !== 100)) return null;
  const correct = Math.min(target, count(v.correct));
  return { id: str(v.id) || newId(), finishedAt: date(v.finishedAt, now),
    settings: { operation, digits, target }, correct,
    attempts: Math.max(correct, count(v.attempts)), points: count(v.points),
    bestCombo: Math.min(correct, count(v.bestCombo)), seconds: count(v.seconds) };
}

/** Version 2 adds per-player math history; version 1 spelling data stays intact. */
function migrate(data: Obj): Obj {
  if (typeof data.version !== "number" || data.version < 2) {
    return { ...data, version: 2, players: (Array.isArray(data.players) ? data.players : [])
      .map((p) => isObj(p) ? { ...p, math: { sessions: [] } } : p) };
  }
  return data;
}

/** Reads a save file's text (an upload, or what's in storage). Throws `SaveFileError` if it isn't one. */
export function parseSaveFile(text: string, now = new Date().toISOString()): SaveData {
  let raw: unknown;
  try {
    raw = JSON.parse(text);
  } catch {
    throw new SaveFileError("That file isn't valid JSON.");
  }
  if (!isObj(raw) || raw.format !== SAVE_FORMAT) {
    throw new SaveFileError(
      Array.isArray(raw) || (isObj(raw) && (raw.format === "level-up-learning.spelling-lists" || "words" in raw))
        ? "That's a word list file. Upload it on the spelling word lists page instead."
        : "That isn't a Level Up Learning save file.",
    );
  }
  const version = typeof raw.version === "number" ? raw.version : 0;
  if (version > SAVE_VERSION) {
    throw new SaveFileError("That save file is from a newer version of the app. Reload the page and try again.");
  }

  const data = migrate(raw);
  const players = (Array.isArray(data.players) ? data.players : [])
    .map((p) => readPlayer(p, now))
    .filter((p): p is Player => !!p);
  // Duplicate ids would make players indistinguishable; give later copies new ones.
  const seen = new Set<string>();
  for (const p of players) {
    if (seen.has(p.id)) p.id = newId();
    seen.add(p.id);
  }

  let lists: SpellingList[] = [];
  const spelling = isObj(data.spelling) ? data.spelling : {};
  if (Array.isArray(spelling.lists) && spelling.lists.length) {
    try {
      // A hand-edited file could reuse a built-in id; those get renamed rather than shadowing it.
      lists = parseListFile(JSON.stringify(spelling.lists), BUILT_IN_IDS);
    } catch {
      lists = [];
    }
  }

  const activePlayerId = str(data.activePlayerId);
  return {
    format: SAVE_FORMAT,
    version: SAVE_VERSION,
    savedAt: date(data.savedAt, now),
    activePlayerId: players.some((p) => p.id === activePlayerId) ? activePlayerId : (players[0]?.id ?? null),
    players,
    spelling: { lists },
  };
}

export function serializeSave(save: SaveData): string {
  return JSON.stringify(save, null, 2) + "\n";
}

// --- Players ---

export function activePlayer(save: SaveData): Player | null {
  return save.players.find((p) => p.id === save.activePlayerId) ?? save.players[0] ?? null;
}

export function addPlayer(save: SaveData, name: string, now = new Date().toISOString()): SaveData {
  const player: Player = { id: newId(), name: name.trim() || "Player", createdAt: now, spelling: { sessions: [] }, math: { sessions: [] } };
  return { ...save, players: [...save.players, player], activePlayerId: player.id };
}

export function renamePlayer(save: SaveData, id: string, name: string): SaveData {
  const trimmed = name.trim();
  if (!trimmed) return save;
  return { ...save, players: save.players.map((p) => (p.id === id ? { ...p, name: trimmed } : p)) };
}

export function removePlayer(save: SaveData, id: string): SaveData {
  const players = save.players.filter((p) => p.id !== id);
  const activePlayerId = save.activePlayerId === id ? (players[0]?.id ?? null) : save.activePlayerId;
  return { ...save, players, activePlayerId };
}

export function setActivePlayer(save: SaveData, id: string): SaveData {
  return save.players.some((p) => p.id === id) ? { ...save, activePlayerId: id } : save;
}

/** Makes sure there's someone to save results to: the first time through, a "Player 1". */
export function ensurePlayer(save: SaveData): SaveData {
  return save.players.length ? save : addPlayer(save, "Player 1");
}

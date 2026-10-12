/**
 * Turning a finished spelling quiz into a saved session, and the stats worked out from a
 * player's sessions (the old app's per-list bests and per-word totals).
 */

import type { DifficultyId } from "../spelling/difficulty";
import { scoreMultiplier } from "../spelling/difficulty";
import type { SpellingList } from "../spelling/lists";
import { normalize, pointsFor, starsFor, summarize, type QuizState } from "../spelling/quiz";
import { newId, type ListResult, type Player, type SaveData, type SpellingSession, type WordResult } from "./schema";

/** Sessions kept per player. Older ones are dropped so storage stays well under browser limits. */
export const MAX_SESSIONS = 500;

export function buildSession(
  quiz: QuizState,
  lists: SpellingList[],
  difficulty: DifficultyId,
  now = new Date().toISOString(),
): SpellingSession {
  const multiplier = scoreMultiplier(quiz.settings);
  const summary = summarize(quiz, multiplier);
  const words: WordResult[] = quiz.words.map((w) => ({
    word: w.word,
    attempts: w.attempts,
    correct: w.correct,
    misspellings: w.misspellings,
  }));
  const byWord = new Map(words.map((w) => [normalize(w.word), w]));

  const listResults: ListResult[] = lists.map((list) => {
    const own = [...new Set(list.words.map((w) => normalize(w.word)))]
      .map((key) => byWord.get(key))
      .filter((w): w is WordResult => !!w);
    const attempts = own.reduce((n, w) => n + w.attempts, 0);
    const correct = own.reduce((n, w) => n + w.correct, 0);
    const accuracy = attempts ? correct / attempts : 0;
    return {
      id: list.id,
      name: list.name,
      attempts,
      correct,
      stars: starsFor(accuracy),
      points: pointsFor(accuracy, multiplier),
    };
  });

  return {
    id: newId(),
    finishedAt: now,
    difficulty,
    settings: quiz.settings,
    attempts: summary.attempts,
    correct: summary.correct,
    stars: summary.stars,
    points: summary.points,
    lists: listResults,
    words,
  };
}

/** Adds a session to a player's history (newest last), trimming the oldest past `MAX_SESSIONS`. */
export function recordSession(save: SaveData, playerId: string, session: SpellingSession): SaveData {
  return {
    ...save,
    players: save.players.map((p) =>
      p.id === playerId
        ? { ...p, spelling: { sessions: [...p.spelling.sessions, session].slice(-MAX_SESSIONS) } }
        : p,
    ),
  };
}

export type ListBest = { stars: number; points: number; plays: number; lastPlayed: string };

/** Best stars and points per list id, over every session that included the list. */
export function listBests(player: Player | null): Map<string, ListBest> {
  const bests = new Map<string, ListBest>();
  for (const session of player?.spelling.sessions ?? []) {
    for (const list of session.lists) {
      const prev = bests.get(list.id);
      bests.set(list.id, {
        stars: Math.max(prev?.stars ?? 0, list.stars),
        points: Math.max(prev?.points ?? 0, list.points),
        plays: (prev?.plays ?? 0) + 1,
        lastPlayed: session.finishedAt,
      });
    }
  }
  return bests;
}

export type WordStat = WordResult & { lastSeen: string };

/** Totals per word across every session, keyed by the normalized spelling. */
export function wordStats(player: Player | null): Map<string, WordStat> {
  const stats = new Map<string, WordStat>();
  for (const session of player?.spelling.sessions ?? []) {
    for (const w of session.words) {
      const key = normalize(w.word);
      const prev = stats.get(key);
      const misspellings = { ...(prev?.misspellings ?? {}) };
      for (const [spelling, n] of Object.entries(w.misspellings)) misspellings[spelling] = (misspellings[spelling] ?? 0) + n;
      stats.set(key, {
        word: w.word,
        attempts: (prev?.attempts ?? 0) + w.attempts,
        correct: (prev?.correct ?? 0) + w.correct,
        misspellings,
        lastSeen: session.finishedAt,
      });
    }
  }
  return stats;
}

/** Words missed most often relative to tries, worst first. Only words missed at least once. */
export function trickyWords(player: Player | null, limit = 10): WordStat[] {
  return [...wordStats(player).values()]
    .filter((w) => w.attempts > w.correct)
    .sort((a, b) => a.correct / a.attempts - b.correct / b.attempts || b.attempts - a.attempts)
    .slice(0, limit);
}

export function totals(player: Player | null) {
  const sessions = player?.spelling.sessions ?? [];
  return {
    sessions: sessions.length,
    points: sessions.reduce((n, s) => n + s.points, 0),
    words: wordStats(player).size,
  };
}

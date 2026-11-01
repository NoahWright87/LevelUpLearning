import { newId, type MatchingSession, type Player, type SaveData } from "../save/schema";
import { elapsed, type Game } from "./game";

export const MAX_MATCHING_SESSIONS = 500;

export function buildMatchingSession(game: Game, source: string, finishedAt = new Date().toISOString()): MatchingSession {
  if (game.finishedAt === null) throw new Error("Only finished matching rounds can be saved.");
  return { id: newId(), finishedAt, source, settings: { ...game.settings }, correct: game.correct,
    mistakes: game.mistakes, seconds: elapsed(game, game.finishedAt) };
}

export function recordMatchingSession(save: SaveData, playerId: string, session: MatchingSession): SaveData {
  return { ...save, players: save.players.map(p => p.id === playerId
    ? { ...p, matching: { sessions: [...p.matching.sessions.filter(s => s.id !== session.id), session].slice(-MAX_MATCHING_SESSIONS) } } : p) };
}

export function matchingTotals(player: Player) {
  return player.matching.sessions.reduce((sum, s) => ({ rounds: sum.rounds + 1, correct: sum.correct + s.correct,
    mistakes: sum.mistakes + s.mistakes }), { rounds: 0, correct: 0, mistakes: 0 });
}

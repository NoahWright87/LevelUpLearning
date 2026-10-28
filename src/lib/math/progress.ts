import { newId, type MathSession, type Player, type SaveData } from "../save/schema";
import { gameStats, type MathGame } from "./game";

export const MAX_MATH_SESSIONS = 500;

export function buildMathSession(game: MathGame, finishedAt = new Date().toISOString()): MathSession {
  if (game.finishedAt === null) throw new Error("Only finished math rounds can be saved.");
  return { id: newId(), finishedAt, settings: { ...game.settings }, correct: game.correct,
    attempts: game.attempts, points: game.points, bestCombo: game.bestCombo, seconds: gameStats(game).seconds };
}

export function recordMathSession(save: SaveData, playerId: string, session: MathSession): SaveData {
  return { ...save, players: save.players.map((p) => p.id === playerId
    ? { ...p, math: { sessions: [...p.math.sessions.filter((s) => s.id !== session.id), session].slice(-MAX_MATH_SESSIONS) } } : p) };
}

export function mathTotals(player: Player) {
  const sessions = player.math.sessions;
  return { rounds: sessions.length, correct: sessions.reduce((n, s) => n + s.correct, 0),
    points: sessions.reduce((n, s) => n + s.points, 0) };
}

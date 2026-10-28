/** UI-free math sprint rules. Times are monotonic milliseconds supplied by the caller. */
export const COMBO_MS = 5_000;
export const MAX_HEARTS = 3;
export const ROUND_TARGETS = [10, 30, 100] as const;
export type MathSettings = {
  operation: "addition" | "multiplication";
  digits: 1 | 2 | 3;
  target: (typeof ROUND_TARGETS)[number];
};
export const DEFAULT_SETTINGS: MathSettings = { operation: "addition", digits: 1, target: 30 };
export const operationLabel = (operation: MathSettings["operation"]) => operation === "addition" ? "Addition" : "Multiplication";
export const symbol = (operation: MathSettings["operation"]) => operation === "addition" ? "+" : "×";
export type Problem = { left: number; right: number };
export type Feedback = { correct: boolean; problem: Problem; expected: number; points: number };
export type MathGame = {
  settings: MathSettings;
  problem: Problem;
  correct: number;
  attempts: number;
  hearts: number;
  points: number;
  combo: number;
  bestCombo: number;
  comboUntil: number | null;
  startedAt: number;
  finishedAt: number | null;
  feedback: Feedback | null;
  /** A miss stays on screen until the learner chooses Next. */
  awaitingNext: boolean;
};

/** Both operands have the chosen digit count; one-digit practice includes zero. */
export function makeProblem(settings: MathSettings, random = Math.random, previous?: Problem): Problem {
  const min = settings.digits === 1 ? 0 : 10 ** (settings.digits - 1);
  const count = 10 ** settings.digits - min;
  const pick = () => min + Math.floor(Math.max(0, Math.min(1 - Number.EPSILON, random())) * count);
  const problem = { left: pick(), right: pick() };
  // Avoid an immediate repeat (including commuted pairs), without retry loops.
  if (previous && ((problem.left === previous.left && problem.right === previous.right)
    || (problem.left === previous.right && problem.right === previous.left))) {
    problem.right = min + (problem.right - min + 1) % count;
  }
  return problem;
}

export function expectedAnswer(problem: Problem, operation: MathSettings["operation"]): number {
  return operation === "addition" ? problem.left + problem.right : problem.left * problem.right;
}

export function startGame(settings: MathSettings, now: number, random = Math.random): MathGame {
  return { settings, problem: makeProblem(settings, random), correct: 0, attempts: 0,
    hearts: MAX_HEARTS, points: 0, combo: 0, bestCombo: 0, comboUntil: null,
    startedAt: now, finishedAt: null, feedback: null, awaitingNext: false };
}

export function comboRemaining(game: MathGame, now: number): number {
  return game.comboUntil === null ? 0 : Math.max(0, Math.min(1, (game.comboUntil - now) / COMBO_MS));
}

export function expireCombo(game: MathGame, now: number): MathGame {
  return game.finishedAt === null && game.comboUntil !== null && now >= game.comboUntil
    ? { ...game, combo: 0, comboUntil: null } : game;
}

export function submitAnswer(game: MathGame, answer: string, now: number, random = Math.random): MathGame {
  // Blank, fractional, exponent and otherwise malformed input never costs a heart.
  if (game.finishedAt !== null || game.awaitingNext || !/^\d+$/.test(answer.trim())) return game;
  const value = Number(answer.trim());
  if (!Number.isSafeInteger(value)) return game;
  const current = expireCombo(game, now);
  const expected = expectedAnswer(current.problem, current.settings.operation);
  const right = value === expected;
  const combo = right ? current.combo + 1 : 0;
  const correct = current.correct + Number(right);
  const hearts = current.hearts - Number(!right);
  const points = right ? 10 * combo : 0;
  const done = correct >= current.settings.target || hearts === 0;
  return { ...current, correct, hearts, combo, attempts: current.attempts + 1,
    points: current.points + points, bestCombo: Math.max(current.bestCombo, combo),
    comboUntil: right && !done ? now + COMBO_MS : null,
    finishedAt: done ? now : null, awaitingNext: !right && !done,
    feedback: { correct: right, problem: current.problem, expected, points },
    problem: right && !done ? makeProblem(current.settings, random, current.problem) : current.problem };
}

export function nextProblem(game: MathGame, random = Math.random): MathGame {
  if (!game.awaitingNext || game.finishedAt !== null) return game;
  return { ...game, awaitingNext: false, feedback: null, problem: makeProblem(game.settings, random, game.problem) };
}

export function gameStats(game: MathGame) {
  return { accuracy: game.attempts ? Math.round(game.correct / game.attempts * 100) : 0,
    seconds: Math.round(((game.finishedAt ?? game.startedAt) - game.startedAt) / 1000),
    won: game.correct >= game.settings.target };
}

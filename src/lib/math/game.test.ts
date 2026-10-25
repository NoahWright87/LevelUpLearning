import { describe, expect, it } from "vitest";
import { DEFAULT_SETTINGS, comboRemaining, expectedAnswer, expireCombo, gameStats, makeProblem, nextProblem, startGame, submitAnswer } from "./game";

const rng = () => 0.45;
const start = () => startGame(DEFAULT_SETTINGS, 0, rng);
const right = (game: ReturnType<typeof start>, time: number) => submitAnswer(game, String(expectedAnswer(game.problem, game.settings.operation)), time, rng);

describe("math sprint", () => {
  it("generates addition and multiplication within each operand's digit range", () => {
    for (const operation of ["addition", "multiplication"] as const) {
      for (const digits of [1, 2, 3] as const) {
        const settings = { ...DEFAULT_SETTINGS, operation, digits };
        const low = makeProblem(settings, () => 0);
        const high = makeProblem(settings, () => 1);
        expect(low.left).toBe(digits === 1 ? 0 : 10 ** (digits - 1));
        expect(high.right).toBe(10 ** digits - 1);
        expect(expectedAnswer(high, operation)).toBe(operation === "addition" ? high.left + high.right : high.left * high.right);
      }
    }
  });

  it("avoids immediate repeated and commuted problems even with a constant RNG", () => {
    const previous = { left: 4, right: 4 };
    expect(makeProblem(DEFAULT_SETTINGS, rng, previous)).not.toEqual(previous);
    expect(makeProblem(DEFAULT_SETTINGS, (() => { let n = 0; return () => n++ ? 0.21 : 0.41; })(), { left: 2, right: 4 })).toEqual({ left: 4, right: 3 });
  });

  it("refills five seconds, rewards combo, and expires exactly at the deadline", () => {
    let game = right(start(), 1000);
    expect(game).toMatchObject({ combo: 1, points: 10, comboUntil: 6000 });
    expect(comboRemaining(game, 3500)).toBe(0.5);
    game = right(game, 5999);
    expect(game).toMatchObject({ combo: 2, points: 30, comboUntil: 10999 });
    // The submit path checks time itself, even if a UI timer was suspended.
    game = right(game, 10999);
    expect(game).toMatchObject({ combo: 1, bestCombo: 2, points: 40 });
    expect(expireCombo(game, 50000)).toMatchObject({ combo: 0, hearts: 3, comboUntil: null });
  });

  it("ignores invalid input without consuming a turn or heart", () => {
    const game = start();
    for (const answer of ["", " ", "1e1", "2.5", "abc", "-1", "9007199254740992"]) {
      expect(submitAnswer(game, answer, 1)).toBe(game);
    }
    expect(submitAnswer(startGame(DEFAULT_SETTINGS, 0, () => 0), "0", 1).correct).toBe(1);
  });

  it("shows corrections, blocks repeat submissions and ends on the third miss", () => {
    let game = right(start(), 10);
    for (let n = 1; n <= 3; n++) {
      game = submitAnswer(game, "999999", n * 100, rng);
      expect(game).toMatchObject({ hearts: 3 - n, combo: 0, correct: 1, points: 10 });
      expect(game.feedback).toMatchObject({ correct: false, expected: expectedAnswer(game.problem, game.settings.operation) });
      expect(submitAnswer(game, "999999", n * 100 + 1)).toBe(game);
      game = nextProblem(game, rng);
    }
    expect(game.finishedAt).toBe(300);
    expect(right(game, 301)).toBe(game);
    expect(gameStats(game).won).toBe(false);
  });

  it("finishes at each round target and freezes the score", () => {
    for (const target of [10, 30, 100] as const) {
      let game = startGame({ ...DEFAULT_SETTINGS, target }, 0, rng);
      for (let n = 1; n <= target; n++) game = right(game, n * 1000);
      expect(game).toMatchObject({ correct: target, bestCombo: target, points: 10 * target * (target + 1) / 2, finishedAt: target * 1000 });
      expect(gameStats(game)).toEqual({ won: true, accuracy: 100, seconds: target });
      expect(right(game, 200000)).toBe(game);
      expect(nextProblem(game)).toBe(game);
    }
  });
});

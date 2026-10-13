import { describe, expect, it } from "vitest";
import { ALL_LETTERS, PRESETS, hintCount, scoreMultiplier, type DifficultySettings } from "./difficulty";
import {
  buildHint,
  isCorrectSpelling,
  isDone,
  nextWord,
  progress,
  startQuiz,
  submitAnswer,
  summarize,
  type QuizState,
} from "./quiz";

/** Deterministic random numbers for repeatable shuffles. */
function seeded(seed = 1) {
  let s = seed;
  return () => {
    s = (s * 16807) % 2147483647;
    return (s - 1) / 2147483646;
  };
}

const WORDS = [
  { word: "cake", sentence: "Blow out the candles on your %% cake." },
  { word: "rain", sentence: "" },
  { word: "isn't", sentence: "Spelling %% hard." },
];

const settings = (patch: Partial<DifficultySettings> = {}): DifficultySettings => ({
  ...PRESETS.easy.settings,
  ...patch,
});

/** Answers whatever is asked, right or wrong, then moves on. */
function answer(state: QuizState, right: boolean, rng = seeded()) {
  const word = state.words[state.current!].word;
  const { state: after } = submitAnswer(state, right ? word : "zzz");
  return nextWord(after, rng);
}

describe("isCorrectSpelling", () => {
  it("ignores case, outer spaces and curly apostrophes", () => {
    expect(isCorrectSpelling("isn't", "  ISN’T ")).toBe(true);
    expect(isCorrectSpelling("cake", "kake")).toBe(false);
  });
});

describe("buildHint", () => {
  it("shows the requested number of letters and always shows punctuation", () => {
    const hint = buildHint("isn't", 2, true, seeded())!;
    expect(hint).toHaveLength(5);
    expect(hint[3]).toEqual({ char: "'", fixed: true });
    expect(hint.filter((c) => !c.fixed && c.char)).toHaveLength(2);
  });

  it("is null with no letters and no blanks, all blanks with blanks on", () => {
    expect(buildHint("cake", 0, false, seeded())).toBeNull();
    expect(buildHint("cake", 0, true, seeded())!.every((c) => c.char === null)).toBe(true);
  });
});

describe("hintCount", () => {
  it("drops letters as the streak grows and caps at the word length", () => {
    const s = settings({ hintLetters: 4, hintLettersDrop: 1 });
    expect(hintCount(s, 3, 0)).toBe(3);
    expect(hintCount(s, 6, 0)).toBe(4);
    expect(hintCount(s, 6, 2)).toBe(2);
    expect(hintCount(s, 6, 9)).toBe(0);
    expect(hintCount(settings({ hintLetters: ALL_LETTERS, hintLettersDrop: 0 }), 6, 3)).toBe(6);
  });
});

describe("quiz", () => {
  it("asks each word until it's been spelled right timesToFinish times", () => {
    const rng = seeded(7);
    let state = startQuiz(WORDS, settings({ timesToFinish: 2 }), rng);
    expect(progress(state)).toEqual({ done: 0, total: 6 });
    let turns = 0;
    while (!isDone(state)) {
      state = answer(state, true, rng);
      turns++;
    }
    expect(turns).toBe(6);
    expect(state.words.every((w) => w.correct === 2 && w.attempts === 2)).toBe(true);
  });

  it("keeps asking a missed word and records the misspelling", () => {
    const rng = seeded(3);
    let state = startQuiz([WORDS[0]], settings({ timesToFinish: 1 }), rng);
    state = answer(state, false, rng);
    expect(isDone(state)).toBe(false);
    expect(state.words[0].misspellings).toEqual({ zzz: 1 });
    state = answer(state, true, rng);
    expect(isDone(state)).toBe(true);
    expect(summarize(state, 0.5)).toEqual({ attempts: 2, correct: 1, accuracy: 0.5, stars: 2.5, points: 250 });
  });

  it("knocks the streak back by the penalty on a miss", () => {
    const rng = seeded(5);
    let state = startQuiz([WORDS[0]], settings({ timesToFinish: 5, streakPenalty: 1 }), rng);
    state = answer(state, true, rng);
    state = answer(state, true, rng);
    expect(state.words[0].streak).toBe(2);
    state = answer(state, false, rng);
    expect(state.words[0].streak).toBe(1);
  });

  it("drops duplicate words across lists", () => {
    const state = startQuiz([...WORDS, { word: "Cake", sentence: "" }], settings(), seeded());
    expect(state.words).toHaveLength(3);
  });

  it("doesn't repeat a word straight away when a new round starts", () => {
    const rng = seeded(11);
    let state = startQuiz(WORDS, settings({ timesToFinish: 3 }), rng);
    let previous = state.current;
    while (!isDone(state)) {
      state = answer(state, true, rng);
      if (state.current !== null && state.words.filter((w) => w.correct < 3).length > 1) {
        expect(state.current).not.toBe(previous);
      }
      previous = state.current;
    }
  });
});

describe("scoreMultiplier", () => {
  it("is positive for every preset and rewards less help", () => {
    const values = Object.values(PRESETS).map((p) => scoreMultiplier(p.settings));
    expect(values.every((v) => v > 0)).toBe(true);
    expect(values).toEqual([...values].sort((a, b) => a - b));
  });
});

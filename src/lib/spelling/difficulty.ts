/**
 * Quiz difficulty: how much of the word is given away, and how many correct answers finish it.
 * The presets and score multiplier come from the old desktop app (archive/KidQuiz,
 * `SpellingQuizSettings`), with its bugs fixed (see `scoreMultiplier`).
 */

/** Hint letters at or above this means "show every letter". */
export const ALL_LETTERS = 99;
/** Hint letters beyond this don't lower the score any further. */
const MAX_SCORED_HINTS = 10;

export type DifficultySettings = {
  /** Show a blank for every letter even when no letters are given away. */
  showBlanks: boolean;
  /** Letters given away on a word you haven't answered right yet. `ALL_LETTERS` shows them all. */
  hintLetters: number;
  /** How many fewer letters are given away for each correct answer in your current streak. */
  hintLettersDrop: number;
  /** Correct answers needed on each word to finish the quiz. */
  timesToFinish: number;
  /** How far a wrong answer knocks a word's streak back, bringing hints back with it. */
  streakPenalty: number;
};

export type PresetId = "intro" | "easy" | "medium" | "hard";
export type DifficultyId = PresetId | "custom";

export const PRESETS: Record<PresetId, { label: string; hint: string; settings: DifficultySettings }> = {
  intro: {
    label: "Intro",
    hint: "See the whole word, spell it twice",
    settings: { showBlanks: true, hintLetters: ALL_LETTERS, hintLettersDrop: 0, timesToFinish: 2, streakPenalty: 0 },
  },
  easy: {
    label: "Easy",
    hint: "A few letters shown, fewer as you get it right",
    settings: { showBlanks: true, hintLetters: 4, hintLettersDrop: 1, timesToFinish: 3, streakPenalty: 0 },
  },
  medium: {
    label: "Medium",
    hint: "Fewer letters, and mistakes bring them back",
    settings: { showBlanks: true, hintLetters: 3, hintLettersDrop: 2, timesToFinish: 3, streakPenalty: 1 },
  },
  hard: {
    label: "Hard",
    hint: "No letters, no blanks: just listen",
    settings: { showBlanks: false, hintLetters: 0, hintLettersDrop: 0, timesToFinish: 3, streakPenalty: 2 },
  },
};

export const PRESET_IDS = Object.keys(PRESETS) as PresetId[];

/** Ranges for the custom controls. */
export const LIMITS = {
  hintLetters: { min: 0, max: 10 },
  hintLettersDrop: { min: 0, max: 3 },
  timesToFinish: { min: 1, max: 5 },
  streakPenalty: { min: 0, max: 3 },
} as const;

export function settingsFor(id: DifficultyId, custom: DifficultySettings): DifficultySettings {
  return id === "custom" ? custom : PRESETS[id].settings;
}

/** How many letters to give away for a word with this streak. */
export function hintCount(settings: DifficultySettings, wordLength: number, streak: number): number {
  const start = Math.min(wordLength, settings.hintLetters);
  return Math.max(0, start - streak * settings.hintLettersDrop);
}

/**
 * Scales the score for how hard the settings are: more help means a lower multiplier.
 * The old app's formula, except that "all letters" counts as `MAX_SCORED_HINTS` (it used
 * int.MaxValue, which made Intro's multiplier hugely negative) and the hints factor can't
 * fall below zero.
 */
export function scoreMultiplier(s: DifficultySettings): number {
  const hints = Math.min(s.hintLetters, MAX_SCORED_HINTS);
  const blanks = s.showBlanks ? 0.75 : 1;
  const hinting = Math.max(0, Math.min(1, 1 - 0.05 * hints + 0.025 * s.hintLettersDrop));
  const repetition = 0.7 + s.timesToFinish * 0.1;
  const penalty = 1 + s.streakPenalty * 0.05;
  return blanks * hinting * repetition * penalty;
}

export function clampSettings(s: DifficultySettings): DifficultySettings {
  const clamp = (v: number, { min, max }: { min: number; max: number }) =>
    Math.min(max, Math.max(min, Math.round(Number.isFinite(v) ? v : min)));
  return {
    showBlanks: !!s.showBlanks,
    hintLetters: s.hintLetters >= ALL_LETTERS ? ALL_LETTERS : clamp(s.hintLetters, LIMITS.hintLetters),
    hintLettersDrop: clamp(s.hintLettersDrop, LIMITS.hintLettersDrop),
    timesToFinish: clamp(s.timesToFinish, LIMITS.timesToFinish),
    streakPenalty: clamp(s.streakPenalty, LIMITS.streakPenalty),
  };
}

/**
 * The spelling quiz itself, as pure functions over a plain state object so it can be tested
 * without a browser. Words come up in shuffled rounds until each one has been spelled right
 * `timesToFinish` times.
 */

import { hintCount, type DifficultySettings } from "./difficulty";
import { PLACEHOLDER, type SpellingWord } from "./lists";

export type Rng = () => number;

export type WordProgress = SpellingWord & {
  attempts: number;
  correct: number;
  /** Correct answers in a row, knocked back by `streakPenalty` on a miss. Drives the hints. */
  streak: number;
  /** Each wrong spelling and how many times it was given. */
  misspellings: Record<string, number>;
};

/** One letter slot of the hint. `char` is null for a blank. */
export type HintCell = { char: string | null; fixed: boolean };

export type AnswerResult = { correct: boolean; expected: string; given: string };

export type QuizState = {
  settings: DifficultySettings;
  words: WordProgress[];
  /** Indexes of words still to come this round. */
  deck: number[];
  /** Index of the word being asked, or null once the quiz is done. */
  current: number | null;
  /** Hint for the current word, or null when nothing is shown. */
  hint: HintCell[] | null;
};

export function shuffle<T>(items: readonly T[], rng: Rng): T[] {
  const out = [...items];
  for (let i = out.length - 1; i > 0; i--) {
    const j = Math.floor(rng() * (i + 1));
    [out[i], out[j]] = [out[j], out[i]];
  }
  return out;
}

/** Lowercased, trimmed, with curly apostrophes straightened, for comparing answers. */
export function normalize(text: string): string {
  return text.trim().replace(/[‘’ʼ]/g, "'").replace(/\s+/g, " ").toLowerCase();
}

export function isCorrectSpelling(expected: string, given: string): boolean {
  return normalize(expected) === normalize(given);
}

/** Letters are hidden or shown; anything else (apostrophes, hyphens, spaces) is always shown. */
const isLetter = (ch: string) => /\p{L}/u.test(ch);

/** The hint for a word: `count` random letters shown, the rest blank. Null when there's nothing to show. */
export function buildHint(word: string, count: number, showBlanks: boolean, rng: Rng): HintCell[] | null {
  if (count <= 0 && !showBlanks) return null;
  const chars = [...word];
  const letterIndexes = chars.flatMap((ch, i) => (isLetter(ch) ? [i] : []));
  const shown = new Set(shuffle(letterIndexes, rng).slice(0, count));
  return chars.map((ch, i) =>
    isLetter(ch) ? { char: shown.has(i) ? ch : null, fixed: false } : { char: ch, fixed: true },
  );
}

/** Words to quiz on, one entry per distinct spelling (the first sentence wins). */
export function uniqueWords(words: SpellingWord[]): SpellingWord[] {
  const seen = new Set<string>();
  return words.filter(({ word }) => {
    const key = normalize(word);
    if (!key || seen.has(key)) return false;
    seen.add(key);
    return true;
  });
}

const isFinished = (w: WordProgress, s: DifficultySettings) => w.correct >= s.timesToFinish;

function draw(state: QuizState, rng: Rng, previous: number | null): QuizState {
  const open = state.words.flatMap((w, i) => (isFinished(w, state.settings) ? [] : [i]));
  if (open.length === 0) return { ...state, deck: [], current: null, hint: null };

  let deck = state.deck.filter((i) => open.includes(i));
  if (deck.length === 0) {
    deck = shuffle(open, rng);
    // Don't ask the same word twice in a row across rounds when there's another to ask.
    if (deck.length > 1 && deck[0] === previous) [deck[0], deck[1]] = [deck[1], deck[0]];
  }

  const [current, ...rest] = deck;
  const word = state.words[current];
  const count = hintCount(state.settings, [...word.word].filter(isLetter).length, word.streak);
  return { ...state, deck: rest, current, hint: buildHint(word.word, count, state.settings.showBlanks, rng) };
}

export function startQuiz(words: SpellingWord[], settings: DifficultySettings, rng: Rng = Math.random): QuizState {
  const progress = uniqueWords(words).map((w) => ({ ...w, attempts: 0, correct: 0, streak: 0, misspellings: {} }));
  return draw({ settings, words: progress, deck: [], current: null, hint: null }, rng, null);
}

/** Records an answer to the current word. The current word stays up until `nextWord`. */
export function submitAnswer(state: QuizState, answer: string): { state: QuizState; result: AnswerResult } {
  if (state.current === null) throw new Error("The quiz is already finished.");
  const word = state.words[state.current];
  const given = answer.trim();
  const correct = isCorrectSpelling(word.word, given);

  const updated: WordProgress = correct
    ? { ...word, attempts: word.attempts + 1, correct: word.correct + 1, streak: word.streak + 1 }
    : {
        ...word,
        attempts: word.attempts + 1,
        streak: Math.max(0, word.streak - state.settings.streakPenalty),
        misspellings: { ...word.misspellings, [normalize(given)]: (word.misspellings[normalize(given)] ?? 0) + 1 },
      };

  const words = state.words.map((w, i) => (i === state.current ? updated : w));
  return { state: { ...state, words }, result: { correct, expected: word.word, given } };
}

export function nextWord(state: QuizState, rng: Rng = Math.random): QuizState {
  return draw(state, rng, state.current);
}

export function isDone(state: QuizState): boolean {
  return state.current === null;
}

/** Correct answers so far out of the total needed, capped per word. */
export function progress(state: QuizState): { done: number; total: number } {
  const target = state.settings.timesToFinish;
  return {
    done: state.words.reduce((n, w) => n + Math.min(w.correct, target), 0),
    total: state.words.length * target,
  };
}

export type QuizSummary = {
  attempts: number;
  correct: number;
  /** Share of answers that were right, 0–1. */
  accuracy: number;
  /** 0–5 in half steps, from accuracy alone so an easy round done well still feels like a win. */
  stars: number;
  /** Points: accuracy scaled by the difficulty multiplier, so harder settings score more. */
  points: number;
};

export function summarize(state: QuizState, multiplier: number): QuizSummary {
  const attempts = state.words.reduce((n, w) => n + w.attempts, 0);
  const correct = state.words.reduce((n, w) => n + w.correct, 0);
  const accuracy = attempts ? correct / attempts : 0;
  const stars = Math.round(accuracy * 5 * 2) / 2;
  const points = Math.round(accuracy * multiplier * 1000);
  return { attempts, correct, accuracy, stars, points };
}

/** Fills a sentence's placeholder with a blank (for reading) or with the word (for speaking). */
export function fillSentence(sentence: string, fill: string): string {
  return sentence.split(PLACEHOLDER).join(fill);
}

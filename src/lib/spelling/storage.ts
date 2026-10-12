/**
 * This device's last spelling quiz setup (lists, difficulty, voice): a convenience, so it lives
 * in its own localStorage key rather than the save file. Lists and results are in the save file
 * (src/lib/save). Every read and write tolerates storage being missing, full, or blocked.
 */

import { clampSettings, PRESETS, type DifficultyId, type DifficultySettings } from "./difficulty";

const SETUP_KEY = "lul.spelling.setup.v1";

function read(key: string): string | null {
  try {
    return window.localStorage.getItem(key);
  } catch {
    return null;
  }
}

function write(key: string, value: string): void {
  try {
    window.localStorage.setItem(key, value);
  } catch {
    // A forgotten setup is harmless.
  }
}

export type QuizSetup = {
  listIds: string[];
  difficulty: DifficultyId;
  custom: DifficultySettings;
  voiceURI?: string;
};

export const DEFAULT_SETUP: QuizSetup = {
  listIds: [],
  difficulty: "easy",
  custom: PRESETS.easy.settings,
};

export function loadSetup(): QuizSetup {
  const raw = read(SETUP_KEY);
  if (!raw) return DEFAULT_SETUP;
  try {
    const data = JSON.parse(raw) as Partial<QuizSetup>;
    const difficulty: DifficultyId =
      data.difficulty === "custom" || (data.difficulty && data.difficulty in PRESETS) ? data.difficulty : "easy";
    return {
      listIds: Array.isArray(data.listIds) ? data.listIds.filter((id) => typeof id === "string") : [],
      difficulty,
      custom: data.custom ? clampSettings({ ...DEFAULT_SETUP.custom, ...data.custom }) : DEFAULT_SETUP.custom,
      voiceURI: typeof data.voiceURI === "string" ? data.voiceURI : undefined,
    };
  } catch {
    return DEFAULT_SETUP;
  }
}

export function saveSetup(setup: QuizSetup): void {
  write(SETUP_KEY, JSON.stringify(setup));
}

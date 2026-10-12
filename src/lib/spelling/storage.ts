/**
 * Browser storage for the spelling quiz: the lists people add, and the last quiz setup.
 * Everything lives in localStorage for now (see MEMORY.md, "where progress is stored").
 * Every read and write tolerates storage being missing, full, or blocked.
 */

import { clampSettings, PRESETS, type DifficultyId, type DifficultySettings } from "./difficulty";
import { parseListFile, serializeLists, type SpellingList } from "./lists";

const LISTS_KEY = "lul.spelling.lists.v1";
const SETUP_KEY = "lul.spelling.setup.v1";
const CHANGE_EVENT = "lul:spelling-lists";

function read(key: string): string | null {
  try {
    return window.localStorage.getItem(key);
  } catch {
    return null;
  }
}

function write(key: string, value: string): boolean {
  try {
    window.localStorage.setItem(key, value);
    return true;
  } catch {
    return false;
  }
}

/** Lists people uploaded or created. Corrupt storage reads as no lists rather than an error. */
export function loadCustomLists(): SpellingList[] {
  const raw = read(LISTS_KEY);
  if (!raw) return [];
  try {
    return parseListFile(raw);
  } catch {
    return [];
  }
}

/** Returns false when the browser refused to store them (private mode, storage full). */
export function saveCustomLists(lists: SpellingList[]): boolean {
  const ok = write(LISTS_KEY, serializeLists(lists));
  window.dispatchEvent(new Event(CHANGE_EVENT));
  return ok;
}

/** Calls `onChange` when the custom lists change, in this tab or another. */
export function subscribeCustomLists(onChange: () => void): () => void {
  const onStorage = (e: StorageEvent) => {
    if (e.key === LISTS_KEY) onChange();
  };
  window.addEventListener(CHANGE_EVENT, onChange);
  window.addEventListener("storage", onStorage);
  return () => {
    window.removeEventListener(CHANGE_EVENT, onChange);
    window.removeEventListener("storage", onStorage);
  };
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

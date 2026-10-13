/**
 * The save file in localStorage, as a tiny external store for `useSyncExternalStore`.
 * Every change goes through `updateSave`, which writes it and tells subscribers (this tab via
 * an event, other tabs via the storage event). Storage being missing, full or blocked never
 * throws: the app keeps working for the visit and `lastWriteFailed` says so.
 */

import { emptySave, parseSaveFile, serializeSave, type SaveData } from "./schema";
import { parseListFile } from "../spelling/lists";
import { BUILT_IN_IDS } from "../spelling/builtInLists";

const SAVE_KEY = "lul.save.v1";
/** Where the first spelling release kept uploaded lists, before the save file existed. */
const LEGACY_LISTS_KEY = "lul.spelling.lists.v1";
const CHANGE_EVENT = "lul:save";

/** What the server render and the first client render see, before storage is read. */
export const SERVER_SAVE: SaveData = emptySave("1970-01-01T00:00:00.000Z");

let cached: SaveData | null = null;
let failed = false;

function read(key: string): string | null {
  try {
    return window.localStorage.getItem(key);
  } catch {
    return null;
  }
}

function load(): SaveData {
  const raw = read(SAVE_KEY);
  if (raw) {
    try {
      return parseSaveFile(raw);
    } catch {
      // Unreadable storage shouldn't lock anyone out; start fresh rather than crash.
    }
  }
  const save = emptySave();
  const legacy = read(LEGACY_LISTS_KEY);
  if (legacy) {
    try {
      save.spelling.lists = parseListFile(legacy, BUILT_IN_IDS);
    } catch {
      // Nothing worth keeping.
    }
  }
  return save;
}

export function getSave(): SaveData {
  if (typeof window === "undefined") return SERVER_SAVE;
  if (!cached) cached = load();
  return cached;
}

export function getServerSave(): SaveData {
  return SERVER_SAVE;
}

/** True when the last write didn't stick (private browsing, storage full). */
export function lastWriteFailed(): boolean {
  return failed;
}

function commit(next: SaveData): SaveData {
  cached = next;
  try {
    window.localStorage.setItem(SAVE_KEY, serializeSave(next));
    window.localStorage.removeItem(LEGACY_LISTS_KEY);
    failed = false;
  } catch {
    failed = true;
  }
  window.dispatchEvent(new Event(CHANGE_EVENT));
  return next;
}

/** Applies a change and saves it. Returns false if the browser wouldn't store it. */
export function updateSave(change: (save: SaveData) => SaveData): boolean {
  const next = change(getSave());
  commit({ ...next, savedAt: new Date().toISOString() });
  return !failed;
}

/** Replaces everything with an uploaded save file (keeping its own `savedAt`). */
export function replaceSave(save: SaveData): boolean {
  commit(save);
  return !failed;
}

export function subscribeSave(onChange: () => void): () => void {
  const onStorage = (e: StorageEvent) => {
    if (e.key === SAVE_KEY || e.key === null) {
      cached = null;
      onChange();
    }
  };
  window.addEventListener(CHANGE_EVENT, onChange);
  window.addEventListener("storage", onStorage);
  return () => {
    window.removeEventListener(CHANGE_EVENT, onChange);
    window.removeEventListener("storage", onStorage);
  };
}

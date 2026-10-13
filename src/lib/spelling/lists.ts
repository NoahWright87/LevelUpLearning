/**
 * Spelling word lists: the shape, and reading/writing the JSON files people upload and download.
 *
 * A sentence marks where its word goes with `%%`, e.g. "Blow out the candles on your %% cake."
 * The format is the one the built-in lists use (src/data/spellingLists.json).
 */

export const PLACEHOLDER = "%%";
export const LIST_FILE_FORMAT = "level-up-learning.spelling-lists";
export const LIST_FILE_VERSION = 1;

export type SpellingWord = {
  word: string;
  /** Example sentence with `%%` where the word goes. May be empty. */
  sentence: string;
};

export type SpellingList = {
  id: string;
  name: string;
  /** School grade the list is aimed at, when known. */
  grade?: number;
  words: SpellingWord[];
};

export type SpellingListFile = {
  format: typeof LIST_FILE_FORMAT;
  version: number;
  lists: SpellingList[];
};

export function slugify(text: string): string {
  return text.toLowerCase().replace(/[^a-z0-9]+/g, "-").replace(/^-|-$/g, "") || "list";
}

/** An id based on `name` that isn't in `taken`. */
export function uniqueId(name: string, taken: Iterable<string>): string {
  const used = new Set(taken);
  const base = slugify(name);
  if (!used.has(base)) return base;
  for (let n = 2; ; n++) {
    if (!used.has(`${base}-${n}`)) return `${base}-${n}`;
  }
}

/**
 * Puts the placeholder in a sentence that spells its word out instead
 * ("I ate the cake" for "cake" → "I ate the %%"). Leaves sentences that already have one alone.
 */
export function withPlaceholder(word: string, sentence: string): string {
  const trimmed = sentence.trim();
  if (!trimmed || trimmed.includes(PLACEHOLDER)) return trimmed;
  const escaped = word.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
  return trimmed.replace(new RegExp(`\\b${escaped}\\b`, "i"), PLACEHOLDER);
}

export class ListFileError extends Error {}

function parseWord(raw: unknown, where: string): SpellingWord {
  if (typeof raw === "string") return { word: raw.trim(), sentence: "" };
  if (raw && typeof raw === "object" && typeof (raw as { word?: unknown }).word === "string") {
    const { word, sentence } = raw as { word: string; sentence?: unknown };
    const w = word.trim();
    return { word: w, sentence: typeof sentence === "string" ? withPlaceholder(w, sentence) : "" };
  }
  throw new ListFileError(`${where}: each word needs a "word" (and optionally a "sentence").`);
}

function parseList(raw: unknown, index: number): Omit<SpellingList, "id"> & { id?: string } {
  const where = `List ${index + 1}`;
  if (!raw || typeof raw !== "object") throw new ListFileError(`${where} isn't a list.`);
  const { id, name, grade, words } = raw as Record<string, unknown>;
  if (typeof name !== "string" || !name.trim()) throw new ListFileError(`${where} needs a "name".`);
  if (!Array.isArray(words)) throw new ListFileError(`"${name}" needs a "words" array.`);
  const parsed = words.map((w) => parseWord(w, `"${name}"`)).filter((w) => w.word);
  if (parsed.length === 0) throw new ListFileError(`"${name}" has no words.`);
  return {
    id: typeof id === "string" && id.trim() ? id.trim() : undefined,
    name: name.trim(),
    grade: typeof grade === "number" && Number.isFinite(grade) ? grade : undefined,
    words: parsed,
  };
}

/**
 * Reads an uploaded file's text. Accepts a full list file, a single list object, or an array
 * of lists, so hand-written files don't need the wrapper. Ids are made unique against `takenIds`.
 */
export function parseListFile(text: string, takenIds: Iterable<string> = []): SpellingList[] {
  let data: unknown;
  try {
    data = JSON.parse(text);
  } catch {
    throw new ListFileError("That file isn't valid JSON.");
  }

  let rawLists: unknown[];
  if (Array.isArray(data)) rawLists = data;
  else if (data && typeof data === "object" && Array.isArray((data as { lists?: unknown }).lists)) {
    rawLists = (data as { lists: unknown[] }).lists;
  } else if (data && typeof data === "object" && "words" in data) rawLists = [data];
  else throw new ListFileError("No spelling lists found in that file.");

  if (rawLists.length === 0) throw new ListFileError("No spelling lists found in that file.");

  const taken = new Set(takenIds);
  return rawLists.map((raw, i) => {
    const list = parseList(raw, i);
    const id = list.id && !taken.has(list.id) ? list.id : uniqueId(list.name, taken);
    taken.add(id);
    return { ...list, id };
  });
}

export function toListFile(lists: SpellingList[]): SpellingListFile {
  return { format: LIST_FILE_FORMAT, version: LIST_FILE_VERSION, lists };
}

export function serializeLists(lists: SpellingList[]): string {
  return JSON.stringify(toListFile(lists), null, 2) + "\n";
}

/**
 * Quick-create: one word per line, optionally followed by a sentence after a `-`, `:` or tab
 * ("cake - I ate the cake."). Blank lines are skipped.
 */
export function parseQuickList(text: string): SpellingWord[] {
  return text
    .split(/\r?\n/)
    .map((line) => line.trim())
    .filter(Boolean)
    .map((line) => {
      const match = line.match(/^(.+?)\s*(?:\t|\s-\s|:)\s*(.*)$/);
      const word = (match ? match[1] : line).trim();
      return { word, sentence: withPlaceholder(word, match ? match[2] : "") };
    });
}

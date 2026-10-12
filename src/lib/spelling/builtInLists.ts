import data from "@/data/spellingLists.json";
import type { SpellingList } from "./lists";

/** The lists that ship with the app, from the old desktop app's "Spelling Lists" folder. */
export const BUILT_IN_LISTS: SpellingList[] = data.lists;

export const BUILT_IN_IDS = new Set(BUILT_IN_LISTS.map((l) => l.id));

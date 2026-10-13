"use client";

import { useCallback, useMemo } from "react";
import { useSave } from "@/components/useSave";
import { updateSave } from "@/lib/save/store";
import { BUILT_IN_IDS, BUILT_IN_LISTS } from "@/lib/spelling/builtInLists";
import type { SpellingList } from "@/lib/spelling/lists";

/** Built-in lists plus the ones in the save file. `custom` is empty until `loaded`. */
export function useSpellingLists() {
  const { save, loaded } = useSave();
  const custom = save.spelling.lists;

  const all = useMemo(() => [...BUILT_IN_LISTS, ...custom], [custom]);
  const takenIds = useMemo(() => [...BUILT_IN_IDS, ...custom.map((l) => l.id)], [custom]);

  /** Returns false when the browser wouldn't store them. */
  const addLists = useCallback(
    (lists: SpellingList[]) =>
      updateSave((s) => ({ ...s, spelling: { ...s.spelling, lists: [...s.spelling.lists, ...lists] } })),
    [],
  );
  const removeList = useCallback(
    (id: string) =>
      updateSave((s) => ({ ...s, spelling: { ...s.spelling, lists: s.spelling.lists.filter((l) => l.id !== id) } })),
    [],
  );

  return { builtIn: BUILT_IN_LISTS, custom, all, takenIds, loaded, addLists, removeList };
}

"use client";

import { useCallback, useEffect, useMemo, useState } from "react";
import { BUILT_IN_IDS, BUILT_IN_LISTS } from "@/lib/spelling/builtInLists";
import type { SpellingList } from "@/lib/spelling/lists";
import { loadCustomLists, saveCustomLists, subscribeCustomLists } from "@/lib/spelling/storage";

/**
 * Built-in lists plus the ones saved in this browser. `custom` is empty until mounted
 * (storage is browser-only), then stays in sync across tabs.
 */
export function useSpellingLists() {
  const [custom, setCustom] = useState<SpellingList[]>([]);
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    const refresh = () => setCustom(loadCustomLists());
    refresh();
    setLoaded(true);
    return subscribeCustomLists(refresh);
  }, []);

  const all = useMemo(() => [...BUILT_IN_LISTS, ...custom], [custom]);
  const takenIds = useMemo(() => [...BUILT_IN_IDS, ...custom.map((l) => l.id)], [custom]);

  /** Returns false when the browser wouldn't store them. */
  const addLists = useCallback((lists: SpellingList[]) => saveCustomLists([...loadCustomLists(), ...lists]), []);
  const removeList = useCallback(
    (id: string) => saveCustomLists(loadCustomLists().filter((l) => l.id !== id)),
    [],
  );

  return { builtIn: BUILT_IN_LISTS, custom, all, takenIds, loaded, addLists, removeList };
}

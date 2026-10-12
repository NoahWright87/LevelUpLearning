"use client";

import { useEffect, useState, useSyncExternalStore } from "react";
import { activePlayer } from "@/lib/save/schema";
import { getSave, getServerSave, subscribeSave } from "@/lib/save/store";

/**
 * The save file, kept in sync with storage and other tabs. `loaded` is false during the server
 * render and the first client render, when the save still reads as empty.
 */
export function useSave() {
  const save = useSyncExternalStore(subscribeSave, getSave, getServerSave);
  const [loaded, setLoaded] = useState(false);
  useEffect(() => setLoaded(true), []);
  return { save, player: activePlayer(save), loaded };
}

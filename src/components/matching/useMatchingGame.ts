"use client";
import { useEffect, useRef, useState } from "react";
import { selectCard, startGame, tick, type Game, type Pair, type Settings } from "@/lib/matching/game";

/** Browser timing only; all rules live in lib/matching. A ref serializes rapid taps. */
export function useMatchingGame() {
  const [game, setGame] = useState<Game | null>(null);
  const [now, setNow] = useState(0);
  const current = useRef<Game | null>(null);
  const running = game !== null && game.finishedAt === null;
  useEffect(() => {
    if (!running) return;
    const id = window.setInterval(() => {
      const time = performance.now();
      setNow(time);
      if (current.current) { current.current = tick(current.current, time); setGame(current.current); }
    }, 50);
    return () => window.clearInterval(id);
  }, [running]);
  return { game, now,
    begin: (pool: Pair[], settings: Settings) => {
      const time = performance.now();
      current.current = startGame(pool, settings, time); setNow(time); setGame(current.current);
    },
    choose: (side: "left" | "right", token: number) => {
      if (!current.current) return;
      const time = performance.now();
      current.current = selectCard(current.current, side, token, time); setNow(time); setGame(current.current);
    },
    quit: () => { current.current = null; setGame(null); },
  };
}

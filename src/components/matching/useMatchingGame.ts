"use client";
import { useRef, useState } from "react";
import { useActivityClock } from "@/components/activity/useActivityClock";
import { selectCard, startGame, tick, type Game, type Pair, type Settings } from "@/lib/matching/game";

/** Browser timing only; all rules live in lib/matching. A ref serializes rapid taps. */
export function useMatchingGame() {
  const [game, setGame] = useState<Game | null>(null);
  const current = useRef<Game | null>(null);
  const running = game !== null && game.finishedAt === null;
  const { now, sample } = useActivityClock(running, time => {
    if (current.current) { current.current = tick(current.current, time); setGame(current.current); }
  });
  return { game, now,
    begin: (pool: Pair[], settings: Settings) => {
      const time = sample();
      current.current = startGame(pool, settings, time); setGame(current.current);
    },
    choose: (side: "left" | "right", token: number) => {
      if (!current.current) return;
      const time = sample();
      current.current = selectCard(current.current, side, token, time); setGame(current.current);
    },
    quit: () => { current.current = null; setGame(null); },
  };
}

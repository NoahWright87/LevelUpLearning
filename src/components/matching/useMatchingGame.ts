"use client";
import { useRef, useState } from "react";
import { useSceneEffects } from "@noahwright/design";
import { useActivityClock } from "@/components/activity/useActivityClock";
import { selectCard, startGame, tick, type Game, type Pair, type Settings } from "@/lib/matching/game";

import { activePlayer, ensurePlayer } from "@/lib/save/schema";
import { getSave, updateSave } from "@/lib/save/store";
import { buildMatchingSession, recordMatchingSession } from "@/lib/matching/progress";

/** Browser timing only; all rules live in lib/matching. A ref serializes rapid taps. */
export function useMatchingGame() {
  const effects = useSceneEffects();
  const [game, setGame] = useState<Game | null>(null);
  const current = useRef<Game | null>(null);
  const owner = useRef({ id: "", name: "Player 1", source: "Matching" });
  const [saved, setSaved] = useState(false);
  const running = game !== null && game.finishedAt === null;
  const { now, sample } = useActivityClock(running, time => {
    if (current.current) publish(tick(current.current, time));
  });

  function publish(next: Game) {
    const previous = current.current;
    if (next === previous) return;
    current.current = next;
    setGame(next);
    if (previous?.finishedAt === null && next.finishedAt !== null) {
      const session = buildMatchingSession(next, owner.current.source);
      setSaved(getSave().players.some(p => p.id === owner.current.id) && updateSave(s => recordMatchingSession(s, owner.current.id, session)));
    }
    if (previous && next.correct > previous.correct) effects.trigger({ type: "firework", size: "small" });
    if (previous?.finishedAt === null && next.finishedAt !== null && next.correct > 0) effects.trigger({ type: "firework", size: "large" });
  }

  return { game, now, effects, saved, playerName: owner.current.name,
    begin: (pool: Pair[], settings: Settings, source = owner.current.source) => {
      const save = ensurePlayer(getSave());
      const player = activePlayer(save)!;
      if (save !== getSave()) updateSave(() => save);
      owner.current = { id: player.id, name: player.name, source };
      setSaved(false);
      const time = sample();
      current.current = startGame(pool, settings, time); setGame(current.current);
    },
    choose: (side: "left" | "right", token: number) => {
      if (!current.current) return;
      const time = sample();
      publish(selectCard(current.current, side, token, time));
    },
    quit: () => { current.current = null; setGame(null); },
  };
}

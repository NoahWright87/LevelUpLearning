"use client";

import { useRef, useState } from "react";
import { useSceneEffects } from "@noahwright/design";
import { useActivityClock } from "@/components/activity/useActivityClock";
import { comboRemaining, expireCombo, nextProblem, startGame, submitAnswer, type MathGame, type MathSettings } from "@/lib/math/game";
import { buildMathSession, recordMathSession } from "@/lib/math/progress";
import { activePlayer, ensurePlayer } from "@/lib/save/schema";
import { getSave, updateSave } from "@/lib/save/store";

/** Browser clock, persistence and celebrations; the game rules live in lib/math. */
export function useMathGame() {
  const effects = useSceneEffects();
  const [game, setGame] = useState<MathGame | null>(null);
  const current = useRef<MathGame | null>(null);
  const owner = useRef({ id: "", name: "Player 1" });
  const [saved, setSaved] = useState(false);
  const playing = game !== null && game.finishedAt === null;
  const { now, sample } = useActivityClock(playing);

  function begin(settings: MathSettings) {
    const save = ensurePlayer(getSave());
    const player = activePlayer(save)!;
    if (save !== getSave()) updateSave(() => save);
    owner.current = { id: player.id, name: player.name };
    const time = sample();
    const next = startGame(settings, time);
    current.current = next;
    setSaved(false);
    setGame(next);
  }

  function answer(value: string) {
    if (!current.current) return false;
    const time = sample();
    const next = submitAnswer(current.current, value, time);
    if (next === current.current) return false;
    current.current = next;
    setGame(next);
    if (next.feedback?.correct) {
      for (let i = 0; i < next.combo; i++) {
        if (!effects.trigger({ type: "firework", size: "small" })) break;
      }
    }
    if (next.finishedAt !== null) {
      const session = buildMathSession(next);
      const exists = getSave().players.some((p) => p.id === owner.current.id);
      setSaved(exists && updateSave((s) => recordMathSession(s, owner.current.id, session)));
      if (next.correct >= next.settings.target) effects.trigger({ type: "firework", size: "large" });
    }
    return true;
  }

  function advance() {
    if (!current.current) return;
    const next = nextProblem(current.current);
    current.current = next;
    setGame(next);
  }

  function quit() {
    current.current = null;
    setGame(null);
  }

  return { game: game ? expireCombo(game, now) : null, effects, begin, answer, advance, quit,
    remaining: game ? comboRemaining(game, now) : 0, saved, playerName: owner.current.name };
}

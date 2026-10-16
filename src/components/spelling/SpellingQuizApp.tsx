"use client";

import { useEffect, useState } from "react";
import { Container, useSceneEffects } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { activePlayer, ensurePlayer, type SpellingSession } from "@/lib/save/schema";
import { buildSession, listBests, recordSession, type ListBest } from "@/lib/save/spellingProgress";
import { getSave, updateSave } from "@/lib/save/store";
import { startQuiz, type QuizState } from "@/lib/spelling/quiz";
import QuizSetupPanel, { type Round } from "./QuizSetupPanel";
import QuizPlay from "./QuizPlay";
import QuizResults from "./QuizResults";
import "./spelling.css";

type Phase =
  | { name: "setup" }
  | { name: "play"; round: Round; quiz: QuizState }
  | { name: "results"; round: Round; quiz: QuizState; saved: SavedResult };

/** What finishing a quiz saved, and the bests it's compared against. */
export type SavedResult = {
  session: SpellingSession;
  playerName: string;
  /** The player's list bests from before this session. */
  previousBests: Map<string, ListBest>;
  /** False when the browser wouldn't store it. */
  stored: boolean;
};

/** Saves a finished quiz to the active player (creating "Player 1" the first time). */
function saveResult(quiz: QuizState, round: Round): SavedResult {
  const save = ensurePlayer(getSave());
  const player = activePlayer(save)!;
  const session = buildSession(quiz, round.lists, round.difficulty);
  const previousBests = listBests(player);
  // Built from `save` (not a fresh read) so a just-created "Player 1" keeps the id used here.
  const stored = updateSave(() => recordSession(save, player.id, session));
  return { session, playerName: player.name, previousBests, stored };
}

/** The /spelling page: pick lists and a difficulty, take the quiz, see how you did. */
export default function SpellingQuizApp() {
  const [phase, setPhase] = useState<Phase>({ name: "setup" });
  const effects = useSceneEffects();

  // Each phase is a new screen: start it at the top (Start sits at the bottom of a long setup).
  // Braces matter: newer Chrome returns a Promise from scrollTo, and React treats an effect's
  // return value as its cleanup, so returning it crashed the page ("u is not a function").
  useEffect(() => {
    window.scrollTo({ top: 0 });
  }, [phase.name]);

  const begin = (round: Round) => setPhase({ name: "play", round, quiz: startQuiz(round.words, round.settings) });

  return (
    <SiteShell effects={effects} compactScene={phase.name === "play"}>
      <Container padding={phase.name === "play" ? "sm" : "lg"}>
        {phase.name === "setup" && <QuizSetupPanel onStart={begin} />}
        {phase.name === "play" && (
          <QuizPlay
            initial={phase.quiz}
            voiceURI={phase.round.voiceURI}
            onCorrectAnswer={(combo) => {
              // One shell per consecutive right answer; the design renderer owns the particle budget.
              for (let i = 0; i < combo; i++) {
                if (!effects.trigger({ type: "firework", size: "small" })) break;
              }
            }}
            onFinish={(quiz) => {
              setPhase({ name: "results", round: phase.round, quiz, saved: saveResult(quiz, phase.round) });
              effects.trigger({ type: "firework", size: "large" });
            }}
            onQuit={() => setPhase({ name: "setup" })}
          />
        )}
        {phase.name === "results" && (
          <QuizResults
            quiz={phase.quiz}
            voiceURI={phase.round.voiceURI}
            saved={phase.saved}
            onAgain={() => begin(phase.round)}
            onSetup={() => setPhase({ name: "setup" })}
          />
        )}
      </Container>
    </SiteShell>
  );
}

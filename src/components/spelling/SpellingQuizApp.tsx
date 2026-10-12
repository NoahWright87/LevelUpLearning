"use client";

import { useEffect, useState } from "react";
import { Container } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { startQuiz, type QuizState } from "@/lib/spelling/quiz";
import type { DifficultySettings } from "@/lib/spelling/difficulty";
import type { SpellingWord } from "@/lib/spelling/lists";
import QuizSetupPanel from "./QuizSetupPanel";
import QuizPlay from "./QuizPlay";
import QuizResults from "./QuizResults";
import "./spelling.css";

type Round = { words: SpellingWord[]; settings: DifficultySettings; voiceURI?: string };

type Phase =
  | { name: "setup" }
  | { name: "play"; round: Round; quiz: QuizState }
  | { name: "results"; round: Round; quiz: QuizState };

/** The /spelling page: pick lists and a difficulty, take the quiz, see how you did. */
export default function SpellingQuizApp() {
  const [phase, setPhase] = useState<Phase>({ name: "setup" });

  // Each phase is a new screen: start it at the top (Start sits at the bottom of a long setup).
  useEffect(() => window.scrollTo({ top: 0 }), [phase.name]);

  const begin = (round: Round) => setPhase({ name: "play", round, quiz: startQuiz(round.words, round.settings) });

  return (
    <SiteShell>
      <Container padding="lg">
        {phase.name === "setup" && <QuizSetupPanel onStart={begin} />}
        {phase.name === "play" && (
          <QuizPlay
            initial={phase.quiz}
            voiceURI={phase.round.voiceURI}
            onFinish={(quiz) => setPhase({ name: "results", round: phase.round, quiz })}
            onQuit={() => setPhase({ name: "setup" })}
          />
        )}
        {phase.name === "results" && (
          <QuizResults
            quiz={phase.quiz}
            voiceURI={phase.round.voiceURI}
            onAgain={() => begin(phase.round)}
            onSetup={() => setPhase({ name: "setup" })}
          />
        )}
      </Container>
    </SiteShell>
  );
}

"use client";

import { useEffect, useRef, useState } from "react";
import { Button, Text } from "@noahwright/design";
import {
  fillSentence,
  isDone,
  nextWord,
  progress,
  submitAnswer,
  type AnswerResult,
  type QuizState,
} from "@/lib/spelling/quiz";
import { sayCongrats, sayCorrection, sayWordPrompt, stopSpeaking } from "@/lib/spelling/speech";
import AnswerInput from "./AnswerInput";

type Props = {
  initial: QuizState;
  voiceURI?: string;
  onFinish: (quiz: QuizState) => void;
  onQuit: () => void;
};

/** A cheer for answers in a row, the way the old app's streak label grew. */
function streakText(streak: number): string | null {
  if (streak < 2) return null;
  if (streak < 5) return `🔥 ${streak} in a row!`;
  if (streak < 10) return `🔥🔥 ${streak} in a row! Wow!`;
  return `🔥🔥🔥 ${streak} in a row! Unstoppable!`;
}

export default function QuizPlay({ initial, voiceURI, onFinish, onQuit }: Props) {
  const [quiz, setQuiz] = useState(initial);
  const [answer, setAnswer] = useState("");
  const [result, setResult] = useState<AnswerResult | null>(null);
  const [streak, setStreak] = useState(0);
  /** Counts questions asked, so a word that comes up twice in a row is still spoken again. */
  const [turn, setTurn] = useState(0);
  const inputRef = useRef<HTMLInputElement>(null);

  const word = quiz.current === null ? null : quiz.words[quiz.current];
  const { done, total } = progress(quiz);
  const options = { voiceURI };

  // Say each new word as it comes up.
  useEffect(() => {
    if (word) {
      sayWordPrompt(word.word, word.sentence, options);
      inputRef.current?.focus({ preventScroll: true });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps -- speak once per question
  }, [turn]);

  useEffect(() => () => stopSpeaking(), []);

  // While the answer is showing, Enter moves on (the answer box is disabled, so it can't).
  useEffect(() => {
    if (!result) return;
    const onKey = (e: KeyboardEvent) => {
      if (e.key === "Enter" && !e.repeat) {
        e.preventDefault();
        advance();
      }
    };
    window.addEventListener("keydown", onKey);
    return () => window.removeEventListener("keydown", onKey);
  });

  if (!word) return null;

  const check = (e: React.FormEvent) => {
    e.preventDefault();
    if (result) return;
    // An empty answer repeats the word, as in the old app.
    if (!answer.trim()) {
      sayWordPrompt(word.word, word.sentence, options);
      inputRef.current?.focus({ preventScroll: true });
      return;
    }
    const outcome = submitAnswer(quiz, answer);
    setQuiz(outcome.state);
    setResult(outcome.result);
    setStreak((s) => (outcome.result.correct ? s + 1 : 0));
    if (outcome.result.correct) sayCongrats(options);
    else sayCorrection(outcome.result.expected, options);
  };

  const advance = () => {
    const next = nextWord(quiz);
    setResult(null);
    setAnswer("");
    if (isDone(next)) onFinish(next);
    else {
      setQuiz(next);
      setTurn((t) => t + 1);
    }
  };

  const cheer = result?.correct ? streakText(streak) : null;

  return (
    <div className="spelling-play">
      <div className="spelling-play__top">
        <div
          className="spelling-progress"
          role="progressbar"
          aria-label="Quiz progress"
          aria-valuemin={0}
          aria-valuemax={total}
          aria-valuenow={done}
        >
          <div className="spelling-progress__fill" style={{ width: `${(done / total) * 100}%` }} />
        </div>
        <Text tone="muted">
          {done} / {total}
        </Text>
        <Button variant="text" onClick={onQuit}>
          Quit
        </Button>
      </div>

      <div className="spelling-card">
        <Button
          size="large"
          variant="outline"
          onClick={() => {
            sayWordPrompt(word.word, word.sentence, options);
            if (!result) inputRef.current?.focus({ preventScroll: true });
          }}
        >
          🔊 Hear it again
        </Button>

        {word.sentence ? (
          <Text align="center" className="spelling-sentence">
            “{fillSentence(word.sentence, "_____")}”
          </Text>
        ) : null}

        {quiz.hint ? (
          <div className="spelling-hint" aria-label="Letter hints">
            {quiz.hint.map((cell, i) => (
              <span
                key={i}
                className={`spelling-hint__cell${cell.fixed ? " spelling-hint__cell--fixed" : ""}`}
                aria-label={cell.char ?? "blank"}
              >
                {cell.char ?? ""}
              </span>
            ))}
          </div>
        ) : null}

        <form className="spelling-form" onSubmit={check}>
          <AnswerInput
            ref={inputRef}
            label="Type the word"
            value={answer}
            onChange={setAnswer}
            disabled={!!result}
          />
          {!result ? (
            <Button type="submit" color="primary">
              Check
            </Button>
          ) : null}
        </form>

        {result ? (
          <div
            className={`spelling-feedback spelling-feedback--${result.correct ? "right" : "wrong"}`}
            role="status"
          >
            {result.correct ? (
              <>
                <strong>✅ You spelled “{result.expected}” right!</strong>
                {cheer ? <span>{cheer}</span> : null}
              </>
            ) : (
              <>
                <strong>❌ Not quite. Don&apos;t worry, you&apos;ll get it next time!</strong>
                <span>
                  You spelled: <s>{result.given}</s>
                </span>
                <span>
                  The word is: <strong className="spelling-feedback__word">{result.expected}</strong>
                </span>
              </>
            )}
            <div className="spelling-feedback__next">
              <Button color={result.correct ? "confirm" : "primary"} onClick={advance}>
                {done >= total ? "See how you did →" : "Next word →"}
              </Button>
              <Text tone="muted" inline>
                or press Enter
              </Text>
            </div>
          </div>
        ) : null}
      </div>
    </div>
  );
}

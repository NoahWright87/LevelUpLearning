"use client";

import { useEffect, useRef, useState } from "react";
import { Button, Text } from "@noahwright/design";
import {
  isDone,
  nextWord,
  progress,
  submitAnswer,
  type AnswerResult,
  type QuizState,
} from "@/lib/spelling/quiz";
import { sayCongrats, sayCorrection, sayWordPrompt, stopSpeaking } from "@/lib/spelling/speech";
import AnswerInput from "./AnswerInput";
import { comboCheer } from "@/lib/spelling/combo";

type Props = {
  initial: QuizState;
  voiceURI?: string;
  onFinish: (quiz: QuizState) => void;
  onQuit: () => void;
  onCorrectAnswer?: (combo: number) => void;
};

export default function QuizPlay({ initial, voiceURI, onFinish, onQuit, onCorrectAnswer }: Props) {
  const [quiz, setQuiz] = useState(initial);
  const [answer, setAnswer] = useState("");
  const [result, setResult] = useState<AnswerResult | null>(null);
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
    if (outcome.result.correct) {
      sayCongrats(options);
      onCorrectAnswer?.(outcome.state.combo);
    } else sayCorrection(outcome.result.expected, options);
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

  const cheer = result?.correct ? comboCheer(quiz.combo) : null;

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
        <strong className="spelling-combo">🔥 Combo ×{quiz.combo}</strong>
        <Button size="small" variant="text" onClick={onQuit}>
          Quit
        </Button>
      </div>

      <div className="spelling-card">
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
          <div className="spelling-actions">
            <Button type="button" variant="outline" onClick={() => {
              sayWordPrompt(word.word, word.sentence, options);
              if (!result) inputRef.current?.focus({ preventScroll: true });
            }}>
              Speak 🔊
            </Button>
            {result ? (
              <Button type="button" color={result.correct ? "confirm" : "primary"} onClick={advance}>
                {done >= total ? "See results →" : "Next word →"}
              </Button>
            ) : (
              <Button type="submit" color="primary">Check</Button>
            )}
          </div>
        </form>

        <div className="spelling-feedback-slot" role="status" aria-live="polite" aria-atomic="true">
        {result ? (
          <div
            className={`spelling-feedback spelling-feedback--${result.correct ? "right" : "wrong"}`}
          >
            {result.correct ? (
              <>
                <strong>✅ Correct!</strong>
                {cheer ? <span>{cheer}</span> : null}
              </>
            ) : (
              <>
                <strong>❌ Not quite. Try again next time!</strong>
                <span>
                  You wrote: <s>{result.given}</s>. Correct spelling: <strong>{result.expected}</strong>.
                </span>
              </>
            )}
          </div>
        ) : null}
        </div>
      </div>
    </div>
  );
}

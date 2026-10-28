"use client";

import { useEffect, useRef, useState } from "react";
import NextLink from "next/link";
import { Button, CardGrid, Heading, Link, Select, Text } from "@noahwright/design";
import { ActivityFeedback, ActivityPage, ActivityResults, ActivityStats, ActivityToolbar } from "@/components/activity/ActivityUI";
import PlayerPicker from "@/components/PlayerPicker";
import { DEFAULT_SETTINGS, ROUND_TARGETS, gameStats, operationLabel, symbol, type MathGame, type MathSettings } from "@/lib/math/game";
import { useMathGame } from "./useMathGame";
import "./math.css";

function Setup({ settings, onChange, onStart }: {
  settings: MathSettings; onChange: (settings: MathSettings) => void; onStart: () => void;
}) {
  return (
    <div className="activity-stack">
      <div>
        <Text tone="muted">THINK FAST. BUILD YOUR STREAK.</Text>
        <Heading level={1}>Math sprint</Heading>
        <Text>Little problems. Big combos. Light up the sky as you learn your math facts.</Text>
      </div>
      <PlayerPicker />
      <CardGrid minCardWidth="min(100%, 200px)" gap="sm">
        <Select label="Practice" value={settings.operation} onChange={(e) => onChange({ ...settings, operation: e.target.value as MathSettings["operation"] })}>
          <option value="addition">Addition +</option>
          <option value="multiplication">Multiplication ×</option>
        </Select>
        <Select label="Digits in each number" value={String(settings.digits)} onChange={(e) => onChange({ ...settings, digits: Number(e.target.value) as MathSettings["digits"] })}>
          <option value="1">1 digit · 0–9</option>
          <option value="2">2 digits · 10–99</option>
          <option value="3">3 digits · 100–999</option>
        </Select>
        <Select label="Round goal" value={String(settings.target)} onChange={(e) => onChange({ ...settings, target: Number(e.target.value) as MathSettings["target"] })}>
          {ROUND_TARGETS.map((target) => <option key={target} value={target}>{target} correct{target === 30 ? " · recommended" : ""}</option>)}
        </Select>
      </CardGrid>
      <div className="activity-rules">
        <Text><strong>♥ Three hearts.</strong> Each wrong answer costs one and resets your combo.</Text>
        <Text><strong>Five seconds to keep it going.</strong> Each correct answer refills the bar. An empty bar resets your combo, with no heart lost.</Text>
        <Text><strong>10 points × your combo.</strong> Keep answering quickly for bigger scores and more fireworks. Take your time whenever you need to.</Text>
      </div>
      <Button color="primary" onClick={onStart}>Let’s play →</Button>
    </div>
  );
}

function Play({ game, remaining, onAnswer, onNext, onQuit }: {
  game: MathGame; remaining: number; onAnswer: (value: string) => boolean; onNext: () => void; onQuit: () => void;
}) {
  const [value, setValue] = useState("");
  const input = useRef<HTMLInputElement>(null);
  const form = useRef<HTMLFormElement>(null);

  useEffect(() => {
    if (game.awaitingNext) form.current?.querySelector("button")?.focus({ preventScroll: true });
    else input.current?.focus({ preventScroll: true });
  }, [game.awaitingNext, game.attempts]);

  const feedback = game.feedback;
  return (
    <div className="math-play">
      <ActivityToolbar className="math-topline" title={`${operationLabel(game.settings.operation)} · ${game.settings.digits} digit${game.settings.digits > 1 ? "s" : ""}`} onQuit={onQuit} />
      <ActivityStats className="math-scoreboard" items={[
        { label: "Correct", value: `${game.correct} / ${game.settings.target}` },
        { label: "Points", value: game.points.toLocaleString() },
        { label: "Hearts", value: <span className="math-hearts" aria-label={`${game.hearts} of 3 hearts remaining`}>{"♥".repeat(game.hearts)}<span aria-hidden="true">{"♡".repeat(3 - game.hearts)}</span></span> },
      ]} />
      <div className="math-combo">
        <div className="math-combo__label"><strong>🔥 Combo ×{game.combo}</strong><span>{game.combo ? `${10 * (game.combo + 1)} points next!` : "A correct answer starts your combo"}</span></div>
        <div className="math-combo__track" role="progressbar" aria-label="Combo time remaining" aria-valuemin={0} aria-valuemax={5} aria-valuenow={Math.ceil(remaining * 50) / 10}>
          <div className="math-combo__fill" style={{ transform: `scaleX(${remaining})` }} />
        </div>
      </div>
      <div className="math-question-card">
        <p className="math-equation" aria-label={`${game.problem.left} ${game.settings.operation === "addition" ? "plus" : "times"} ${game.problem.right}`}>{game.problem.left} {symbol(game.settings.operation)} {game.problem.right} <span>= ?</span></p>
        <form ref={form} className="math-answer-form" onSubmit={(e) => {
          e.preventDefault();
          if (onAnswer(value)) setValue("");
        }}>
          {/* Native input for focus and numeric keyboard, pending NoahWright87/design#32. */}
          <label className="nw-input math-answer">
            <span className="nw-input__label">Your answer</span>
            <input ref={input} className="nw-input__field" value={value} onChange={(e) => setValue(e.target.value)} type="text" inputMode="numeric" pattern="[0-9]*" maxLength={7} autoComplete="off" enterKeyHint="go" disabled={game.awaitingNext} />
          </label>
          {game.awaitingNext ? (
            <Button type="button" color="primary" onClick={onNext}>Next problem →</Button>
          ) : <Button type="submit" color="primary" disabled={!/^\d+$/.test(value.trim())}>Check →</Button>}
        </form>
        <ActivityFeedback className={`math-feedback${feedback ? feedback.correct ? " math-feedback--right" : " math-feedback--wrong" : ""}`}>
          {feedback ? <span key={game.attempts}>{feedback.correct ? `✓ ${feedback.problem.left} ${symbol(game.settings.operation)} ${feedback.problem.right} = ${feedback.expected}. +${feedback.points} points!` : `Not quite: ${feedback.problem.left} ${symbol(game.settings.operation)} ${feedback.problem.right} = ${feedback.expected}.`}</span> : "Type your answer and press Enter."}
        </ActivityFeedback>
      </div>
    </div>
  );
}

function Results({ game, saved, playerName, onAgain, onSetup }: {
  game: MathGame; saved: boolean; playerName: string; onAgain: () => void; onSetup: () => void;
}) {
  const stats = gameStats(game);
  return (
    <ActivityResults
      title={stats.won ? "🎉 Sprint complete!" : "♥ Good practice!"}
      description={stats.won ? `You reached ${game.settings.target} correct answers. Look at that sky!` : "Your hearts ran out. Try another round and beat your score."}
      onAgain={onAgain} onSetup={onSetup}
    >
      <strong className="activity-result-score">{game.points.toLocaleString()} <span>points</span></strong>
      <ActivityStats items={[
        { label: "Correct", value: `${game.correct} / ${game.settings.target}` },
        { label: "Accuracy", value: `${stats.accuracy}%` },
        { label: "Best combo", value: `×${game.bestCombo}` },
        { label: "Time", value: `${stats.seconds}s` },
      ]} />
      {game.feedback && !game.feedback.correct && <Text>Last one: {game.feedback.problem.left} {symbol(game.settings.operation)} {game.feedback.problem.right} = <strong>{game.feedback.expected}</strong></Text>}
      <Text tone="muted">{saved ? `Saved to ${playerName}’s progress.` : "This round couldn’t be saved to this browser."} <Link as={NextLink} href="/progress" motion="none">View progress →</Link></Text>
    </ActivityResults>
  );
}

export default function MathGameApp() {
  const [settings, setSettings] = useState<MathSettings>(DEFAULT_SETTINGS);
  const { game, effects, begin, answer, advance, quit, remaining, saved, playerName } = useMathGame();
  return (
    <ActivityPage effects={effects}>
      {!game ? <Setup settings={settings} onChange={setSettings} onStart={() => begin(settings)} />
        : game.finishedAt !== null ? <Results game={game} saved={saved} playerName={playerName} onAgain={() => begin(game.settings)} onSetup={quit} />
        : <Play game={game} remaining={remaining} onAnswer={answer} onNext={advance} onQuit={quit} />}
    </ActivityPage>
  );
}

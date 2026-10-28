"use client";
import { useEffect, useRef, useState } from "react";
import NextLink from "next/link";
import { Button, CardGrid, Heading, Input, Link, Select, Text } from "@noahwright/design";
import { ActivityFeedback, ActivityPage, ActivityResults, ActivityStats, ActivityToolbar } from "@/components/activity/ActivityUI";
import { useSave } from "@/components/useSave";
import FittedText from "@/components/FittedText";
import { newId } from "@/lib/save/schema";
import { updateSave } from "@/lib/save/store";
import { DEFAULT_SETTINGS, MATH_LABELS, elapsed, mathPairs, normalizeSettings, validateDeck, type Deck, type Game, type MathKind, type Settings } from "@/lib/matching/game";
import { useMatchingGame } from "./useMatchingGame";
import "./matching.css";

function DeckEditor({ deck, onSave, onCancel }: { deck: Deck | null; onSave: (deck: Deck) => void; onCancel: () => void }) {
  const [name, setName] = useState(deck?.name ?? "");
  const [pairs, setPairs] = useState(deck?.pairs.map(p => ({ left: p.left, right: p.right })) ?? [{ left: "", right: "" }, { left: "", right: "" }]);
  const [error, setError] = useState("");
  return <form className="activity-stack" onSubmit={e => {
    e.preventDefault();
    const problem = validateDeck(name, pairs);
    if (problem) { setError(problem); return; }
    const id = deck?.id ?? newId();
    onSave({ id, name: name.trim(), pairs: pairs.map((p, i) => ({ id: `${id}-${i}`, left: p.left.trim(), right: p.right.trim() })) });
  }}>
    <Heading level={1}>{deck ? "Edit your deck" : "Create a matching deck"}</Heading>
    <Text>Words and meanings, countries and capitals, dates and events. Put a matching pair on each row.</Text>
    <Input label="Deck name" value={name} onChange={e => setName(e.target.value)} />
    <div className="matching-editor">
      {pairs.map((pair, i) => <div className="matching-editor-row" key={i}>
        <Input label={`Left card ${i + 1}`} value={pair.left} onChange={e => setPairs(pairs.map((p, j) => j === i ? { ...p, left: e.target.value } : p))} />
        <Input label={`Right card ${i + 1}`} value={pair.right} onChange={e => setPairs(pairs.map((p, j) => j === i ? { ...p, right: e.target.value } : p))} />
        <Button type="button" variant="text" size="small" disabled={pairs.length <= 2} onClick={() => setPairs(pairs.filter((_, j) => j !== i))} aria-label={`Remove pair ${i + 1}`}>×</Button>
      </div>)}
    </div>
    <Button type="button" variant="outline" disabled={pairs.length >= 200} onClick={() => setPairs([...pairs, { left: "", right: "" }])}>+ Add pair</Button>
    {error && <p role="alert">{error}</p>}
    <Text tone="muted">Saved in this browser. Download a backup from <Link as={NextLink} href="/progress" motion="none">Progress</Link> to keep or move your decks.</Text>
    <div className="activity-actions"><Button type="submit" color="primary">Save deck</Button><Button type="button" variant="text" onClick={onCancel}>Cancel</Button></div>
  </form>;
}

function Play({ game, now, onChoose, onQuit, title }: { game: Game; now: number; onChoose: (side: "left" | "right", token: number) => void; onQuit: () => void; title: string }) {
  const seconds = elapsed(game, now);
  const remaining = Math.max(0, Math.ceil(game.settings.amount - seconds));
  const board = useRef<HTMLDivElement>(null);
  const keyboard = useRef(false);
  useEffect(() => {
    // A cleared batch can remove the focused card; keep keyboard play on the board.
    if (keyboard.current && document.activeElement === document.body) board.current?.querySelector<HTMLButtonElement>("button[tabindex='0']")?.focus({ preventScroll: true });
  }, [game.left]);
  // Start Tab navigation at the board without highlighting an untouched card.
  useEffect(() => { board.current?.focus({ preventScroll: true }); }, []);
  return <div className="activity-stack">
    <ActivityToolbar title={title} onQuit={onQuit} />
    <ActivityStats items={[
      { label: "Matches", value: `${game.correct}${game.settings.goal === "matches" ? ` / ${game.settings.amount}` : ""}` },
      { label: game.settings.goal === "time" ? "Time left" : "Time", value: <span role="timer">{game.settings.goal === "time" ? `${remaining}s` : `${seconds.toFixed(1)}s`}</span> },
      { label: "Misses", value: game.mistakes },
    ]} />
    <Text tone="muted">Pick one card from each side. You can start on either side.</Text>
    <div className="matching-board" ref={board} tabIndex={-1}>
      {(["left", "right"] as const).map(side => <div className="matching-column" role="group" aria-label={`${side === "left" ? "Left" : "Right"} cards`} key={side}>
        {game[side].map((card, i) => <div className="matching-slot" key={i}>
          {card && <button type="button" className={`matching-card${game.selected?.token === card.token ? " matching-card--selected" : ""}${card.clearedAt !== null ? " matching-card--matched" : ""}${game.wrong.includes(card.token) ? " matching-card--wrong" : ""}`}
            aria-pressed={game.selected?.token === card.token} aria-label={`${card.pair[side]}${card.clearedAt !== null ? ", matched" : ""}`}
            tabIndex={card.clearedAt !== null ? -1 : 0} aria-disabled={game.wrong.length > 0 || card.clearedAt !== null}
            onPointerDown={() => { keyboard.current = false; }}
            onClick={e => {
              keyboard.current = e.detail === 0;
              if (!keyboard.current) e.currentTarget.blur();
              onChoose(side, card.token);
            }}>
            <FittedText>{card.pair[side]}</FittedText>
          </button>}
        </div>)}
      </div>)}
    </div>
    <ActivityFeedback>
      {game.wrong.length ? "Not a match. Try another pair." : game.correct ? `${game.correct} matched. Keep going!` : "Find your first match."}
    </ActivityFeedback>
  </div>;
}

function Results({ game, onAgain, onSetup }: { game: Game; onAgain: () => void; onSetup: () => void }) {
  const seconds = elapsed(game, game.finishedAt!);
  const attempts = game.correct + game.mistakes;
  return <ActivityResults
    title={game.settings.goal === "time" ? "Time’s up!" : "All matched!"}
    description={game.settings.goal === "time" ? "Quick thinking. Good practice." : `You made ${game.correct} matches in ${seconds.toFixed(1)} seconds.`}
    onAgain={onAgain} onSetup={onSetup}
  >
    <strong className="activity-result-score">{game.correct}<span> matches</span></strong>
    <ActivityStats items={[
      { label: "Time", value: `${seconds.toFixed(1)}s` },
      { label: "Accuracy", value: `${attempts ? Math.round(game.correct / attempts * 100) : 0}%` },
      { label: "Misses", value: game.mistakes },
    ]} />
  </ActivityResults>;
}

export default function MatchingApp() {
  const { save, loaded } = useSave();
  const [source, setSource] = useState("addition");
  const [settings, setSettings] = useState<Settings>(DEFAULT_SETTINGS);
  const [customAmount, setCustomAmount] = useState<string | null>(null);
  const [editor, setEditor] = useState<{ deck: Deck | null } | null>(null);
  const [notice, setNotice] = useState("");
  const [roundTitle, setRoundTitle] = useState("");
  const { game, now, begin, choose, quit } = useMatchingGame();
  const deck = save.matching.decks.find(d => `deck:${d.id}` === source);
  const isMath = source in MATH_LABELS;
  const canStart = isMath || !!deck;
  const start = () => {
    if (!canStart) return;
    setCustomAmount(null);
    setRoundTitle(deck?.name ?? MATH_LABELS[source as MathKind]);
    begin(deck?.pairs ?? mathPairs(source as MathKind), normalizeSettings(settings));
  };
  return <ActivityPage>
    {editor ? <DeckEditor deck={editor.deck} onCancel={() => setEditor(null)} onSave={d => {
      const stored = updateSave(s => ({ ...s, matching: { decks: [...s.matching.decks.filter(old => old.id !== d.id), d] } }));
      setSource(`deck:${d.id}`); setEditor(null);
      setNotice(stored ? `“${d.name}” saved in this browser.` : "Your deck is available for this visit, but browser storage failed. Download a backup from Progress before leaving.");
    }} /> : game ? game.finishedAt !== null ? <Results game={game} onAgain={() => begin(game.pool, game.settings)} onSetup={quit} /> : <Play game={game} now={now} onChoose={choose} onQuit={quit} title={roundTitle} /> : <div className="activity-stack">
      <div><Text tone="muted">FIND THE PAIRS. FIND YOUR FLOW.</Text><Heading level={1}>Matching</Heading><Text>A little focus. A little speed. How many pairs can you find?</Text></div>
      <Select label="What are we matching?" value={canStart ? source : ""} onChange={e => { setSource(e.target.value); setNotice(""); }}>
        {!canStart && <option value="" disabled>Choose a deck</option>}
        <optgroup label="Math facts">{Object.entries(MATH_LABELS).map(([id, label]) => <option key={id} value={id}>{label}</option>)}</optgroup>
        {save.matching.decks.length > 0 && <optgroup label="Your decks">{save.matching.decks.map(d => <option key={d.id} value={`deck:${d.id}`}>{d.name} · {d.pairs.length} pairs</option>)}</optgroup>}
      </Select>
      <div className="activity-actions"><Button variant="text" size="small" disabled={!loaded} onClick={() => setEditor({ deck: null })}>+ Create custom cards</Button>{deck && <><Button variant="text" size="small" onClick={() => setEditor({ deck })}>Edit deck</Button><Button variant="text" size="small" color="danger" onClick={() => {
        if (!window.confirm(`Delete “${deck.name}” from this browser?`)) return;
        const stored = updateSave(s => ({ ...s, matching: { decks: s.matching.decks.filter(d => d.id !== deck.id) } }));
        setSource("addition"); setNotice(stored ? "Deck deleted." : "The browser couldn’t save this change. The deck may return when you reload.");
      }}>Delete deck</Button></>}</div>
      <CardGrid minCardWidth="min(100%, 260px)" gap="sm">
        <Select label="Round goal" value={settings.goal} onChange={e => { setCustomAmount(null); setSettings({ ...settings, goal: e.target.value as Settings["goal"], amount: e.target.value === "time" ? 60 : 20 }); }}><option value="time">Beat the clock</option><option value="matches">Race to a match count</option></Select>
        <Select label={settings.goal === "time" ? "Time limit" : "Match target"} value={String(settings.amount)} onChange={e => { setCustomAmount(null); setSettings({ ...settings, amount: Number(e.target.value) }); }}>
          {(settings.goal === "time" ? [30, 60, 120] : [10, 20, 50]).map(n => <option key={n} value={n}>{settings.goal === "time" ? `${n} seconds` : `${n} matches`}</option>)}
          {!(settings.goal === "time" ? [30, 60, 120] : [10, 20, 50]).includes(settings.amount) && <option value={settings.amount}>{settings.amount} {settings.goal === "time" ? "seconds" : "matches"} · custom</option>}
        </Select>
      </CardGrid>
      <details className="matching-customize"><summary>Customize your round</summary><div className="activity-stack">
        <Select label="Board style" value={settings.style} onChange={e => setSettings({ ...settings, style: e.target.value as Settings["style"] })}><option value="refill">Keep it flowing · refill matched cards</option><option value="clear">Clear the board · then deal a new set</option></Select>
        <Select label="Cards on each side" value={String(settings.rows)} onChange={e => setSettings({ ...settings, rows: Number(e.target.value) })}>{[2, 3, 4, 5, 6].map(n => <option key={n} value={n}>{n} cards</option>)}</Select>
        <Input label={settings.goal === "time" ? "Custom seconds · 1–1800" : "Custom match target · 1–500"} type="number" value={customAmount ?? String(settings.amount)} onChange={e => { setCustomAmount(e.target.value); setSettings(normalizeSettings({ ...settings, amount: Number(e.target.value) })); }} />
      </div></details>
      <div className="activity-rules"><Text>{settings.style === "refill" ? "Match at least two pairs to refill the empty spaces. More matches make the refill arrive sooner." : "Clear every pair to reveal a fresh board."} {settings.goal === "time" ? "Make as many matches as you can before time runs out." : "Reach your target as quickly as you can."}</Text>{deck && <Text tone="muted">Pairs repeat during the round. A small deck may show fewer cards.</Text>}</div>
      {notice && <p role="status">{notice}</p>}
      <Button color="primary" disabled={!canStart} onClick={start}>Let’s match →</Button>
    </div>}
  </ActivityPage>;
}

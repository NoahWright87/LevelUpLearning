"use client";

import { useRef, useState } from "react";
import NextLink from "next/link";
import { Button, Container, Heading, Input, Text } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import PlayerPicker from "@/components/PlayerPicker";
import { useSave } from "@/components/useSave";
import { downloadText } from "@/lib/download";
import { PRESETS } from "@/lib/spelling/difficulty";
import {
  emptySave,
  parseSaveFile,
  removePlayer,
  renamePlayer,
  serializeSave,
  type SaveData,
} from "@/lib/save/schema";
import { listBests, totals, trickyWords } from "@/lib/save/spellingProgress";
import { replaceSave, updateSave } from "@/lib/save/store";
import { mathTotals } from "@/lib/math/progress";
import { operationLabel } from "@/lib/math/game";
import "./progress.css";

type Notice = { tone: "success" | "error"; text: string } | null;

const dateFormat = new Intl.DateTimeFormat(undefined, { dateStyle: "medium", timeStyle: "short" });
const formatDate = (iso: string) => dateFormat.format(new Date(iso));

function describeSave(save: SaveData): string {
  const sessions = save.players.reduce((n, p) => n + p.spelling.sessions.length + p.math.sessions.length, 0);
  const players = save.players.map((p) => p.name).join(", ") || "no players";
  return `${players}; ${sessions} quiz${sessions === 1 ? "" : "zes"}; ${save.spelling.lists.length} word list${save.spelling.lists.length === 1 ? "" : "s"}`;
}

function PlayerSettings() {
  const { player } = useSave();
  const [editing, setEditing] = useState(false);
  const [name, setName] = useState("");
  if (!player) return null;

  if (editing) {
    return (
      <form
        className="progress-row"
        onSubmit={(e) => {
          e.preventDefault();
          updateSave((s) => renamePlayer(s, player.id, name));
          setEditing(false);
        }}
      >
        <Input label="New name" value={name} onChange={(e) => setName(e.target.value)} />
        <Button type="submit" color="primary" disabled={!name.trim()}>
          Save
        </Button>
        <Button variant="text" onClick={() => setEditing(false)}>
          Cancel
        </Button>
      </form>
    );
  }

  return (
    <div className="progress-row">
      <Button
        size="small"
        variant="outline"
        onClick={() => {
          setName(player.name);
          setEditing(true);
        }}
      >
        Rename {player.name}
      </Button>
      <Button
        size="small"
        variant="text"
        color="danger"
        onClick={() => {
          if (window.confirm(`Delete ${player.name} and all of their progress? This can't be undone.`)) {
            updateSave((s) => removePlayer(s, player.id));
          }
        }}
      >
        Delete {player.name}
      </Button>
    </div>
  );
}

function SpellingProgress() {
  const { save, player } = useSave();
  if (!player) return null;

  const sessions = [...player.spelling.sessions].reverse();
  if (!sessions.length) {
    return (
      <Text tone="muted">
        No spelling quizzes yet for {player.name}.{" "}
        <NextLink href="/spelling" className="progress-link">
          Take one →
        </NextLink>
      </Text>
    );
  }

  const sum = totals(player);
  const listNames = new Map(save.spelling.lists.map((l) => [l.id, l.name]));
  const bests = [...listBests(player).entries()].map(([id, best]) => ({
    id,
    name: listNames.get(id) ?? sessions.flatMap((s) => s.lists).find((l) => l.id === id)?.name ?? id,
    ...best,
  }));
  const tricky = trickyWords(player);

  return (
    <>
      <div className="progress-totals">
        <div>
          <strong>{sum.sessions}</strong>
          <span>quiz{sum.sessions === 1 ? "" : "zes"}</span>
        </div>
        <div>
          <strong>{sum.words}</strong>
          <span>words practiced</span>
        </div>
        <div>
          <strong>{sum.points.toLocaleString()}</strong>
          <span>total points</span>
        </div>
      </div>

      <Heading level={3}>Recent quizzes</Heading>
      <div className="progress-table-wrap">
        <table className="progress-table">
          <thead>
            <tr>
              <th scope="col">When</th>
              <th scope="col">Lists</th>
              <th scope="col">Difficulty</th>
              <th scope="col">Right / tries</th>
              <th scope="col">Stars</th>
              <th scope="col">Points</th>
            </tr>
          </thead>
          <tbody>
            {sessions.slice(0, 10).map((s) => (
              <tr key={s.id}>
                <td>{formatDate(s.finishedAt)}</td>
                <td>{s.lists.map((l) => l.name).join(", ")}</td>
                <td>{s.difficulty === "custom" ? "Custom" : PRESETS[s.difficulty].label}</td>
                <td>
                  {s.correct} / {s.attempts}
                </td>
                <td>★ {s.stars}</td>
                <td>{s.points}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <Heading level={3}>Best on each list</Heading>
      <div className="progress-table-wrap">
        <table className="progress-table">
          <thead>
            <tr>
              <th scope="col">List</th>
              <th scope="col">Best stars</th>
              <th scope="col">Best points</th>
              <th scope="col">Times played</th>
            </tr>
          </thead>
          <tbody>
            {bests.map((b) => (
              <tr key={b.id}>
                <td>{b.name}</td>
                <td>★ {b.stars}</td>
                <td>{b.points}</td>
                <td>{b.plays}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {tricky.length ? (
        <>
          <Heading level={3}>Tricky words</Heading>
          <div className="progress-table-wrap">
            <table className="progress-table">
              <thead>
                <tr>
                  <th scope="col">Word</th>
                  <th scope="col">Right / tries</th>
                  <th scope="col">Misspelled as</th>
                </tr>
              </thead>
              <tbody>
                {tricky.map((w) => (
                  <tr key={w.word}>
                    <td>{w.word}</td>
                    <td>
                      {w.correct} / {w.attempts}
                    </td>
                    <td>
                      {Object.entries(w.misspellings)
                        .sort(([, a], [, b]) => b - a)
                        .slice(0, 3)
                        .map(([spelling]) => spelling)
                        .join(", ")}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </>
      ) : null}
    </>
  );
}

function MathProgress() {
  const { player } = useSave();
  if (!player) return null;
  const sum = mathTotals(player);
  if (!sum.rounds) return <Text tone="muted">No math rounds yet. <NextLink href="/math" className="progress-link">Play a sprint →</NextLink></Text>;
  return (
    <>
      <div className="progress-totals">
        <div><strong>{sum.rounds}</strong><span>rounds</span></div>
        <div><strong>{sum.correct}</strong><span>correct answers</span></div>
        <div><strong>{sum.points.toLocaleString()}</strong><span>total points</span></div>
      </div>
      <Heading level={3}>Recent math rounds</Heading>
      <div className="progress-table-wrap">
        <table className="progress-table">
          <thead><tr><th scope="col">When</th><th scope="col">Practice</th><th scope="col">Right / tries</th><th scope="col">Goal</th><th scope="col">Best combo</th><th scope="col">Points</th></tr></thead>
          <tbody>{[...player.math.sessions].reverse().slice(0, 10).map((s) => (
            <tr key={s.id}><td>{formatDate(s.finishedAt)}</td><td>{operationLabel(s.settings.operation)} · {s.settings.digits} digit{s.settings.digits > 1 ? "s" : ""}</td><td>{s.correct} / {s.attempts}</td><td>{s.correct >= s.settings.target ? "✓ " : ""}{s.settings.target}</td><td>×{s.bestCombo}</td><td>{s.points}</td></tr>
          ))}</tbody>
        </table>
      </div>
    </>
  );
}

function SaveDataPanel({ onNotice }: { onNotice: (n: Notice) => void }) {
  const { save } = useSave();
  const fileRef = useRef<HTMLInputElement>(null);

  const upload = async (files: FileList | null) => {
    const file = files?.[0];
    if (!file) return;
    try {
      const incoming = parseSaveFile(await file.text());
      const ok = window.confirm(
        `Replace everything in this browser with this save file?\n\nIn the file: ${describeSave(incoming)}.\nIn this browser now: ${describeSave(save)}.`,
      );
      if (!ok) return;
      onNotice(
        replaceSave(incoming)
          ? { tone: "success", text: `Loaded the save file: ${describeSave(incoming)}.` }
          : { tone: "error", text: "This browser wouldn't store the save file (private browsing or storage full)." },
      );
    } catch (err) {
      onNotice({ tone: "error", text: err instanceof Error ? err.message : "That file couldn't be read." });
    } finally {
      if (fileRef.current) fileRef.current.value = "";
    }
  };

  return (
    <>
      <Text>
        Everything is saved in this browser only: players, progress and your own word lists. Download a save file
        to back it up or move it to another device, then upload it there.
      </Text>
      <div className="progress-row">
        <Button
          color="primary"
          onClick={() =>
            downloadText(`level-up-learning-save-${new Date().toISOString().slice(0, 10)}.json`, serializeSave(save))
          }
        >
          ⬇️ Download save file
        </Button>
        <Button variant="outline" onClick={() => fileRef.current?.click()}>
          ⬆️ Upload save file
        </Button>
        <input
          ref={fileRef}
          type="file"
          accept=".json,application/json"
          hidden
          aria-label="Upload a save file"
          onChange={(e) => upload(e.target.files)}
        />
      </div>
      <div className="progress-row">
        <Button
          size="small"
          variant="text"
          color="danger"
          onClick={() => {
            if (window.confirm("Erase all players, progress and word lists from this browser? Download a save file first if you might want it back.")) {
              replaceSave(emptySave());
              onNotice({ tone: "success", text: "Erased everything in this browser." });
            }
          }}
        >
          Erase everything
        </Button>
      </div>
    </>
  );
}

/** /progress: players, their activity history, and the save file. */
export default function ProgressPage() {
  const { player, loaded } = useSave();
  const [notice, setNotice] = useState<Notice>(null);

  return (
    <SiteShell>
      <Container padding="lg">
        <div className="progress-page">
          <Heading level={1}>📈 Progress</Heading>

          {notice ? (
            <div className={`progress-notice progress-notice--${notice.tone}`} role="status">
              {notice.text}
            </div>
          ) : null}

          <section className="progress-panel">
            <Heading level={2}>Players</Heading>
            <PlayerPicker />
            <PlayerSettings />
          </section>

          {loaded && player ? (
            <section className="progress-panel">
              <Heading level={2}>⚡ Math: {player.name}</Heading>
              <MathProgress />
            </section>
          ) : null}

          {loaded && player ? (
            <section className="progress-panel">
              <Heading level={2}>🔤 Spelling: {player.name}</Heading>
              <SpellingProgress />
            </section>
          ) : null}

          <section className="progress-panel">
            <Heading level={2}>💾 Save data</Heading>
            <SaveDataPanel onNotice={setNotice} />
          </section>
        </div>
      </Container>
    </SiteShell>
  );
}

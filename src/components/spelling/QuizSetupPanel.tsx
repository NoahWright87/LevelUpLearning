"use client";

import { useEffect, useMemo, useState } from "react";
import NextLink from "next/link";
import { Button, Checkbox, Heading, RadioGroup, Select, Text } from "@noahwright/design";
import {
  ALL_LETTERS,
  LIMITS,
  PRESET_IDS,
  PRESETS,
  scoreMultiplier,
  settingsFor,
  type DifficultyId,
  type DifficultySettings,
} from "@/lib/spelling/difficulty";
import type { SpellingList, SpellingWord } from "@/lib/spelling/lists";
import { uniqueWords } from "@/lib/spelling/quiz";
import { sayWordPrompt } from "@/lib/spelling/speech";
import { DEFAULT_SETUP, loadSetup, saveSetup, type QuizSetup } from "@/lib/spelling/storage";
import { useSpellingLists } from "./useSpellingLists";
import { useVoices } from "./useVoices";

type Props = {
  onStart: (round: { words: SpellingWord[]; settings: DifficultySettings; voiceURI?: string }) => void;
};

const range = (min: number, max: number) => Array.from({ length: max - min + 1 }, (_, i) => min + i);

/** Plain-English lines describing what a difficulty does. */
export function describeSettings(s: DifficultySettings): string[] {
  const lines: string[] = [];
  if (s.hintLetters >= ALL_LETTERS) lines.push("Every letter is shown");
  else if (s.hintLetters > 0) lines.push(`${s.hintLetters} letter${s.hintLetters === 1 ? "" : "s"} shown to start`);
  else lines.push(s.showBlanks ? "Blanks show how many letters" : "No letters or blanks shown");
  if (s.hintLetters > 0 && s.hintLetters < ALL_LETTERS && s.hintLettersDrop > 0) {
    lines.push(`${s.hintLettersDrop} fewer for each right answer in a row`);
  }
  if (s.streakPenalty > 0 && s.hintLetters > 0 && s.hintLettersDrop > 0) lines.push("Mistakes bring letters back");
  lines.push(`Spell each word right ${s.timesToFinish} time${s.timesToFinish === 1 ? "" : "s"}`);
  return lines;
}

function groupByGrade(lists: SpellingList[]): Array<{ title: string; lists: SpellingList[] }> {
  const grades = new Map<number, SpellingList[]>();
  for (const list of lists) {
    const g = list.grade ?? 0;
    grades.set(g, [...(grades.get(g) ?? []), list]);
  }
  return [...grades.entries()]
    .sort(([a], [b]) => a - b)
    .map(([grade, lists]) => ({ title: grade ? `Grade ${grade}` : "Other", lists }));
}

function NumberSelect({
  label,
  value,
  min,
  max,
  onChange,
  extra,
}: {
  label: string;
  value: number;
  min: number;
  max: number;
  onChange: (n: number) => void;
  extra?: { value: number; label: string };
}) {
  return (
    <Select label={label} value={String(value)} onChange={(e) => onChange(Number(e.target.value))}>
      {range(min, max).map((n) => (
        <option key={n} value={n}>
          {n}
        </option>
      ))}
      {extra ? <option value={extra.value}>{extra.label}</option> : null}
    </Select>
  );
}

export default function QuizSetupPanel({ onStart }: Props) {
  const { builtIn, custom, all, loaded } = useSpellingLists();
  const { voices, supported } = useVoices();
  const [setup, setSetup] = useState<QuizSetup>(DEFAULT_SETUP);

  // Storage is browser-only, so the remembered setup arrives after the first render.
  useEffect(() => setSetup(loadSetup()), []);

  const update = (patch: Partial<QuizSetup>) =>
    setSetup((prev) => {
      const next = { ...prev, ...patch };
      saveSetup(next);
      return next;
    });

  const selected = useMemo(() => new Set(setup.listIds), [setup.listIds]);
  const chosenLists = all.filter((l) => selected.has(l.id));
  const words = uniqueWords(chosenLists.flatMap((l) => l.words));
  const settings = settingsFor(setup.difficulty, setup.custom);

  const toggle = (id: string, on: boolean) =>
    update({ listIds: on ? [...setup.listIds.filter((x) => x !== id), id] : setup.listIds.filter((x) => x !== id) });

  const setCustom = (patch: Partial<DifficultySettings>) =>
    update({ difficulty: "custom", custom: { ...setup.custom, ...patch } });

  const groups = [...groupByGrade(builtIn), ...(custom.length ? [{ title: "My lists", lists: custom }] : [])];

  return (
    <div className="spelling-setup">
      <div className="spelling-setup__intro">
        <Heading level={1}>🔤 Spelling</Heading>
        <Text>Listen to each word, then type how it&apos;s spelled. Pick some lists and a difficulty to begin.</Text>
      </div>

      <section className="spelling-panel" aria-labelledby="lists-title">
        <div className="spelling-panel__head">
          <Heading level={2}>
            <span id="lists-title">1. Pick word lists</span>
          </Heading>
          <NextLink href="/spelling/lists" className="spelling-link">
            Upload or download lists →
          </NextLink>
        </div>
        <div className="spelling-groups">
          {groups.map((group) => (
            <fieldset key={group.title} className="spelling-group">
              <legend>{group.title}</legend>
              {group.lists.map((list) => (
                <Checkbox
                  key={list.id}
                  label={`${list.name} (${list.words.length})`}
                  checked={selected.has(list.id)}
                  onChange={(e) => toggle(list.id, e.target.checked)}
                />
              ))}
            </fieldset>
          ))}
        </div>
        <div className="spelling-panel__foot">
          <Text tone="muted">
            {words.length
              ? `${words.length} word${words.length === 1 ? "" : "s"} from ${chosenLists.length} list${chosenLists.length === 1 ? "" : "s"}`
              : loaded
                ? "No lists picked yet"
                : "Loading lists…"}
          </Text>
          {setup.listIds.length ? (
            <Button variant="text" onClick={() => update({ listIds: [] })}>
              Clear
            </Button>
          ) : null}
        </div>
      </section>

      <section className="spelling-panel" aria-labelledby="difficulty-title">
        <Heading level={2}>
          <span id="difficulty-title">2. Choose a difficulty</span>
        </Heading>
        <RadioGroup
          label="Difficulty"
          name="difficulty"
          value={setup.difficulty}
          onChange={(e) => update({ difficulty: e.target.value as DifficultyId })}
          options={[
            ...PRESET_IDS.map((id) => ({ value: id, label: `${PRESETS[id].label}: ${PRESETS[id].hint}` })),
            { value: "custom", label: "Custom: set it up yourself" },
          ]}
        />

        {setup.difficulty === "custom" ? (
          <div className="spelling-custom">
            <Checkbox
              label="Show blanks for every letter"
              checked={setup.custom.showBlanks}
              onChange={(e) => setCustom({ showBlanks: e.target.checked })}
            />
            <NumberSelect
              label="Letters shown to start"
              value={setup.custom.hintLetters}
              {...LIMITS.hintLetters}
              extra={{ value: ALL_LETTERS, label: "All of them" }}
              onChange={(n) => setCustom({ hintLetters: n })}
            />
            <NumberSelect
              label="Fewer letters per right answer"
              value={setup.custom.hintLettersDrop}
              {...LIMITS.hintLettersDrop}
              onChange={(n) => setCustom({ hintLettersDrop: n })}
            />
            <NumberSelect
              label="Times to spell each word right"
              value={setup.custom.timesToFinish}
              {...LIMITS.timesToFinish}
              onChange={(n) => setCustom({ timesToFinish: n })}
            />
            <NumberSelect
              label="Streak lost on a mistake"
              value={setup.custom.streakPenalty}
              {...LIMITS.streakPenalty}
              onChange={(n) => setCustom({ streakPenalty: n })}
            />
          </div>
        ) : null}

        <ul className="spelling-summary">
          {describeSettings(settings).map((line) => (
            <li key={line}>{line}</li>
          ))}
          <li>Difficulty bonus: ×{scoreMultiplier(settings).toFixed(2)} points</li>
        </ul>
      </section>

      <section className="spelling-panel" aria-labelledby="voice-title">
        <Heading level={2}>
          <span id="voice-title">3. Check the sound</span>
        </Heading>
        {supported ? (
          <div className="spelling-voice">
            {voices.length > 1 ? (
              <Select
                label="Voice"
                value={setup.voiceURI ?? voices[0].voiceURI}
                onChange={(e) => update({ voiceURI: e.target.value })}
              >
                {voices.map((v) => (
                  <option key={v.voiceURI} value={v.voiceURI}>
                    {v.name}
                  </option>
                ))}
              </Select>
            ) : null}
            <Button
              variant="outline"
              onClick={() => sayWordPrompt("spelling", "I love practicing my %% words!", { voiceURI: setup.voiceURI })}
            >
              🔊 Test the voice
            </Button>
          </div>
        ) : (
          <Text tone="error">
            This browser can&apos;t speak the words aloud. You can still play by reading the example sentences.
          </Text>
        )}
      </section>

      <div className="spelling-start">
        <Button
          size="large"
          color="primary"
          motion="bouncy"
          disabled={words.length === 0}
          onClick={() => onStart({ words, settings, voiceURI: setup.voiceURI })}
        >
          Start the quiz
        </Button>
      </div>
    </div>
  );
}
